#include <arduinoFFT.h>

int photo = 0;
int sound = 1;
int modeButton = 10;
int modeswitch = 9;
int sosButton = 11;
int buzzer = 12;
int laser = 13;

int referenceValue = 1024 * 0.2;
int referenceSoundLevel = 100.0;
int duration = 100;
String code = "";

void setup() {
  Serial.begin(9600);
  pinMode(laser, OUTPUT);
  pinMode(buzzer, OUTPUT);
  digitalWrite(buzzer, HIGH);

  pinMode(sosButton, INPUT_PULLUP);
  pinMode(modeButton, INPUT_PULLUP);

  pinMode(modeswitch, INPUT_PULLUP);
  pinMode(2, INPUT_PULLUP);
  attachInterrupt(0, default_light, LOW);
}

void loop() {
  if (digitalRead(modeButton)) {  // input mode
    int dotDuration = duration * 0.9;
    int dashDuration = dotDuration * 3;

    if (!digitalRead(modeswitch)) {  //light mode
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
        int deactiveTime = 0;
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
      if (extractSoundVolume(sound) > referenceSoundLevel) {
        int activeTime = 0;
        int startTime = millis();
        while (extractSoundVolume(sound) > referenceSoundLevel) {
          activeTime = millis() - startTime;
        }
        if (activeTime > dashDuration) {
          code += '2';
        } else if (activeTime > dotDuration) {
          code += '1';
        }
      } else {
        int deactiveTime = 0;
        int startTime = millis();
        int endDuration = dotDuration * 20;
        while (extractSoundVolume(sound) <= referenceSoundLevel) {
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

double extractSoundVolume(int pinNumber) {
  arduinoFFT FFT = arduinoFFT();

  const uint16_t samples = 8;
  double vReal[samples];
  double vImag[samples];

  for (int i = 0; i < samples; i++)
  {
    vReal[i] = analogRead(pinNumber);
    vImag[i] = 0;
  }

  FFT.Windowing(vReal, samples, FFT_WIN_TYP_HAMMING, FFT_FORWARD);
  FFT.Compute(vReal, vImag, samples, FFT_FORWARD);
  FFT.ComplexToMagnitude(vReal, vImag, samples);

  return vReal[3];
}

void default_light() {
  int i = analogRead(photo);
  if (i > 400) {
    referenceValue = i - 200;
  } else {
    referenceValue = 100;
  }
}
