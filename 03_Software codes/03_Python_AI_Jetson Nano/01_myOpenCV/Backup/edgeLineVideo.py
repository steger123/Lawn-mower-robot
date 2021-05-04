# jetbot@jetson-4-3:~/Desktop/pyPro/myOpenCV$ /usr/bin/python3 /home/jetbot/Desktop/pyPro/myOpenCV/edgeLineVideo.py
import cv2
import numpy as np
import time

cam=cv2.VideoCapture(0)
# used to record the time when we processed last frame 
prev_frame_time = 0
# used to record the time at which we processed current frame 
new_frame_time = 0

while True:
    ret, roi =cam.read()
    #roi = cv2.imread('f1.jpg')
    
    frame=roi[280:480, 220:420]   # 640x480
    cv2.imshow('ROI', frame)
    cv2.moveWindow('ROI',0,0)

    hsv = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)
    hueImg, satImg, valImg = cv2.split(hsv)
    hueMask = cv2.inRange(hueImg, 10,80)  #10,80
    satMask = cv2.inRange(satImg, 100,255)  #100,250
    valMask = cv2.inRange(valImg, 100,255)  #100,250
    hueMask = hueMask & satMask & valMask
    cv2.imshow("COMBO_MASK", hueMask)
    cv2.moveWindow('COMBO_MASK',1000,0)

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
    cv2.moveWindow('COMBO_VERTICAL',1200,0)

    contours, hierarhy = cv2.findContours(vertical, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)
    frame = cv2.drawContours(frame, contours, -1, (0,0,250), 1)
    cv2.fillPoly(frame, pts =contours, color=(255,0,255))
    cv2.imshow('CONTOUR', frame)
    cv2.moveWindow('CONTOUR',1400,0)

#get conture centers
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
	    frameCircle = cv2.circle(frame, center, radius, (255,0,0), 2)
	    if x>0 and x<640 and y>0 and y<480:  #ROI window for region of interes can be smaller (not the full window)
		    if int(radius) > rMax:
			    rMax = int(radius)
			    origo = center
    print("rMax & origo: ", rMax, origo)  #rMax or origo can be 0 !
    #print(rMax, origo)
    frameCircle = cv2.line(frame, (100,200), (100,100), (0,255,255),2)   # ideal line
    frameCircle = cv2.line(frame, (100,200), origo, (0,0,250),2)		# direction required

    x11 = 100
    y11 = 200
    x12 = 100
    y12 = 100
    #origo
    x21 = 100
    y21 = 200
    x22 = origo[0]
    y22 = origo[1]

    vector_1 = [x11 - x12, y11 - y12]
    vector_2 = [x21 - x22, y21 - y22]
    unit_vector_1 = vector_1 / np.linalg.norm(vector_1)
    unit_vector_2 = vector_2 / np.linalg.norm(vector_2)
    dot_product = np.dot(unit_vector_1, unit_vector_2)
    angle = np.arccos(dot_product)
    if x22 < x11 :
        angle = -1* angle
    print("Angle :", angle*180/np.pi)
    cv2.imshow('CIRCLE', frameCircle)
    cv2.moveWindow('CIRCLE',1600,0)

    # cv2.waitKey(0)
    if cv2.waitKey(1)==ord('q'):
        break

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
    x, y, w, h = 0, 0, 150, 50
    sub_img = roi[y:y+h, x:x+w]
    white_rect = np.ones(sub_img.shape, dtype=np.uint8) * 20  #255
    res = cv2.addWeighted(sub_img, 0.5, white_rect, 0.5, 1.0)
    # Putting the image back to its position
    roi[y:y+h, x:x+w] = res

    roi[280:480, 220:420]=frameCircle
    roi = cv2.rectangle(roi, (220,280),(420,480), (255,0,0),2) # big blue target window
    cv2.putText(roi, fps, (7, 35), font, 2, (0, 0, 0), 2, cv2.LINE_AA) 
    cv2.imshow('FINAL', roi)
    cv2.moveWindow('FINAL',1000,600)


cam.release()
cv2.destroyAllWindows()

