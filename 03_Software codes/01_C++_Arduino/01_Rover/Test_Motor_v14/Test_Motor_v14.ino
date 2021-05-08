//https://www.open-electronics.org/a-robotic-lawn-mowers-powered-by-solar-energy-with-an-arduino-heart/
#include "setup.h"
#include <Wire.h>

#include <Adafruit_Sensor.h>  //Manuall
#include <Adafruit_BNO055.h> //Adafruit BNO055 by Adafruit v1.3.0
Adafruit_BNO055 bno = Adafruit_BNO055(55, 0x29);
#include <utility/imumaths.h>

#include <LiquidCrystal_I2C.h>
LiquidCrystal_I2C lcd(0x27, 20, 4); //*

#include <OneWire.h>      // OneWire DS18S20, DS18B20, DS1822 http://www.pjrc.com/teensy/td_libs_OneWire.html
OneWire oneWire(Temp_pin);
#include <DallasTemperature.h>
DallasTemperature sensors(&oneWire);  // Pass our oneWire reference to Dallas Temperature.

#include <ADS1X15.h>  //by Rob.Tillaart
ADS1115 ads(0x48); // choose you sensor ADS1013 ADS(0x48) ...

#include <PS2X_lib.h>  //for v1.6
PS2X ps2x; // create PS2 Controller Class

void setup() {
  Serial.begin(9600); Serial3.begin(9600); 
  while (!Serial) {
    ; // wait for serial port to connect. Needed for native USB port only
  }
  Wire.begin();
  SerialLCDprint(0, "Solar Rover V1.0", "", "");
  SerialLCDprint(1, "Setup motor, sensors", "", "");
  setupPins();  Serial.println("setup Pins");
  setupLCD();   Serial.println("setup LCD");   lcd.clear();
  setupSensors(); Serial.println("setup Sensors");
  setupMotor(); Serial.println("setup Motor");
  sensors.begin();  // Start up the DallasTemperature library

  ads.begin(); // initiallize the ADS1115 ADC ofr current measurement

  setupSDcard(); Serial.println("setup SD card");
  setupRTC(); // this to be run only one time to set the date & time in RTC. After that the battery will ensure the update
  //or run separately: 'RTC_setDateTime.ino'
  setupManual(); //Setting PS2 in "modMan"
  //  writeSDcard();

  // TEST MOTORS:
  // BtnMotor  = 1; BtnSensor = 2; BtnOther  = 3;
  if (button(Button_pin) == BtnMotor)  {  // BtnMotor  = 1 -> Button3 pressed; ; volt= GND = 0V; pushValue=0 *** if it is not pushed, then actcited !!! -> WRONG !
    while (1)    {
      SerialLCDprint(0, "***MOTOR test start", "", "");  // BtnMotor  = 1 -> Button1 pressed
      delay(1000);
      testMotor();
    }
  } //end button
  //TEST SENSORS:
  if (button(Button_pin) == BtnSensor)  {   // BtnSensor  = 2 -> Button2 pressed
    SerialLCDprint(0, "***SENSOR test start", "", "");
    //delay(1000);
    while (1)    {
      debugSerialLCD();
      writeSDcard(String(batTemp), String(VBat / 1000), String(ISupply));
      readSDcard();
    }
  }

  if ( button(Button_pin) == BtnOther)  {  // Other  = 3 -> Button3 pressed;
    //delay(1000);
    while (1)    {
      Serial.println("***OTHER test start");
      delay(1000);
    }
  }
  //end button
} // end setup


void loop() {
  //  digitalWrite(Panel_pin, HIGH);  //#define solear Panel_pin      7  // Solar Panel on/off
  //Serial.println("Entered in the MAIN LOOP");
  //PS2/manual MODE:
  if (digitalRead(manualMode_pin) == HIGH) {  //PS2
    //Serial.println("Entered in MANUAL mode");
    SerialLCDprint(0, "In MANUAL mode", "", "");
    modManual();
  }

  //fully AUTONOMUSE MODE:
  if (digitalRead(manualMode_pin) == LOW) {  //JETSON NANO
     //Serial.println("Entered in AUTONOM mode");
     SerialLCDprint(0, "Ent. in AUTO mode", "", "");
   //  modGo();
  }

  // if ( ....) // Remorte mode SMS or XBee (or both)
  if (digitalRead(remoteMode_pin) == LOW) {  // XBEE
    //Serial.println("Entered in REMOTE mode");
    SerialLCDprint(0, "In REMOTE mode", "", "");
    //interrupts ();  // Enable interrupts
    // Hall/Encoder sensor detection - Count steps:
    attachInterrupt(digitalPinToInterrupt(Encoder_pin_A), encCounterA, CHANGE); // functional issue !!!
    //attachInterrupt(digitalPinToInterrupt(Encoder_pin_B), encCounterB, CHANGE);

    modRemote();
  }

  if (digitalRead(remoteMode_pin) == LOW) {  // XBEE
    Serial.println("In MISSION mode");
  //  interrupts ();  // Enable interrupts
    // Hall/Encoder sensor detection - Count steps:
  //  attachInterrupt(digitalPinToInterrupt(Encoder_pin_A), encCounterA, CHANGE); // functional issue !!!
    //attachInterrupt(digitalPinToInterrupt(Encoder_pin_B), encCounterB, CHANGE);

  //  modMission();
  }

  
  //debugSerial();
  //debugLCD();

}  // end loop
