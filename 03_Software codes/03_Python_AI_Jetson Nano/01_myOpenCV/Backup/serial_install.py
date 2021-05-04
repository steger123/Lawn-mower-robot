Arduino IDE install on Ubuntu:
$ git clone https://github.com/JetsonHacksNano/installArduinoIDE
$ cd installArduinoIDE
$ ./installArduinoIDE.sh
================================
sudo -H pip3 install pyserial

******** List serial ports: ******
$ dmesg | grep tty
Result:
[    0.001747] console [tty0] enabled
[    1.059145] console [ttyS0] disabled
[    1.059228] 70006000.serial: ttyS0 at MMIO 0x70006000 (irq = 63, base_baud = 25500000) is a Tegra
[    1.059341] console [ttyS0] enabled
[    1.060546] 70006040.serial: ttyTHS1 at MMIO 0x70006040 (irq = 64, base_baud = 0) is a TEGRA_UART
[    1.061047] 70006200.serial: ttyTHS2 at MMIO 0x70006200 (irq = 65, base_baud = 0) is a TEGRA_UART
[ 3848.199914] usb 1-2.2: ch341-uart converter now attached to ttyUSB0  <-ARDUINO MEGA

not done: sudo apt-get install -y python-serial
===================
import serial
with serial.Serial('/dev/ttyACM0', 9600, timeout=10) as ser:
    while True:
        print(ser.readline())
===================================
import serial

with serial.Serial('/dev/ttyACM0', 9600, timeout=10) as ser:
    while True:
        led_on = input('Do you want the LED on? ')[0]
        if led_on in 'yY':
            ser.write(bytes('YES\n','utf-8'))
        if led_on in 'Nn':
            ser.write(bytes('NO\n','utf-8'))
=================================
void setup() {
  Serial.begin(9600);
  pinMode(LED_BUILTIN, OUTPUT);
  while (!Serial) {
    ; // wait for serial port to connect.
  }

}

void loop() {
  char buffer[16];
  if (Serial.available() > 0) {
    int size = Serial.readBytesUntil('\n', buffer, 12);
    if (buffer[0] == 'Y') {
      digitalWrite(LED_BUILTIN, HIGH);
    }
    if (buffer[0] == 'N') {
      digitalWrite(LED_BUILTIN, LOW);
    }
  }
}

===============================================
import time
import serial
ser = serial.Serial('/dev/ttyACM0', 9600) # Establish the connection on a specific port
time.sleep(1)
#counter = 32 # Below 32 everything in ASCII is gibberish
while True:
     #counter +=1
     ser.write(str("fo,050").encode('utf-8')) # Convert the decimal number to ASCII then send it to the Arduino
     time.sleep(1)
     out = ser.readline()
     print(out) # Read the newest output from the Arduino
     out = ""



# void setup() {
#   Serial.begin(9600); // set the baud rate
#   Serial.println("Ready"); // print "Ready" once
# }


# void loop() {
#   char inByte = ' ';
#   if (Serial.available()) { // only send data back if data has been sent
#     // char inByte = Serial.read(); // read the incoming data
#     String inString = Serial.readString();
#     Serial.println(String(inString[0]) + String(inString[1])); // send the data back in a new line so that it is not all one long line
#     int a1 = 100*(inString[3] - 48);  // ascii 48 =  '0'
#     int a2 = 10*(inString[4] - 48);   // ascii 48 =  '0'
#     int a3 = 1*(inString[5] - 48);   // ascii 48 =  '0'
#     int sum = a1 + a2 + a3;
#     Serial.println(String(sum));
#     delay(100); // delay for 1/10 of a second
#   }
# }