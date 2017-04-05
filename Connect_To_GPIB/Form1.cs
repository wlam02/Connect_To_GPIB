using System;
using System.Drawing;
using System.Windows.Forms;
using Ivi.Visa.Interop;

namespace Connect_To_GPIB
{
    public partial class Form1 : Form
    {
        private readonly ResourceManager _rm = new ResourceManager();
        private readonly FormattedIO488 _io488Con = new FormattedIO488();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var resourceString = ("GPIB0::" + GPIB.Text + "::INSTR");
            richTextBox1.Clear();
            richTextBox1.BackColor = Color.White;
            try
            {
                _io488Con.IO = (IMessage)_rm.Open(resourceString, AccessMode.NO_LOCK, 0);
                _io488Con.IO.Clear();
                _io488Con.WriteString(richTextBox2.Text);
                if (checkBox1.CheckState == CheckState.Checked)
                {
                    richTextBox1.Text = _io488Con.ReadString();
                }
                else
                {
                    richTextBox1.BackColor = Color.LimeGreen;
                    richTextBox1.Text = @"Command Execution Completed";
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _io488Con.IO.Close();
            }
        }
    }
}
