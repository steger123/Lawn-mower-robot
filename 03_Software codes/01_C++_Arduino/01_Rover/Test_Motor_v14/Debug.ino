
void debugSerial() // result for global variable coming from "Sensors: tab ; void sensorReading(void)  
{
  Serial.print("VBat= ");  //Battery voltage
  Serial.print(VBat);
  Serial.println(" V");

  Serial.print("IBat= ");   //Battery current
  Serial.print(ISupply);
  Serial.println(" A");

  Serial.print("US_L= ");    //Ultrasonic sensor
  Serial.println(USDistaneLeft);
  Serial.print("US_R= ");
  Serial.println(USDistaneRight);
  Serial.print("US_C= ");
  Serial.println(USDistaneCenter);

  Serial.print("Mover Status= ");
  Serial.println(mowerStatus);

  Serial.println();
}

void debugLCD()  // not used
{

  lcd.clear();
  lcd.setCursor(0, 0);
  switch (mowerStatus)
  {
    case 0:
      lcd.print("CHARGE ");
      break;
    case 1:
      lcd.print("RUN    ");
      break;
    case 2:
      lcd.print("STUCK  ");
      break;
    case 3:
      lcd.print("SEARCH ");
      break;
    case 4:
      lcd.print("BATLOW ");
      break;
    case 5:
      lcd.print("CHR RS "); //charge and restart when full
      break;
    case 6:
      lcd.print("RAIN "); //raining
      break;
    case 7:
      lcd.print("CURR ER "); //current too much on the battery/motor controller
      break;
  }  // end mover status

    lcd.print("%  ");
  lcd.setCursor(2, 0);  lcd.print("IC=");  lcd.print(ISupply);   //ICut
  // lcdPosition(1,9);    lcd.print("IP=");    lcd.print(IPanel);   //solar panel

}

void debugSerialLCD()
{
  sensorReading();
  //SerialLCDprint(int row, String txt1, String txt2, String txt3)  //row 0,11,17 used for txt1...3
  lcd.clear();
  SerialLCDprint(0, "Bat Temp=", String(batTemp), "C");
  SerialLCDprint(1, "Bat Volt=", String(VBat / 1000), "V");
  SerialLCDprint(2, "Bat cap =", String(VBatPC), "%");
  SerialLCDprint(3, "Curret  =", String(ISupply), "mA");
  //lcdPosition(0,0);  LCD.print("BT=");  lcdPosition(1,5); LCD.print(batTemp);
  delay(5000);
  lcd.clear();
  SerialLCDprint(0, "Compass: ", String(compassHeading), "deg");
  SerialLCDprint(1, "Dist Left = ", String(USDistaneLeft), "cm");
  SerialLCDprint(2, "Dist Right= ", String(USDistaneRight), "cm");
  SerialLCDprint(3, "Dist Cent.= ", String(USDistaneCenter), "cm");
  delay(5000);
  lcd.clear();
  if (rain == false) SerialLCDprint(0, "Not raining", "", "");
  else SerialLCDprint(0, "Raining", "", "");  //row nr

  String statusText;
  switch (mowerStatus)
  {
    case 0:
      statusText = "CHARGE ";
      break;
    case 1:
      statusText = "RUN    ";
      break;
    case 2:
      statusText = "STUCK  ";
      break;
    case 3:
      statusText = "SEARCH ";
      break;
    case 4:
      statusText = "BATLOW ";
      break;
    case 5:
      statusText = "CHR RS "; //charge and restart when full
      break;
    case 6:
      statusText = "RAIN "; //raining
      break;
    case 7:
      statusText = "CURR ER "; //current too much on the battery/motor controller
      break;
  }  // End Switch
//SerialLCDprint(1, "Dist Cent.=", String(USDistaneCenter), "cm");
  lcd.setCursor(0, 1);  lcd.print("Rob status:");lcd.setCursor(0,2);  lcd.print(statusText);  //column, row
}
