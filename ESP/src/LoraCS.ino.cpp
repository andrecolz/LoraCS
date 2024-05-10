# 1 "C:\\Users\\Andre\\AppData\\Local\\Temp\\tmpglnmkeg5"
#include <Arduino.h>
# 1 "C:/Users/Andre/Documents/LoraCS/ESP/src/LoraCS.ino"
#include "TFT_eSPI.h"
#include "connManager.h"


TFT_eSPI tft= TFT_eSPI();
TFT_eSprite sprite = TFT_eSprite(&tft);
connManager LoraCS;
bool lastStatus;
void setup();
void loop();
#line 10 "C:/Users/Andre/Documents/LoraCS/ESP/src/LoraCS.ino"
void setup() {
  Serial.begin(9600);
  tft.init();
  tft.setRotation(0);
  tft.setSwapBytes(true);
  tft.fillScreen(TFT_RED);
  sprite.createSprite(158, 308);
}

void loop() {
  sprite.fillScreen(TFT_WHITE);
  if (Serial.available() > 0) {
    String command = Serial.readString();
    Serial.println(command);
    LoraCS.read(command);
  }

  if(lastStatus != LoraCS.status){
    if(LoraCS.status){
      lastStatus = LoraCS.status;
      tft.fillScreen(TFT_GREEN);
    } else {
      lastStatus = LoraCS.status;
      tft.fillScreen(TFT_RED);
    }
  }

  sprite.pushSprite(6,6);
}