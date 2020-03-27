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
    public partial class EditEventForm : Form
    {
        int ind;
        string text;
        Event event1;
        public EditEventForm()
        {
            InitializeComponent();
        }
        public EditEventForm(string s, Event @event)
        {
            InitializeComponent();
            NumDeleg.nD = new NumDeleg.ND(this.Num);
            text = s;
            event1 = @event;
        }
        void Num (int N)
        {
            ind = N;
        }
        private void EditEventForm_Load(object sender, EventArgs e)
        {

            textBox1.Text = event1.text;
            comboBox1.Text = event1.type.ToString();
            domainUpDown1.Text = event1.time;
            List<string> vals = InitValues();
            domainUpDown1.Items.AddRange(vals);
            comboBox1.Items.Add("Meeting");
            comboBox1.Items.Add("Memo");
            comboBox1.Items.Add("Task");
        }
        public List<string> InitValues()
        {
            List<string> values = new List<string>();
            for (int i = 0; i < 24; i++)
            {
                for (int j = 0; j < 60; j++)
                {
                    if (i < 10 && j < 10)
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

        private void button1_Click(object sender, EventArgs e)
        {
            int num = 0;
            string Text = textBox1.Text;
            int currentnum = 0;
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
            else if (Date == string.Empty || Text == string.Empty || comboBox1.Text == string.Empty)
            {
                MessageBox.Show("Проверьте ввод");
            }
            else
            {
                MessageBox.Show("Correct!");
                List<string> fromfile = new List<string>();
                StreamReader reader = File.OpenText(@"C:\Users\User\source\repos\LW2\LW2\Users\" + text);
                while (!reader.EndOfStream)
                {
                    fromfile.Add(reader.ReadLine());
                }                
                reader.Close();
                File.Delete(@"C:\Users\User\source\repos\LW2\LW2\Users\" + text);
                fromfile[ind] = $"{Date}/ {Text}/ {GetType(comboBox1.Text)}/ {Time}/";
                StreamWriter writer = File.AppendText(@"C:\Users\User\source\repos\LW2\LW2\Users\" + text);
                for (int i = 0; i < fromfile.Count; i++)
                {
                    writer.WriteLine(fromfile[i]);
                }
                writer.Close();
                EditEv.editor(new Event() { date = Date, text = Text, type = GetType(comboBox1.Text), time = Time });
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
    }
}
