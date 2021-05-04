
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

roi = cv.imread('d.jpg')
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

while(True):
#    ret, frame = cap.read()


	hueMask = cv.inRange(hueImg, 10,80)  #10,80
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
	#vector_1 = [0, 1]
	#vector_2 = [2, 3]

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

	#cv.waitKey(0)
	if cv.waitKey(1)==ord('q'):
		break
cv.destroyAllWindows()

"""for i in range(0,len(contours)):
	contours_poly[i] = cv.approxPolyDP(contours[i], 2, True)
	center[i], radius[i] = cv.minEnclosingCircle(contours_poly[i])
	cv.circle(frame, (center[i].x, center[i].y), radius[i], (255,0,0), 2)
"""

"""
#Rotated rectangle
for i in range(0,len(contours)):
	cont = contours[i]
	rect = cv.minAreaRect(cont)
	box = cv.boxPoints(rect)
	box = np.int0(box)
	frameRect = cv.drawContours(frame, [box],0,(0,255,0),2)
cv.imshow('RECT', frameRect)
cv.moveWindow('RECT',1000,600)
"""

"""
gray = cv.cvtColor(frame, cv.COLOR_BGR2GRAY)
# Show gray image
#show_wait_destroy("gray", gray)
cv.imshow("GRAY", gray)
cv.moveWindow('GRAY',0,150)
# [gray]
# [bin]
# Apply adaptiveThreshold at the bitwise_not of gray, notice the ~ symbol
gray = cv.bitwise_not(gray)
bw = cv.adaptiveThreshold(gray, 255, cv.ADAPTIVE_THRESH_MEAN_C, \
                                cv.THRESH_BINARY, 15, -2)
# Show binary image
#show_wait_destroy("binary", bw)
cv.imshow("BINARY", bw)
cv.moveWindow('BINARY',0,300)
# [bin]
# [init]
# Create the images that will use to extract the horizontal and vertical lines
horizontal = np.copy(bw)
vertical = np.copy(bw)
# [init]
# [horiz]
# Specify size on horizontal axis
cols = horizontal.shape[1]
horizontal_size = cols // 50
# Create structure element for extracting horizontal lines through morphology operations
horizontalStructure = cv.getStructuringElement(cv.MORPH_RECT, (horizontal_size, 1))
# Apply morphology operations
horizontal = cv.erode(horizontal, horizontalStructure)
horizontal = cv.dilate(horizontal, horizontalStructure)
# Show extracted horizontal lines
#show_wait_destroy("horizontal", horizontal)
###cv.imshow("HORIZONTAL", horizontal)
###cv.moveWindow('HORIZONTAL',0,450)
# [horiz]
# [vert]
# Specify size on vertical axis
rows = vertical.shape[0]
verticalsize = rows // 50
# Create structure element for extracting vertical lines through morphology operations
verticalStructure = cv.getStructuringElement(cv.MORPH_RECT, (1, verticalsize))
# Apply morphology operations
vertical = cv.erode(vertical, verticalStructure)
vertical = cv.dilate(vertical, verticalStructure)
# Show extracted vertical lines
#show_wait_destroy("vertical", vertical)
cv.imshow("VERTICAL", vertical)
cv.moveWindow('VERTICAL',0,450)

# Refine / Smooth edges
# [vert]
# Inverse vertical image
vertical = cv.bitwise_not(vertical)
#show_wait_destroy("vertical_bit", vertical)
cv.imshow("VERTICAL_INVERSE", vertical)  #bit
cv.moveWindow('VERTICAL_INVERSE',0,600)
"""

"""
    Extract edges and smooth image according to the logic
    1. extract edges
    2. dilate(edges)
    3. src.copyTo(smooth)
    4. blur smooth img
    5. smooth.copyTo(src, edges)
"""

"""
# Step 1
edges = cv.adaptiveThreshold(vertical, 255, cv.ADAPTIVE_THRESH_MEAN_C, \
                                cv.THRESH_BINARY, 3, -2)
###show_wait_destroy("edges", edges)
# Step 2
kernel = np.ones((2, 2), np.uint8)
edges = cv.dilate(edges, kernel)
###show_wait_destroy("dilate", edges)
# Step 3
smooth = np.copy(vertical)
# Step 4
smooth = cv.blur(smooth, (2, 2))
# Step 5
(rows, cols) = np.where(edges != 0)
vertical[rows, cols] = smooth[rows, cols]
# Show final result
#show_wait_destroy("smooth - final", vertical)
cv.imshow("smooth - final", vertical)
cv.moveWindow('smooth - final',0,750)
# [smooth]
#myGray = cv.cvtColor(vertical,cv.COLOR_BGR2GRAY)

minLineLenght = 400
maxLineGap = 40
lines = cv.HoughLinesP(vertical, 10, np.pi/180, 500, minLineLenght, maxLineGap)
#print(lines.shape)
#lines2 = cv.HoughLines(vertical,1,np.pi/180,200) # returns an array of (\rho, \theta) values
#print(lines2.shape)
#for x1,x2,y1,y2 in lines[0]:
#    cv.line(frame, (x1,y1),(x2,y2), (0,255,0),2)
"""

"""
if lines is not None:
	for line in lines:
		for x1,y1,x2,y2 in line:
			cv.line(frame,(x1,y1),(x2,y2),(0,255,0),2)

cv.imshow('Final',frame)
cv.moveWindow('Final',500,0)
"""
"""
# detect circles in the image
circles = cv.HoughCircles(vertical, cv.HOUGH_GRADIENT, 1.3, 4)
print(circles.shape)
# ensure at least some circles were found
if circles is not None:
	# convert the (x, y) coordinates and radius of the circles to integers
	circles = np.round(circles[0, :]).astype("int")
	# loop over the (x, y) coordinates and radius of the circles
	for (x, y, r) in circles:
		# draw the circle in the output image, then draw a rectangle
		# corresponding to the center of the circle
		#cv.circle(frame, (x, y), r, (0, 255, 0), 2)
		cv.rectangle(frame, (x - 2, y - 2), (x + 2, y + 2), (0, 50, 255), 2)
	# show the output image
cv.imshow('Final',frame)
cv.moveWindow('Final',500,0)
"""

