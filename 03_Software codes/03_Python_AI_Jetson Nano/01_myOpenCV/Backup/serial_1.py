import serial

with serial.Serial('/dev/ttyUSB1', 9600, timeout=10) as ser:
    while True:
        led_on = input('Do you want the LED on? ')[0]
        if led_on in 'yY':
            ser.write(bytes('YES\n','utf-8'))
        if led_on in 'Nn':
            ser.write(bytes('NO\n','utf-8'))
