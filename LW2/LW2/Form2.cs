using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LW2
{
    public partial class Form2 : Form
    {
        string text;
        public Form2()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = (char)0;
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> logins = new List<string>();
            List<string> passwords = new List<string>();
            StreamReader readlogins = File.OpenText(@"C:\Users\User\source\repos\LW2\LW2\logins.txt");
            while (!readlogins.EndOfStream)
            {
                logins.Add(readlogins.ReadLine());
            }
            readlogins.Close();
            StreamReader readpassords = File.OpenText(@"C:\Users\User\source\repos\LW2\LW2\passwords.txt");
            while (!readpassords.EndOfStream)
            {
                passwords.Add(readpassords.ReadLine());
            }
            readpassords.Close();
            try
            {
                if (textBox1.Text == string.Empty || textBox2.Text == string.Empty)
                {
                    MessageBox.Show("Login or password are empty");
                }
                else
                {
                    int userindex = logins.IndexOf(textBox1.Text);
                    if (textBox2.Text == passwords[userindex])
                    {
                        DialogResult = DialogResult.OK;
                        text = textBox1.Text;
                        Hide();
                        OrganaizerForm form = new OrganaizerForm(this.text);
                        form.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Wrong login or password!");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Wrong login or password!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }
    }
}
