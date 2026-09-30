import cv2

def resizewithAspectRatio(img, width = None, height = None, inter = cv2.INTER_AREA):
    dimension = None
    (h, w) = img.shape[:2]
    
    if width is None and height is None:
        return img

    if width is None:
        ratio = height / float(h)
        dimension = (int(w*ratio), height)

    else:
        ratio = width / float(w)
        dimension = (width, int(h*ratio))

    resized = cv2.resize(img, dimension, interpolation=inter)

    return resized 

img = cv2.imread("Clone_Commander_Gree_Trooper_Cosplay_at_Star_Wars_Celebration_V.jpg")

img1 = resizewithAspectRatio(img, width = None, height = 600, inter = cv2.INTER_AREA)

cv2.imshow("Original", img)
cv2.imshow("Resized", img1)
cv2.waitKey(0)
cv2.destroyAllWindows()

