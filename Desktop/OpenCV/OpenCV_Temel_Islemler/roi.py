import cv2
import cv2

img = cv2.imread("Clone_Commander_Gree_Trooper_Cosplay_at_Star_Wars_Celebration_V.jpg")

roi = img[30:400, 1000:1300]
cv2.imshow("Clone Asker", img)
cv2.imshow("ROI", roi)


cv2.waitKey(0)
cv2.destroyAllWindows()