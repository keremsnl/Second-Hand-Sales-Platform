import cv2

cap = cv2.VideoCapture(0)

fileName = "/Users/keremberk/Desktop/OpenCV/OpenCV_Giris/webcam.mp4"
codec = cv2.VideoWriter_fourcc(*'avc1')
frameRate = 30.0
width = int(cap.get(cv2.CAP_PROP_FRAME_WIDTH))
height = int(cap.get(cv2.CAP_PROP_FRAME_HEIGHT))
resolution = (width, height)

videoFileOutput = cv2.VideoWriter(fileName, codec, frameRate, resolution)

while True:
    ret, frame = cap.read()
    frame = cv2.flip(frame, 1)
    videoFileOutput.write(frame)

    cv2.imshow("Webcam Live", frame)

    if cv2.waitKey(1) & 0xFF == ord("q"):
        break

videoFileOutput.release()
cap.release()
cv2.destroyAllWindows()