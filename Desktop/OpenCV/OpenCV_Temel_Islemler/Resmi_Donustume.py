import cv2
import numpy as np

img = cv2.imread("images.png",0)
cv2.imshow("img",img)
row,col = img.shape

M= cv2.getRotationMatrix2D((col/5,row/3),180,1)

dst = cv2.warpAffine(img,M,(col,row))

cv2.imshow("dst",dst)

cv2.waitKey(0)
cv2.destroyAllWindows()