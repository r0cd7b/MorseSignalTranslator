using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        public class DoubleBufferPanel : Panel
        {
            public DoubleBufferPanel()
            {
                this.SetStyle(ControlStyles.DoubleBuffer, true);

                this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

                this.SetStyle(ControlStyles.UserPaint, true);

                this.UpdateStyles();

            }
        }
        Bitmap textbox_image = new Bitmap(WindowsFormsApp5.Properties.Resources.textbox1);
        Bitmap textbox2_image = new Bitmap(WindowsFormsApp5.Properties.Resources.textbox2);
        string received_sign;
        bool panel_flag;
        public Form1()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(0, 255, 255, 255);
            panel_flag = false;
            Panel_control_unvisible(panel1);
            label6.Size = new Size(140, 100);
            label3.Size = new Size(190, 40);
            textbox_image = new Bitmap(textbox_image, new Size(140, 100));
            textbox2_image = new Bitmap(textbox2_image, new Size(190, 40));
            label6.Image = textbox_image;
            label3.Image = textbox2_image;
            label6.BackColor = Color.FromArgb(0, 255, 255, 255);
            label3.BackColor = Color.FromArgb(0, 255, 255, 255);
            label5.BackColor = Color.FromArgb(0, 255, 255, 255);
            this.MaximizeBox = false;

        }
        private void Panel_control_unvisible(Panel P)
        {
            foreach (Control ps in P.Controls)
            {
                ps.Visible = false;
            }

        }
        private void Panel_control_visible(Panel P)
        {
            foreach (Control ps in P.Controls)
            {
                ps.Visible = true;
            }

        }
        private void PortWrite(string message)
        {
            serialPort1.Write(message);
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            trans.totalnumber = "";
            MessageBox.Show(trans.Ko_div(textBox3.Text));
            trans.totalnumber = "";
            PortWrite(trans.Ko_div(textBox3.Text));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            // set COM port in properties
            serialPort1.BaudRate = 9600;
            serialPort1.DataBits = 8;
            serialPort1.Parity = Parity.None;
            serialPort1.StopBits = StopBits.One;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1 != null && serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
        }

        private void Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (panel_flag == false)
            {

                int count = 0;
                while (count <= 200)
                {
                    panel_flag = true;
                    count++;
                    panel1.BackColor = Color.FromArgb(count, 255, 255, 255);
                    panel1.Invalidate();
                    panel1.Update();
                    DateTime ThisMoment = DateTime.Now;

                    TimeSpan duration = new TimeSpan(0, 0, 0, 0, 3);

                    DateTime AfterWards = ThisMoment.Add(duration);

                    while (AfterWards >= ThisMoment)

                    {

                        System.Windows.Forms.Application.DoEvents();

                        ThisMoment = DateTime.Now;

                    }
                }
                Panel_control_visible(panel1);
                textBox4.Focus();
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = "COM" + textBox4.Text;
            serialPort1.Open();
            textBox3.Enabled = true;
            button1.Enabled = true;
            textBox4.Enabled = false;
            button3.Enabled = false;
        }

        private void TextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))    //숫자와 백스페이스를 제외한 나머지를 바로 처리
            {
                e.Handled = true;
            }
        }

        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            received_sign = "";
            received_sign = serialPort1.ReadLine();
            received_sign = received_sign.Remove(received_sign.Length - 1);
            if (received_sign != "")
            {
                try
                {
                    string[] S = trans.Ko_mul(received_sign);
                    textBox1.Text = S[0];
                    textBox2.Text = S[1];
                }
                catch(Exception a) { MessageBox.Show(a.Message + "\n" + received_sign); }
            }
        }

    }
}
