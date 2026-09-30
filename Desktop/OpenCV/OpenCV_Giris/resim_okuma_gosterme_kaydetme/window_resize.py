from asyncio import selector_events
import cv2
cv2.namedWindow("Klon", cv2.WINDOW_NORMAL)
img = cv2.imread("Clone_Commander_Gree_Trooper_Cosplay_at_Star_Wars_Celebration_V.jpg")

img = cv2.resize(img, (150, 150))

cv2.imshow("Klon", img)
cv2.waitKey(0)
cv2.destroyAllWindows()

