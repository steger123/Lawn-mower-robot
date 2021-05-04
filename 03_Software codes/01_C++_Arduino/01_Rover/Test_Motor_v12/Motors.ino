void setupMotor()
{
  pinMode(PWMA_pin,       OUTPUT);
  pinMode(motDIR_pin_A1,  OUTPUT);
  pinMode(motDIR_pin_A2,  OUTPUT);
  pinMode(PWMB_pin,       OUTPUT);
  pinMode(motDIR_pin_B1,  OUTPUT);
  pinMode(motDIR_pin_B2,  OUTPUT);
  pinMode(Encoder_pin_A,  INPUT_PULLUP);
  pinMode(Encoder_pin_B,  INPUT_PULLUP);
}

void testMotor()
{
  robStart();
  robForward(50);
  delay(1000);
  robBackward(50);
  delay(1000);
  robStop();
}

void robStart() {
  Serial.println("START start");
  robDirection = "st";
  digitalWrite(motDIR_pin_A1, HIGH);  // set direction for forward
  digitalWrite(motDIR_pin_A2, LOW);   // set direction for forward
  digitalWrite(motDIR_pin_B1, HIGH);  // set direction for forward
  digitalWrite(motDIR_pin_B2, LOW);   // set direction for forward
  // startMotor  = true;          //START motor
  PWM_speed   = 0;
  // oldSpeed    = 0;
  //  gear        = 2;  // only for manual (PS2 controll) mode
  digitalWrite(PWMA_pin, PWM_speed);
  digitalWrite(PWMB_pin, PWM_speed);
}

void robForward(int newSpeed) {
  //  if (startMotor == true) {   // new // other vise the motor in 'stop'/break condiction and shoudn't start till 'robStart'
  Serial.println("FORWARD start");
  constrain(newSpeed, 0, 255);
  robDirection = "fo";
  //  if (robDirection != prevDirection) { // the previouse was the same, then no stopping required. Only if direction changed !
  //    Serial.println("New FORWARD");
  //    accelerate(oldSpeed , 0); // from actual to zero. Change direction
  digitalWrite(motDIR_pin_A1, HIGH);
  digitalWrite(motDIR_pin_A2, LOW);
  digitalWrite(motDIR_pin_B1, HIGH);
  digitalWrite(motDIR_pin_B2, LOW);
  analogWrite(PWMA_pin, newSpeed);
  analogWrite(PWMB_pin, newSpeed);
  //  }
  //  Serial.println("old FORWARD speed");
  //  accelerate(oldSpeed , newSpeed); // from ancual to new speed
  //  PWM_speed     = newSpeed;       // just save the actual speed for futher use
  //  prevDirection = robDirection;   // just save the actual direction for futher use
  //  }  // end startMotor == true
  //  else Serial.println("'robForward' command given, but no 'robStart' command before that !");  // new
}

void robBackward(int newSpeed) {
  //  if (startMotor == true) {   // new // other vise the motor in 'stop'/break condiction and shoudn't start till 'robStart'
  Serial.println("BACKWARD start");
  constrain(newSpeed, 0, 255);
  robDirection = "ba";
  //    if (robDirection != prevDirection) { // the previouse was the same, then no stopping required. Only if direction chanheg !
  //      Serial.println("set BACKWARD dir");
  //      accelerate(newSpeed , 0); // from actual to zero. Change direation
  digitalWrite(motDIR_pin_A1, LOW);
  digitalWrite(motDIR_pin_A2, HIGH);
  digitalWrite(motDIR_pin_B1, LOW);
  digitalWrite(motDIR_pin_B2, HIGH);
  analogWrite(PWMA_pin, newSpeed);
  analogWrite(PWMB_pin, newSpeed);
  //    }
  //    Serial.println("set BACKWARD speed");
  //    accelerate(oldSpeed , newSpeed); // from actual to new speed
  //    PWM_speed     = newSpeed;       // just save the actual speed for futher use
  //    prevDirection = robDirection;
  //  }  // end startMotor == true
  //  else Serial.println("'robBackward' command given, but no 'robStart' command before that !");  // new
}

void robLeft(int newSpeed) {
  //  if (startMotor == true) {   // new // other vise the motor in 'stop'/break condiction and shoudn't start till 'robStart'
  Serial.println("LEFT start");
  constrain(newSpeed, 0, 255);
  robDirection = "le";
  //    if (robDirection != prevDirection) { // the previouse was the same, then no stopping required. Only if direction chanheg !
  //      accelerate(newSpeed , 0); // from ancual to zero. Chage direation
  digitalWrite(motDIR_pin_A1, HIGH);
  digitalWrite(motDIR_pin_A2, LOW);
  digitalWrite(motDIR_pin_B1, LOW);
  digitalWrite(motDIR_pin_B2, HIGH);
  analogWrite(PWMA_pin, newSpeed);
  analogWrite(PWMB_pin, newSpeed);
  //    }
  //    accelerate(oldSpeed , newSpeed); // from actual to new speed
  //    PWM_speed     = newSpeed;       // just save the actual speed for futher use
  //    prevDirection = robDirection;
  //  }  // end startMotor == true
  //  else Serial.println("'robBackward' command given, but no 'robStart' command before that !");  // new
}

void robRight(int newSpeed) {
  //  if (startMotor == true) {   // new // other vise the motor in 'stop'/break condiction and shoudn't start till 'robStart'
  Serial.println("RIGHT start");
  constrain(newSpeed, 0, 255);
  robDirection = "ri";
  //    if (r/obDirection != prevDirection) { // the previouse was the same, then no stopping required. Only if direction chanheg !
  //      accelerate(PWM_speed , 0); // from ancual to zero. Chage direation
  digitalWrite(motDIR_pin_A1, LOW);
  digitalWrite(motDIR_pin_A2, HIGH);
  digitalWrite(motDIR_pin_B1, HIGH);
  digitalWrite(motDIR_pin_B2, LOW);
  analogWrite(PWMA_pin, newSpeed);
  analogWrite(PWMB_pin, newSpeed);
  //    }
  //    accelerate(oldSpeed , newSpeed); // from actual to new speed
  //    PWM_speed     = newSpeed;        // just save the actual speed for futher use
  //    prevDirection = robDirection;
  //  }  // end startMotor == true
  //  else Serial.println("'robRight' command given, but no 'robStart' command before that !");  // new
}

void robHold() {                // this is just reduce the speed to 0, but no BREAK on. Like ball coming out of pictrue but in back, so can start immediatly 'automatically'.
  Serial.println("HOLD start");
  robDirection = "ho";
  accelerate(oldSpeed, 0);  // PWM_speed, from actual speed to 0
  PWM_speed = 0;            // just save the actual speed for futher use //this can be deleted, becuse accelerate will do
  prevDirection = robDirection;
}

void robStop() {
  Serial.println("STOP start");
  robDirection = "sp";
  //  accelerate(oldSpeed , 0); // PWM_speed, from anctual to 0 pwm.
  digitalWrite(motDIR_pin_A1, LOW);  // BREAK to GND ; HIGH = Break to VCC
  digitalWrite(motDIR_pin_A2, LOW);  // BREAK to GND ; HIGH = Break to VCC
  digitalWrite(motDIR_pin_B1, LOW);  // BREAK to GND ; HIGH = Break to VCC
  digitalWrite(motDIR_pin_B2, LOW);  // BREAK to GND ; HIGH = Break to VCC
  analogWrite(PWMA_pin, 0);
  analogWrite(PWMB_pin, 0);

  // prevDirection = robDirection;
  //  PWM_speed = 0;              // just save the actual speed for futher use
  //  startMotor  = false;          //Stop other directions until unless not started with 'robStart' again
  Serial.println("Stopped ----");
}

// ------------- Axulury functions:

void accelerate(int fromSpeed, int toSpeed) {
  // Serial.println("==== ACCELERATE ====");
  if (toSpeed > fromSpeed)                     //gyoslul
  {
    Serial.println("Gyorsul");
    for (i = fromSpeed; i <= toSpeed; i++)
    {
      analogWrite(PWMA_pin, i);
      analogWrite(PWMB_pin, i);
      delay(accelerateTime);
    }
  }
  else  //toSpeed < fromSpeed                 // lassul
  {
    Serial.println("Lassul");
    for (i = fromSpeed; i >= toSpeed; i--)
    {
      analogWrite(PWMA_pin, i);
      analogWrite(PWMB_pin, i);
      delay(accelerateTime);
    }
  }
  oldSpeed = toSpeed;   // record the acual PWM value
}

void resetEncoder()  //reset value for block wheels
{
  wheelTime = millis();
}

void obstacleAvoidLeft()
{
  accelerate(PWM_speed , 0); // Chaging only PWM, from actual to 0 pwm.
    Serial.println("1. Obst avoid: Forward lassul");
  robBackward(1);           // Just chage the direction
  accelerate(1 , 100);
    Serial.println("2. Obst avoid: Backwatd gyorsul");
  delay(timeReverse);
  accelerate(100 , 0);
  Serial.println("3. Obst avoid: Backwatd lassul");
  
  robRight(1);             // Just chage the direction
  accelerate(1 , 100);
  Serial.println("4. Obst avoid: Right lassul");
  delay(timeReverse);
  accelerate(100 , 1);
  Serial.println("5. Obst avoid: Right gyorsul");
 
  robForward(1);          // Just chage the direction
  accelerate(1 , PWM_speed);
  Serial.println("6. Obst avoid: Forward gyorsul");
}

void obstacleAvoidRight()
{
  robBackward(50);
  delay(timeReverse);
  robLeft(50); //gira a sinistra
  delay(timeRotate);
  robForward(50);
}

void obstacleAvoidCenter()
{
  robBackward(50);
  delay(timeReverse);
  robRight(50); // or Left ?
  delay(timeRotate);
  robForward(50);
}
