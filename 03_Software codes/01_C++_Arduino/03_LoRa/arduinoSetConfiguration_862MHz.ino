/*
 * LoRa E32-TTL-100
 * Set configuration.
 * https://www.mischianti.org/2019/10/29/lora-e32-device-for-arduino-esp32-or-esp8266-configuration-part-3/
 * https://www.mischianti.org/2019/10/21/lora-e32-device-for-arduino-esp32-or-esp8266-library-part-2/  !!!!
 * https://www.mischianti.org/2021/01/28/ebyte-lora-e22-device-for-arduino-esp32-or-esp8266-library-part-2/
 * https://www.mischianti.org/2019/10/21/lora-e32-device-for-arduino-esp32-or-esp8266-library-part-2/ // I posted on 18.June'21
 * 
 * https://github.com/xreef/LoRa_E32_Series_Library
 * E32-TTL-100----- Arduino UNO
 * M0         ----- 3.3v (or D7)
 * M1         ----- 3.3v (or D6)
 * TX         ----- D2 (add pull-up resistor (4,7Kohm) to get good stability)
 * RX         ----- D3 (add pull-up resistor (4,7Kohm) to get good stability & Voltage divider)
 * AUX        ----- Not connected or D5 (input)
 * VCC        ----- 3.3v/5v
 * GND        ----- GND
 *
 */

//My LoRa module:
// 868 MHz LoRa SX 1276 RF long range E32-868T30D UART 1W IoT RF Transreceiver 868MHz
//My Antenna:
// 824 - 960 MHz & 1710 - 2170 MHz dual band 4/6 dBi magnetic mount antenna

// #define E32_TTL_100  // POWER_20(def) POWER_17 POWER_14 POWER_10
// #define E32_TTL_500  // POWER_27(def) POWER_24 POWER_21 POWER_18
#define E32_TTL_1W      // POWER_30(def)  POWER_27 POWER_24 POWER_21
#define FREQUENCY_868   //#define FREQUENCY_868
/*
#define FREQUENCY_433
#define FREQUENCY_170
#define FREQUENCY_470
#define FREQUENCY_868
#define FREQUENCY_915
*/

#include "Arduino.h"
#include "LoRa_E32.h"

LoRa_E32 e32ttl100(2, 3); // e32 TX e32 RX

void printParameters(struct Configuration configuration);
void printModuleInformation(struct ModuleInformation moduleInformation);

void setup() {
	Serial.begin(9600);
	delay(500);

	// Startup all pins and UART
	e32ttl100.begin();

	ResponseStructContainer c;
	c = e32ttl100.getConfiguration();
	// It's important get configuration pointer before all other operation
	Configuration configuration = *(Configuration*) c.data;
	Serial.println(c.status.getResponseDescription());
	Serial.println(c.status.code);

  printParameters(configuration);
  //configuration.ADDL = 0x0; // 0x0; (def: 00H /00H-FFH) // First part of address
  //configuration.ADDH = 0x1; // 0x1; (def: 00H /00H-FFH) // Second part of address
  //configuration.CHAN = 0x06; // 0x19; Communication channel (def 17H == 23d == 433MHz / 410 M + CHAN*1 MHz)
                               // def 06H == 868 MHz / 862 + CHAN*1MHz

	//configuration.OPTION.fec = FEC_0_OFF;
//	configuration.OPTION.fixedTransmission = FT_TRANSPARENT_TRANSMISSION;
	//configuration.OPTION.ioDriveMode = IO_D_MODE_PUSH_PULLS_PULL_UPS;

 // #define E32_TTL_100 // default value without set  //POWER_20 POWER_17 POWER_14 POWER_10
 // #define E32_TTL_1W                                //POWER_30 POWER_27 POWER_24 POWER_21
	configuration.OPTION.transmissionPower = POWER_21; // dBm transmission power 
	
	//configuration.OPTION.wirelessWakeupTime = WAKE_UP_1250; // Wait time for wake up

//	configuration.SPED.airDataRate = AIR_DATA_RATE_011_48;  // Air data rate
//	configuration.SPED.uartBaudRate = UART_BPS_115200;      // 9600bps (default) Communication baud rate
//	configuration.SPED.uartParity = MODE_00_8N1;

	// Set configuration changed and set to not hold the configuration
	ResponseStatus rs = e32ttl100.setConfiguration(configuration, WRITE_CFG_PWR_DWN_LOSE);
	Serial.println(rs.getResponseDescription());
	Serial.println(rs.code);
	printParameters(configuration);
	c.close();
}

void loop() {

}
void printParameters(struct Configuration configuration) {
	Serial.println("----------------------------------------");

	Serial.print(F("HEAD : "));  Serial.print(configuration.HEAD, BIN);Serial.print(" ");Serial.print(configuration.HEAD, DEC);Serial.print(" ");Serial.println(configuration.HEAD, HEX);
	Serial.println(F(" "));
	Serial.print(F("AddH : "));  Serial.println(configuration.ADDH, BIN);
	Serial.print(F("AddL : "));  Serial.println(configuration.ADDL, BIN);
	Serial.print(F("Chan : "));  Serial.print(configuration.CHAN, DEC); Serial.print(" -> "); Serial.println(configuration.getChannelDescription());
	Serial.println(F(" "));
	Serial.print(F("SpeedParityBit     : "));  Serial.print(configuration.SPED.uartParity, BIN);Serial.print(" -> "); Serial.println(configuration.SPED.getUARTParityDescription());
	Serial.print(F("SpeedUARTDatte  : "));  Serial.print(configuration.SPED.uartBaudRate, BIN);Serial.print(" -> "); Serial.println(configuration.SPED.getUARTBaudRate());
	Serial.print(F("SpeedAirDataRate   : "));  Serial.print(configuration.SPED.airDataRate, BIN);Serial.print(" -> "); Serial.println(configuration.SPED.getAirDataRate());

	Serial.print(F("OptionTrans        : "));  Serial.print(configuration.OPTION.fixedTransmission, BIN);Serial.print(" -> "); Serial.println(configuration.OPTION.getFixedTransmissionDescription());
	Serial.print(F("OptionPullup       : "));  Serial.print(configuration.OPTION.ioDriveMode, BIN);Serial.print(" -> "); Serial.println(configuration.OPTION.getIODroveModeDescription());
	Serial.print(F("OptionWakeup       : "));  Serial.print(configuration.OPTION.wirelessWakeupTime, BIN);Serial.print(" -> "); Serial.println(configuration.OPTION.getWirelessWakeUPTimeDescription());
	Serial.print(F("OptionFEC          : "));  Serial.print(configuration.OPTION.fec, BIN);Serial.print(" -> "); Serial.println(configuration.OPTION.getFECDescription());
	Serial.print(F("OptionPower        : "));  Serial.print(configuration.OPTION.transmissionPower, BIN);Serial.print(" -> "); Serial.println(configuration.OPTION.getTransmissionPowerDescription());

	Serial.println("----------------------------------------");

}
