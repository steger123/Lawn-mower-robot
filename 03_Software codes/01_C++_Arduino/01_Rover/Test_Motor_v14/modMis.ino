// For Mission planning

// Get the Starting point vise XBee
// Get the next waypoint via XBee
// First Aling (turn) robot heading (compass) to required moving direction
// just GO until uless distance < 1m

int wheelTurns = 0;

void modMission() {
  float latStart, longStart, latNext, longNext;
  float robLat, robLong, WP_Lat, WP_Long;
  float dist, bear, head;


  while (1)    {
    robLat = 28.458505;  robLong = 77.287437;

    if (Serial3.available() > 0) {   // XBee is on serial3 !
      String command = Serial3.readString();
      //WP,22.73223,78.324222  (Waypoint where to GO)
      //WPS,22.73223,78.324222  (Waypoint Start coordinates)
      //WPN,22.73223,78.324222  (Waypoint Next coordinates)
      //WPE,22.73223,78.324222  (Waypoint End coordinates)


      int sepPos[3];  //Position of the coma separtor in the string
      int j = 0;
      for (int i = 0; i < command.length(); i++) {   // find the comas in the string
        if (command.substring(i, i + 1) == ",") {
          sepPos[j] = i;
          j++;
        }
      }
      // Parsing:
      String WP_type = command.substring(0, sepPos[0]);
      WP_Lat = command.substring(sepPos[0] + 1, sepPos[1]).toFloat();
      WP_Long = command.substring(sepPos[1] + 1).toFloat();

      Serial.println("Rover received: ");
      Serial.println(WP_type);
      Serial.println(WP_Lat);
      Serial.println(WP_Long);
      Serial.println("----------");
      /*    if (typeWP == "WPS") {  // this is just for test, later can be only WP
            latStart = latWP;
            longStart = longWP;
          }
      */
      if (WP_type == "WP") {      //if (typeWP == "WPN") {
        // latNext = latWP;
        //longNext = longWP;
        // latCurrent, longCurrent = Get Rover corrected current location.
        // float d = distance(latCurrent, longCurrent, latNext, longNext);
        // float b = bearing(latCurrent, longCurrent, latNext, longNext);

        dist = distance(robLat, robLong, WP_Lat, WP_Long);  //distance(28.458500, 177.287418, 28.458505, 77.287437);
        bear = bearing(robLat, robLong, WP_Lat, WP_Long);
        head = compass();     // heading
        Serial3.print("Rob Lat: "); Serial3.println(robLat);     //Send back the info via XBee to the BASE
        Serial3.print("Rob Long: "); Serial3.println(robLong);     //Send back the info via XBee to the BASE
        Serial3.print("Distance: "); Serial3.println(dist);     //Send back the info via XBee to the BASE
        Serial3.print("Bearing: "); Serial3.println(bear);          //Distance =  1.9386 m Bearing = 73.33608 deg
        Serial3.print("Heading: "); Serial3.println(head);
        //  latStart = latNext; longStart = longNext;
      }

    } // End serial

    //align(bear, head);   // turn robot's heading in direaction of the waypoint (bearing)
    // bearing fix, but heading change as robot turn !!!
    align(bear);
    
    //calulate whhe's turns (= wheelTurnRequired) based in distance and wheel circumference
    encCountA = 0;   // cointinousy counting;
    wheelTurnCount = 0; //let say one meter 3 wheel turn.
    wheelTurnRequired = dist * 3; // for the encoder. To be calculated based on distance. How many wheel's turtn allowed. The routine is in "interup"
    robForward(50);

  } //end while (1)

} // End mission

void align(float bear) // align the robot nose to the GPS direction of the destination
{
  float head = compass(); // as robot turn continouse angel required.
  encCountA = 0;   //let say one degree 2 wheel turn. (BUT much less turn approx 0.1 !)
  turnTickCount = abs((head - bear) * 2); // for the encoder. To be caslated based on head - bear. how many wheel's turtn allowed. The routine is in "interup"

  if ((head - bear) > 2)    {     // 2 degree error allowed
    //calulate wheel's turnes based on head - bear
    robLeft(50);  //Speed
  }

  if ((head - bear) < -2) {     // 2 degree error allowed
    //calulate steps based on head - bear
     robRight(50);  //Speed
  }
}

float distance(float Lat_A, float Long_A, float Lat_B, float Long_B)
{
  char valueString[10];
  char distance[10];

  //  if (Lat_B>0) || (Long_B>0) {   // initially no value !
  float distLat = abs(Lat_A - Lat_B) * 111194.9;
  float distLong = 111194.9 * abs(Long_A - Long_B) * cos(radians((Lat_A + Lat_B) / 2));
  float distance_f = sqrt(pow(distLat, 2) + pow(distLong, 2));
  // String distance = String(distance_f);  giving only 2 digit precision
  // dtostrf(distance_f, 4, 3, distance); //dtostrf(floatVar, minStringWidthIncDecimalPoint, numVarsAfterDecimal, charBuf);  // convert to string
  return distance_f;
}



// Distance: =ACOS( SIN(lat1)*SIN(lat2) + COS(lat1)*COS(lat2)*COS(lon2-lon1) ) * 6371008.8
//=ACOS( SIN(teta1)*SIN(teta2) + COS(teta1)*COS(teta2)*COS(delta2) ) * 6371008.8

/*
  5 digit accuracy:  0.00001   1.5   m 150   cm
  6 digit accuracy: 0.000001  0.15  m  15   cm
  7 digit accuracy: 0.0000001 0.015 m   1.5 cm

  distance(28.458500, 177.287418, 28.458505, 77.287437);
*/

float distance2(float lat1, float lon1, float lat2, float lon2) {
  float teta1 = radians(lat1);
  float teta2 = radians(lat2);
  float delta1 = radians(lat2 - lat1);
  float delta2 = radians(lon2 - lon1);

  float dist = acos( sin(teta1) * sin(teta2) + cos(teta1) * cos(teta2) * cos(delta2) ) * 6371008.8;
  Serial.print("Distance [m]: ");
  Serial.println(dist);

  return dist;
}

//bear = bearing(28.458500, 177.287418, 28.458505, 77.287437) Distance =  1.9386 m  Bearing = 73.33608 deg

//lat = your current gps latitude.  lon = your current gps longitude.
//lat2 = your destiny gps latitude. lon2 = your destiny gps longitude.

float bearing(float Lat_A, float Long_A, float Lat_B, float Long_B) {
  float teta1 = radians(Lat_A);
  float teta2 = radians(Lat_B);
  float delta1 = radians(Lat_B - Lat_A);
  float delta2 = radians(Long_B - Long_A);

  float y = sin(delta2) * cos(teta2);
  float x = cos(teta1) * sin(teta2) - sin(teta1) * cos(teta2) * cos(delta2);
  float brng = atan2(y, x);
  brng = degrees(brng);// radians to degrees
  brng = ( ((int)brng + 360) % 360 );
  Serial.print("Bering: "); Serial.println(brng);
  return brng;
}
