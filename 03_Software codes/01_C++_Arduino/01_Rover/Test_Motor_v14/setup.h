//*********** Global Variables: ***********
float VBat;                  // Battery voltage [V]
float VBatPC;                // Battery voltage percentage
#define VBat_Level_Min  10.0
#define VBat_Level_Max  13.8
#define IBat_Max 2.0         // defines the maximum permissible current to the full device ACS712

const unsigned long BatVoltMax = 13500; //mV
const unsigned long BatVoltMin = 10400; //mV
const unsigned long BatRs[]    = {47250, 10020};    // 47150 or 45300 or 45390 or 45900 ; 47k & 10k = R2 & R3 in ohm, must be measured accurately before soldering !!!
double InverseVoltDividerRatio = 5.6918; //divison give only integer !; //5.7355289421157684630738522954092
const unsigned int NrSamples   = 10;    // Usually from 5 to 64 How many samples to be taken for batVolt measuring

float batTemp;             // Battery temperature
int compassHeading;
boolean rain;
//float ISupply;  //Instead of ICut , but IPanel (for solar panel requried)
long ISupply;

#define timeReverse 3000  // duration reverse motor when obstacle detect
#define timeRotate  2000  // duration rotate motor when obstacle detect
#define USdistance  100   // obstacle distance for ultrasonic sensor
int USDistaneLeft;        // Utrasonic sensor distance in cm
int USDistaneRight;
int USDistaneCenter;

unsigned long previousMillis = 0;
unsigned long currentMillis; 
#define timeClock   500   // time diffrence for reading new sensor data
volatile int mowerStatus=0; // 0=oncharge (press pen for run)
                            // 1=run
                            // 2=stuck
                            // 3=search
                            // 4=batlow
                            // 5=charge and restart when full
                            // 6=raining
                            // 7=current too much on the battery/motor controller
volatile unsigned long wheelTime = 0;
                            
#define accelerateTime   50   // 2 total time accelerate [msec]
int encCountA = 0;          // how many times the encoders wheel tunred 
int wheelTurnCount = 0;              // one wheel tunr = 1240 turn on encoder's wheel. How many wheel turn required for X meter.
String mode = "---";      // this required in Interrrup, becuse for turn (aling heading to bearing) less ecoder tick reuires than cover distance.
int turnTickCount = 0;      //how many ecoder tick required lat se for X degree tunr

int PWM_speed   = 0;          // actual speed of the robot, first select "start" to start the robot
int oldSpeed    = 0;          // for proper acceleration 
int wheelTurnRequired    = -1;
String robDirection = "st";   // start
String prevDirection;
boolean suddenStop = false;  // it is in the Interrrup when ultrasonic senso triggered.
int gear        = 2;
boolean startMotor = false;
int i;                        // for 'for' cyclus
int error =     0;
byte type =     0;
byte vibrate =  0;

int BtnMotor  = 1;
int BtnSensor = 2;
int BtnOther  = 3;

// ***** Digital pins: ******
#define manualMode_pin    6
#define remoteMode_pin    7


#define Encoder_pin_A    2   // 2 INT4 Signal pin # Encoder_A- from encoder UNO only: 2 & 3 pins ! MEGA only: 2, 3, 18, 19, 20, 21
#define Encoder_pin_B     3    // 3 INT5 Signal pin # Encoder_B - from encoder
//#define Panel_pin         8  // Solar panel on/off

#define PWMA_pin          4    // 4 PWM; orange PWM_A                   enable_A
#define motDIR_pin_A1    22    // 6 IN-1 ; Direction_1; brown  DIR_A1  motor_A1
#define motDIR_pin_A2    24    // 5 IN-2 ; Direction_2; green  DIR_A2  motor_A2 
#define PWMB_pin          5    // 8 PWM; orange PWM_pin_B                   enable_B 
#define motDIR_pin_B1    28    // 9 IN-2 ; Direction_1; green  DIR_B1  motor_B1 
#define motDIR_pin_B2    26    // 10 IN-1 ; Direction_2; brown DIR_B2  motor_B2

#define Trig_pinL  31  //Ultrasonic LEFT
#define Echo_pinL  33
#define Trig_pinR  35  //Ultrasonic RIGHT
#define Echo_pinR  37
#define Trig_pinC  39  //Ultrasonic CENTER
#define Echo_pinC  41

#define Temp_pin 46   // for temperature sensor

//**** Analog pins: ******
#define Button_pin     A10  // #A10 Push button pin
#define BatVolt_Pin    A11  // #A11 Battery voltage const byte BatVoltPin = A11;     //Analog pin # A11
#define IBat_pin       A5  // Battery supply full current with ACS712
#define RainingA_pin   A6  // Rain drop sensor anlaoge output
#define RainingD_pin   40  // Rain drop sensor digital output 

#define SDA_pin        20  // #20 SDA for I2C
#define SCL_pin        21  // #21 SCL for I2C

//#define BatVolt_Pin 2  // ???? A11

void setupPins(){
  pinMode(motDIR_pin_A1,  OUTPUT);
  pinMode(motDIR_pin_A2,  OUTPUT);
  pinMode(PWMA_pin,       OUTPUT);
  pinMode(motDIR_pin_B1,  OUTPUT);
  pinMode(motDIR_pin_B2,  OUTPUT);
  pinMode(PWMB_pin,       OUTPUT);
  //  pinMode(A_Signal_Enc, INPUT);
  pinMode(Encoder_pin_A,  INPUT_PULLUP);
  //pinMode(Encoder_pin_A,  INPUT);
   pinMode(Encoder_pin_B,  INPUT_PULLUP);
  //pinMode(Encoder_pin_B,  INPUT);
  pinMode(manualMode_pin, INPUT);
  //  pinMode(Panel_pin,      OUTPUT);  //solar panel
  pinMode(manualMode_pin, INPUT);

  digitalWrite(motDIR_pin_A1, LOW);
  digitalWrite(motDIR_pin_A2, LOW);
  digitalWrite(motDIR_pin_B1, LOW);
  digitalWrite(motDIR_pin_B2, LOW);
  analogWrite(PWMA_pin, 0);
  analogWrite(PWMB_pin, 0);
  //  digitalWrite(Panel_pin, LOW);

  // Sensors ?:
  //pinMode(Button_pin, INPUT);
  //analogWrite(Button_pin, HIGH);
  pinMode(Trig_pinL, OUTPUT);
  pinMode(Echo_pinL, INPUT);
  pinMode(Trig_pinR, OUTPUT);
  pinMode(Echo_pinR, INPUT);
  pinMode(Trig_pinC, OUTPUT);
  pinMode(Echo_pinC, INPUT);
}
