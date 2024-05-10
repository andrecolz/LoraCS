using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LoraCS_win
{
    public class ESPController
    {
        SerialPort serialPort;
        public String lastMsg;
        public String port;

        public ESPController() 
        {
            port = "";
        }

        public bool createConnection(String port)
        {
            try
            {
                serialPort = new SerialPort(port, 9600); // Imposta la velocità di comunicazione desiderata
                serialPort.WriteTimeout = 2500;
                serialPort.Open();
                Thread.Sleep(200);
                serialPort.Write("connesso"); // Invia un comando di test all'ESP32
                Thread.Sleep(500);
                String response = "";
                while (response != null)
                {
                    response = serialPort.ReadLine();
                    if (response.Contains("ok\r"))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore durante la comunicazione con la porta " + port + ": " + ex.Message);
            }
           

            return false;
        }

        public bool ping()
        {

            return false;
        }

        public bool sendMsg(String msg, User friend)
        {
            try
            {
                String toSend = "send;" + friend.toString1();
                serialPort.Write(toSend);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public String read()
        {
            String msg = serialPort.ReadLine();

            if(msg != lastMsg)
            {
                return msg;
            }
            lastMsg = msg;
            return "";
        }

        public bool setConfg(int ADDL, int ADDH, int CHAN)
        {
            serialPort.Write("setconfg;" + ADDL + ";" + ADDH + ";" + CHAN + ";" + true);
            return false;
        }
    }
}