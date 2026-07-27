const mqtt = require('mqtt');

// Connect to a public MQTT broker (HiveMQ) instead of localhost
const client = mqtt.connect('mqtt://broker.hivemq.com:1883');

client.on('connect', () => {
    console.log('Publisher connected to public MQTT broker (HiveMQ)');
    
    // Send data every 5 seconds
    setInterval(() => {
        // Generate a random value between 10 and 50
        const randomValue = (Math.random() * 40 + 10).toFixed(1);
        
        console.log(`Publishing new data: ${randomValue}`);
        // Publish to a unique topic so we don't mix with other users
        client.publish('keremberk/sensor/data/12345', randomValue.toString());
    }, 5000);
});

client.on('error', (error) => {
    console.error('MQTT Connection Error:', error);
});
