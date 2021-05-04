//ZED-F9R (with IMU)

#include <SoftwareSerial.h>

// Connect the GPS RX/TX to arduino pins 3 and 5
SoftwareSerial softSerial = SoftwareSerial(3,5);

const unsigned char UBX_HEADER[]     = { 0xB5, 0x62 };
const unsigned char NAV_PVT_HEADER[] = { 0x01, 0x07 };	//NMEA-Standard-GGA
const unsigned char ESF_ALG_HEADER[] = { 0x10, 0x14 };  //UBX-ESF-ALG or UBX-ESF-STATUS  | GGA NMEA-Standard-THS or NMEA-Standard-VTG

enum _ubxMsgType {
  MT_NONE,
  MT_NAV_PVT,
  MT_ESF_ALG
};

struct NAV_PVT {
  unsigned char cls;			// ID1
  unsigned char id;				// ID2
  unsigned short len;			// message length
  unsigned long iTOW;          // GPS time of week of the navigation epoch (ms)
  
  unsigned short year;         // Year (UTC) 
  unsigned char month;         // Month, range 1..12 (UTC)
  unsigned char day;           // Day of month, range 1..31 (UTC)
  unsigned char hour;          // Hour of day, range 0..23 (UTC)
  unsigned char minute;        // Minute of hour, range 0..59 (UTC)
  unsigned char second;        // Seconds of minute, range 0..60 (UTC)
  char valid;                  // Validity Flags (see graphic below)
  unsigned long tAcc;          // Time accuracy estimate (UTC) (ns)
  long nano;                   // Fraction of second, range -1e9 .. 1e9 (UTC) (ns)
  unsigned char fixType;       // GNSSfix Type, range 0..5: 0 = no fix 1 = dead reckoning only 2 = 2D-fix 3 = 3D-fix 4 = GNSS + dead reckoning combined 5 = time only fix
  char flags;                  // Fix Status Flags
  unsigned char reserved1;     // reserved
  unsigned char numSV;         // Number of satellites used in Nav Solution
  
  long lon;                    // Longitude (deg)
  long lat;                    // Latitude (deg)
  long height;                 // Height above Ellipsoid (mm)
  long hMSL;                   // Height above mean sea level (mm)
  unsigned long hAcc;          // Horizontal Accuracy Estimate (mm)
  unsigned long vAcc;          // Vertical Accuracy Estimate (mm)
  
  long velN;                   // NED north velocity (mm/s)
  long velE;                   // NED east velocity (mm/s)
  long velD;                   // NED down velocity (mm/s)
  long gSpeed;                 // Ground Speed (2-D) (mm/s)
  long heading;                // Heading of motion 2-D (deg)
  unsigned long sAcc;          // Speed Accuracy Estimate
  unsigned long headingAcc;    // Heading Accuracy Estimate
  unsigned short pDOP;         // Position dilution of precision
  short reserved2;             // Reserved
  unsigned long reserved3;     // Reserved
};

struct ESF_ALG {
  unsigned char cls;		// ID1
  unsigned char id;			// ID2
  unsigned short len;		// message length
  unsigned long iTOW;		//U4 GPS time
  unsigned char version;   	//U1
  unsigned char flags;		//U1
  unsigned char error;		//U1 IMU-mount errors
  unsigned char reserved;		//U1
  unsigned long yaw;		//U4
  short pitch;				//I2
  short roll;				//I2
};


union UBXMessage {
  NAV_PVT navPvt;  		// ZED-F9R
  ESF_ALG esfalg;  		// IMU
};

UBXMessage ubxMessage;

// The last two bytes of the message is a checksum value, used to confirm that the received payload is valid.
// The procedure used to calculate this is given as pseudo-code in the uBlox manual.
void calcChecksum(unsigned char* CK, int msgSize) {
  memset(CK, 0, 2);
  for (int i = 0; i < msgSize; i++) {
    CK[0] += ((unsigned char*)(&ubxMessage))[i];
    CK[1] += CK[0];
  }
}


// Compares the first two bytes of the ubxMessage struct with a specific message header.
// Returns true if the two bytes match.
boolean compareMsgHeader(const unsigned char* msgHeader) {
  unsigned char* ptr = (unsigned char*)(&ubxMessage);
  return ptr[0] == msgHeader[0] && ptr[1] == msgHeader[1];
}


// Reads in bytes from the GPS module and checks to see if a valid message has been constructed.
// Returns the type of the message found if successful, or MT_NONE if no message was found.
// After a successful return the contents of the ubxMessage union will be valid, for the 
// message type that was found. Note that further calls to this function can invalidate the
// message content, so you must use the obtained values before calling this function again.
int processGPS() {
  static int fpos = 0;
  static unsigned char checksum[2];
  
  static byte currentMsgType = MT_NONE;
  static int payloadSize = sizeof(UBXMessage);

  while ( softSerial.available() ) {
    
    byte c = softSerial.read();    
    //Serial.write(c);
    
    if ( fpos < 2 ) {
      // For the first two bytes we are simply looking for a match with the UBX header bytes (0xB5,0x62)
      if ( c == UBX_HEADER[fpos] )
        fpos++;
      else
        fpos = 0; // Reset to beginning state.
    }
    else {
      // If we come here then fpos >= 2, which means we have found a match with the UBX_HEADER
      // and we are now reading in the bytes that make up the payload.
      
      // Place the incoming byte into the ubxMessage struct. The position is fpos-2 because
      // the struct does not include the initial two-byte header (UBX_HEADER).
      if ( (fpos-2) < payloadSize )
        ((unsigned char*)(&ubxMessage))[fpos-2] = c;

      fpos++;
      
      if ( fpos == 4 ) {
        // We have just received the second byte of the message type header, 
        // so now we can check to see what kind of message it is.
        if ( compareMsgHeader(NAV_PVT_HEADER) ) {
          currentMsgType = MT_NAV_PVT;
          payloadSize = sizeof(NAV_PVT);
        }
        else if ( compareMsgHeader(ESF_ALG_HEADER) ) {
          currentMsgType = MT_ESF_ALGS;
          payloadSize = sizeof(ESF_ALG);
        }
        else {
          // unknown message type, bail
          fpos = 0;
          continue;
        }
      }

      if ( fpos == (payloadSize+2) ) {
        // All payload bytes have now been received, so we can calculate the 
        // expected checksum value to compare with the next two incoming bytes.
        calcChecksum(checksum, payloadSize);
      }
      else if ( fpos == (payloadSize+3) ) {
        // First byte after the payload, ie. first byte of the checksum.
        // Does it match the first byte of the checksum we calculated?
        if ( c != checksum[0] ) {
          // Checksum doesn't match, reset to beginning state and try again.
          fpos = 0; 
        }
      }
      else if ( fpos == (payloadSize+4) ) {
        // Second byte after the payload, ie. second byte of the checksum.
        // Does it match the second byte of the checksum we calculated?
        fpos = 0; // We will reset the state regardless of whether the checksum matches.
        if ( c == checksum[1] ) {
          // Checksum matches, we have a valid message.
          return currentMsgType; 
        }
      }
      else if ( fpos > (payloadSize+4) ) {
        // We have now read more bytes than both the expected payload and checksum 
        // together, so something went wrong. Reset to beginning state and try again.
        fpos = 0;
      }
    }
  }
  return MT_NONE;
}

void setup() 
{
  Serial.begin(9600);
  softSerial.begin(9600);
}

long lat;
long lon;

void loop() {
  int msgType = processGPS();
  if ( msgType == MT_NAV_PVT ) {
    Serial.print("iTOW:");      Serial.print(ubxMessage.navPvt.iTOW);  //GPS time
    Serial.print(" lat/lon: "); Serial.print(ubxMessage.navPvt.lat/10000000.0f);
	Serial.print(","); Serial.print(ubxMessage.navPvt.lon/10000000.0f);
	Serial.print("gpsFix:");    Serial.print(ubxMessage.navPvt.fixType);
    Serial.print(" hAcc: ");    Serial.print(ubxMessage.navPvt.hAcc/1000.0f);
    Serial.println();
  }
  else if ( msgType == MT_ESF_ALG ) {
    Serial.print("IMU yaw:");    Serial.print(ubxMessage.esfalg.yaw);  // may be diviede by 1000.0f ???
    Serial.println();
  }
}

