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
    public partial class AddEventForm : Form
    {
        string text;
        public AddEventForm(string s)
        {
            InitializeComponent();
            text = s;
        }
        public AddEventForm()
        {
            InitializeComponent();
        }
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            string s = monthCalendar1.SelectionRange.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int num=0;
            string Text = textBox1.Text;
            int currentnum=0;
            string Time = domainUpDown1.Text;
            string Date = monthCalendar1.SelectionStart.ToString().Remove(10);
            string[] mas = new string[3];
            mas = Date.Split('.');
            string[] currentmas = DateTime.Now.ToString().Remove(10).Split('.');
            for (int i = 0, m = 1; i < mas.Length; i++, m *= 60)
            {
                num += int.Parse(mas[i]) * m;
                currentnum += int.Parse(currentmas[i]) * m;
            }
            if (num < currentnum)
            {
                MessageBox.Show("Этот день уже прошел!");
            }            
            else if (Date == string.Empty|| Text == string.Empty||comboBox1.Text==string.Empty)
            {
                MessageBox.Show("Проверьте ввод");
            }
            else
            {
                MessageBox.Show("Correct!");
                
                StreamWriter writer = File.AppendText(@"C:\Users\User\source\repos\LW2\LW2\Users\"+text);                
                writer.WriteLine($"{Date}/ {Text}/ {GetType(comboBox1.Text)}/ {Time}/");
                writer.Close();
                Test.callbackEventHandler(new Event() { date = Date, text = Text, type = GetType(comboBox1.Text), time=Time });
                Close();
            }
        }        
        public Type GetType(string s)
        {
            switch (comboBox1.Text)
            {
                case "Meeting":
                    return Type.Meeting;
                case "Memo":
                    return Type.Memo;
                case "Task":
                    return Type.Task;
                default:
                    MessageBox.Show("Проверьте ввод");
                    return Type.Error;
            }
        }
        private void AddEventForm_Load(object sender, EventArgs e)
        {
            List<string> vals = InitValues();
            domainUpDown1.Items.AddRange(vals);
            comboBox1.Items.Add("Meeting");
            comboBox1.Items.Add("Memo");
            comboBox1.Items.Add("Task");
        }
        public List<string> InitValues()
        {
            List<string> values = new List<string>();
            for(int i=0; i<24; i++)
            {
                for (int j=0; j<60; j++)
                {
                    if (i < 10&&j<10)
                    {
                        values.Add($"0{i}:0{j}");
                    }
                    else if (i < 10 && j >= 10)
                    {
                        values.Add($"0{i}:{j}");
                    }
                    else if (i >= 10 && j < 10)
                    {
                        values.Add($"{i}:0{j}");
                    }
                    else
                    {
                        values.Add($"{i}:{j}");
                    }
                }
            }
            return values;
        }
    }
}
