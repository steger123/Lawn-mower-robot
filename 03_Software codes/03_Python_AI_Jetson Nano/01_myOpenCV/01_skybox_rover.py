# jetbot@jetson-4-3:~/Desktop/pyPro/myOpenCV$ /usr/bin/python3 /home/jetbot/Desktop/pyPro/myOpenCV/edgeLineVideo.py
import cv2
import numpy as np
import time
import serial
#List serial ports:
#$ dmesg | grep tty

usb = 'USB0'
dangWidth = 20  # danger zone width
dangHeight = 50
roiWidth = 80  #total is 2*60=120

cam=cv2.VideoCapture(0)
# used to record the time when we processed last frame 
prev_frame_time = 0
# used to record the time at which we processed current frame 
new_frame_time = 0
font = cv2.FONT_HERSHEY_PLAIN

while True:

    ret, frame =cam.read()
    #roi = cv2.imread('f1.jpg')qq

    # cv2.waitKey(0)
    key = cv2.waitKey(1)
    if key == 27 or key == ord('q'):  #esc
    #if cv2.waitKey(1)==ord('q'):
        break
    if cv2.waitKey(1)==ord('c'):
        cv2.imwrite('/home/jetbot/Desktop/pyPro/myOpenCV/img/captured.jpg', frame)
        print('image captured')

    roi = frame[280:480, (320 - roiWidth):(320 + roiWidth)]  # 640x480
    #roi = frame[280:480, 220:420]   # 640x480
    cv2.imshow('ROI', roi)
    cv2.moveWindow('ROI',0,0)

    hsv = cv2.cvtColor(roi, cv2.COLOR_BGR2HSV)
    hueImg, satImg, valImg = cv2.split(hsv)
    hueMask = cv2.inRange(hueImg, 50,170)  #10,80
    satMask = cv2.inRange(satImg, 70,220)  #100,250 
    valMask = cv2.inRange(valImg, 30,80)  #100,250  
    hueMask = hueMask & satMask & valMask
    cv2.imshow("COMBO_MASK", hueMask)
    cv2.moveWindow('COMBO_MASK',230,0)

#noNoise = cv2.fastNlMeansDenoising(hueMask,250,7,21)
#cv2.imshow("NO_NOISE", noNoise)
#cv2.moveWindow('NO_NOISE',1000,150)

    vertical = np.copy(hueMask)
    rows = vertical.shape[0]
    verticalsize = rows // 9   #influencing the target direction !!!
# Create structure element for extracting vertical lines through morphology operations
    verticalStructure = cv2.getStructuringElement(cv2.MORPH_RECT, (1, verticalsize))
    vertical = cv2.erode(vertical, verticalStructure)
    vertical = cv2.dilate(vertical, verticalStructure)
    cv2.imshow("COMBO_VERTICAL", vertical)
    cv2.moveWindow('COMBO_VERTICAL',400,0)

    contours, hierarhy = cv2.findContours(vertical, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)
    roi = cv2.drawContours(roi, contours, -1, (0,0,250), 1)
    cv2.fillPoly(roi, pts =contours, color=(255,0,255))
    cv2.imshow('CONTOUR', roi)
    cv2.moveWindow('CONTOUR',600,0)

#get contur centers
    contours_poly = len(contours)
    center = len(contours)
    radius = len(contours)
#find enclosing polygon which fits around the contures
    rMax = 1
    origo = (100,100)
    for i in range(0,len(contours)):
	    cont = contours[i]
	    approx = cv2.approxPolyDP(cont, 2, True)
	    #(x,y),radius = cv2.minEnclosingCircle(approx)
	    (x,y),radius = cv2.minEnclosingCircle(cont)
	    center = (int(x), int(y))
	    radius = int(radius)
	    frameCircle = cv2.circle(roi, center, radius, (255,0,0), 2)
	    if x>0 and x<640 and y>0 and y<480:  #ROI window for region of interes can be smaller (not the full window)
		    if int(radius) > rMax:
			    rMax = int(radius)
			    origo = center
    # print("rMax & origo: ", rMax, origo)  # rMax or origo can be 0 !
    frameCircle = cv2.line(roi, (roiWidth, 200), (roiWidth, 100), (0, 255, 255), 2)  # heading yellow line
    frameCircle = cv2.line(roi, (roiWidth, 200), origo, (0, 0, 250), 2)  # direction required red line

    # calculate angle:
    x11 = roiWidth  # bottom point for heading
    y11 = 200
    x12 = roiWidth  # top point for heading
    y12 = 100
    # origo:
    x21 = roiWidth  # bottom point
    y21 = 200
    x22 = origo[0]  # circle center constantly changing
    y22 = origo[1]

    vector_1 = [x11 - x12, y11 - y12]
    vector_2 = [x21 - x22, y21 - y22]
    unit_vector_1 = vector_1 / np.linalg.norm(vector_1)
    unit_vector_2 = vector_2 / np.linalg.norm(vector_2)
    dot_product = np.dot(unit_vector_1, unit_vector_2)
    angle = np.arccos(dot_product)
    if x22 < x11 :
        angle = -1* angle
    # print("Angle :", angle * 180 / np.pi)
    angleTxt = str("%.1f" % round(angle * 180 / np.pi, 1))
    cv2.imshow('CIRCLE', frameCircle)
    cv2.moveWindow('CIRCLE',800,0)


# *** TRANSPARENT BOX ****************************************************
    # font which we will be using to display FPS 
    font = cv2.FONT_HERSHEY_PLAIN
    # time when we finish processing for this frame 
    new_frame_time = time.time() 
    fps = 1/(new_frame_time-prev_frame_time) 
    prev_frame_time = new_frame_time 
    fps = int(fps) 
    fps = "FPS: " + str(fps)
    # Trasparent rectangle below FTP text
    # First we crop the sub-rect from the image
    x, y, w, h = 0, 0, 250, 60
    sub_img = frame[y:y+h, x:x+w]
    white_rect = np.ones(sub_img.shape, dtype=np.uint8) * 200  #255
    res = cv2.addWeighted(sub_img, 0.5, white_rect, 0.5, 1.0)
    # Putting the image back to its position
    frame[y:y+h, x:x+w] = res


    frame[280:480, (320 - roiWidth):(320 + roiWidth)] = frameCircle
    frame = cv2.rectangle(frame, (320 - roiWidth, 280), (320 + roiWidth, 480), (255, 0, 0), 2) # big blue target window
    cv2.putText(frame, fps, (7, 15), font, 1, (0, 0, 0), 1, cv2.LINE_AA) 
    cv2.putText(frame, "Angle: " + angleTxt, (7, 35), font, 1, (0, 0, 0), 1, cv2.LINE_AA)
    cv2.putText(frame, "Radius & origo: " + str(rMax) + str(origo), (7, 55), font, 1, (0, 0, 0), 1,
                    cv2.LINE_AA)


# *** RED rectangulars   # origo = circle center
    aP1 = (320 - roiWidth + 1, 281)  # area Point 1
    aP2 = (320 - roiWidth + dangWidth, 281 + dangHeight)
    frame = cv2.rectangle(frame, aP1, aP2, (0, 0, 255), 1)  # 11,20
    if 320 - roiWidth + origo[0] > aP1[0] and 320 - roiWidth + origo[0] < aP2[0] and 280 + origo[1] > aP1[
        1] and 280 + origo[1] < aP2[1]:
        print("forward,50,20")
        print("RED left,50,20")
    bP1 = (320 + roiWidth - dangWidth, 281)
    bP2 = (320 + roiWidth - 1, 281 + dangHeight)
    frame = cv2.rectangle(frame, bP1, bP2, (0, 0, 255), 1)
    if 320 - roiWidth + origo[0] > bP1[0] and 320 - roiWidth + origo[0] < bP2[0] and 280 + origo[1] > bP1[
        1] and 280 + origo[1] < bP2[1]:
        print("forward,50,20")
        print("RED right,50,20")
   
# *** ORANGE rectangulars ****************************************************
    cP1 = (320 - roiWidth + 1, 281 + dangHeight)
    cP2 = (320 - roiWidth + dangWidth, 281 + 2 * dangHeight)
    frame = cv2.rectangle(frame, cP1, cP2, (0, 102, 255), 1)
    if 320 - roiWidth + origo[0] > cP1[0] and 320 - roiWidth + origo[0] < cP2[0] and 280 + origo[1] > cP1[
        1] and 280 + origo[1] < cP2[1]:
        print("forward,50,20")
        print("ORANGE left,50,20")
    dP1 = (320 + roiWidth - dangWidth, 281 + dangHeight)
    dP2 = (320 + roiWidth - 1, 281 + 2 * dangHeight)
    frame = cv2.rectangle(frame, dP1, dP2, (0, 102, 255), 1)
    if 320 - roiWidth + origo[0] > dP1[0] and 320 - roiWidth + origo[0] < dP2[0] and 280 + origo[1] > dP1[
            1] and 280 + origo[1] < dP2[1]:
        print("forward,50,20")
        print("ORANGE right,50,20")

# *** YELLOW rectangulars ****************************************************
    eP1 = (320 - roiWidth + 1, 281 + 2 * dangHeight)
    eP2 = (320 - roiWidth + dangWidth, 281 + 3 * dangHeight)
    frame = cv2.rectangle(frame, eP1, eP2, (0, 255, 255), 1)
    if 320 - roiWidth + origo[0] > eP1[0] and 320 - roiWidth + origo[0] < eP2[0] and 280 + origo[1] > eP1[
        1] and 280 + origo[1] < eP2[1]:
        print("forward,50,20")
        print("YELLOW left,50,20")
    fP1 = (320 + roiWidth - dangWidth, 281 + 2 * dangHeight)
    fP2 = (320 + roiWidth - 1, 281 + 3 * dangHeight)
    frame = cv2.rectangle(frame, fP1, fP2, (0, 255, 255), 1)
    if 320 - roiWidth + origo[0] > fP1[0] and 320 - roiWidth + origo[0] < fP2[0] and 280 + origo[1] > fP1[
            1] and 280 + origo[1] < fP2[1]:
        print("forward,50,20")
        print("YELLOW right,50,20")

# *** GREEEN rectangulars ****************************************************
    gP1 = (320 - roiWidth + 1, 281 + 3 * dangHeight)
    gP2 = (320 - roiWidth + dangWidth, 281 + 4 * dangHeight)
    frame = cv2.rectangle(frame, gP1, gP2, (0, 255, 0), 1)
    if 320 - roiWidth + origo[0] > gP1[0] and 320 - roiWidth + origo[0] < gP2[0] and 280 + origo[1] > gP1[
            1] and 280 + origo[1] < gP2[1]:
        print("forward,50,20")
        print("GREEEN left,50,20")
        #ser.write(bytes('YES\n','utf-8'))
        with serial.Serial('/dev/tty'+usb, 9600, timeout=10) as ser:
            #while True:
            ser.write(bytes('forward,50,20\n','utf-8'))
            ser.write(bytes('left,50,20\n','utf-8'))
        # print("Radius & origo: " + str(rMax) + str(origo))
    hP1 = (320 + roiWidth - dangWidth, 281 + 3 * dangHeight)
    hP2 = (320 + roiWidth - 1, 281 + 4 * dangHeight)
    frame = cv2.rectangle(frame, hP1, hP2, (0, 255, 0), 1)
    if 320 - roiWidth + origo[0] > hP1[0] and 320 - roiWidth + origo[0] < hP2[0] and 280 + origo[1] > hP1[
            1] and 280 + origo[1] < hP2[1]:
        print("forward,50,20")
        print("GREEEN right,50,20")
        with serial.Serial('/dev/tty'+usb, 9600, timeout=10) as ser:
            #while True:
            ser.write(bytes('forward,50,20\n','utf-8'))
            ser.write(bytes('right,50,20\n','utf-8'))
    
    pP1 = (320 - roiWidth + dangWidth, 281)
    pP2 = (320 + roiWidth - dangWidth, 480)
    if 320 - roiWidth + origo[0] > pP1[0] and 320 - roiWidth + origo[0] < pP2[0] and 280 + origo[1] > pP1[
            1] and 280 + origo[1] < pP2[1]:
        print("simple forward,50,20")
    # constant movement:
        with serial.Serial('/dev/tty'+usb, 9600, timeout=10) as ser:
            ser.write(bytes('forward,50,20\n','utf-8')) 

    cv2.imshow('FINAL', frame)
    cv2.moveWindow('FINAL',0,300)


cam.release()
cv2.destroyAllWindows()

