import cv2

# iPhone kamerasını açar (Eğer açılmazsa 1 yerine 0 veya IP adresi yazın)
cap = cv2.VideoCapture(1, cv2.CAP_AVFOUNDATION)

# Çözünürlüğü otomatik al (Hatasız kayıt için zorunlu)
width = int(cap.get(cv2.CAP_PROP_FRAME_WIDTH))
height = int(cap.get(cv2.CAP_PROP_FRAME_HEIGHT))

# Mac uyumlu kayıt motorunu hazırla
codec = cv2.VideoWriter_fourcc(*'avc1')
kayit = cv2.VideoWriter("telefon_kayit.mp4", codec, 30.0, (width, height))

while True:
    ret, frame = cap.read()
    if not ret: break

    # Videoyu hem kaydet hem ekranda göster
    kayit.write(frame)
    cv2.imshow("iPhone Kamera", frame)

    # 'q' tuşuna basılınca güvenlice çık
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

# İşlem bitince her şeyi serbest bırak
kayit.release()
cap.release()
cv2.destroyAllWindows()
