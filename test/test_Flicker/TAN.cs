﻿using System;
using System.Windows.Forms;

using HBCI = libfintx;

namespace test_Flicker
{
    public partial class TAN : Form
    {
        public TAN()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(HBCI.Transaction.TAN(Program.connectionDetails, textBox1.Text));
        }
    }
}