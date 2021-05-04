#Run indesame directory where the image file is 1111111111
# https://opencv-python-tutroals.readthedocs.io/en/latest/py_tutorials/py_imgproc/py_houghlines/py_houghlines.html
# jetbot@jetson-4-3:~/Desktop/pyPro/myOpenCV$ /usr/bin/python3 /home/jetbot/Desktop/pyPro/myOpenCV/edgeLinePic.py
import cv2 as cv
import numpy as np

def nothing(x):
    pass

#frame = cv.imread('input.jpg')
#roi = cv.imread('input.jpg')
#roi = cv.imread('f1.jpg')
# Create a window
cv.namedWindow('Control Panel')
# create trackbars for color change
cv.createTrackbar('lowH','Control',0,179,nothing)
cv.createTrackbar('highH','Control',150,179,nothing)


while(True):
#    ret, frame = cap.read()

    roi = cv.imread('d.jpg', cv.IMREAD_UNCHANGED) #/home/jetbot/Desktop/pyPro/myOpenCV/

    dim = (640, 480)
    # resize image
    roi = cv.resize(roi, dim, interpolation = cv.INTER_AREA)

    frame=roi[280:480, 220:420]   # 640x480

    cv.imshow('SOURCE', frame)
    cv.moveWindow('SOURCE',0,0)

    hsv = cv.cvtColor(frame, cv.COLOR_BGR2HSV)
    hueImg, satImg, valImg = cv.split(hsv)
    cv.imshow("HUE", hueImg)
    cv.moveWindow('HUE',300,0)
    cv.imshow("SATURATION", satImg)
    cv.moveWindow('SATURATION',500,0)
    cv.imshow("VALUE", valImg)
    cv.moveWindow('VALUE',700,0)
# get current positions of the trackbars:

	lowH = cv.getTrackbarPos('lowH','Control')
	highH = cv.getTrackbarPos('highH','Control')
	hueMask = cv.inRange(hueImg, lowH,highH)  #10,80
	satMask = cv.inRange(satImg, 100,255)  #100,250
	valMask = cv.inRange(valImg, 100,255)  #100,250
	hueMask = hueMask & satMask & valMask
	cv.imshow("COMBO_MASK", hueMask)
	cv.moveWindow('COMBO_MASK',1000,0)

#noNoise = cv.fastNlMeansDenoising(hueMask,250,7,21)
#cv.imshow("NO_NOISE", noNoise)
#cv.moveWindow('NO_NOISE',1000,150)

	vertical = np.copy(hueMask)
	rows = vertical.shape[0]
	verticalsize = rows // 10
# Create structure element for extracting vertical lines through morphology operations
	verticalStructure = cv.getStructuringElement(cv.MORPH_RECT, (1, verticalsize))
	vertical = cv.erode(vertical, verticalStructure)
	vertical = cv.dilate(vertical, verticalStructure)
	cv.imshow("COMBO_VERTICAL", vertical)
	cv.moveWindow('COMBO_VERTICAL',1200,0)

	contours, hierarhy = cv.findContours(vertical, cv.RETR_TREE, cv.CHAIN_APPROX_SIMPLE)
	frame = cv.drawContours(frame, contours, -1, (0,0,250), 1)
	cv.fillPoly(frame, pts =contours, color=(255,0,255))

	cv.imshow('CONTOUR', frame)
	cv.moveWindow('CONTOUR',1400,0)

#get conture centers
	contours_poly = len(contours)
	center = len(contours)
	radius = len(contours)
#find enclosing polygon which fits around the contures
	rMax = 1
	origo = (100,100)
	for i in range(0,len(contours)):
		cont = contours[i]
		approx = cv.approxPolyDP(cont, 2, True)
		#(x,y),radius = cv.minEnclosingCircle(approx)
		(x,y),radius = cv.minEnclosingCircle(cont)
		center = (int(x), int(y))
		radius = int(radius)
		frameCircle = cv.circle(frame, center, radius, (255,0,0), 2)
		if x>0 and x<640 and y>0 and y<480:  #ROI window for region of interes can be smaller (not the full window)
			if int(radius) > rMax:
				rMax = int(radius)
				origo = center
	print("rMax & origo: ")
	print(rMax, origo)

	frameCircle = cv.line(frame, (100,200), (100,100), (0,255,255),2)   # ideal line
	frameCircle = cv.line(frame, (100,200), origo, (0,0,250),2)		# direction required

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

	cv.imshow('CIRCLE', frameCircle)
	cv.moveWindow('CIRCLE',1600,0)

	roi[280:480, 220:420]=frameCircle
	roi = cv.rectangle(roi, (220,280),(420,480), (255,0,0),2)
	cv.imshow('FINAL', roi)
	cv.moveWindow('FINAL',1000,600)

    if cv.waitKey(1)==ord('q'):
        break

cv.destroyAllWindows()
