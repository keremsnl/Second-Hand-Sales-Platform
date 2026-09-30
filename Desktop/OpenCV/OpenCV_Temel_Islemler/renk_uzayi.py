import cv2
img = cv2.imread("Clone_Commander_Gree_Trooper_Cosplay_at_Star_Wars_Celebration_V.jpg")

cv2.imshow("Klon", img)
img1 = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
img2 = cv2.cvtColor(img, cv2.COLOR_BGR2HSV)
img3 = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)


cv2.imshow("Klon BGR", img)
cv2.imshow("Klon RGB", img1)
cv2.imshow("Klon HSV", img2)
cv2.imshow("Klon GRAY", img3)

cv2.waitKey(0)
cv2.destroyAllWindows()