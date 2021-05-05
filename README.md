# Lawn-mower-robot
More information:

https://github.com/steger123/Lawn-mower-robot/wiki
![alt text](https://github.com/steger123/Lawn-mower-robot/blob/master/pics/openLogo.png)

## The main purpose of the project:

Provide a platform for small and medium size robot rovers in sofware as well as hardware side for maunal, semi and full autonomouse navigation.
The mechanical side can be anyting. For exmaple a lawnmower robot or little bit bigger scouting robot for agricultural purpose
As the project progressing and evolving **You can learn more and more**, **and of course, have the designs and codes on free.**

## Sofrware:

### BASE:
Mission Planner: C#
https://github.com/steger123/Lawn-mower-robot/tree/master/03_Software%20codes/02_C%23_Mission%20planner

### Rover:
Main MCU : C++ (Arduino)
https://github.com/steger123/Lawn-mower-robot/tree/master/03_Software%20codes/01_C%2B%2B_Arduino/01_Rover/Test_Motor_v12
AI: Python (Jetson NANO)
https://github.com/steger123/Lawn-mower-robot/tree/master/03_Software%20codes/03_Python_AI_Jetson%20Nano/01_myOpenCV

### Add on Hardware (electronic):
1. Arduino MEGA later Shieldbuddy TC375 (3 cores !
2. Custom made shield, sitting on the Arduino MEGA to have enough space for connectors (4xseial, 4xSPI, 3xI2C, buttons, 3xultrasonic sensos, volt & ampere meter etc.).
3. GPS: 2 x GNSS modules with RTK / Xbee
4. Base's RTK: simpleRTK2Blite from Ardusimple or GPS Development Tools SparkFun GPS-RTK-SMA Breakout - ZED-F9P
5. Rovers RTK: RTK+INS simpleRTK2B-F9R V3 from Ardusimple or SparkFun Accessories SparkFun GPS-RTK Dead Reckoning Breakout - ZED-F9R
6. Motor controllers: 2x DC
7. RF radio: for additional Base to Rover and back communication (remote controll, getting sensor date from rover to mission control software).
8. Jetson Nano with normal webcam. Communicating via serial port to the MEGA (detect plant diseases, following plant line)
9. Sensors on the shield (utlrasonoic distance, battery volt, curret mesuring, rain etc.)

## Body:
As you like. In scouting for example: 1m x 1m x 0.5m

## Wheels:
As you like. In scouting for example: driven 280 mm in diamteter + castor wheels.
it can be smoot like a bicycle or thick lugs like on a tractor tire (good grippong during scouting on the field).

## Engine/motor:
As you like. In scouting for example: 2x 50W DC motors with encoders. 50 rpm

## Transmission:
As you like. In scouting for example: plannetary gerabox.

## Collaboration (software, electronic hardware, new PCB/shiled, mechanical parts etc.):
If You like to join, please contact me:
andras.steger@protonmail.com

## Latest news:
https://twitter.com/StegerAndras

![alt text](https://github.com/steger123/Lawn-mower-robot/blob/master/01_Pictures/99_GitHub/Rover_1.jpg)
![alt text](https://github.com/steger123/Lawn-mower-robot/blob/master/01_Pictures/99_GitHub/Mission%20planner_6%20C%23.jpg)
![alt text](https://github.com/steger123/Lawn-mower-robot/blob/master/01_Pictures/99_GitHub/Shield_Arduino%20MEGA_2.jpg)
