using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using IronOcr;
using System.IO.Ports;
using ImageProcessor;
using System.Windows.Threading;
using System.Threading;

namespace R6LifeCounter
{
    class Program
    {
        static Point Checkpoint50 = new Point(134, 1020);
        static Point Checkpoint75 = new Point(175, 1020);
        static Point Checkpoint25 = new Point(93, 1020);
        static SerialPort Port;
        static System.Threading.Timer timer; 
        static Thread thread = new Thread(Timer);

        static void Main(string[] args)
        {
            Port = new SerialPort();
            Port.PortName = "COM4";
            Port.BaudRate = 19200;
            Port.Open();

        LOOP:
            Console.WriteLine("Input Command");
            switch (Console.ReadLine())
            {
                default:
                    goto LOOP;

                case "Check":
                    CheckHP("Test");
                    goto LOOP;

                case "StartTimer":
                    thread.Start();
                    goto LOOP;

                case "Close":
                    break;
            }
        }

        static void CheckHP(object state)
        {
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.CopyFromScreen(new Point(Screen.PrimaryScreen.Bounds.Left, Screen.PrimaryScreen.Bounds.Top), Point.Empty, Screen.PrimaryScreen.Bounds.Size);
            Console.WriteLine(image.GetPixel(Checkpoint75.X, Checkpoint75.Y));

            if (image.GetPixel(Checkpoint75.X, Checkpoint75.Y).Equals(Color.FromArgb(255, 255, 255, 255)))
            {
                Console.WriteLine(">75%");
                SwitchLED(2);
            }
            else if (image.GetPixel(Checkpoint50.X, Checkpoint50.Y).Equals(Color.FromArgb(255, 255, 255, 255)))
            {
                Console.WriteLine(">50%");
                SwitchLED(1);
            }
            else if (image.GetPixel(Checkpoint25.X, Checkpoint25.Y) != Color.FromArgb(255, 255, 255, 255))
            {
                Console.WriteLine(">25%");
                SwitchLED(0);
            }
            else
            {
                SwitchLED(3);
            }
            //image.SetPixel(Checkpoint75.X, Checkpoint75.Y, Color.Green);
            //image.SetPixel(Checkpoint50.X, Checkpoint50.Y, Color.Yellow);
            //image.SetPixel(Checkpoint25.X, Checkpoint25.Y, Color.Red);
            //image.Save(@"C:\Programmieren\Test\Test.png");
        }

        static void SwitchLED(int Index)
        {
            switch (Index)
            {
                case 0:
                    Port.Write("0"); //RED
                    break;

                case 1:
                    Port.Write("1"); //YELLOW
                    break;

                case 2:
                    Port.Write("2"); //GREEN
                    break;

                case 3:
                    Port.Write("3"); //OFF
                    break;
            }
        }

        static void Timer()
        {
            TimerCallback callback = new TimerCallback(CheckHP);
            timer = new System.Threading.Timer(callback, null, 0, 3000);
        }
    }
}
