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
    public partial class Registration : Form
    {
        public Registration()
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
            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty)
                MessageBox.Show("Login or password are empty. Try again");
            else
            {
                List<string> loginscheck = new List<string>();
                StreamReader readlogins = File.OpenText(@"C:\Users\User\source\repos\IT\LW2\LW2\logins.txt");
                while (!readlogins.EndOfStream)
                {
                    loginscheck.Add(readlogins.ReadLine());
                }
                readlogins.Close();
                if (loginscheck.Contains(textBox1.Text))
                {
                    MessageBox.Show("The user with this username already exists");
                    return;
                }
                StreamWriter toLogins = File.AppendText(@"C:\Users\User\source\repos\IT\LW2\LW2\logins.txt");               
                logins.Add(textBox1.Text);
                passwords.Add(textBox2.Text);
                toLogins.WriteLine(textBox1.Text);
                toLogins.Close();
                StreamWriter toPasswords = File.AppendText(@"C:\Users\User\source\repos\IT\LW2\LW2\passwords.txt");
                toPasswords.WriteLine(textBox2.Text);
                toPasswords.Close();
                StreamWriter writer = File.CreateText(@"C:\Users\User\source\repos\IT\LW2\LW2\Users\" + textBox1.Text + ".txt");
                DialogResult = DialogResult.OK;
                //Form2 form2 = new Form2();
                //form2.ShowDialog();
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }
    }
}
