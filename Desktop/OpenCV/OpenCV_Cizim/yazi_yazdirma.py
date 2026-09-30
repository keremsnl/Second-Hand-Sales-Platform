import cv2
import numpy as np

canvas = np.zeros((512,512,3), dtype=np.uint8) + 255

font1 = cv2.FONT_HERSHEY_SIMPLEX
font1 = cv2.FONT_HERSHEY_COMPLEX
font1 = cv2.FONT_HERSHEY_SCRIPT_COMPLEX

cv2.putText(canvas, "OpenCV", (100,200), font1, 1, (0,0,0), 2)



cv2.imshow("Canvas", canvas)
cv2.waitKey(0)
cv2.destroyAllWindows()