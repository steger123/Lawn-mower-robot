#include <ds3231.h>
ts t;     //ts is a struct findable in ds3231.h

void setupRTC() { // this to be run only one time to set the date & time in RTC. After that the battery will ensure the update
                  // or run separately: 'RTC_setDateTime.ino'
  // Serial.begin(9600);  
  // Wire.begin();
  Serial.println("Setup RTC ...");
  DS3231_init(DS3231_INTCN); //register the ds3231 (DS3231_INTCN is the default address of ds3231, this is set by macro for no performance loss)
  // DS3231_init(DS3231_CONTROL_INTCN);
  /*----------------------------------------------------------------------------
    In order to synchronise your clock module, insert timetable values below !
    ----------------------------------------------------------------------------*/
  t.year = 2021;
  t.mon = 04;
  t.mday = 1;
  t.hour = 12;
  t.min = 30;
  t.sec = 0;
  
  DS3231_set(t);
}

void getRTCtime() {
  DS3231_get(&t);
  Serial.print("Date : ");
  Serial.print(t.mday);
  Serial.print("/");
  Serial.print(t.mon);
  Serial.print("/");
  Serial.print(t.year);
  Serial.print("\t Hour : ");
  Serial.print(t.hour);
  Serial.print(":");
  Serial.print(t.min);
  Serial.print(".");
  Serial.println(t.sec);
}
