int photo = 0;
int sound = 1;
int modeButton = 10;
int modeswitch = 9;
int sosButton = 11;
int buzzer = 12;
int laser = 13;
int deactiveTime = 0;
int startTime1 = 0;
int low_startTime = 0;
int referenceValue = 1024 * 0.2;
int duration = 100;
String code = "";
String s = "";
bool flag = false;
bool flag1 = false;
bool flag2 = false;
bool flag3 = false;
bool flag4 = false;
bool spaceflag = false;
int sen = 580;
void setup() {
  Serial.begin(9600);
  pinMode(buzzer, OUTPUT);
  digitalWrite(buzzer, HIGH);
  pinMode(laser, OUTPUT);
  pinMode(modeButton, INPUT_PULLUP);
  pinMode(modeswitch, INPUT_PULLUP);
  pinMode(sosButton, INPUT_PULLUP);
  pinMode(2, INPUT_PULLUP);
  attachInterrupt(0, default_light, LOW);
}

void loop() {
  if (digitalRead(modeButton)) {  // input mode
    if (!digitalRead(modeswitch)) {  //light mode
      int dotDuration = duration * 0.9;
      int dashDuration = dotDuration * 3;
      if (analogRead(photo) < referenceValue) {
        int activeTime = 0;
        int startTime = millis();
        while (analogRead(photo) < referenceValue) {
          activeTime = millis() - startTime;
        }
        if (activeTime > dashDuration) {
          code += '2';
        } else if (activeTime > dotDuration) {
          code += '1';
        }
      } else {
        deactiveTime = 0;
        int startTime = millis();
        int endDuration = dotDuration * 30;
        while (analogRead(photo) >= referenceValue) {
          deactiveTime = millis() - startTime;
          if (deactiveTime > endDuration) {
            break;
          }
        }
        if (code != "") {
          if (deactiveTime > endDuration) {
            Serial.println(code);
            code = "";
          } else if (deactiveTime > dotDuration * 7) {
            code += "343";
          } else if (deactiveTime > dashDuration) {
            code += '3';
          }
        }
      }
    } else {  //sound mode
      int dotDuration = duration * 0.9;
      int dashDuration = dotDuration * 3;
      int spaceDuration = dotDuration * 7;
      int endDuration = dotDuration * 20;
      if (analogRead(sound) > sen && flag == false) {
        startTime1 = millis();
        flag = true;
        flag3 = false;
        flag4 = false;
        if (spaceflag == true) {
          s = s + "43";
          spaceflag = false;
        }
      }
      if (analogRead(sound) > sen) {
        flag2 = true;
      }
      if (analogRead(sound) <= sen && flag2 == true) {
        low_startTime = millis();
        flag2 = false;
      }
      deactiveTime = millis() - low_startTime;
      if (deactiveTime > dotDuration && flag == true) {
        int test = millis() - startTime1;
        if (test > dashDuration) {
          s = s + "2";
          flag1 = true;
          flag = false;
        } else if (test > dotDuration) {
          s = s + "1";
          flag1 = true;
          flag = false;
        } else {
          flag3 = false;
        }
      }
      if (deactiveTime > dashDuration && flag1 == true) {
        s = s + "3";
        flag1 = false;
        flag3 = true;
      }
      if (deactiveTime > spaceDuration && flag3 == true) {
        spaceflag = true;
        flag3 = false;
        flag4 = true;
      }
      if (deactiveTime > endDuration && flag4 == true) {
        flag4 = false;
        s.remove(s.length() - 1);
        Serial.println(s);
        s = "";
        deactiveTime = 0;
        spaceflag = false;
      }
    }
  } else {  // output mode
    int dotDuration = duration;
    int dashDuration = dotDuration * 3;
    if (digitalRead(sosButton)) {
      while (Serial.available()) {
        code = Serial.readString();
      }
    } else {
      code = "11132223111";
    }
    for (int i = 0; i < code.length(); i++) {
      switch (code[i]) {
        case '1':
          digitalWrite(laser, HIGH);
          digitalWrite(buzzer, LOW);
          delay(dotDuration);
          digitalWrite(laser, LOW);
          digitalWrite(buzzer, HIGH);
          break;
        case '2':
          digitalWrite(laser, HIGH);
          digitalWrite(buzzer, LOW);
          delay(dashDuration);
          digitalWrite(laser, LOW);
          digitalWrite(buzzer, HIGH);
          break;
        case '3':
          delay(dotDuration * 2);
          break;
        default:
          delay(dotDuration);
          break;
      }
      delay(dotDuration);
    }
    delay(dotDuration * 20);
  }
}
void default_light() {
  int i = analogRead(0);
  if (i > 400) {
    referenceValue = i - 200;
  } else {
    referenceValue = 100;
  }
}
