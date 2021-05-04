void setupLCD()
{
  //LiquidCrystal_I2C lcd(0x27, 20, 4);
  // initialize the LCD
  lcd.begin();
  // Turn on the blacklight and print a message.
  lcd.backlight();
  lcd.print("Solar Rover V1.0");
}

void SerialLCDprint(int row, String txt1, String txt2, String txt3)  //0,10,17 used for txt1...3
{
  //LiquidCrystal_I2C lcd(0x27, 20, 4);
  Serial.print(txt1 + " "); Serial.print(txt2+ " "); Serial.println(txt3);
  lcd.setBacklight(HIGH); //Set Back light turn On
  lcd.setCursor(0, row);  lcd.print(txt1);
  lcd.setCursor(11, row);  lcd.print(txt2);
  lcd.setCursor(17, row);  lcd.print(txt3);
}
