import React, { useState, useEffect, useCallback } from 'react';
import { StyleSheet, Text, View, Dimensions, SafeAreaView } from 'react-native';
import { LineChart } from 'react-native-chart-kit';
import Paho from 'paho-mqtt';

// paho-mqtt requires localStorage, which isn't available in React Native.
// We mock it to prevent crashes.
if (typeof window !== 'undefined' && !window.localStorage) {
  window.localStorage = {
    getItem: (key) => null,
    setItem: (key, value) => {},
    removeItem: (key) => {}
  };
}

export default function App() {
  const [dataPoints, setDataPoints] = useState([0]); 
  const [labels, setLabels] = useState(['']);
  const [connectionStatus, setConnectionStatus] = useState('Connecting...');
  
  // Memoize client creation
  const client = React.useMemo(() => {
    const clientId = 'mqttjs_' + Math.random().toString(16).substr(2, 8);
    // Connect to public HiveMQ WebSocket broker
    return new Paho.Client('broker.hivemq.com', 8000, '/mqtt', clientId);
  }, []);

  const onMessageArrived = useCallback((message) => {
    console.log("Message Arrived: " + message.payloadString);
    const value = parseFloat(message.payloadString);
    
    if (!isNaN(value)) {
      setDataPoints((prevPoints) => {
        const newPoints = [...prevPoints, value];
        if (newPoints.length > 20) {
          return newPoints.slice(newPoints.length - 20);
        }
        return newPoints;
      });
      setLabels((prevLabels) => {
        const newLabels = [...prevLabels, ''];
        if (newLabels.length > 20) {
          return newLabels.slice(newLabels.length - 20);
        }
        return newLabels;
      });
    }
  }, []);

  useEffect(() => {
    client.onConnectionLost = (responseObject) => {
      if (responseObject.errorCode !== 0) {
        console.log("Connection Lost: " + responseObject.errorMessage);
        setConnectionStatus('Disconnected');
      }
    };

    client.onMessageArrived = onMessageArrived;

    const connectOptions = {
      onSuccess: () => {
        console.log("Connected to MQTT Broker");
        setConnectionStatus('Connected');
        client.subscribe('keremberk/sensor/data/12345');
      },
      onFailure: (err) => {
        console.log("Failed to connect", err);
        setConnectionStatus('Failed to connect (Check broker IP/Port)');
      },
    };

    try {
        client.connect(connectOptions);
    } catch (e) {
        console.log("Error connecting:", e);
        setConnectionStatus('Error: ' + e.message);
    }

    return () => {
      if (client.isConnected()) {
        client.disconnect();
      }
    };
  }, [client, onMessageArrived]);

  return (
    <SafeAreaView style={styles.container}>
      <Text style={styles.title}>MQTT Data Real-Time Graph</Text>
      <Text style={styles.status}>Status: {connectionStatus}</Text>
      
      <View style={styles.chartContainer}>
        <LineChart
          data={{
            labels: labels,
            datasets: [
              {
                data: dataPoints.length > 0 ? dataPoints : [0],
              }
            ]
          }}
          width={Dimensions.get("window").width - 32} // from react-native
          height={300}
          yAxisLabel=""
          yAxisSuffix=""
          yAxisInterval={1} 
          chartConfig={{
            backgroundColor: "#ffffff",
            backgroundGradientFrom: "#ffffff",
            backgroundGradientTo: "#ffffff",
            decimalPlaces: 1, 
            color: (opacity = 1) => `rgba(0, 122, 255, ${opacity})`,
            labelColor: (opacity = 1) => `rgba(0, 0, 0, ${opacity})`,
            style: {
              borderRadius: 16
            },
            propsForDots: {
              r: "4",
              strokeWidth: "2",
              stroke: "#007AFF"
            }
          }}
          bezier
          style={{
            marginVertical: 8,
            borderRadius: 16
          }}
        />
      </View>
      <View style={styles.instructionsContainer}>
         <Text style={styles.instructionText}>
           To test, run the provided 'node publisher.js' script.
           It will publish data to the public HiveMQ broker on topic 'keremberk/sensor/data/12345'.
         </Text>
      </View>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#f5f5f5',
    alignItems: 'center',
    paddingTop: 40,
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 8,
    marginTop: 24,
    color: '#333',
    textAlign: 'center'
  },
  status: {
    fontSize: 16,
    marginBottom: 24,
    color: '#666'
  },
  chartContainer: {
    backgroundColor: 'white',
    borderRadius: 16,
    padding: 8,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.1,
    shadowRadius: 4,
    elevation: 3,
  },
  instructionsContainer: {
    marginTop: 32,
    padding: 16,
    backgroundColor: '#e6f7ff',
    borderRadius: 8,
    width: '90%',
  },
  instructionText: {
    fontSize: 14,
    color: '#0050b3',
    textAlign: 'center',
    lineHeight: 20
  }
});
