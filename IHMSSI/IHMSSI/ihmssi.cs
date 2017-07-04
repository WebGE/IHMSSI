using System;
using Microsoft.SPOT;

using Microtoolskit.Hardware.IO;
using Microtoolskit.Hardware.Displays;

namespace testMicroToolsKit
{
    namespace Hardware
    {
        namespace IHM
        {  /// <summary>
           /// IHMSSI - Personal IHM Class
           /// </summary>
            public class IHMSSI
            {/// <summary>
             /// Eight LEDs are connected to a PCF8574(A)
             /// </summary>
                public class Led
                {
                    private bool state;
                    private byte position;
                    private PCF8574 leds;

                    /// <summary>
                    /// Constructor
                    /// </summary>
                    /// <param name="state">true or false</param>
                    /// <param name="position">position 0 to 7</param>
                    /// <param name="leds">I2C port I/O</param>
                    public Led(bool state, byte position, PCF8574 leds)
                    {
                        this.state = state;
                        this.position = position;
                        this.leds = leds;
                    }

                    /// <summary>
                    /// Write true or false on Led n (n = 0...7)
                    /// </summary>
                    /// <param name="state">true or false</param>
                    public void Write(bool state)
                    {
                        byte mask = 1; byte stateLeds;
                        mask = (byte)(mask << position);

                        if (state)
                        {
                            stateLeds = (byte)(leds.Read() & (byte)~mask);
                        }
                        else
                        {
                            stateLeds = (byte)(leds.Read() | mask);
                        }
                        leds.Write(stateLeds);
                    }

                }

                /// <summary>
                /// Eight buttons are connected to a PCF8574(A)
                /// </summary>
                public class BP
                {
                    private byte position;
                    private PCF8574 buttons;

                    public BP(PCF8574 buttons, byte position)
                    {
                        this.buttons = buttons;
                        this.position = position;
                    }

                    public bool Read()
                    {
                        byte value = buttons.Read();
                        if (position == (value & position))
                            return true;
                        else
                            return false;
                    }
                }

                #region Components
                private PCF8574 BPs;
                private BP bpPlus;
                private BP bpMoins;
                private BP bpFleHaut;
                private BP bpFleBas;
                private BP bpOk;
                private BP bpEnter;
                private BP bpEchap;
                private BP bpSet;

                private PCF8574 leds;
                private Led d0;
                private Led d1;
                private Led d2;
                private Led d3;
                private Led d4;
                private Led d5;
                private Led d6;
                private Led d7;

                private I2CLcd lcd;
                #endregion

                #region Properties
                /// <summary>
                /// Access to Led D0 methods
                /// </summary>
                public Led D0
                {
                    get { return d0; }
                }
                /// <summary>
                /// Access to Led D1 methods
                /// </summary>
                public Led D1
                {
                    get { return d1; }
                }
                /// <summary>
                /// Access to Led D2 methods
                /// </summary>
                public Led D2
                {
                    get { return d2; }
                }
                /// <summary>
                /// Access to Led D3 methods
                /// </summary>
                public Led D3
                {
                    get { return d3; }
                }
                /// <summary>
                /// Access to Led D4 methods
                /// </summary>
                public Led D4
                {
                    get { return d4; }
                }
                /// <summary>
                /// Access to Led D5 methods
                /// </summary>
                public Led D5
                {
                    get { return d5; }
                }
                /// <summary>
                /// Access to Led D6 methods
                /// </summary>
                public Led D6
                {
                    get { return d6; }
                }
                /// <summary>
                /// Access to Led D7 methods
                /// </summary>
                public Led D7
                {
                    get { return d7; }
                }
                /// <summary>
                /// Access to BP methods
                /// </summary>
                public BP BpPlus
                {
                    get
                    {
                        return bpPlus;
                    }

                }
                /// <summary>
                /// Access to BP methods
                /// </summary>
                public BP BpFleHaut
                {
                    get
                    {
                        return bpFleHaut;
                    }

                }
                /// <summary>
                /// Access to BP methods
                /// </summary>
                public BP BpMoins
                {
                    get
                    {
                        return bpMoins;
                    }

                }
                /// <summary>
                /// Access to BP methods
                /// </summary>
                public BP BpFleBas
                {
                    get
                    {
                        return bpFleBas;
                    }
                }
                /// <summary>
                /// Access to BP methods
                /// </summary>
                public BP BpSet
                {
                    get
                    {
                        return bpSet;
                    }

                }
                /// <summary>
                /// Access to BP methods
                /// </summary>
                public BP BpOk
                {
                    get
                    {
                        return bpOk;
                    }

                }
                /// <summary>
                /// Access to BP methods
                /// </summary>
                public BP BpEnter
                {
                    get
                    {
                        return bpEnter;
                    }
                }
                /// <summary>
                /// Access to BP methods
                /// </summary>
                public BP BpEchap
                {
                    get
                    {
                        return bpEchap;
                    }

                }
                /// <summary>
                /// Access to Lcd methods
                /// </summary>
                public I2CLcd Lcd
                {
                    get
                    {
                        return lcd;
                    }
                }
                #endregion

                #region Constructors
                /// <summary>
                /// IHMSSI constructor with LCD = MIDAS(0x3A); PCF8574(A) SLA are 0x3f for BP and 0x38 for Led; I2C Bus Frequency = 100kHz
                /// </summary>
                /// <remarks>
                /// All Led => Off; Lcd => Init
                /// </remarks>
                public IHMSSI() : this(new PCF8574(0x38, 100), new PCF8574(0x3f, 100), new I2CLcd(I2CLcd.LcdManufacturer.MIDAS, 100)) { }

                /// <summary>
                /// IHMSSI constructor with PCF8574(A) SLA are 0x3f for BP and 0x38 for Led; I2C Bus Frequency = 100kHz
                /// </summary>
                /// <param name="lcd">MIDAS(0x3A) or BATRON(0x3B)</param>
                /// <remarks>
                /// All Led => Off; Lcd => Init
                /// </remarks>
                public IHMSSI(I2CLcd lcd) : this(new PCF8574(0x38, 100), new PCF8574(0x3f, 100), lcd) { }

                /// <summary>
                ///IHMSSI constructor with LCD = MIDAS(0x3A); PCF8574(A) SLA are 0x3f for BP and 0x38 for Led; I2C Bus Frequency = 100kHz
                /// </summary>
                /// <param name="leds">if changed PCF8574 SLA must be in [0x20, 0x27], PCF8574A in [0x38, 0x3F] and Frequency in [100kHz,400kHz]</param>
                /// <param name="lcd">lcd is MIDAS(0x3A) or BATRON(0x3B)</param>
                /// <remarks>
                /// All Led => Off; Lcd => Init
                /// </remarks>
                public IHMSSI(PCF8574 leds, I2CLcd lcd) : this(leds, new PCF8574(0x3f, 100), lcd) { }

                /// <summary>
                /// IHMSSI constructor
                /// </summary>
                /// <param name="leds">if changed PCF8574 SLA must be in [0x20, 0x27], PCF8574A in [0x38, 0x3F] and Frequency in [100kHz,400kHz]</param>
                /// <param name="BPs">if changed PCF8574 SLA must be in [0x20, 0x27], PCF8574A in [0x38, 0x3F] and Frequency in [100kHz,400kHz]</param>
                /// <param name="lcd">lcd is MIDAS(0x3A) or BATRON(0x3B), Frequency in [100kHz,400kHz]</param>
                /// <remarks>
                /// All Led => Off; Lcd => Init
                /// </remarks> 
                public IHMSSI(PCF8574 leds, PCF8574 BPs, I2CLcd lcd)
                {
                    this.leds = leds;
                    this.BPs = BPs;
                    this.lcd = lcd;
                    d0 = new Led(false, 0, leds);
                    d1 = new Led(false, 1, leds);
                    d2 = new Led(false, 2, leds);
                    d3 = new Led(false, 3, leds);
                    d4 = new Led(false, 4, leds);
                    d5 = new Led(false, 5, leds);
                    d6 = new Led(false, 6, leds);
                    d7 = new Led(false, 7, leds);
                    bpPlus = new BP(BPs, 1);
                    bpFleHaut = new BP(BPs, 2);
                    bpFleBas = new BP(BPs, 4);
                    bpMoins = new BP(BPs, 8);
                    bpSet = new BP(BPs, 16);
                    bpOk = new BP(BPs, 32);
                    bpEnter = new BP(BPs, 64);
                    bpEchap = new BP(BPs, 128);
                    leds.Write(0xff); // Leds Off
                    lcd.Init();
                    lcd.ClearScreen();
                }
                #endregion

                #region Methods
                /// <summary>
                /// All Leds Off
                /// </summary>
                public void LedsOff()
                {
                    leds.Write(0xff);
                }
                /// <summary>
                /// All Leds On
                /// </summary>
                public void LedsOn()
                {
                    leds.Write(0x00);
                }
                /// <summary>
                /// "0" => Led On, "1" => Led Off
                /// </summary>
                /// <param name="value">byte to write</param>
                public void LedsWrite(byte value)
                {
                    leds.Write((byte)~value);
                }

                /// <summary>
                /// Reads and return state of all the IHMSSI board buttons
                /// </summary>
                /// <param name="BPEchap"></param>
                /// <param name="BPEnter"></param>
                /// <param name="BPOk"></param>
                /// <param name="BPSet"></param>
                /// <param name="BPMoins"></param>
                /// <param name="BPFleBas"></param>
                /// <param name="BPFleHaut"></param>
                /// <param name="BPPlus"></param>
                public void BPsRead(out bool BPEchap, out bool BPEnter, out bool BPOk, out bool BPSet, out bool BPMoins, out bool BPFleBas, out bool BPFleHaut, out bool BPPlus)
                {
                    byte EtatBp = BPs.Read();
                    BPEchap = ((EtatBp & 128) == 128);
                    BPEnter = !((EtatBp & 64) == 64);
                    BPOk = ((EtatBp & 32) == 32);
                    BPSet = ((EtatBp & 16) == 16);
                    BPMoins = ((EtatBp & 8) == 8);
                    BPFleBas = ((EtatBp & 4) == 4);
                    BPFleHaut = ((EtatBp & 2) == 2);
                    BPPlus = ((EtatBp & 1) == 1);
                }
                #endregion

            }
        }
    }
}


