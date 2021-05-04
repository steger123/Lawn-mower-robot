void setupSensors()
{

}

void sensorReading(void)   //give global values for LCD print
{
  batTemp = batTemperature(); //see in "Sensors" tab
  VBat =  batteryVoltage(); // see in "Battery" tab
  VBatPC = batteryPercentage(batTemp);     //% or battVoltage add value to global variables with function
  compassHeading = compass();
  rain =  raining();
  //ISupply = current();  // only ACS712
  //ISupply = currentADS1115(); // ACS712 + ADS1115 ADC
  ISupply = currentMy();
  /*  ICut=float(analogRead(ICut_pin))-ICutOffset;
    ICut=(ICut*5.0/1024.0)/ICutScale;
    ICut=constrain(ICut, 0, 5.0);
  */
  USDistaneLeft = USSensor(Trig_pinL, Echo_pinL);  //Ultrasonic sensor
  USDistaneRight = USSensor(Trig_pinR, Echo_pinR);
  USDistaneCenter = USSensor(Trig_pinC, Echo_pinC);
}

//Dallas DS18B20
//https://create.arduino.cc/projecthub/TheGadgetBoy/ds18b20-digital-temperature-sensor-and-arduino-9cc806
//https://github.com/milesburton/Arduino-Temperature-Control-Library

float batTemperature()
{
  sensors.requestTemperatures(); // Send the command to get temperature readings
  unsigned long temp = sensors.getTempCByIndex(0); // Why "byIndex"?  You can have more than one DS18B20 on the same bus.
  return temp;
}

/* Connect SCL to analog 5
   Connect SDA to analog 4
   Connect VDD to 3.3V DC
   Connect GROUND to common ground
*/
int compass() {
  // Adafruit_BNO055 bno = Adafruit_BNO055(55);
  imu::Vector<3> magneto = bno.getVector(Adafruit_BNO055::VECTOR_MAGNETOMETER);
  float Pi = 3.14159;
  // Calculate the angle of the vector y,x
  float mX = magneto.x();
  float mY = magneto.y();
  float heading = (atan2(mY, mX) * 180) / Pi;
  // Normalize to 0-360
  if (heading < 0) {
    heading = 360 + heading;
  }
  // Serial.print("Compass Heading: "); Serial.println(heading);
  // SerialLCDprint(0, "Compass: ", String(heading), "deg");  //row 0,10,13 used for txt1...3
  return heading;
}

//Rain sensor:
//https://robu.in/product/raindrops-detection-sensor-module-rain-weather-humidity/
//https://www.ardumotive.com/how-to-use-the-raindrops-sensor-moduleen.html
boolean raining() {
  int sensorReading = analogRead(RainingA_pin);
  // map the sensor range (four options):
  // ex: 'long int map(long int, long int, long int, long int, long int)'
  int range = map(sensorReading, 0, 1024, 0, 3);
  switch (range) {
    case 0:    // Sensor getting wet
      return true;
      break;
    case 1:    // Sensor dry
      return false;
      break;
  }
}

// *************************************************************
//https://www.engineersgarage.com/arduino/acs712-current-sensor-with-arduino/
// Size 31x13x13.5 mm
//ACS712ELCTR-05B-T can measure 5 to -5 Ampere current. Where 185mV change in Output voltage from initial state represents 1-Ampere change in Input current.
//ACS712ELCTR-20A-T can measure 20 to -20 Ampere current. Where 100mV change in Output voltage from initial state represents 1-Ampere change in Input current.
//ACS712ELCTR-30A-T can measure 30 to -30 Ampere current. Where 66mV change in Output voltage from initial state represents 1-Ampere change in Input current.

long current()
{
  unsigned int x = 0;
  float AcsValue = 0.0, Samples = 0.0, AvgAcs = 0.0, AcsValueF = 0.0;

  for (int x = 0; x < 10; x++) // 150 sampels
  { //Get 150 samples
    AcsValue = analogRead(A5);     //Read current sensor values; float(ads.readADC_Differential_2_3());
    Samples = Samples + AcsValue;  //Add samples together
    delay (3); // let ADC settle before next sample 3ms
  }
  AvgAcs = Samples / 10.0; //150
  Serial.print("__ADC Current VALUE: "); Serial.println(AvgAcs);
  // Serial.print("__Inv volt div: "); Serial.println(InverseVoltDividerRatio);



  //((AvgAcs * (5.0 / 1024.0)) is converitng the read voltage in 0-5 volts
  //2.5 is offset(I assumed that arduino is working on 5v so the viout at no current comes
  //out to be 2.5 which is out offset. If your arduino is working on different voltage than
  //you must change the offset according to the input voltage)
  //0.185v(185mV) is rise in output voltage when 1A current flows at input 0.185 V/A
  //AcsValueF = (2.5 - (AvgAcs * (5.0 / 1024.0)) )/0.185;

  //AcsValueF = (2.5 - (AvgAcs * (5.0 / 1024.0)) )/0.066; /A
  // AcsValueF = -1000 * (2.4855 - 5.0 * (AvgAcs / 1024.0)) / 0.066; //conv. A to mA; ACS712ELCTR-30A-T can measure 30 to -30 Ampere current.
  //  AcsValueF = -(2485.5 - 5000 * (AvgAcs / 1024.0)) / 0.066; //mA
  AcsValueF = -37659.1 + 73.982 * AvgAcs; //mA simplify the equasion for less data loss
  //AcsValueF = AcsValueF / 1000; //A
  //Serial.println(AcsValueF);
  return AcsValueF;
}


/*There is the issue of knowing your exact reference volt and the fact it's not fix, also,
  ASC sensors (the Bidirectional) are half Ratiometric (Vout [Zero] = VCC/2) but the current factor is fix (185/100/66.....mv/A).
  I'm using ADS1115. This ADC support 4 single or 2 differential inputs. I'm using the differential mode.
  I have two 10K resistors connected as divider to the Vcc, hence expecting to get Vcc/2 out of the divider.
  The ADS two inputs are one connected to the divider and the 2nd to ASC output (I'm adding 0.1 yF capacitor to the ground).
  The code itself become more simple
  (I don't need to include Vcc at all , neither subtract the Zero current output.
  Pay att. I'm doing some adjustment  (at Zero current).
*/
//https://wolles-elektronikkiste.de/en/ads1115-a-d-converter-with-amplifier
//ACS712ELCTR-05B-T can measure 5 to -5 Ampere current. Where 185mV change in Output voltage from initial state represents 1-Ampere change in Input current.
//ACS712ELCTR-20A-T can measure 20 to -20 Ampere current. Where 100mV change in Output voltage from initial state represents 1-Ampere change in Input current.
//ACS712ELCTR-30A-T can measure 30 to -30 Ampere current. Where 66mV change in Output voltage from initial state represents 1-Ampere change in Input current.

#define ADS1115Factor 0.125 // 1/0.125mv ADS Amplifire, dpend on ads.setGain
//ADS is setup during Setup()

#define ASC714Sensitivity 0.1   //100mv/A
#define ASC758BSensitivity 0.04 //40mv/A
#define ASC712_30A_Sensitivity 0.066   //100mv/A -> this is used

long currentADS1115() { //MA Charge Plus, Discharge Minus
  float lastSensedCurrent = 0.00;
  float numSamples = 10.00;
  //  float sensitivity = ASC758BSensitivity; // V/A
  float sensitivity = ASC712_30A_Sensitivity; // V/A
  float ADSFactor = ADS1115Factor;

  for (int i = 0; i < numSamples; i++) { // For better accurecy, multiple measurements
    lastSensedCurrent = lastSensedCurrent + float(ads.readADC_Differential_2_3()); // I'm using "Differential Conversion" between the ASC output and the 5V VCC
    delay(5);
  }
  lastSensedCurrent = lastSensedCurrent / numSamples;
  lastSensedCurrent = lastSensedCurrent * ADSFactor; // giving the Vin in mv (this is teh Vout of the ACS)
  //Next row for adjustment
  lastSensedCurrent += 4.76; // Empiric . ADC is comperring ACS vout to VCC/2 (two resistors devider), need to adjust
  lastSensedCurrent = lastSensedCurrent / sensitivity;
  return long(lastSensedCurrent);         // mAmpere long
  // return long(lastSensedCurrent/1000);  // Ampere long
}

long currentMy(){
  unsigned int x = 0;
  float AcsValue = 0.0, Samples = 0.0, AvgAcs = 0.0, AcsValueF = 0.0;
  ads.setGain(0);
  float f = ads.toVoltage(1);  // voltage factor
  int16_t val_3 = ads.readADC(3);
  float ADSvolt = val_3 * f; // convert to voltage coming from curret sensor = half of the Vcc.(5-5.2V) + 66mV change in Output voltage per 1-Ampere change
  int16_t val_2 = ads.readADC(2);
  float VCCvolt = val_2 * f; // convert to voltage = half of the Vcc. (5-5.2V) because of rsistor deivider

  //AcsValueF = -1000 * ((VCCvolt - ADSvolt) / 0.066); //mA ; 66mV change in Output voltage from initial state represents 1-Ampere change in Input current
   AcsValueF = 1000 * ((ADSvolt - 2.6032) / 0.066); //mA ; 66mV 2.6031 change in Output voltage from initial state represents 1-Ampere change in Input current
 // AcsValueF = 1000 * ((ADSvolt - VCCvolt) / 0.066); //mA ; // -2.6081

  for (int x = 0; x < 10; x++) // 150 sampels
  { //Get 150 samples
    //  AcsValue = analogRead(A5);     //Read current sensor values; float(ads.readADC_Differential_2_3());
    Samples = Samples + AcsValue;  //Add samples together
    delay (3); // let ADC settle before next sample 3ms
  }
  AvgAcs = Samples / 10.0; //150

  Serial.print("__ADS1115's half Vcc [V]: "); Serial.println(VCCvolt);
  Serial.print("__curret sensor [V]: "); Serial.println(ADSvolt);
  Serial.print("__ADS1115's Current VALUE [mA]: "); Serial.println(AcsValueF);
  return AcsValueF;

  /*

    //((AvgAcs * (5.0 / 1024.0)) is converitng the read voltage in 0-5 volts
    //2.5 is offset(I assumed that arduino is working on 5v so the viout at no current comes
    //out to be 2.5 which is out offset. If your arduino is working on different voltage than
    //you must change the offset according to the input voltage)
    //0.185v(185mV) is rise in output voltage when 1A current flows at input 0.185 V/A
    //AcsValueF = (2.5 - (AvgAcs * (5.0 / 1024.0)) )/0.185;

    //AcsValueF = (2.5 - (AvgAcs * (5.0 / 1024.0)) )/0.066; /A
    // AcsValueF = -1000 * (2.4855 - 5.0 * (AvgAcs / 1024.0)) / 0.066; //conv. A to mA; ACS712ELCTR-30A-T can measure 30 to -30 Ampere current.
    //  AcsValueF = -(2485.5 - 5000 * (AvgAcs / 1024.0)) / 0.066; //mA
    AcsValueF = -37659.1 + 73.982 * AvgAcs; //mA simplify the equasion for less data loss
    //AcsValueF = AcsValueF / 1000; //A
    //Serial.println(AcsValueF);
    return AcsValueF;
  */
}

// *************************************************************
int button(int buttonPin){
  int pushValue = analogRead(buttonPin);
  Serial.print("Push value: (>850 no btn pushed)"); Serial.println(pushValue);
  if ( pushValue < 164 )  //Button1 -> 1 = MOTOR test start; pushValue = 0
    return 3;  //change to 3
  else if ( pushValue < 502 ) //Button2 -> 2 = SENSOR test start; pushValue = 320-321
    return 2;   //2
  else if ( pushValue < 850 ) //Button3 -> 3 = OTHER test start; pushValue = 669 - 673
    return 1;  //1
  else
    return 0;  //no button pressed pushValue > 850; pull up resistor set the Btn_pin on high; pushValue   = 1023
}
