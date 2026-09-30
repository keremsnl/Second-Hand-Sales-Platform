import cv2
import numpy as np

img = cv2.imread("Clone_Commander_Gree_Trooper_Cosplay_at_Star_Wars_Celebration_V.jpg")



dimension = img.shape
print(dimension)

color = img[420, 500]
print("BGR: ", color)

blue = img[420, 500, 0]
print("blue: ", blue)

green = img[420, 500, 1]
print("green: ", green)

red = img[420, 500, 2]
print("red: ", red)


cv2.imshow("Koln Asker", img)
cv2.waitKey(0)
cv2.destroyAllWindows()