using Microtoolskit.Hardware.Displays;

using System.Threading;
using testMicroToolsKit.Hardware.IHM;

namespace FezPanda
{
    public class Program
    {
        public static void Main()
        {
            IHMSSI ihm = new IHMSSI(new I2CLcd(I2CLcd.LcdManufacturer.BATRON, 100));

            ihm.Lcd.PutString(0, 0, "IHM SSI");
            ihm.Lcd.PutString(8, 1, "WebGE");
            Thread.Sleep(3000);

            while (true)
            {
                ihm.D0.Write(ihm.BpPlus.Read());
                ihm.D1.Write(ihm.BpFleHaut.Read());
                ihm.D2.Write(ihm.BpFleBas.Read());
                ihm.D3.Write(ihm.BpMoins.Read());
                ihm.D4.Write(ihm.BpOk.Read());
                ihm.D5.Write(ihm.BpSet.Read());
                ihm.D6.Write(ihm.BpEchap.Read());
                ihm.D7.Write(ihm.BpEnter.Read());
                Thread.Sleep(10);
            }
        }
    }
}
