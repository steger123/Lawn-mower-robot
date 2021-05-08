
byte batteryPercentage(unsigned long temp) { 
  unsigned int batVoltage = batteryVoltage();
  byte percentage;
  if (temp >-15 && temp <= -5) // -10 Celsius (green) curve
  {
  switch(batVoltage){
    case 12000 ... 12700:
            percentage = percentage = -65*batVoltage*batVoltage/1000000 - 1639.8*batVoltage/1000 + 10342;
            break;
    case 11850 ... 11999:
            percentage = percentage = percentage = -230*batVoltage/1000 + 2784;
            break;
    case 10000 ... 11849:
            percentage = -6.6703*batVoltage*batVoltage/1000000 + 136.85*batVoltage/1000 - 627.28;
            break;
    default:
            percentage = 999;   //wrong value
            break;
  }
  percentage = 100* (75 - percentage)/75;   // as per graph @75% the battery fully depleted ! So 75% will be equvivalent to 100%
  }
  /* 
  else if (temp >-5 && temp <= 5)  //percentage = ? You can calculate as above approach 0 Celsius (red) curve
  else if (temp >5 && temp <= 15)  //percentage = ?...   +10 Celsius (gray) curve
  else if (temp >15 && temp <= 25) //percentage = ?...  +20 Celsius (orange) curve
  else if (temp >25 && temp <= 35) //percentage = ?...  +30 Celsius (lilac) curve
  else if (temp >35 && temp <= 45) //percentage = ?...  +40 Celsius (light blue) curve
  else if (temp >45 && temp <= 55) //percentage = ?...  +50 Celsius (black) curve
 
  else Serial.println("Wrong value!");       // this can happen if the battery is freshly charged
   */
    // In this case it quickly drop from 14.6V to 13.5V or less! (characteristic curve required)
  // or the cell voltage below cut-off i.e: 10 V/4 = 2.5V
  // or battery temperature below -15C or over 55 C.
  //Serial.print("Battery capacity [%]: ");  Serial.println(percentage);
  return percentage;
}

float batteryVoltage() { 
 // analogReference(EXTERNAL);
 analogReference(INTERNAL2V56);
  unsigned int adcValue = averageRead(BatVolt_Pin);
 analogReference(DEFAULT);
  
 // Serial.print("__ADC VALUE: "); Serial.println(adcValue);
 // Serial.print("__Inv volt div: "); Serial.println(InverseVoltDividerRatio);
  
 // unsigned int voltageOnPinA11 = 4 * adcValue; // mV; 4096 / 1024 =4 ; If LM4040_4.1 used as exetral ref.! -> 4.096V
  //unsigned int batVoltage = InverseVoltDividerRatio * voltageOnPinA11;  // mV; EXTERANL ref LM4040 used, 10k & 47k resistors on voltage divider.
  
  //unsigned int batVoltage = InverseVoltDividerRatio * 5055 * adcValue/1024; //mV  
  unsigned int batVoltage = InverseVoltDividerRatio * 2630 * adcValue/1024; //mV  //2531 INTERNAL2V56 used, but 2.56V has an error as well !-> mesasure with separte Arduino scetch
  // Info: BatRs[] = {44730, 10500};    // R2 & R3 in ohm, must be measured accurately !!! 13.5V (bat full chage volage)*10020/(10020+44730) must <2.56V (internak ref volage)
  // Info: InverseVoltDividerRatio = (BatRs[0] + BatRs[1]) / BatRs[1];
  Serial.print("BAT VOLTAGE: "); Serial.println(batVoltage);

  //analogReference(DEFAULT); // default (+5V) required for temperature sensor
 return batVoltage;
}

int averageRead(int pin)
{
 // delay(10);
  unsigned long total = 0;
  for( int i=0; i < 40; i++) //50
  {
   total = analogRead(pin);  //burn some data
   delay (3); // let settle before next sample 3ms
  }
 total = 0;
  for( int i=0; i < NrSamples; i++)
  {
    total += analogRead(pin);
    delay (3); // let settle before next sample 3ms
  }
  total += NrSamples / 2;   // add half a bit
  return(total / NrSamples);
}
