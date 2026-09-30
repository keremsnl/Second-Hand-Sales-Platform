import cv2
import numpy as np

img_fliter = cv2.imread("filter.png")
img_median = cv2.imread("median.png")
img_bilateral = cv2.imread("bilateral.png")

blur = cv2.blur(img_fliter,(7,7))
blur2 = cv2.GaussianBlur(img_fliter,(7,7),cv2.BORDER_DEFAULT)
blur_m = cv2.medianBlur(img_median,5)
blur_b = cv2.bilateralFilter(img_bilateral,9,75,75)

cv2.imshow("original", img_bilateral)
cv2.imshow("blur_b", blur_b)

cv2.waitKey(0)
cv2.destroyAllWindows()