# Lawn-mower-robot
More information:

https://github.com/steger123/Lawn-mower-robot/wiki
![alt text](https://github.com/steger123/Lawn-mower-robot/blob/master/pics/openLogo.png)

## As of now (may 2021) the project is in progress.

The aim is to design a lawnmower robot based on design proposals.
Concepts You can fing the the directiries.
So we can decide together the main parameters. As the project progressing and evolving **You can learn more and more**,
**and of course, have the designs and codes on free.**
As a starter, it will be "just" a lawnmower. Parallel robot rover designed for scouting purpose on the filed. So the AI capabilites can be tried out more beneficcialy.

## Name of the robot:
# Golem
(It can be changed ! Please propose better :-)

## Robot logo:
![alt text](https://github.com/steger123/Lawn-mower-robot/blob/master/pics/Golem_n.png)

(It can be changed ! Please propose better :-)

## Body:
Max size 600x400 mm. Weight: max. 20 kg.
1. What the shape, layout looks like?

![alt text](https://github.com/steger123/Lawn-mower-robot/blob/master/pics/concept_body.jpg)

## Driven Wheels:
Size: 8-13 inch. Weight: max. 1000 g/pc
1.	How many wheels required ?

## Not driven wheels:
Size: 8-13 inch. Weight: max. 500 g/pc
1.	How many wheels required ?
2.	Caster wheel or fixed ?

## Engine/motor:
Weight: max. 1000 g/pc
1. Used DC or AC motors ?
2. Use HUB motor or brushed DC or brush less ?
![alt text](https://github.com/steger123/Lawn-mower-robot/blob/master/pics/concept_motor.jpg)

## Transmission:
Size: max. 200x100 mm. Weight: max. 1 kg/pc
How to solve the PRM reduction ?
A.  Gear box with timing sprockets and belt ?
B.  Worm gearbox ?
C.  Spur gearbox ?
D.  Bevel gearbox ?
E.  Cycloid gearbox ?

## Electronic:
1.  Arduino MEGA, later Shieldbuddy TC375 (3 cores !)
2.  Custom shield for proper connections (4xseial, 4xSPI, 3xI2C, buttons, 3xultrasonic sensos, volt & ampere meter etc.) 
3.  Jetson Nano with normal webcam
4.  2x DC motor controllers
5.  2xGNSS modules with RTK (BASE+Xbee / Rover+XBee) 

## Communication:
1. Between the robot and PC: radio
2. Accuatre (10cm) positioning requried: RTK, GNSS/GPS/NTRIP

## Softwares:
1. On the robot [Called: Golem]: Python with TensorFlow Light, PyTorch
The software have to ensure object recognition (plant disease), obstacle avoidance and route following (AI, visual servoing / GNSS+RTK).
3. Mission palanning and controlling PC [Called: Mission Planner]: C#

## Colaboration:
If You like to join, please contact me:
andras.steger@protonmail.com

## Latest news:
https://twitter.com/StegerAndras
