#ifndef CONNMANAGER_H
#define CONNMANAGER_H

#include <Arduino.h>
#include "ArduinoJson.h"
#include "LoRa_E220.h"

class connManager {
  private:
    LoRa_E220 e220ttl;
    
  public:
    bool status;
    bool loraStatus;

    connManager();
    void read(String msg);
    bool setConfg(byte ADDL, byte ADDH, byte CHAN, bool RSSI);
    void getConfg();
    void readLora();
    void sendLora(byte ADDL, byte ADDH, byte CHAN, bool RSSI, String msg);


};

#endif