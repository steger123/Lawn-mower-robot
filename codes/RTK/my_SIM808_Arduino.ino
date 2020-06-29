# https://www.youtube.com/watch?v=qQnjlwf-8yQ&t=678s


#include <SoftwareSerial.h>
SoftwareSerial sim808(7,8);

char phone_no[] = "xxxxxxx"; // replace with your phone no.
String data[5];
#define DEBUG true
String state,timegps,latitude,longitude;

void setup() {
 sim808.begin(9600);
 Serial.begin(9600);
 delay(50);

 sim808.print("AT+CSMP=17,167,0,0");  // set this parameter if empty SMS received
 delay(100);
 sim808.print("AT+CMGF=1\r"); // to send SMS, set to text mode
 delay(400);

 sendData("AT+CGNSPWR=1",1000,DEBUG);  //GNSS Turned on.  // =0 off
 // AT+CGPSPWR=1 //make the GPS on
 // AT+CGPSRST=0 // GOS cold start mode
 // AT+CGPSSTATUS? //GPS got fixed or not ?
 // if not: AT+CGPSINF=0  / returns single NMEA sentence
 // AT+CGPSINF=32 // returns GPRMC sentence
 // AT+CGPSSTATUS?  // status must be fixed
 // AT+CGPSOUT=32 // output continpuse RMC sentence
 // stop: AT+CGPSOUT=0
 
 
 delay(50);
 sendData("AT+CGNSSEQ=RMC",1000,DEBUG);  //set the nevigation sentence to GPRMS AT+CGNSSEQ="RMC"
 delay(150);
 
}

void loop() {
  sendTabData("AT+CGNSINF",1000,DEBUG);  // return single RMC sentence
  if (state !=0) {
    Serial.println("State  :"+state);  // start ot build up the SMS content
    Serial.println("Time  :"+timegps);
    Serial.println("Latitude  :"+latitude);
    Serial.println("Longitude  :"+longitude);

    sim808.print("AT+CMGS=\"");
    sim808.print(phone_no);  // Sending the SMS to the phone#
    sim808.println("\"");
    
    delay(300);

    sim808.print("http://maps.google.com/maps?q=loc:");
    sim808.print(latitude);
    sim808.print(",");
    sim808.print (longitude);
    delay(200);
    sim808.println((char)26); // End AT command with a ^Z, ASCII code 26
    delay(200);
    sim808.println();
    delay(20000);
    sim808.flush();
    
  } else {
    Serial.println("GPS Initialising...");
  }
}

void sendTabData(String command , const int timeout , boolean debug){

  sim808.println(command);
  long int time = millis();
  int i = 0;

  while((time+timeout) > millis()){
    while(sim808.available()){
      char c = sim808.read();
      if (c != ',') {
         data[i] +=c;
         delay(100);
      } else {
        i++;  
      }
      if (i == 5) {
        delay(100);
        goto exitL;
      }
    }
  }exitL:
  if (debug) {
    state = data[1];
    timegps = data[2];
    latitude = data[3];
    longitude =data[4];  
  }
}
String sendData (String command , const int timeout ,boolean debug){
  String response = "";
  sim808.println(command);
  long int time = millis();
  int i = 0;

  while ( (time+timeout ) > millis()){
    while (sim808.available()){
      char c = sim808.read();
      response +=c;
    }
  }
  if (debug) {
     Serial.print(response);
     }
     return response;
}




