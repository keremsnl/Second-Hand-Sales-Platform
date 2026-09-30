import cv2
import numpy as np

circle = np.zeros((512, 512, 3), np.uint8) + 255
cv2.circle(circle, (256, 256), 60, (255, 0, 0), -1)

rectangle = np.zeros((512, 512, 3), np.uint8) +255
cv2.rectangle(rectangle, (100, 100), (400, 400), (0, 0, 255), -1)

add = cv2.add(circle, rectangle)
print(add[256, 256])

cv2.imshow("Circle", circle)
cv2.imshow("Rectangle", rectangle)
cv2.waitKey(0)
cv2.destroyAllWindows()