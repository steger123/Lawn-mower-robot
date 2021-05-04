
import cv2

cap = cv2.VideoCapture(0)

while True:
    _, frame = cap.read()

    cv2.imshow("Frame", frame)



    key = cv2.waitKey(1)    
    if key == 27 or key == ord('q'):  #esc
        break


cap.release()
cv2.destroyAllWindows()
