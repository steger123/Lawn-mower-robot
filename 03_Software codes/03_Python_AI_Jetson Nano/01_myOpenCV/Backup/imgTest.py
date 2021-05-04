import cv2
import numpy as np

def nothing(x):
    pass

#frame = cv2.imread('input.jpg')
#roi = cv2.imread('input.jpg')
#roi = cv2.imread('f1.jpg')
# Create a window
cv2.namedWindow('Control Panel')
cv2.moveWindow('Control Panel',0,600)
# create trackbars for color change
cv2.createTrackbar('lowH','Control Panel',10,255, nothing)
cv2.createTrackbar('highH','Control Panel',80,255,nothing)
cv2.createTrackbar('lowS','Control Panel',100,255,nothing)
cv2.createTrackbar('highS','Control Panel',250,255,nothing)
cv2.createTrackbar('lowV','Control Panel',100,255,nothing)
cv2.createTrackbar('highV','Control Panel',250,255,nothing)
fileName = '/home/jetbot/Desktop/pyPro/myOpenCV/leaf.jpeg'

roi = cv2.imread(fileName, cv2.IMREAD_UNCHANGED) #jetbot@jetson-4-3:~/Desktop/pyPro/myOpenCV
dimensions = roi.shape
# height, width, number of channels in image
height = roi.shape[0]
width = roi.shape[1]
channels = roi.shape[2]
print('Image Dimension    : ',dimensions)
print('Image Height       : ',height)
print('Image Width        : ',width)
print('Number of Channels : ',channels)

while(True):
    roi = cv2.imread(fileName, cv2.IMREAD_UNCHANGED) #jetbot@jetson-4-3:~/Desktop/pyPro/myOpenCV
    dim = (640, 480)
	# resize image
    roi = cv2.resize(roi, dim, interpolation = cv2.INTER_AREA)
    frame=roi[280:480, 220:420]   # 640x480
    
    cv2.imshow('SOURCE', frame)
    cv2.moveWindow('SOURCE',0,0)
    
    hsv = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)
    hueImg, satImg, valImg = cv2.split(hsv)
    cv2.imshow("orig HUE", hueImg)
    cv2.moveWindow('orig HUE',300,0)
    cv2.imshow("orig SATURATION", satImg)
    cv2.moveWindow('orig SATURATION',500,0)
    cv2.imshow("orig VALUE", valImg)
    cv2.moveWindow('orig VALUE',700,0)

# get current positions of the trackbars:
    lowH = cv2.getTrackbarPos('lowH', 'Control Panel')
    highH = cv2.getTrackbarPos('highH', 'Control Panel')
    lowS = cv2.getTrackbarPos('lowS', 'Control Panel')
    highS = cv2.getTrackbarPos('highS', 'Control Panel')
    lowV = cv2.getTrackbarPos('lowV', 'Control Panel')
    highV = cv2.getTrackbarPos('highV', 'Control Panel')
    
    hueMask = cv2.inRange(hueImg, lowH, highH)  #10,80
    satMask = cv2.inRange(satImg, lowS, highS)  #100,250
    valMask = cv2.inRange(valImg, lowV, highV)  #100,250
    cv2.imshow("mask HUE", hueImg)
    cv2.moveWindow('mask HUE',300,300)
    cv2.imshow("mask SATURATION", satImg)
    cv2.moveWindow('mask SATURATION',500,300)
    cv2.imshow("mask VALUE", valImg)
    cv2.moveWindow('mask VALUE',700,300)

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
    cv2.moveWindow('COMBO_VERTICAL',1000,0)

    contours, hierarhy = cv2.findContours(vertical, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)
    frame = cv2.drawContours(frame, contours, -1, (0,0,250), 1)
    cv2.fillPoly(frame, pts =contours, color=(255,0,255))
    cv2.imshow('CONTOUR', frame)
    cv2.moveWindow('CONTOUR',1200,0)

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
    print("rMax & origo: ")
    print(rMax, origo)
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
	#vector_1 = [0, 1]
	#vector_2 = [2, 3]
    unit_vector_1 = vector_1 / np.linalg.norm(vector_1)
    unit_vector_2 = vector_2 / np.linalg.norm(vector_2)
    dot_product = np.dot(unit_vector_1, unit_vector_2)
    angle = np.arccos(dot_product)
    if x22 < x11 :
        angle = -1* angle
    print("Angle :", angle*180/np.pi)
    cv2.imshow('CIRCLE', frameCircle)
    cv2.moveWindow('CIRCLE',1600,0)
    roi[280:480, 220:420]=frameCircle
    roi = cv2.rectangle(roi, (220,280),(420,480), (255,0,0),2)
    cv2.imshow('FINAL', roi)
    cv2.moveWindow('FINAL',1000,400)


    if cv2.waitKey(1)==ord('q'):
        break

cv2.destroyAllWindows()
