#include "connManager.h"
#include "ArduinoJson.h"

connManager::connManager() : e220ttl(&Serial2, 21, 13, 12, UART_BPS_RATE_9600) {
  status = false;

  Serial2.begin(9600, SERIAL_8N1, 18, 17);
  e220ttl.begin();
}

void connManager::read(String msg) {
  if(msg == "connesso"){
    status = true;
	Serial.println("ok");
  } 
  if(msg == "confg"){
	setConfg(40, 45, 20, false);
  }
}

bool connManager::setConfg(byte ADDL, byte ADDH, byte CHAN, bool RSSI){
  	ResponseStructContainer c;
	c = e220ttl.getConfiguration();
	Configuration configuration = *(Configuration*) c.data;

	if(String(c.status.code) == "1"){
		configuration.ADDL = ADDL;
		configuration.ADDH = ADDH;
		configuration.CHAN = CHAN;

		configuration.SPED.uartBaudRate = UART_BPS_9600;
		configuration.SPED.airDataRate = AIR_DATA_RATE_010_24;
		configuration.SPED.uartParity = MODE_00_8N1;
		configuration.OPTION.subPacketSetting = SPS_200_00;
		configuration.OPTION.RSSIAmbientNoise = RSSI_AMBIENT_NOISE_DISABLED;
		configuration.OPTION.transmissionPower = POWER_22;

		if(RSSI)
			configuration.TRANSMISSION_MODE.enableRSSI = RSSI_ENABLED;
		else
			configuration.TRANSMISSION_MODE.enableRSSI = RSSI_DISABLED;
		
		configuration.TRANSMISSION_MODE.fixedTransmission = FT_FIXED_TRANSMISSION;
		configuration.TRANSMISSION_MODE.enableLBT = LBT_DISABLED;
		configuration.TRANSMISSION_MODE.WORPeriod = WOR_2000_011;



		ResponseStatus rs = e220ttl.setConfiguration(configuration, WRITE_CFG_PWR_DWN_SAVE);
		if(String(rs.code) != "1")
			return false;

		c = e220ttl.getConfiguration();
		configuration = *(Configuration*) c.data;
		if(String(c.status.code) != "1")
			return false;

		c.close();
		getConfg();
		return true;
	}	
	return false;
}

void connManager::getConfg(){
	JsonDocument doc;
	ResponseStructContainer c;
	c = e220ttl.getConfiguration();
	Configuration configuration = *(Configuration*) c.data;
	
	if(String(c.status.code) == "1"){
		doc["response"] = "200";
		doc["addh"] = configuration.ADDH;
		doc["addl"] = configuration.ADDL;
		doc["chan"] = configuration.CHAN;
	} else {
		doc["response"] = "400";
	}
	
	String jsonString;
	serializeJson(doc, jsonString);
	Serial.println(jsonString);
}

void connManager::readLora(){

}

void connManager::sendLora(byte ADDL, byte ADDH, byte CHAN, bool RSSI, String msg){

}