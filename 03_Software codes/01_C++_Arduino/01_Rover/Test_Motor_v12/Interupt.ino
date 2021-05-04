
void encCounterA() {
  encCountA++;       //count steps of encoder shall be motPosA & motPosB
  //  Serial.print("Encoder motPosCount: "); //blocking the serial communication
  //  Serial.print("pos: "); Serial.println(motPosCount);
  Serial.println(">>> === *** interup loop: ***");
  Serial.print("encCountA: "); Serial.println(encCountA);
  Serial.print("wheelTurnCount: "); Serial.println(wheelTurnCount);
  Serial.print("wheelTurnRequired: "); Serial.println(wheelTurnRequired);
  Serial.print("mode: "); Serial.println(mode);

  //  Serial.print("wheelTurnRequired: ");
  //  Serial.println(wheelTurnRequired);
  //  if ( motPosCount >= wheelTurnRequired && digitalRead(manualMode_pin) == LOW ) {
  /* wheelTurnRequired = 100;
    if ( motPosCount >= wheelTurnRequired ) {
      robStop();
      motPosCount = 0;
      wheelTurnRequired = -1;
      prevDirection = "stop";
    }
  */

  if (encCountA >= 53) { //1240  76  //57
    wheelTurnCount++;       // egy kerekfodulat 1240 impulzus
    Serial.print("** Wheel rotation: "); Serial.println(wheelTurnCount);
    encCountA = 0;
    return;
  }
  //if (wheelTurnCount == 10)
  //  wheelTurnCount = 0;
  if ( wheelTurnCount >= wheelTurnRequired ) {
    robStop();
    wheelTurnCount = 0;
    wheelTurnRequired = -2;
    return;
    // prevDirection = "sp";
  }
 
  if ((encCountA >= turnTickCount) && (mode == "turn")) {       // for rotation, align the robot's heading with moving direction's bearing
    robStop();
    turnTickCount = 0;
    wheelTurnRequired = -3;
    mode = "---";
  }

  // stop if distance < 50 cm in all ultrasinc sensors !!
  // sensorReading();
   // if (USDistaneLeft > 50) {
    // robStop();
    //}

}  // end interupt function

void encCounterB() { // just for test
  // encCountB++;       //count steps of encoder shall be motPosA & motPosB
  //  Serial.print("Encoder motPosCount: "); //blocking the serial communication
  //  Serial.print("pos: "); Serial.println(motPosCount);
  Serial.print("encCounterB: ");// Serial.println(encCounterA);
}

void setupTimer() {
  cli();//stop interrupts

  //set timer4 interrupt at 1Hz
  TCCR5A = 0;  // set entire TCCR1A register to 0
  TCCR5B = 0;  // same for TCCR1B
  TCNT5  = 0;  //initialize counter value to 0
  // set compare match register for 1hz increments
  OCR4A = 15624 / 0.25;   // = [(16*10^6) / (1*1024) - 1]/freq. (must be <65536)  1= 1Hz


  // turn on CTC mode
  TCCR5B |= (1 << WGM12);
  // Set CS12 and CS10 bits for 1024 prescaler
  TCCR5B |= (1 << CS12) | (1 << CS10);
  // enable timer compare interrupt
  TIMSK5 |= (1 << OCIE5A);

  sei();//allow interrupts
}

ISR(TIMER5_COMPA_vect) { //timer1 interrupt 1Hz toggles pin 13 (LED)
  //generates pulse wave of frequency 1Hz/2 = 0.5kHz (takes two cycles for full wave- toggle high then toggle low)
  ISupply = currentMy();
  lcd.clear(); lcd.setCursor(0, 0);
  SerialLCDprint(3, "Curret  =", String(ISupply), "mA");

}
