#!/usr/bin/env python

# Import required modules
import time
import RPi.GPIO as GPIO

# Declare the GPIO settings
GPIO.setmode(GPIO.BOARD)

# set up GPIO pins
GPIO.setup(7, GPIO.OUT) # Connected to PWMA
GPIO.setup(11, GPIO.OUT) # Connected to AIN2
GPIO.setup(12, GPIO.OUT) # Connected to AIN1
GPIO.setup(13, GPIO.OUT) # Connected to STBY

# Drive the motor clockwise
GPIO.output(12, GPIO.HIGH) # Set AIN1
GPIO.output(11, GPIO.LOW) # Set AIN2

# Set the motor speed
GPIO.output(7, GPIO.HIGH) # Set PWMA

# Disable STBY (standby)
GPIO.output(13, GPIO.HIGH)

# Wait 5 seconds
time.sleep(5)

# Drive the motor counterclockwise
GPIO.output(12, GPIO.LOW) # Set AIN1
GPIO.output(11, GPIO.HIGH) # Set AIN2

# Set the motor speed
GPIO.output(7, GPIO.HIGH) # Set PWMA

# Disable STBY (standby)
GPIO.output(13, GPIO.HIGH)

# Wait 5 seconds
time.sleep(5)

# Reset all the GPIO pins by setting them to LOW
GPIO.output(12, GPIO.LOW) # Set AIN1
GPIO.output(11, GPIO.LOW) # Set AIN2
GPIO.output(7, GPIO.LOW) # Set PWMA
GPIO.output(13, GPIO.LOW) # Set STBY
=========================================

import RPi.GPIO as GPIO
import time
GPIO.setmode(GPIO.BCM)
GPIO.setup(16,GPIO.OUT)	# Connected to PWMA ?
GPIO.setup(20,GPIO.OUT) # Connected to AIN1 ?
GPIO.setup(21,GPIO.OUT) # Connected to AIN2 ?
			# Connected to STBY ?
p=GPIO.PWM(16,100)

while True:
    p.start(20) #Motor will run at slow speed
    GPIO.output(21,True)
    GPIO.output(20,False)
    GPIO.output(16,True)
    time.sleep(3)
    p.ChangeDutyCycle(100) #Motor will run at High speed
    GPIO.output(21,True)
    GPIO.output(20,False)
    GPIO.output(16,True)
    time.sleep(3)
    GPIO.output(16,False)
    p.stop()
