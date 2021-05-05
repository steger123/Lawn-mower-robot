# Lawn-mower-robot
![alt text](https://github.com/steger123/Lawn-mower-robot/blob/master/01_Pictures/99_GitHub/logo.png)

## The main purpose of the project:

Create a general, affordable, computational platform, which can be used for small & medium size robots.
The navication can be: fully maunal, semi and full autonomouse.
The mechanical side not restricted.
As the project progressing and evolving **You can learn more and more**, **and of course, have the designs and codes on free.**

## Software:

### BASE:
Mission Planner: C#
https://github.com/steger123/Lawn-mower-robot/tree/master/03_Software%20codes/02_C%23_Mission%20planner

### ROVER:
Main MCU : C++ (Arduino)
https://github.com/steger123/Lawn-mower-robot/tree/master/03_Software%20codes/01_C%2B%2B_Arduino/01_Rover/Test_Motor_v12

AI: Python (Jetson NANO)
https://github.com/steger123/Lawn-mower-robot/tree/master/03_Software%20codes/03_Python_AI_Jetson%20Nano/01_myOpenCV

### ROVER Electronic boards:
1. Arduino MEGA later Shieldbuddy TC375 (It has 3 cores !)
2. Custom made shield, sitting on the Arduino MEGA to have enough space for connectors (4xseial, 4xSPI, 3xI2C, buttons, 3xultrasonic sensos, volt & ampere meter etc.).
3. GPS: 2 x GNSS modules with RTK / Xbee. More here: https://github.com/steger123/Lawn-mower-robot/issues/8
5. Base's RTK: simpleRTK2Blite from Ardusimple or GPS Development Tools SparkFun GPS-RTK-SMA Breakout - ZED-F9P
6. Rovers RTK: RTK+INS simpleRTK2B-F9R V3 from Ardusimple or SparkFun Accessories SparkFun GPS-RTK Dead Reckoning Breakout - ZED-F9R
7. Motor controllers: 2x DC
8. RF radio: for additional Base to Rover and back communication (remote controll, getting sensor date from rover to mission control software).
9. Jetson Nano with normal webcam. Communicating via serial port to the MEGA (detect plant diseases, following plant line)
10. Sensors on the shield (utlrasonoic distance, battery volt, curret mesuring, rain etc.)

## Body:
As you like. In scouting for example: 1m x 1m x 0.5m

## Wheels:
As you like. In scouting for example: driven 280 mm in diamteter + castor wheels.
it can be smoot like a bicycle or thick lugs like on a tractor tire (good grippong during scouting on the field).

## Engine/motor:
As you like. In scouting for example: 2x 50W DC motors with encoders. 50 rpm

## Transmission:
As you like. In scouting for example: plannetary gerabox.

## Collaboration (software, electronic hardware, new PCB/shield, mechanical parts etc.):
If You like to join, please contact me:
andras.steger@protonmail.com

More information:
https://github.com/steger123/Lawn-mower-robot/wiki

## Latest news:
https://twitter.com/StegerAndras

![alt text](https://github.com/steger123/Lawn-mower-robot/blob/master/01_Pictures/99_GitHub/Rover_1.jpg)
![alt text](https://github.com/steger123/Lawn-mower-robot/blob/master/01_Pictures/99_GitHub/Mission%20planner_6%20C%23.jpg)

Custom robot rover shield for Arduino MEGA or ShieldBuddy TC375:
![alt text](https://github.com/steger123/Lawn-mower-robot/blob/master/01_Pictures/99_GitHub/Shield_Arduino%20MEGA_2.jpg)
