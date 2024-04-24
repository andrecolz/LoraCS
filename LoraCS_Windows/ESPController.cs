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
        public String port;

        public ESPController() 
        {
            port = "";
        }

        public bool createConnection(String port)
        {
            try
            {
                SerialPort serialPort = new SerialPort(port, 9600); // Imposta la velocità di comunicazione desiderata
                serialPort.WriteTimeout = 2000;
                serialPort.ReadTimeout = 2000;
                serialPort.Open();
                serialPort.Write("connesso"); // Invia un comando di test all'ESP32
                Thread.Sleep(500);
                String response = "";
                while (response != null)
                {
                    response = serialPort.ReadLine();
                    if (response.Contains("ok\r"))
                    {
                        serialPort.Close();
                        return true;
                    }
                }
                serialPort.Close();
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

        public void sendMsg()
        {

        }
    }
}