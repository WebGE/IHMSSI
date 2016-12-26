using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;

using Microtoolskit.Hardware.IHM;
using Microtoolskit.Hardware.Displays;

namespace NetduinoIHMSSI
{
    public class Program
    {
        public static void Main()
        {
            // Construction d'un objet IHM
            // IHMSSI Ihm = new IHMSSI();
            //IHMSSI Ihm = new IHMSSI(new PCF8574(0x39,200), new I2CLcd(I2CLcd.LcdManufacturer.BATRON,100));
            IHMSSI Ihm = new IHMSSI(new I2CLcd(I2CLcd.LcdManufacturer.MIDAS));
            Ihm.Lcd.PutString(0, 0, "Classe IHMSSI");
            Ihm.Lcd.PutString(0, 1, "Test des Leds");

            // Leds test
            Ihm.LedsWrite(0xff); // Toutes les Leds éclairée
            Thread.Sleep(1000);
            Ihm.LedsOff();
            Ihm.LedsWrite(0x0f); // 4 leds de droite éclairée
            Thread.Sleep(1000);
            Ihm.LedsOff();
            Thread.Sleep(1000);
            Ihm.D4.Write(true);
            Ihm.D5.Write(true);
            Ihm.D6.Write(true);
            Ihm.D7.Write(true);
            Thread.Sleep(1000);
            Ihm.LedsOff();
            Thread.Sleep(1000);
            // BPs test
            Ihm.Lcd.ClearScreen();
            Ihm.Lcd.PutString(0, 0, "Classe IHMSSI");
            Ihm.Lcd.PutString(0, 1, "Test des Bps");

            while (true)
            {
                if (Ihm.BpPlus.Read() == true)
                    Ihm.D0.Write(true);
                else
                    Ihm.D0.Write(false);

                if (Ihm.BpFleHaut.Read() == true)
                    Ihm.D1.Write(true);
                else
                    Ihm.D1.Write(false);

                if (Ihm.BpMoins.Read() == true)
                    Ihm.D2.Write(true);
                else
                    Ihm.D2.Write(false);

                if (Ihm.BpFleBas.Read() == true)
                    Ihm.D3.Write(true);
                else
                    Ihm.D3.Write(false);

                if (Ihm.BpSet.Read() == true)
                    Ihm.D4.Write(true);
                else
                    Ihm.D4.Write(false);

                if (Ihm.BpOk.Read() == true)
                    Ihm.D5.Write(true);
                else
                    Ihm.D5.Write(false);

                if (Ihm.BpEnter.Read() == true)
                    Ihm.D6.Write(true);
                else
                    Ihm.D6.Write(false);

                if (Ihm.BpEchap.Read() == true)
                    Ihm.D7.Write(true);
                else
                    Ihm.D7.Write(false);
                Thread.Sleep(10);

            }


        }

    }
}
