/*
 * Create on 25/8/2016 to Netduino
 * Update : 30:08:2016
 * Author : Philippe Mariano
 */
using System;
using Microsoft.SPOT;

namespace ToolBoxes
{  // Documentation de la classe IHMSSI sur Github https://webge.github.io/IHMSSI/
    public class IHMSSI
    {
// ------------------------------------------------------------------------------
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
// ------------------------------------------------------------------------------
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
// ------------------------------------------------------------------------------
        // IHMSSI
        #region Components
        // BP
        private PCF8574 BPs;
        private BP bpPlus;
        private BP bpMoins;
        private BP bpFleHaut;
        private BP bpFleBas;
        private BP bpOk;
        private BP bpEnter;
        private BP bpEchap;
        private BP bpSet;

        // Leds
        private PCF8574 leds;
        private Led d0;
        private Led d1;
        private Led d2;
        private Led d3;
        private Led d4;
        private Led d5;
        private Led d6;
        private Led d7;

        // Lcd
        private I2CLcd lcd;
        #endregion

        // Properties
        #region Properties
        public Led D0
        {
            get { return d0; }
        }
        public Led D1
        {
            get { return d1; }
        }
        public Led D2
        {
            get { return d2; }
        }
        public Led D3
        {
            get { return d3; }
        }
        public Led D4
        {
            get { return d4; }
        }
        public Led D5
        {
            get { return d5; }
        }
        public Led D6
        {
            get { return d6; }
        }
        public Led D7
        {
            get { return d7; }
        }

        public BP BpPlus
        {
            get
            {
                return bpPlus;
            }

        }
        public BP BpFleHaut
        {
            get
            {
                return bpFleHaut;
            }

        }
        public BP BpMoins
        {
            get
            {
                return bpMoins;
            }

        }
        public BP BpFleBas
        {
            get
            {
                return bpFleBas;
            }
        }
        public BP BpSet
        {
            get
            {
                return bpSet;
            }

        }
        public BP BpOk
        {
            get
            {
                return bpOk;
            }

        }
        public BP BpEnter
        {
            get
            {
                return bpEnter;
            }
        }
        public BP BpEchap
        {
            get
            {
                return bpEchap;
            }

        }

        public I2CLcd Lcd
        {
            get
            {
                return lcd;
            }
        }
        #endregion

        // Constructors
        #region Constructors
        /// <summary>
        /// Constructor : LCD = MIDAS(0x3A); PCF8574 (addBPs_I2C = 0x3f, addLeds_I2C=0x38); Frequence_Bus=100kHz;
        /// Leds => Off; Lcd => Init
        /// </summary>
        public IHMSSI() : this(new PCF8574(0x38, 100), new PCF8574(0x3f, 100), new I2CLcd(I2CLcd.LcdManufacturer.MIDAS, 100)) { }

        /// <summary>
        /// Constructor : PCF8574 (addBPs_I2C = 0x3f, addLeds_I2C=0x38); Frequence_Bus=100kHz;
        /// </summary>
        /// <param name="lcd">MIDAS(0x3A) or BATRON(0x3B), F[100kHz,400kHz]</param>
        public IHMSSI(I2CLcd lcd) : this(new PCF8574(0x38, 100), new PCF8574(0x3f, 100),lcd){}

        /// <summary>
        /// Constructor : LCD = MIDAS(0x3A); PCF8574 (addBPs_I2C = 0x3f, addLeds_I2C=0x38); Frequence_Bus=100kHz;
        /// </summary>
        /// <param name="leds">addLeds_I2C=> PCF8574[0x20, 0x27], PCF8574A[0x38, 0x3F], F[100kHz,400kHz]</param>
        /// <param name="lcd">MIDAS(0x3A) or BATRON(0x3B)</param>
        public IHMSSI(PCF8574 leds, I2CLcd lcd ) : this(leds, new PCF8574(0x3f, 100), lcd) { }      
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="leds">addLeds_I2C=> PCF8574[0x20, 0x27], PCF8574A[0x38, 0x3F], F[100kHz,400kHz]</param>
        /// <param name="BPs">addLeds_I2C=> PCF8574[0x20, 0x27], PCF8574A[0x38, 0x3F], F[100kHz,400kHz]</param>
        /// <param name="lcd">MIDAS(0x3A) or BATRON(0x3B), F[100kHz,400kHz]</param>
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

        // Methods
        #region
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
        /// <param name="value">value to write</param>
        public void LedsWrite(byte value)
        {
            leds.Write((byte)~value);
        }
        
        /// <summary>
        /// Reads all the IHMSSI board buttons
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
            BPMoins  = ((EtatBp & 8) == 8);
            BPFleBas = ((EtatBp & 4) == 4);
            BPFleHaut = ((EtatBp & 2) == 2);
            BPPlus = ((EtatBp & 1) == 1);  
        }
        #endregion
// ------------------------------------------------------------------------------
    }
}
