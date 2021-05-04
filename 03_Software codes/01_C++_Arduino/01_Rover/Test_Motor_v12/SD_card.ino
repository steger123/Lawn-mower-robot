#include <SPI.h>
#include <SD.h>
File myFile;

void setupSDcard() {
  // Open serial communications and wait for port to open:
  Serial.begin(9600);
  while (!Serial) {
    ; // wait for serial port to connect. Needed for native USB port only
  }
  Serial.print("Initializing SD card...");
  if (!SD.begin(53)) { //chipSelect pin#53
    Serial.println("SD card initialization failed!");
   // while (1);
  }
  else {
  Serial.println("SD card initialization done.");
  }
}

void writeSDcard(String txt1, String txt2, String txt3){
//void writeSDcard() {
  // open the file. note that only one file can be open at a time,
  // so you have to close this one before opening another.
  myFile = SD.open("test.txt", FILE_WRITE);
  // if the file opened okay, write to it:
  if (myFile) {
    Serial.print("Writing to test.txt...");
   /* myFile.println("This is a test file :)");
    myFile.println("testing 1, 2, 3.");
    for (int i = 0; i < 20; i++) {
      myFile.println(i);
    } */
    DS3231_get(&t); // Get the time stamp from RTC
    myFile.println(t.year + t.mon + t.mday + t.hour + t.min + t.sec);  // See 't' in RTC
    myFile.println(txt1);  myFile.println(txt2);  myFile.println(txt3);  //batTemp , VBat/1000), ISupply
    myFile.close();
    Serial.println("SD card writing done.");
  } else {
    // if the file didn't open, print an error:
    Serial.println("Error opening test.txt");
  }
}

void readSDcard() {   // open the file for reading:
  myFile = SD.open("test.txt");
  if (myFile) {
    Serial.println("READINGS from 'test.txt':");
    // read from the file until there's nothing else in it:
    while (myFile.available()) {
      //Serial.print("batTemp: ");  //fgets() in 'SdFat' library reads a file by line.
      Serial.write(myFile.read());  //Serial.write(myFile.read());
      //Serial.print("VBat:    ");  Serial.write(myFile.read());
      //Serial.print("ISupply: ");  Serial.write(myFile.read());
    }
    // close the file:
    myFile.close();
  } else {
   Serial.println("error opening test.txt");
  }
}
