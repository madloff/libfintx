﻿using System;
using System.Threading.Tasks;
using System.Windows.Forms;

using HBCI = libfintx.Main;

using libfintx;
using libfintx.Data;

namespace test_Flicker
{
    class Program
    {
        static bool anonymous = false;

        static string receiver = string.Empty;
        static string receiverIBAN = string.Empty;
        static string receiverBIC = string.Empty;
        static decimal amount = 0;
        static string usage = string.Empty;
        public static ConnectionDetails connectionDetails;

        public static PictureBox pictureBox { get; set; }

        [STAThread]
        static void Main(string[] args)
        {
            connectionDetails = new ConnectionDetails()
            {
                Account = "xxx",
                Blz = 76061482,
                BIC = "GENODEF1HSB",
                IBAN = "xxx",
                Url = "https://hbci11.fiducia.de/cgi-bin/hbciservlet",
                HBCIVersion = 300,
                UserId = "xxx",
                Pin = "xxx"
            };

            receiver = "xxxxxx";
            receiverIBAN = "xxxxxx";
            receiverBIC = "xxx";
            amount = 1.0m;
            usage = "TEST";

            HBCI.Assembly("libfintx", "1");

            HBCI.Tracing(true);

            if (HBCI.Synchronization(connectionDetails, anonymous))
            {
                Task oFlicker = new Task(() => openFlickerWindow());
                oFlicker.Start();

                Task oTAN = new Task(() => openTANWindow());
                oTAN.Start();

                Segment.HIRMS = "972";

                System.Threading.Thread.Sleep(5000);

                Console.WriteLine(EncodingHelper.ConvertToUTF8(HBCI.Transfer(connectionDetails, receiver, receiverIBAN, receiverBIC, amount, usage, "972", pictureBox, anonymous)));
            }

            var timer = new System.Threading.Timer(
                e => Output(),
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(10));

            Console.ReadLine();
        }

        static bool openFlickerWindow()
        {
            Application.Run(new Flicker());
            return true;
        }

        static bool openTANWindow()
        {
            Application.Run(new TAN());
            return true;
        }

        static void Output()
        {
            Console.WriteLine(HBCI.Transaction_Output());
        }
    }
}