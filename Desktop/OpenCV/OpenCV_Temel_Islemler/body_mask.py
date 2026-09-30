import cv2
import numpy as np

cap = cv2.VideoCapture(0)

def nothing(x):
    pass

cv2.namedWindow("Frame")
cv2.createTrackbar("Lower - H", "Frame", 0, 180, nothing)
cv2.createTrackbar("Lower - S", "Frame", 0, 255, nothing)
cv2.createTrackbar("Lower - V", "Frame", 0, 255, nothing)
cv2.createTrackbar("Upper - H", "Frame", 0, 180, nothing)
cv2.createTrackbar("Upper - S", "Frame", 0, 255, nothing)
cv2.createTrackbar("Upper - V", "Frame", 0, 255, nothing)

cv2.setTrackbarPos("Upper - H", "Frame", 180)
cv2.setTrackbarPos("Upper - S", "Frame", 255)
cv2.setTrackbarPos("Upper - V", "Frame", 255)


while True:
    ret, frame = cap.read()
    frame = cv2.flip(frame, 1)

    frame_hsv = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)
    lower_h = cv2.getTrackbarPos("Lower - H", "Frame")
    lower_s = cv2.getTrackbarPos("Lower - S", "Frame")
    lower_v = cv2.getTrackbarPos("Lower - V", "Frame")
    upper_h = cv2.getTrackbarPos("Upper - H", "Frame")
    upper_s = cv2.getTrackbarPos("Upper - S", "Frame")
    upper_v = cv2.getTrackbarPos("Upper - V", "Frame")

    lover_color = np.array([lower_h, lower_s, lower_v])
    upper_color = np.array([upper_h, upper_s, upper_v])
    
    mask = cv2.inRange(frame_hsv, lover_color, upper_color)
    
    result = cv2.bitwise_and(frame, frame, mask=mask)
    
    cv2.imshow("Frame", frame)
    cv2.imshow("Mask", mask)
    cv2.imshow("Result", result)
    
    if cv2.waitKey(20) & 0xFF == ord("q"):
        break

cap.release()
cv2.destroyAllWindows()