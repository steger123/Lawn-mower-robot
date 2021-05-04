/* Can be:
1. Manual - PS2 - modMan
2. Manual - Xbee - modRemote
3. Manual - BT/Mobile phone

1. Fully autonomouse just Go - modGo
2. Misson planned with GPS - XBee - modMis
3. Visual Servoing just Go + AI Jetson Nano
*/




/*Hercules 6V-36V, 16Amp Motor Driver
* Peak output current: 30Amps
* Maximum PWM Frequency: 10KHz 
* VIL (Low level logic input) <0.8V 
* VIH (High level logic input)  3.5V to 5V 
//http://www.nex-robotics.com/products/motor-drivers/hercules-6v-36v-16amp-motor-driver.html
*/

/*
 * Orange Planetary Gear DC Motor 12V 50 RPM 392.4 N-cm PGM45775-99.5K
 * Model: PGM45775-99.5K
 * Operating Voltage: 12V DC
 * Rated Torque: 392.4 N-cm
 * Rated Speed: 50 RPM
 * Rated Current: 4.28 A
 * Rated Power: 50.97 W
 * Gear Ratio: 99.5 : 1
 */

// Power consution @ 12V, no load on wheels
// @ 50 pwm: 0.8A - 9.6W
// @ 80 pwm: 1.2A - 14.4W
// @ 150 pwm: 1.5 A - 18W
