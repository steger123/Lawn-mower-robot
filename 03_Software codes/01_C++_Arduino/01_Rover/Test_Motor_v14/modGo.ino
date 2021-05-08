
//fully AUTONOMUSE GO MODE:

void modGo(){
  main:
  Serial.print("**** Mower's status: "); Serial.println(mowerStatus);
   //if (Serial.available() > 0) {  // Jetson Nanon is on serial0 !
    
   
    //wait for PEN press: Press PEN during power-on for ESC test
    while (button(Button_pin) != 3)  {   // if pushValue < 850 ) return 3; Press PEN during power-on for ESC test PEN=3;
      currentMillis = millis();
      if (currentMillis - previousMillis > timeClock)      {
        //sensorReading();  debugLCD();
        Serial.println("-------===== DEBUG =====-------");
        debugSerialLCD();
      }
    }
    SerialLCDprint(2, "GO !!!", String(USDistaneCenter), "cm");
    mowerStatus = 1;
    robStart();
    PWM_speed = 100;
    robForward(1);    //JETSON NANO
    accelerate(1 , 100);  // No 'accelerate' subrutinbe in robForward()
  

    //***** main loop for autonomous movement *******
    while (1)    {
      // (1) stop everything:
      if (button(Button_pin) == BtnMotor || button(Button_pin) == BtnSensor)  {   // button 1 or 2 pressed
        robStop();
        mowerStatus = 0;  // 0=oncharge (press pen for run)
        goto main;
      }
      
      // (2) if no interrupt occurs ont he wheels within 10 seconds, the robot is blocked
      if (millis() > wheelTime + 10000)  {
        robStop();
        mowerStatus = 2;  // 2=stuck
        goto main;
      }

      // (3) obstacle sensor management with mechanicla switch
      //if (digitalRead(SWOL_pin)==LOW) obstacleAvoidSX();   // mechanical switch for left obstacle
      //if (digitalRead(SWOR_pin)==LOW) obstacleAvoidDX();   // mechanical switch for right obstacle

      currentMillis = millis();
      
      // (4) reads the sensors every timeClock:
      if (currentMillis - previousMillis > timeClock)    {   // **** big if statement for sensors !!!!
        previousMillis = currentMillis;
        sensorReading();  /// *** READ the sensors !!!!!!!!!

        if ((USDistaneLeft > 5) && (USDistaneLeft < USdistance))  { //#define USdistance 100 (cm)
          obstacleAvoidLeft();
          resetEncoder();
        }
        if ((USDistaneRight > 5) && (USDistaneRight < USdistance)) { //#define USdistance 100 (cm)
          obstacleAvoidRight();
          resetEncoder();
        }
        if ((USDistaneCenter > 5) && (USDistaneCenter < USdistance))  { //#define USdistance 100 (cm)
          obstacleAvoidCenter();
          resetEncoder();
        }

        //**********************************************************************
        //if (IPanel>IPanelMax) IPanelMax=IPanel; //just memory/record for futher use the to max current on solar panel

        //battery lower then 10% then searching the point to charge
        if (VBatPC < 10) { // or Voltage or use the "tp = batTemperature();" and " if (batteryPercentage(tp) < 10) : functions !
          mowerStatus = 3;    // 3=search place for charging  check in SolarMover the ******* .... section !!!
        }
        //continues to advance until it finds a light source at least 80% of the previous maximum.
        /*
          if (mowerStatus==3) // 3=search
          {
          if  (IPanel>IPanelMax*0.8)  // Actual Solar panel current, good place founf for charging ? :-), no shade ???
          {
          setMowerSpeed(0);
          mowerStatus=5;    // 5=charge and restart when full
          }
          }

          if (mowerStatus==5)   // 5 = charge and restart when full
          {
          resetEncoder();
          // full charge then restart
          if (VBatPC>=100)  ////or use "tp = batTemperature();" and " if (batteryPercentage(tp) > 80) : functions !
          {
            mowerStatus=1;  // 1=run
            IPanelMax=0;
            cutON();
            setMowerSpeed(255);
          }
           }
        */
        //**********************************************************************

        if (VBat > VBat_Level_Max)  { //avoid over charing - this can't happe if separate chager installed.
          //lead battery never stop charge
          //disable charge only for lipo battery
          //digitalWrite(Panel_pin, LOW);  //Solar panel switch
        }

        //battery is completely discharged
        if (VBat < VBat_Level_Min)  {
          robStop();
//          digitalWrite(Panel_pin, HIGH);   // Swicn on the solar panels
          mowerStatus = 4;    // 4=batlow
          goto main;
        }
        if (raining == true)  {
          robStop();
          mowerStatus = 6;    // 6=raining
          goto main;
        }
        if (ISupply > IBat_Max) {      // too much current (32A) consumed, overload
          robStop();
          mowerStatus = 7;    // 6= motor overload
          goto main;
        }

      //debugSerial();  debugLCD();
      debugSerialLCD();
      
      } // end **** big if statement for sensors !!!!
    } // end while(1)

}
