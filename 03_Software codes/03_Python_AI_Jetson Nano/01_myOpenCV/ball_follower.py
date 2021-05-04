#https://www.youtube.com/watch?v=Mx-zXDWYWoA
#https://pysource.com/2019/06/05/control-webcam-with-servo-motor-and-raspberry-pi-opencv-with-python/

import cv2
import numpy as np
import serial
import time

usb = 'ACM0'
#usb = 'USB0'
cap = cv2.VideoCapture(0)

# Set camera resolution
#cap.set(3, 480)
#cap.set(4, 320)

# to have initial value, other cise if no red ball in the picture, then error coming.
_, frame = cap.read()
rows, cols, _ = frame.shape

ballCenter_x = int(cols / 2)
imgCenter = int(cols / 2)
#position = 90 # degrees
heading = "fo"
velocity = 50  # speed of the robot
halfWindow = 100

wait = 0.5 #waiting for serial comm.

ser = serial.Serial('/dev/tty' + usb, 9600)
time.sleep(wait)

while True:
    _, frame = cap.read()
    hsv_frame = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)
    
    # red color
    low_red = np.array([170, 155, 84])   # 161, 155, 84
    high_red = np.array([179, 255, 255])  # 179, 255, 255
    red_mask = cv2.inRange(hsv_frame, low_red, high_red)
    contours, _ = cv2.findContours(red_mask, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)
    
    #find the biggest one, sort from biggest to smallest:
    contours = sorted(contours, key=lambda x:cv2.contourArea(x), reverse=True)
    if contours == []:
        ballCenter_x = int(cols / 2)

    for cnt in contours:
        (x, y, w, h) = cv2.boundingRect(cnt)
        #cv2.rectangle(frame, x, y, (x+w, y+h), (0,255,0),2)
        ballCenter_x = int((x + x + w) / 2)
        break
        
   # velocity = 0.1*(ballCenter_x - imgCenter)  #motor speed proportional to the distance from ceter
                                                # if the ball more out of center -> quicker turning required
# Move motor
    if ballCenter_x < imgCenter - halfWindow and heading != "left":
        print("turn left")
        velocity = 40 + int(0.2*(imgCenter - ballCenter_x))
        print(velocity)
        ser.write(str("le,030").encode('utf-8'))
     #   time.sleep(wait)
        heading = "left"

    if ballCenter_x > imgCenter + halfWindow and heading != "right":
        print("turn right")
        velocity = 40 + int(0.2*(ballCenter_x - imgCenter))
        print(velocity)
        ser.write(str("ri,030").encode('utf-8'))
      #  time.sleep(wait)
        heading = "right"

    if imgCenter - halfWindow < ballCenter_x and ballCenter_x < imgCenter + halfWindow and heading != "forward" and contours != []:
        print("forward")
        velocity = 50
        print(velocity)
        ser.write(str("fo,030").encode('utf-8'))
      #  time.sleep(wait)
        heading = "forward"
    if contours == [] and heading != "hold":
        print("hold")  # no ball in the picture
        velocity = 0
        print(velocity)
        ser.write(str("ho,000").encode('utf-8'))
        time.sleep(wait)
        heading = "hold"
    #time.sleep(wait)
    
    cv2.line(frame, (ballCenter_x , 0), (ballCenter_x , 480), (0, 255, 0), 2)
    cv2.line(frame, (imgCenter - halfWindow , 0), (imgCenter - halfWindow , 480), (0, 0, 255), 2)
    cv2.line(frame, (imgCenter + halfWindow , 0), (imgCenter + halfWindow , 480), (0, 0, 255), 2)

    cv2.imshow("Frame", frame)
    key = cv2.waitKey(1)    
    if key == 27 or key == ord('q'):  #esc
        break

    #out = ser.readline()  #camera freesing !!!!!
    #print(out) # Read the newest output from the Arduino
    #out = "" 

cap.release()
cv2.destroyAllWindows()