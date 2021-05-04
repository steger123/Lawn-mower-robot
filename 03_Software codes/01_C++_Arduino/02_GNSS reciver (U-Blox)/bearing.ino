//https://gis.stackexchange.com/questions/252672/calculate-bearing-between-two-decimal-gps-coordinates-arduino-c

//https://www.movable-type.co.uk/scripts/latlong.html

/*
lat1 = your current gps latitude.
lon1 = your current gps longitude.
lat2 = your destiny gps latitude.
lon2 = your destiny gps longitude.
*/
// =ATAN2(COS(lat1)*SIN(lat2)-SIN(lat1)*COS(lat2)*COS(lon2-lon1), SIN(lon2-lon1)*COS(lat2)) 
	   
float bearing(float lat1,float lon1,float lat2,float lon2){
    float teta1 = radians(lat1);
    float teta2 = radians(lat2);
    float delta1 = radians(lat2-lat1);
    float delta2 = radians(lon2-lon1);

    float y = sin(delta2) * cos(teta2);
    float x = cos(teta1)*sin(teta2) - sin(teta1)*cos(teta2)*cos(delta2);
    float brng = atan2(y,x);
    brng = degrees(brng);// radians to degrees *180/pi
    brng = ( ((int)brng + 360) % 360 ); 

    Serial.print("Bearing angle: ");
    Serial.println(brng);

    return brng;
}

/*
bear = bearing(28.458500, 177.287418, 28.458505, 77.287437);
Distance =	1.9386 m
Bearing =	73.33608 deg

head = compass(); // angle from magnetic north to robot nose
aling (bear, head);

void align(bear, head); // align the robot nose to the GPS direction of the destination
{
	if (head - bear)>2 		// 2 degree error allowed
	  robMotLeft(EncoderSteps);
	  
	if (head - bear)<-2		// 2 degree error allowed
	  robMotRight(EncoderSteps);
	  
}


*/
// Distance: =ACOS( SIN(lat1)*SIN(lat2) + COS(lat1)*COS(lat2)*COS(lon2-lon1) ) * 6371008.8
//=ACOS( SIN(teta1)*SIN(teta2) + COS(teta1)*COS(teta2)*COS(delta2) ) * 6371008.8

/*
5 digit accuracy:	0.00001		1.5   m	150   cm
6 digit accuracy:	0.000001	0.15  m	 15   cm
7 digit accuracy:	0.0000001	0.015 m	  1.5 cm

distance(28.458500, 177.287418, 28.458505, 77.287437);
*/

float distance(float lat1,float lon1,float lat2,float lon2){
    float teta1 = radians(lat1);
    float teta2 = radians(lat2);
    float delta1 = radians(lat2-lat1);
    float delta2 = radians(lon2-lon1);

    float dist = acos( sin(teta1)*sin(teta2) + cos(teta1)*cos(teta2)*cos(delta2) ) * 6371008.8;
    Serial.print("Distance [m]: ");
    Serial.println(dist);

    return dist;
}