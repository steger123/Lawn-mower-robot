import numpy as np
import sys
import cv2 as cv

cam=cv.VideoCapture(0)

while True:
    if cv.waitKey(1)==ord('q'):
        break

    ret, frame =cam.read()
    cv.imshow("SOURCE", frame)
    cv.moveWindow('SOURCE',0,0)

    #imScale = 50 # percent of original size
    #width = int(frame.shape[1]*imScale/100)
    #height = int(frame.shape[0]*imScale/100)
    #dim = (width,height)
    #resizedFrame = cv.resize(frame,dim,interpolation = cv.INTER_AREA)
    ##print("Resized Dimansions: ", resizedFrame.shape)
    ##cv.imshow("Resized", resizedFrame)

    roi=frame[0:50,0:300]
    roiGray=cv.cvtColor(roi,cv.COLOR_BGR2GRAY)

    #gray = cv.cvtColor(Frame, cv.COLOR_BGR2GRAY)
    #gray = cv.cvtColor(resizedFrame, cv.COLOR_BGR2GRAY)

    # Apply adaptiveThreshold at the bitwise_not of gray, notice the ~ symbol
    # gray = cv.bitwise_not(gray)
    gray = cv.bitwise_not(roiGray)
    bw = cv.adaptiveThreshold(gray, 255, cv.ADAPTIVE_THRESH_MEAN_C, \
                                cv.THRESH_BINARY, 15, -2)
    # Show BINARY image:
    cv.imshow("BINARY", bw)
    cv.moveWindow('BINARY',0,300)
    # Create the images that will use to extract the horizontal and vertical lines
    horizontal = np.copy(bw)
    vertical = np.copy(bw)

    # Specify size on horizontal axis:
    cols = horizontal.shape[1]
    horizontal_size = cols // 30
    # Create structure element for extracting horizontal lines through morphology operations
    horizontalStructure = cv.getStructuringElement(cv.MORPH_RECT, (horizontal_size, 1))
    # Apply morphology operations
    horizontal = cv.erode(horizontal, horizontalStructure)
    horizontal = cv.dilate(horizontal, horizontalStructure)
    # Show extracted HORIZONTAL lines:
    cv.imshow("HORIZONTAL", horizontal)
    cv.moveWindow('HORIZONTAL',0,450)

    # Specify size on vertical axis:
    rows = vertical.shape[0]
    verticalsize = rows // 30
    # Create structure element for extracting vertical lines through morphology operations
    verticalStructure = cv.getStructuringElement(cv.MORPH_RECT, (1, verticalsize))
    # Apply morphology operations
    vertical = cv.erode(vertical, verticalStructure)
    vertical = cv.dilate(vertical, verticalStructure)
    # Show extracted vertical lines
    #show_wait_destroy("vertical", vertical)
    cv.imshow("VERTICAL", vertical)
    cv.moveWindow('VERTICAL',0,600)


    # Refine / Smooth edges
    # [vert]
    # Inverse vertical image
    vertical = cv.bitwise_not(vertical)
    #show_wait_destroy("vertical_bit", vertical)
    cv.imshow("VERTICAL_INV_BIT", vertical)
    cv.moveWindow('VERTICAL_INV_BIT',0,750)
    '''
    Extract edges and smooth image according to the logic
    1. extract edges
    2. dilate(edges)
    3. src.copyTo(smooth)
    4. blur smooth img
    5. smooth.copyTo(src, edges)
    '''
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
    # Show final result:
    cv.imshow("smooth - final", vertical)
    cv.moveWindow('smooth - final',0,900)


cam.release()
cv.destroyAllWindows()
