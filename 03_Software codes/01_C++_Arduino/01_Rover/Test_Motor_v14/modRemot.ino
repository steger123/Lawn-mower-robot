//XBee or SMS

void modRemote() {
  // **************************************************************
  // ******************* SERIAL COMMANDS **************************
  Serial.println("In modRemote loop !");
  while (1)    {
    if (Serial3.available() > 0) {   // XBee is on serial3 !
      //String command = Serial3.readString();   //read the message from the serial port
      String command = Serial3. readStringUntil('\n');
      Serial.print("received: "); Serial.println(command);
      Serial3.print("R> rec.: "); Serial3.println(command); // Send message to Radio
      // forward,50,30 -- direction_speed_steps
      // backward,50,30
      // stop,0,0
      // start,10,5

      int sepPos[6];  //Position of the coma separtor in the string
      int j = 0;
      for (int i = 0; i < command.length(); i++) {   // find the comas in the string
        if (command.substring(i, i + 1) == ",") {
          sepPos[j] = i;
          j++;
        }
      }
      // Parsing:
      robDirection = command.substring(0, sepPos[0]);
      PWM_speed = command.substring(sepPos[0] + 1, sepPos[1]).toInt();
      wheelTurnCount = 0; encCountA = 0;
      wheelTurnRequired = command.substring(sepPos[1] + 1).toInt();  // **** how many wheel's turtn allowed. The routine is in "interup"

      Serial.println(robDirection);
      Serial.println(PWM_speed);
      Serial.println(wheelTurnRequired);
      Serial.println("-----*-----");
      // forward,50,30 -- direction_speed_steps
      // backward,50,30
      // stop,0,0
      // if (digitalRead(manualMode) == LOW) {
      //  Serial.println("Serial Mode");
      drive(robDirection, PWM_speed);    // ************ Main driving function  !!!!!!!
      // }
    }   // end serial if

    USDistaneLeft = USSensor(Trig_pinL, Echo_pinL);
    Serial.println("___REMOTE____");
    /*   Serial.print("_USDistaneLeft: ");  Serial.println(USDistaneLeft);
       Serial.print("_robDirection: ");  Serial.println(robDirection);
       Serial.print("_prevDirection: ");  Serial.println(prevDirection);
       Serial.print("_PWM_speed: ");  Serial.println(PWM_speed);
    */  // if (( USDistaneLeft > 20) && (suddenStop == true) && (prevDirection != "stop")) //100 cm  // restart the rover
    if (( USDistaneLeft > 20) && (suddenStop == true)) //100 cm  // restart the rover
    {
      suddenStop = false;
      Serial.print(">>> robDirection: ");  Serial.println(robDirection);
      drive(robDirection, PWM_speed); Serial.println(">>> No obsticle !");
    } // end if

  } //end while (1)
}

void drive(String Direction, int motSpeed) {
  // forward,50,30    direction, speed, step
  if (Direction == "status") {
    robStatus();   // Send the robot status to BASE
  }
  if (Direction == "forward") {
    Serial.println("Robot Forwarding");
    robDirection = "forward";  // global variable required, if the same direction selected again
    robForward(motSpeed);
  }
  else if (Direction == "backward") {
    Serial.println("Robot Backwarding");
    robDirection = "backward";  // global variable required, if the same direction selected again
    robBackward(motSpeed);
  }
  else if (Direction == "left") {
    Serial.println("Robot go Left");
    robDirection = "left";  // global variable required, if the same direction selected again
    robLeft(motSpeed);
  }
  else if (Direction == "right") {
    Serial.println("Robot go Right");
    robDirection = "right";  // global variable required, if the same direction selected again
    robRight(motSpeed);
  }
  else if (Direction == "start") {
    Serial.println("Robot Starting");
    robDirection = "start";  // global variable required, if the same direction selected again
    robStart();
  }
  else if (Direction == "hold") {
    Serial.println("Robot Holding");
    robDirection = "hold";
    robHold();
    wheelTurnCount = 0;
  }
  else {
    Serial.println("Robot Stopping");
    robDirection = "stop";
    robStop();
    wheelTurnCount = 0;
  }
}

void robStatus ()
{
  sensorReading();


  // String s1 = String(batTemp,2) + ","+ String(VBat,2)  +","+ String(VBatPC, 2); //+","+
  // ISupply+","+compassHeading+","+USDistaneLeft+","+USDistaneRight+","+USDistaneCenter;
  //  Serial3.print(s1);
  //String s2 = GPSMessage;
  char buff[14];
  float testLat = 28.459111;  // sting lenth =11
  float testLng = 77.286910;
  float testHead = 242.23;
  /*
    dtostrf(testLat, 6, 7, buff);  //4 is mininum width, 6 is precision
    Serial3.println(buff);
    dtostrf(testLng, 6, 7, buff);  //4 is mininum width, 6 is precision
    Serial3.println(buff);
  */
  Serial3.print("temp     [C] : "); Serial3.println(batTemp);
  Serial3.print("volt     [V] : "); Serial3.println(VBat / 1000);
  Serial3.print("capacity [%] : "); Serial3.println(VBatPC);
  Serial3.print("current [mA] : "); Serial3.println(ISupply);
  delay(100); //requried other case next package not arriving. Or make a protocol and decode ont he other side !
  Serial3.print("compass [deg]: "); Serial3.println(compassHeading);
  Serial3.println("LAT" + String(testLat, 6));
  Serial3.println("LNG" + String(testLng, 6));
  Serial3.println("HED" + String(testHead, 2));
  //  Serial3.println("LNG" + testLng);
  //  Serial3.println("HED" + testHead);
  delay(100);
  Serial3.print("Left   [cm] : "); Serial3.println(USDistaneLeft);
  Serial3.print("Right  [cm] : "); Serial3.println(USDistaneRight);
  Serial3.print("Center [cm] : "); Serial3.println(USDistaneCenter);

}
