using System;
//using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Text.RegularExpressions;

namespace edCalc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public bool CheckInputTextBox(char theCharacter, TextBox theTextBox)
        {
            // Only allow control characters, digits, plus and minus signs.
            // Only allow ONE plus sign.
            // Only allow ONE minus sign.
            // Only allow the plus or minus sign as the FIRST character.
            // Only allow ONE decimal point.
            // Do NOT allow decimal point or digits BEFORE any plus or minus sign.

            if (
                !char.IsControl(theCharacter)
                && !char.IsDigit(theCharacter)
                && (theCharacter != '.')
                && (theCharacter != '-')
                && (theCharacter != '+')
            )
            {
                return false;
            }


            // Only allow one decimal point
            if (theCharacter == '.'
                && theTextBox.Text.IndexOf('.') > -1)
            {
                // Already a decimal point
                return false;
            }

            // Only allow one minus sign
            if (theCharacter == '-'
                && theTextBox.Text.IndexOf('-') > -1)
            {
                // Already a minus sign
                return false;
            }

            // Only allow one plus sign.
            if (theCharacter == '+'
                && theTextBox.Text.IndexOf('+') > -1)
            {
                // Already a plus sign
                return false;
            }

            // Only allow one plus sign OR minus sign, but not both
            if (
                (
                    (theCharacter == '-')
                    || (theCharacter == '+')
                )
                &&
                (
                    (theTextBox.Text.IndexOf('-') > -1)
                    ||
                    (theTextBox.Text.IndexOf('+') > -1)
                )
                )
            {
                // Trying to enter a plus or minus sign and there exists a plus or minus sign
                return false;
            }

            // Only allow a minus or plus sign at the first character positio
            if (
                (
                    (theCharacter == '-')
                    || (theCharacter == '+')
                )
                && theTextBox.SelectionStart != 0
                )
            {
                // Trying to enter a minus or plus sign at position other than the first character 
                return false;
            }

            // Only allow digits and decimal point AFTER any existing plus or minus sign
            if (
                    (
                        // Is digit or decimal point
                        char.IsDigit(theCharacter)
                        ||
                        (theCharacter == '.')
                    )
                    &&
                    (
                        // A plus or minus sign EXISTS
                        (theTextBox.Text.IndexOf('-') > -1)
                        ||
                        (theTextBox.Text.IndexOf('+') > -1)
                    )
                    &&
                        // Attempting to put the character at the beginning of the field
                        theTextBox.SelectionStart == 0
                )
            {
                // Trying to enter a digit or decimal point in front of a minus or plus sign
                return false;
            }

            return true;
        }


        public void Number_only(object sender, KeyPressEventArgs e)
        {
            try
            {

                TextBox textbox = (TextBox)sender;
                Console.WriteLine(textbox.Text);
                if (CheckInputTextBox(e.KeyChar, textbox) == true)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {   

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {   // Only numbers, letters, backspace and space.
            e.Handled = e.KeyChar != (char)Keys.Back && !char.IsSeparator(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void ClearTextBoxes(Control.ControlCollection ctrlCollection)
        {
            foreach (Control ctrl in ctrlCollection)
            {
                if (ctrl is TextBoxBase)
                {
                    ctrl.Text = String.Empty;
                }
                else
                {
                    ClearTextBoxes(ctrl.Controls);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(this.Controls);  //---> only in first groupbox
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string specifier;
            if (!String.IsNullOrEmpty(textBox1.Text))
            {                
                double outnum = Double.Parse(textBox1.Text);
                //dBm to Watt
                outnum = Math.Pow(10, ((outnum - 30.0) / 10.0));
                outnum = Math.Round(outnum, 6);
                // Console.WriteLine(outnum.ToString());
                specifier = "G";
                textBox2.Text = outnum.ToString(specifier, CultureInfo.InvariantCulture);
            }

            if (!String.IsNullOrEmpty(textBox3.Text))
            {
                double outnum = Double.Parse(textBox3.Text);
                //Watt to dBm
                outnum = 10 * Math.Log10(outnum / 1.0) + 30.0;
                outnum = Math.Round(outnum, 6);
                // Console.WriteLine(outnum.ToString());
                specifier = "G";
                textBox4.Text = outnum.ToString(specifier, CultureInfo.InvariantCulture);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void TH_Click(object sender, EventArgs e)
        {

        }

        private bool CheckInputAllFields(object sender, EventArgs e)
        {
            if (((radioButton1.Checked == true) || (radioButton2.Checked == true)) 
                && ((radioButton3.Checked == true) || (radioButton4.Checked == true))
                && (listBox1.SelectedItems.Count > 0)
                && (listBox2.SelectedItems.Count > 0)
                && (listBox3.SelectedItems.Count > 0)
                && (string.IsNullOrWhiteSpace(textBox5.Text) == false)
                && (string.IsNullOrWhiteSpace(textBox6.Text) == false)
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Function to calculate throughput according to 3GPP TS 38.306.
        private double CalcThroughput(int carrierNo, int layersNo, int mod_order, double RbNo, double overhead, double Tsymbol, int mode)
        {
            double sumrate = 0, format = 0;
            for (int i = 0; i < carrierNo; i++)
            {
                sumrate += (layersNo * mod_order * 1 * 0.92578125 * (RbNo * 12 / Tsymbol) * (1 - overhead));
            }
            if (mode == 1)
            {   //https://stackoverflow.com/questions/3575331/how-do-extract-decimal-number-from-string-in-c-sharp/3575807
                if (listBox3.SelectedItems.Count > 0) 
                    format = Double.Parse(listBox3.SelectedItem.ToString());
                sumrate *= format;
            }
            return Math.Round(sumrate / 1000000, 2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int direction = 0, mode = 0, mod_order = 0, carrierNo = 0, layersNo = 0;
            double Tsymbol = 0, mu = 0, RbNo = 0, overhead = 0, Throughput = 0;
            string specifier;
            // FR1 15, 30, 60 kHz: for 10, 20, 40, 100 MHz only, FR2 60, 120 kHz: for 50, 100, 200, 400 MHz only
            double[] RBsFR = new double[] { 52, 106, 216, 24, 51, 106, 273, 11, 24, 51, 135, 66, 132, 264, 32, 66, 132, 264}; 

            //Check if options are selected and textboxes 5-12 have numbers.
            bool input_ok = CheckInputAllFields(sender, e);

            if (input_ok)
            {
                //Get selected direction and mode: if first option, direction/mode=1, else default is 0 for second options.
                if ((radioButton1.Checked == true) && (radioButton1.Text.Equals("Downlink")))
                    direction = 1;
                if ((radioButton4.Checked == true) && (radioButton2.Checked == true) && (radioButton2.Text.Equals("Uplink")))
                { 
                    // mode=0 for FDD (default), 1 for TDD
                    mode = 1; // FDD and TDD make sense only in UL: if TDD in UL, only x% of slots used (38.213: Table 11.1.1-1: Slot formats)
                    label5.Visible = true;
                    listBox3.Visible = true;
                }
                //Get index of selected item FR1-subcarrier spacing-bandwidth
                if (listBox1.SelectedItems.Count>0)
                {
                    RbNo = RBsFR[listBox1.SelectedIndex];
                }
                else
                {
                    MessageBox.Show("Select FR, BW and Df.");
                }

                if (listBox1.SelectedIndex <= 10)
                {
                    if (direction == 1) overhead = 0.14;
                    else overhead = 0.08;
                }
                else
                {
                    if (direction == 1) overhead = 0.18;
                    else overhead = 0.1;
                }
                specifier = "G";
                textBox11.Text = overhead.ToString(specifier, CultureInfo.InvariantCulture);

                //Print number of RBs
                textBox13.Text = RbNo.ToString(specifier, CultureInfo.InvariantCulture);
                //MessageBox.Show(listBox1.SelectedIndex.ToString());
                //MessageBox.Show(RbNo.ToString());

                //Get modulation order
                if (listBox2.SelectedItems.Count > 0)
                {
                    mod_order = Int32.Parse(listBox2.SelectedItem.ToString());
                }
                else
                {
                    MessageBox.Show("Select mod. order.");
                }

                //Get number of component carriers
                carrierNo = Int32.Parse(textBox5.Text);
                if (1 > carrierNo || carrierNo > 16)
                {
                    MessageBox.Show("Insert No. of carriers in [1,16].");
                    textBox5.Clear();
                }

                //Get number of MIMO layers
                layersNo = Int32.Parse(textBox6.Text);
                if ((direction ==  1 ) && (layersNo > 8))
                {
                    MessageBox.Show("Insert No. of layers in [1,8].");
                }
                else if ((direction == 0) && (layersNo > 4))
                {
                    MessageBox.Show("Insert No. of layers in [1,4].");
                }
                else
                {
                    //MessageBox.Show("Check MIMO layer number.");
                }

                //Get configuration value \mu according to subcarrier spacing.
                if ((listBox1.SelectedIndex>=0) && (listBox1.SelectedIndex <= 2)){
                    mu = 0; //15 kHz
                }
                else if ((listBox1.SelectedIndex >= 3) && (listBox1.SelectedIndex <= 6))
                {
                    mu = 1; //30 kHz
                }
                else if ((listBox1.SelectedIndex >= 7) && (listBox1.SelectedIndex <= 13))
                {
                    mu = 2; //60 kHz
                }
                else
                {
                    mu = 3; //120 kHz
                }

                //Print symbol duration for current \mu
                specifier = "G";
                Tsymbol = 1 / (1000 * 14 * Math.Pow(2, mu));
                textBox12.Text = Tsymbol.ToString(specifier, CultureInfo.InvariantCulture);
                Throughput = CalcThroughput(carrierNo, layersNo, mod_order, RbNo, overhead, Tsymbol, mode);
                textBox14.Clear();
                //Console.WriteLine(Throughput.ToString());
                textBox14.Text = Throughput.ToString(specifier, CultureInfo.InvariantCulture);
            }
            else
            {
                MessageBox.Show("Fill in all parameters.");
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            label5.Visible = false;
            listBox3.Visible = false;
            listBox1.SetSelected(0, true);
            listBox2.SetSelected(0, true);
            radioButton1.Checked = true;
            radioButton3.Checked = true;
            textBox5.Clear();
            textBox6.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}
