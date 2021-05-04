void setupManual() {
  error = ps2x.config_gamepad(29, 25, 27, 23, false, false);   //setup pins and settings:  GamePad(Blue clock, Gray command, Green attention, White data, Pressures?, Rumble?) check for error

  if (error == 0) {
    Serial.println("Found Controller, configured successful");  // The RED and GREEN lights on trasmitter & contoller are continously ON.
    /*   Serial.println("Try out all the buttons, X will vibrate the controller, faster as you press harder;");
        Serial.println("holding L1 or R1 will print out the analog stick values.");
        Serial.println("Go to www.billporter.info for updates and to report bugs.");
    */
  }

  else if (error == 1)
    Serial.println("No controller found, check wiring, see readme.txt to enable debug. visit www.billporter.info for troubleshooting tips");

  else if (error == 2)
    Serial.println("Controller found but not accepting commands. see readme.txt to enable debug. Visit www.billporter.info for troubleshooting tips");

  else if (error == 3)
    Serial.println("Controller refusing to enter Pressures mode, may not support it. ");

  type = ps2x.readType();

  switch (type) {
    case 0:
      Serial.println("Unknown Controller type");
      break;
    case 1:
      Serial.println("DualShock Controller Found");
      break;
    case 2:
      Serial.println("GuitarHero Controller Found");
      break;
    default:
      break;
  }
}

void modManual() {
  //***** main loop for MANUAL movement *******
  //setupTimer();

  while (1)    {   //infinite loop silimer to //modGO
    ps2x.read_gamepad(false, vibrate);          //read controller and set large motor to spin at 'vibrate' speed

    if (ps2x.Button(PSB_START))   {               //will be TRUE as long as button is pressed
      Serial.println("START is being held");
      robStart();
    }
    if (ps2x.Button(PSB_SELECT)) {          //== Stop
      Serial.println("STOP is being held");
      robStop();
    }
    if (ps2x.Button(PSB_PAD_UP)) {        //will be TRUE as long as button is pressed
      Serial.println("UP is being held");
      //  PWM_speed = ps2x.Analog(PSAB_PAD_UP);
      //  Serial.println( PWM_speed );
    }
    if (ps2x.Button(PSB_PAD_DOWN)) {
      Serial.println("DOWN is being held");
    }
    if (ps2x.Button(PSB_PAD_LEFT)) {
      Serial.println("LEFT is being held");
    }
    if (ps2x.Button(PSB_PAD_RIGHT)) {
      Serial.println("Right is being held");
    }

    if (ps2x.NewButtonState())               //will be TRUE if any button changes state (on to off, or off to on)
    {
      if (ps2x.Button(PSB_L3))
        Serial.println("L3 pressed");
      if (ps2x.Button(PSB_R3))
        Serial.println("R3 pressed");
      if (ps2x.Button(PSB_L2))
        Serial.println("L2 pressed");
      if (ps2x.Button(PSB_R2))
        Serial.println("R2 pressed");
    }

    if (ps2x.ButtonPressed(PSB_RED))            //will be TRUE if button was JUST pressed
      Serial.println("Circle just pressed");

    if (ps2x.ButtonReleased(PSB_RED))            //will be TRUE if button was JUST released
      Serial.println("Circle just released");

    if (ps2x.ButtonPressed(PSB_PINK))            //will be TRUE if button was JUST pressed
      Serial.println("Square just pressed");

    if (ps2x.ButtonReleased(PSB_PINK))            //will be TRUE if button was JUST released
      Serial.println("Square just released");

    if (ps2x.ButtonPressed(PSB_GREEN))            //will be TRUE if button was JUST pressed
      Serial.println("Triangle just pressed");

    if (ps2x.ButtonReleased(PSB_GREEN))            //will be TRUE if button was JUST released
      Serial.println("Triangle just released");

    if (ps2x.ButtonPressed(PSB_BLUE))            //will be TRUE if button was JUST pressed
      Serial.println("X just pressed");

    if (ps2x.ButtonReleased(PSB_BLUE))            //will be TRUE if button was JUST released
      Serial.println("X just released");

    if (ps2x.Button(PSB_L1) || ps2x.Button(PSB_R1)) // print stick values if either is TRUE
    {
      int horiz = ps2x.Analog(PSS_LX); //Left joystick X
      int vert  = ps2x.Analog(PSS_LY); //Left joystick Y
      //    Serial.print("Hoziz:    "); Serial.print(horiz);  Serial.print("  Vertical: "); Serial.println(vert);
      if ((horiz > 127) && (horiz < 129) && (vert > 126) && (vert < 128)) {
        robStop();
      }
      if (vert < 126)  //Joystick UP
      {
        robForward(127 - vert);
      }
      if (vert > 129)  //Joystick DOWN
      {
        robBackward(vert - 127);
      }

      if (horiz < 126)  //Joystick UP
      {
        robLeft(127 - horiz);
      }
      if (horiz > 129)  //Joystick DOWN
      {
        robRight(horiz - 127);
      }
      /*
        Serial.print("Stick Values:");
        Serial.print(ps2x.Analog(PSS_LY), DEC); // Left joystick Y: UP 127 -> 0  DOWN -> 127 ->255
        Serial.print(",");
        Serial.print(ps2x.Analog(PSS_LX), DEC); // Left joystick X: LEFT 128 -> 0  RIGHT -> 128 ->255
        Serial.print(",");
        Serial.print(ps2x.Analog(PSS_RY), DEC); // RY
        Serial.print(",");
        Serial.println(ps2x.Analog(PSS_RX), DEC); // RX
      */
      
      lcd.clear(); lcd.setCursor(0, 0);
      SerialLCDprint(0, "Bat Volt=", String(batteryVoltage() / 1000), "V");
      SerialLCDprint(1, "Curret  =", String(currentMy()), "mA");
    } //end Button(LSB_L1)


    delay(10);  //50
  } // end while (1)
}
