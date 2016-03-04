using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.System.Threading;

namespace o433Mhz
{
    public class o433MhzSendException : Exception
    {
        public o433MhzSendException()
        {

        }
        public o433MhzSendException(string msg) : base(msg)
        {

        }
    }

    public static class Send
    {
        private const byte WTRUE = 136;
        private const byte WFALSE = 142;
        private const double PULSELENGTH = 300;
        private const int REPEAT = 10;

        static long PULSE_DELAY_IN_TICKS = (long)(TimeSpan.TicksPerMillisecond * (PULSELENGTH / 1000));
        static TimeSpan PULSE_DELAY = TimeSpan.FromTicks(PULSE_DELAY_IN_TICKS);
        static void printCode(byte[] code)
        {
            string scode = "";
            for (int i = 0; i < code.Length; i++)
            {
                scode += code[i].ToString();
            }

            Log.i("Sending {0} via Ether", scode);
        }

        private static void NOP(long durationTicks)
        {
            var sw = Stopwatch.StartNew();
            while (sw.ElapsedTicks < durationTicks)
            {
                // unfortunatly thats the best way i know to block such precisley.. :/
            }
        }

        static void sendEther(int gpio, byte[] code)
        {
            var controller = GpioController.GetDefault();

            if (controller == null) throw new o433MhzSendException("GpioController is null. Can't init controller.");

            using (var pin = controller.OpenPin(gpio, GpioSharingMode.Exclusive))
            {
                pin.Write(GpioPinValue.High);
                pin.SetDriveMode(GpioPinDriveMode.Output);

                int x = 0;
                for (int r = 0; r < REPEAT; r++)
                {
                    for (int c = 0; c < 16; c++)
                    {
                        x = 128;
                        for (int i = 1; i < 9; i++)
                        {
                            if ((code[c] & x) > 0)
                                pin.Write(GpioPinValue.High);
                            else
                                pin.Write(GpioPinValue.Low);

                            NOP(PULSE_DELAY_IN_TICKS);
                            x = x >> 1;
                        }
                    }
                }
                
            }
        }
        

        // TODO: Go async ...
        public static void Send433Mhz(int gpio, string codeStr, int active)
        {
            // Validate code
            if (string.IsNullOrEmpty(codeStr) || codeStr.Length != 6)
            {
                throw new o433MhzSendException("The code must be 6 characters long");
            }

            for (int i = 0; i < 5; i++)
            {
                if (codeStr[i] != '1' && codeStr[i] != '0')
                {
                    throw new o433MhzSendException("The code must be in this format: 10101A\n");
                }
            }

            if (!(codeStr[5] >= (int)'A' && codeStr[5] <= (int)'E'))
            {
                throw new o433MhzSendException("The code must be in this format: 10101B\n");
            }

            byte[] code = new byte[] { 142, 142, 142, 142, 142, 142, 142, 142, 142, 142, 142, 142, 128, 0, 0, 0 };

            // Parse device-code
            for (int i = 0; i < 5; i++)
            {
                if (codeStr[i] == '1') code[i] = WTRUE;
                else code[i] = WFALSE;
            }

            // Parse device-id (A - E)
            int id = (int)Math.Pow(2, (int)codeStr[5] - 65);

            // Set device-id
            int x = 1;
            for (int i = 1; i < 6; i++)
            {
                if ((id & x) > 0) code[4 + i] = WTRUE;
                else code[4 + i] = WFALSE;
                x = x << 1;
            }

            // Set Status
            if (active == 1)
            {
                code[10] = WTRUE;
                code[11] = WFALSE;
            }
            else {
                code[10] = WFALSE;
                code[11] = WTRUE;
            }

            sendEther(gpio, code);

            printCode(code);
        }

    }
}
