
void encCounterA() {
  encCountA++;       //count steps of encoder shall be motPosA & motPosB
  //  Serial.print("Encoder motPosCount: "); //blocking the serial communication
  //  Serial.print("pos: "); Serial.println(motPosCount);
  Serial.println(">>> === *** interup loop: ***");
  /* Serial.print("> encCountA: "); Serial.println(encCountA);
    Serial.print("> wheelTurnCount: "); Serial.println(wheelTurnCount);
    Serial.print("> wheelTurnRequired: "); Serial.println(wheelTurnRequired);
    Serial.print("> mode: "); Serial.println(mode);  // for turning different resolution [encCountA] required as for just go forward [wheelTurnCount]

    Serial.print("> ****** PREV- DIRECTION: "); Serial.println(prevDirection);
    Serial.print("> ******* ROBDIRECTION: "); Serial.println(robDirection);
  */
  if (encCountA >= 76) { //1240  76  //57
    wheelTurnCount++;       // 53 egy kerekfodulat 1240 impulzus
    Serial.print("** Wheel rotation: "); Serial.println(wheelTurnCount);
    encCountA = 0;
    return;
  }

  if ( wheelTurnCount > wheelTurnRequired - 1 ) { // wheel reached the distance wheelTurnCount start from 0 so -1 required
    robStop();
    wheelTurnCount = 0;
    wheelTurnRequired = 0;
    return;
  }

  Serial.print("encCountA: "); Serial.println(encCountA);
  Serial.print("wheelTurnCount: "); Serial.println(wheelTurnCount);

  USDistaneLeft = USSensor(Trig_pinL, Echo_pinL);
  if ( USDistaneLeft < 20 )  //100 cm
  {
    digitalWrite(PWMA_pin, 0); digitalWrite(PWMB_pin, 0);
    suddenStop = true;
    Serial.println(">>> BLOCK !!!");
    encCountA = encCountA - 5;  // comensate one stop
    return;
  }

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
