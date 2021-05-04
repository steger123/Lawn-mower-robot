from panda3d.core import loadPrcFile
loadPrcFile("config/conf.prc")
import random
import cv2
import numpy as np
from PIL import ImageGrab
import time

from direct.showbase.ShowBase import ShowBase
from panda3d.core import PointLight, AmbientLight, NodePath, DirectionalLight, Camera, WindowProperties, TextureStage
from math import sin, cos
from panda3d.core import Vec4

keyMap = {
    "up": False,
    "down": False,
    "left": False,
    "right": False,
    "w": False,
    "start": False

}
roiWidth = 60  #total is 2*60=120
prev_frame_time = 0
new_frame_time = 0
font = cv2.FONT_HERSHEY_PLAIN


# callback function to update the keyMap
def updateKeyMap(key, state):
    keyMap[key] = state


class LightsAndShadows(ShowBase):
    def __init__(self):
        super().__init__()
       # self.set_background_color(0, 0, 0, 1)
       # base.camera.setPos(0, 0, 0)
       # print("Skybox")
        self.skysphere = loader.loadModel("SkySphere.bam")
        self.skysphere.setBin('background', 1)
        self.skysphere.setDepthWrite(0)
        self.skysphere.reparentTo(self.render)
        lightIntensity = 0.7

# C:\Users\Bill\anaconda3\Lib\site-packages\panda3d\models
        #self.floor = self.loader.loadModel('myModels/soil_6')  # material texture must be png (jpg not okay)
        #self.floor.setPos(0, 0, -0.5)  #-2.5
        #self.floor.reparentTo(self.render)

        self.floor = self.loader.loadModel('myModels/slab')
        self.floor.setPos(0, 0, -1.5)  # -2.5
        self.floor.reparentTo(self.render)
        tex = self.loader.loadTexture('myModels/tex/soil_6.jpg')
        self.floor.setTexture(tex, 1)
        self.floor.setTexScale(TextureStage.getDefault(), 50, 50)

        rndRange=1.2
        for i in range(20):   # PUT 3x100 lettuce on teh scene:
            self.lett_1 = self.loader.loadModel('myModels/lettuce_3')  # material texture must be png (jpg not okay)
            rnd = random.random()*0.1*rndRange
            self.lett_1.setPos(-0.5-rnd, 2+i/2, -0.5)
            self.lett_1.setScale(0.1, 0.1, 0.1)
            self.lett_1.reparentTo(self.render)
            self.lett_2 = self.loader.loadModel('myModels/lettuce_32')  # material texture must be png (jpg not okay)
            rnd = random.random() * 0.1*rndRange
            self.lett_2.setPos(0-rnd, 2+i/2, -0.5)
            self.lett_2.setScale(0.1, 0.1, 0.1)
            self.lett_2.reparentTo(self.render)
            self.lett_3 = self.loader.loadModel('myModels/lettuce_32')  # material texture must be png (jpg not okay)
            rnd = random.random() * 0.1 *rndRange
            self.lett_3.setPos(0.5-rnd, 2+i/2, -0.5)
            self.lett_3.setScale(0.1, 0.1, 0.1)
            self.lett_3.reparentTo(self.render)


        #self.cone = self.loader.loadModel('myModels/cone')  # material texture must be png (jpg not okay)
        #self.cone.setPos(1, 2, -1)
        ##self.cone.setScale(0.1, 0.1, 0.1)
        #self.cone.reparentTo(self.render)

        self.tree3 = self.loader.loadModel('myModels/christmas_tree')
        self.tree3.setPos(-4, 7, -0.5)
        self.tree3.reparentTo(self.render)
        self.box = self.loader.loadModel('models/box')
        self.box.setPos(0, 0, 0)  #  liked to camera -0.2 0.2
        self.box.setScale(0.2, 0.2, 0.2)
        self.box.reparentTo(self.cam)

# ********* END MODELS ********

# ****** AMBIENT light **********
        ambientLight  = AmbientLight("ambient light")
        ambientLight.setColor(Vec4(0.7, 0.7, 0.7, 1))  # 0.2 0.2 0.2 lightIntensity
        self.ambientLightNodePath = self.render.attachNewNode(ambientLight)  # attach to renderer ambiant light node path
        #self.trees.setLight(ambientLightNodePath)
        self.render.setLight(self.ambientLightNodePath)  # illuminate everything

        # ****** DIRECTIONAL light **********
        mainLight = DirectionalLight("main light")
        mainLight.setColor(Vec4(0.5, 0.5, 0.5, 1))
        self.mainLightNodePath = self.render.attachNewNode(mainLight)
        # Turn it around by 45 degrees, and tilt it down by 45 degrees: setHpr(45, -45, 0)
        self.mainLightNodePath.setHpr(45, -90, 0)
        render.setLight(self.mainLightNodePath)

        self.render.setShaderAuto()             # create shadow

# ********* CAMERA ********
        #self.camera.lookAt(0, 1, -0.5)  # Camera tilted with Y & Z coordinates
        self.camera.setPos(0, 0, 0)
        #self.camera.setHpr(90, 0, 0)  #not reacting
        #base.camera.setHpr(90, 0, 0)


    # SECOND stationery:
        wp = WindowProperties()
        wp.setSize(500, 500)
        wp.setOrigin(800, 50)
        win2 = base.openWindow(props=wp, aspectRatio=1)
        cam2 = base.camList[1]
        cam2.setPos(0, 18, 5)  # 2nd relative to 'camera' 2 -20 10
        cam2.lookAt(0, 0, 0)
        cam2.reparentTo(render)

        taskMgr.add(self.skysphereTask, "SkySphere Task")

    def skysphereTask(self, task):
        self.skysphere.setPos(base.camera, 0, 0, 0)
#=====================================

# MOVING THE CAMERA
        self.accept("arrow_left", updateKeyMap, ["left", True])
        self.accept("arrow_left-up", updateKeyMap, ["left", False])
        self.accept("arrow_right", updateKeyMap, ["right", True])
        self.accept("arrow_right-up", updateKeyMap, ["right", False])

        self.accept("arrow_up", updateKeyMap, ["up", True])
        self.accept("arrow_up-up", updateKeyMap, ["up", False])
        self.accept("arrow_down", updateKeyMap, ["down", True])
        self.accept("arrow_down-up", updateKeyMap, ["down", False])

        self.accept("w", updateKeyMap, ["w", True])
        self.accept("w-up", updateKeyMap, ["w", False])
        self.accept("s", updateKeyMap, ["start", True])
        self.accept("s-up", updateKeyMap, ["start", False])

        self.camManualSpeed = 1.5  # Camera manual movment speed
        #self.angle = 0


#        self.taskMgr.add(self.move_light, "move-light")
        self.taskMgr.add(self.update, "update")

    def move_light(self, task):
        ft = globalClock.getFrameTime()
        self.light_model.setPos(cos(ft)*4, sin(ft)*4, 0)
        return task.cont


    def update(self, task):   # ******************** MAIN LOOP ********************
        dt = globalClock.getDt()
        pos = self.cam.getPos()
        #boxx = self.box.getPos()
        camAutoSpeed = 1.5   # When I press "s"


        if keyMap["left"]:
            pos.x -= self.camManualSpeed * dt
        if keyMap["right"]:
            pos.x += self.camManualSpeed * dt
        if keyMap["up"]:
            pos.y += self.camManualSpeed * dt
        if keyMap["down"]:
            pos.y -= self.camManualSpeed * dt
            # Compenstae to achive horizontal movenr, becuse Camera tilted with Y & Z coordinates
            #pos.y -= camAutoSpeed * (1 / 1) * dt # 2= how fast moving forward
            #pos.z -= camAutoSpeed * 2*(0.3 / 1) * dt
        #if keyMap["w"]:
         #   lightIntensity += 0.1  # not working
        #if keyMap["start"]:
        #    #base.camera.lookAt(0, 1, -0.3)
            # Compenstae to achive horizontal movenr, becuse Camera tilted with Y & Z coordinates
        #    pos.y += camAutoSpeed * (1/1) * dt # 2= how fast moving forward
        #    pos.z += camAutoSpeed * (0.3/1) * dt
        #else:
        #    camAutoSpeed = 0.5

        #print(keyMap["start"])
        # not woting here: ambientLight.setColor(Vec4(lightIntensity, lightIntensity, lightIntensity, 1))
        # ********* NAVIGATION
        ###base.camera.lookAt(0, 1, -0.3)
        ###pos.y += camSpeed*(1/100)   # 2= how fast moving forward
        ###pos.z += camSpeed*(0.3/100)
       # base.camera.setHpr(self.angle , 0, 0)
        #base.camera.setHpr(30, 0, 0)
       # base.camera.setHpr(self.heading, self.pitch, 0)
        #base.camera.setX(22)
        #base.camera.setY(-6)
        #base.camera.setZ(14)
        #x = base.camera.getX()
        #y = base.camera.getY()
        #z = base.camera.getZ()
       # self.title = OnscreenText(text=str(x) + ":" + str(y) + ":" + str(z), style=1, fg=(1, 1, 0, 1), pos=(0, 0),
         #                         scale=0.07)

        #base.disableMouse()
        self.cam.setPos(pos)  # 'box' moving along, becuse parented

        grabFrame = ImageGrab.grab(bbox=(0, 30, 640, 510))
        frame = cv2.cvtColor(np.array(grabFrame), cv2.COLOR_RGB2BGR)

        roi = frame[280:480, (320 - roiWidth):(320 + roiWidth)]  # 640x480
        cv2.imshow('ROI', roi)
        cv2.moveWindow('ROI', 250, 520)  # 650,0

        hsv = cv2.cvtColor(roi, cv2.COLOR_BGR2HSV)
        hueImg, satImg, valImg = cv2.split(hsv)  # *** Here can be set the detected image sensitivity !!!
        hueMask = cv2.inRange(hueImg, 10, 80)  # 10,80
        satMask = cv2.inRange(satImg, 100, 255)  # 100,250
        valMask = cv2.inRange(valImg, 100, 255)  # 100,250
        hueMask = hueMask & satMask & valMask
        cv2.imshow("COMBO_MASK", hueMask)
        cv2.moveWindow('COMBO_MASK', 400, 520)  # 650, 200

        vertical = np.copy(hueMask)  # copy only the HUE for erode and dilate operations
        rows = vertical.shape[0]  # 640
        verticalsize = rows // 9  # influencing the target direction !!!
        # Create structure element for extracting vertical lines through morphology operations
        verticalStructure = cv2.getStructuringElement(cv2.MORPH_RECT, (1, verticalsize))
        vertical = cv2.erode(vertical, verticalStructure)
        vertical = cv2.dilate(vertical, verticalStructure)
        cv2.imshow("COMBO_VERTICAL", vertical)
        cv2.moveWindow('COMBO_VERTICAL', 550, 520)  # 650, 400

        contours, hierarhy = cv2.findContours(vertical, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)
        roi = cv2.drawContours(roi, contours, -1, (0, 0, 250), 1)
        cv2.fillPoly(roi, pts=contours, color=(255, 0, 255))
        cv2.imshow('CONTOUR', roi)
        cv2.moveWindow('CONTOUR', 700, 0)

        # get conture centers
        contours_poly = len(contours)
        center = len(contours)
        radius = len(contours)
        # find enclosing polygon which fits around the contures
        rMax = 1
        origo = (roiWidth, 100)
        for i in range(0, len(contours)):
            cont = contours[i]
            # approx = cv2.approxPolyDP(cont, 2, True)
            # (x,y),radius = cv2.minEnclosingCircle(approx)
            (x, y), radius = cv2.minEnclosingCircle(cont)
            center = (int(x), int(y))
            radius = int(radius)
            frameCircle = cv2.circle(roi, center, radius, (255, 0, 0), 2)  # put circle on the image
            if x > 0 and x < 640 and y > 0 and y < 480:  # ROI window for region of interes can be smaller (not the full window)
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
        if x22 < x11:
            angle = -1 * angle
        # print("Angle :", angle * 180 / np.pi)
        angleTxt = str("%.1f" % round(angle * 180 / np.pi, 1))
        cv2.imshow('CIRCLE', frameCircle)
        cv2.moveWindow('CIRCLE', 900, 0)



        frame[280:480, (320 - roiWidth):(
                    320 + roiWidth)] = frameCircle  # 280:480, 220:420 put back the detected image (200x200) to the original (640x480)
        # big blue target window  640/2=320  320 - 200/2 = 220
        #                         480 - 200 = 280
        frame = cv2.rectangle(frame, (320 - roiWidth, 280), (320 + roiWidth, 480), (255, 0, 0), 2)
      #  cv2.putText(frame, fps, (7, 15), font, 1, (255, 255, 255), 1, cv2.LINE_AA)
        cv2.putText(frame, "Angle: " + angleTxt, (7, 15), font, 1, (50, 50, 50), 1, cv2.LINE_AA)
        cv2.putText(frame, "Radius & origo: " + str(rMax) + str(origo), (7, 35), font, 1, (50, 50, 50), 1,
                    cv2.LINE_AA)

        dangWidth = 20  # danger zone width
        dangHeight = 50

        # *** RED rectangulars   # origo = circle center
        aP1 = (320 - roiWidth + 1, 281)  # area Point 1
        aP2 = (320 - roiWidth + dangWidth, 281 + dangHeight)
        frame = cv2.rectangle(frame, aP1, aP2, (0, 0, 255), 1)  # 11,20
        if 320 - roiWidth + origo[0] > aP1[0] and 320 - roiWidth + origo[0] < aP2[0] and 280 + origo[1] > aP1[
            1] and 280 + origo[1] < aP2[1]:
            print("forward, 50,20")
            print("hard left,50,20")
            self.cam.setPos(self.cam.getPos()[0], self.cam.getPos()[1]+0.3, self.cam.getPos()[2])
            self.cam.setHpr(self.cam.getHpr()[0] + 1.8, self.cam.getHpr()[1], self.cam.getHpr()[2])
            # print("Radius & origo: " + str(rMax) + str(origo))
        bP1 = (320 + roiWidth - dangWidth, 281)
        bP2 = (320 + roiWidth - 1, 281 + dangHeight)
        frame = cv2.rectangle(frame, bP1, bP2, (0, 0, 255), 1)
        if 320 - roiWidth + origo[0] > bP1[0] and 320 - roiWidth + origo[0] < bP2[0] and 280 + origo[1] > bP1[
            1] and 280 + origo[1] < bP2[1]:
            #print("forward, 50,20")
            #print("hard right,50,20")
            self.cam.setPos(self.cam.getPos()[0], self.cam.getPos()[1]+0.3, self.cam.getPos()[2])  # path correction
            self.cam.setHpr(self.cam.getHpr()[0] - 1.8, self.cam.getHpr()[1], self.cam.getHpr()[2])

        # *** ORANGE rectangulars ****************************************************
        cP1 = (320 - roiWidth + 1, 281 + dangHeight)
        cP2 = (320 - roiWidth + dangWidth, 281 + 2 * dangHeight)
        frame = cv2.rectangle(frame, cP1, cP2, (0, 102, 255), 1)
        if 320 - roiWidth + origo[0] > cP1[0] and 320 - roiWidth + origo[0] < cP2[0] and 280 + origo[1] > cP1[
            1] and 280 + origo[1] < cP2[1]:
            # print("forward, 50,20")
            #print("strong left,50,20")
            self.cam.setPos(self.cam.getPos()[0], self.cam.getPos()[1] + 0.3, self.cam.getPos()[2])
            self.cam.setHpr(self.cam.getHpr()[0] + 1.8, self.cam.getHpr()[1], self.cam.getHpr()[2])
        dP1 = (320 + roiWidth - dangWidth, 281 + dangHeight)
        dP2 = (320 + roiWidth - 1, 281 + 2 * dangHeight)
        frame = cv2.rectangle(frame, dP1, dP2, (0, 102, 255), 1)
        if 320 - roiWidth + origo[0] > dP1[0] and 320 - roiWidth + origo[0] < dP2[0] and 280 + origo[1] > dP1[
            1] and 280 + origo[1] < dP2[1]:
            #print("forward, 50,20")
            #print("strong right,50,20")
            self.cam.setPos(self.cam.getPos()[0], self.cam.getPos()[1]+0.3, self.cam.getPos()[2])  # path correction
            self.cam.setHpr(self.cam.getHpr()[0] - 1.8, self.cam.getHpr()[1], self.cam.getHpr()[2])

        # *** YELLOW rectangulars ****************************************************
        eP1 = (320 - roiWidth + 1, 281 + 2 * dangHeight)
        eP2 = (320 - roiWidth + dangWidth, 281 + 3 * dangHeight)
        frame = cv2.rectangle(frame, eP1, eP2, (0, 255, 255), 1)
        if 320 - roiWidth + origo[0] > eP1[0] and 320 - roiWidth + origo[0] < eP2[0] and 280 + origo[1] > eP1[
            1] and 280 + origo[1] < eP2[1]:
            #print("forward, 50,20")
            print("medium left,50,20")
            self.cam.setPos(self.cam.getPos()[0], self.cam.getPos()[1]+0.3, self.cam.getPos()[2])
            self.cam.setHpr(self.cam.getHpr()[0] + 1.8, self.cam.getHpr()[1], self.cam.getHpr()[2]) # *** !!!!
        fP1 = (320 + roiWidth - dangWidth, 281 + 2 * dangHeight)
        fP2 = (320 + roiWidth - 1, 281 + 3 * dangHeight)
        frame = cv2.rectangle(frame, fP1, fP2, (0, 255, 255), 1)
        if 320 - roiWidth + origo[0] > fP1[0] and 320 - roiWidth + origo[0] < fP2[0] and 280 + origo[1] > fP1[
            1] and 280 + origo[1] < fP2[1]:
            #print("forward, 50,20")
            print("medium right,50,20")
            self.cam.setPos(self.cam.getPos()[0], self.cam.getPos()[1]+0.3, self.cam.getPos()[2])
            self.cam.setHpr(self.cam.getHpr()[0] - 1.8, self.cam.getHpr()[1], self.cam.getHpr()[2])

        # *** GREEEN rectangulars ****************************************************
        gP1 = (320 - roiWidth + 1, 281 + 3 * dangHeight)
        gP2 = (320 - roiWidth + dangWidth, 281 + 4 * dangHeight)
        frame = cv2.rectangle(frame, gP1, gP2, (0, 255, 0), 1)
        if 320 - roiWidth + origo[0] > gP1[0] and 320 - roiWidth + origo[0] < gP2[0] and 280 + origo[1] > gP1[
            1] and 280 + origo[1] < gP2[1]:
            # print("forward, 50,20")
            #print("small left,50,20")
            self.cam.setPos(self.cam.getPos()[0], self.cam.getPos()[1]+0.3, self.cam.getPos()[2])
            self.cam.setHpr(self.cam.getHpr()[0] + 1.8, self.cam.getHpr()[1], self.cam.getHpr()[2])
        hP1 = (320 + roiWidth - dangWidth, 281 + 3 * dangHeight)
        hP2 = (320 + roiWidth - 1, 281 + 4 * dangHeight)
        frame = cv2.rectangle(frame, hP1, hP2, (0, 255, 0), 1)
        if 320 - roiWidth + origo[0] > hP1[0] and 320 - roiWidth + origo[0] < hP2[0] and 280 + origo[1] > hP1[
            1] and 280 + origo[1] < hP2[1]:
            #print("forward, 50,20")
            #print("small right,50,20")
            self.cam.setPos(self.cam.getPos()[0], self.cam.getPos()[1]+0.3, self.cam.getPos()[2])
            self.cam.setHpr(self.cam.getHpr()[0] - 1.8, self.cam.getHpr()[1], self.cam.getHpr()[2])

        #frame = cv2.rectangle(frame, (320 - roiWidth + 1, 281 + 3 * dangHeight),
            #                  (320 - roiWidth + dangWidth, 281 + 4 * dangHeight), (0, 255, 0), 1)
        #frame = cv2.rectangle(frame, (320 + roiWidth - 1, 281 + 3 * dangHeight),
             #                 (320 + roiWidth - dangWidth, 281 + 4 * dangHeight), (0, 255, 0), 1)

        #print("forward, 50,20")  # constant movement
        #constMove = self.camManualSpeed * dt
        if keyMap["start"]:
            self.cam.setPos(self.cam.getPos()[0], self.cam.getPos()[1] +self.camManualSpeed * dt*0.2 , self.cam.getPos()[2])


        cv2.imshow('FINAL', frame)
        cv2.moveWindow('FINAL', 700, 250)

        return task.cont


game = LightsAndShadows()

game.run()