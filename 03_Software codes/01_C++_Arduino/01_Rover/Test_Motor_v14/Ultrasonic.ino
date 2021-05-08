// Ultrasoninc Sensor HC-SR04
// https://create.arduino.cc/projecthub/abdularbi17/ultrasonic-sensor-hc-sr04-with-arduino-tutorial-327ff6

int USSensor(int trigPin, int echoPin){  // mesure distance
  // defines variables
  long duration; // variable for the duration of sound wave travel
  int distance; // variable for the distance measurement
  //Serial.print("U TRIG: ");Serial.println(trigPin);
  //Serial.print("U ECHO: ");Serial.println(echoPin);
  // Clears the trigPin condition
  digitalWrite(trigPin, LOW);
  delayMicroseconds(2);
  // Sets the trigPin HIGH (ACTIVE) for 10 microseconds
  digitalWrite(trigPin, HIGH);
  delayMicroseconds(10);
  digitalWrite(trigPin, LOW);
  // Reads the echoPin, returns the sound wave travel time in microseconds
  duration = pulseIn(echoPin, HIGH);
  // Calculating the distance
  distance = duration * 0.034 / 2; // in cm. Speed of sound wave divided by 2 (go and back)
  // Serial.print("U DISTANCE: ");Serial.println(distance);
  return distance;
}
