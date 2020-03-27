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
    public enum Type
    {
        Meeting,
        Memo,
        Task,
        Error
    }    
    public struct Event
    {
        public string time;
        public string text;
        public string date;
        public Type type;
    }
    public partial class OrganaizerForm : Form
    {
        List<Event> events = new List<Event>();
        string text;
        public OrganaizerForm(string s)
        {
            InitializeComponent();
            listView1.SmallImageList = imageList1;
            Test.callbackEventHandler = new Test.test(this.Reload);
            Finder.finder = new Finder.timeToFind(this.TimeCather);
            EditEv.editor = new EditEv.Editor(this.CatchEdit);
            text = s + ".txt";
        }
        void AddImage(string s, ListViewItem item)
        {
            switch (s)
            {
                case "Meeting":
                    item.ImageIndex = 0;
                    break;
                case "Memo":
                    item.ImageIndex = 1;
                    break;
                case "Task":
                    item.ImageIndex = 2;
                    break;
            }
        }
        void CatchEdit(Event e)
        { 
            events[listView1.FocusedItem.Index] = new Event() { date = e.date, text = e.text, time = e.time, type = e.type };
            listView1.Items.Clear();
            foreach (Event evs in events)
            {
                ListViewItem item = new ListViewItem(evs.date);
                item.SubItems.Add(evs.time);
                item.SubItems.Add(evs.text);
                listView1.Items.Add(item);
                AddImage(evs.type.ToString(), item);
            }
        }
        public OrganaizerForm()
        {
                   
            InitializeComponent();
        }
        void TimeCather(string s)
        {
            listView1.Items.Clear();
            for (int i=0; i<events.Count; i++)
            {
                if (TimeToNum(events[i].time)==TimeToNum(s))
                {
                    ListViewItem item = new ListViewItem(events[i].date);
                    item.SubItems.Add(events[i].time);
                    item.SubItems.Add(events[i].text);
                    listView1.Items.Add(item);
                    AddImage(events[i].type.ToString(), item);
                }
            }
        }
        public int TimeToNum(string s)
        {
            string[] mas = s.Split(':');
            int res = 0;
            for(int i=0, m=1; i<mas.Length; i++, m*=60)
            {
                res += int.Parse(mas[i]) * m;
            }
            return res;
        }
        public int DateToNum(string s)
        {
            string[] arr = s.Split('.');
            int num = 0;
            for (int i = 0, m = 1; i < arr.Length; i++, m *= 60)
            {
                num += int.Parse(arr[i]) * m;
            }
            return num;
        }

        void Reload(Event A)
        {
            events.Add(new Event() { date = A.date, text = A.text, type = A.type, time=A.time });
            ListViewItem item = new ListViewItem(A.date);
            item.SubItems.Add(A.time);
            item.SubItems.Add(A.text);
            listView1.Items.Add(item);
            AddImage(A.type.ToString(), item);
        } 
      
        private void OrganaizerForm_Load(object sender, EventArgs e)
        {
            
            AddEventForm addEventForm = new AddEventForm();
            comboBox1.Items.Add("Meeting");
            comboBox1.Items.Add("Memo");
            comboBox1.Items.Add("Task");                        
        }
        private void button3_Click(object sender, EventArgs e)
        {
            AddEventForm add = new AddEventForm(this.text);
            add.ShowDialog();
            
        }
        private void OrganaizerForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control&&e.KeyCode == Keys.O)
            {
                listView1.Items.Clear();
                events.Clear();
                StreamReader reader = File.OpenText(@"C:\Users\User\source\repos\LW2\LW2\Users\" + text);
                while (!reader.EndOfStream)
                {
                    string[] arr = reader.ReadLine().Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    ListViewItem item = new ListViewItem(arr[0]);
                    item.SubItems.Add(arr[3]);
                    item.SubItems.Add(arr[1]);
                    listView1.Items.Add(item);
                    events.Add(new Event() { date = arr[0], text = arr[1], type = (Type)Enum.Parse(typeof(Type), arr[2]), time=arr[3] });
                }
                reader.Close();
                listView1.Items.Clear();
                for (int i=0; i<events.Count; i++)
                {
                    ListViewItem item = new ListViewItem(events[i].date);
                    item.SubItems.Add(events[i].time);
                    item.SubItems.Add(events[i].text);
                    listView1.Items.Add(item);
                    AddImage(events[i].type.ToString(), item);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> pairs = new Dictionary<string, int>();
            for (int i=0; i<events.Count; i++)
            {
                int counter = 0;
                string temp = events[i].date;
                for (int j=0; j<events.Count; j++)
                {
                    if (DateToNum(temp) == DateToNum(events[j].date))
                    {
                        counter++;
                    }
                }
                try
                {
                    pairs.Add(events[i].date, counter);
                }
                catch
                {

                }
            }
            pairs = pairs.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
            List<Event> sorted = new List<Event>();
            foreach(var pair in pairs)
            {
                for (int i=0; i<events.Count; i++)
                {
                    if (DateToNum(pair.Key) == DateToNum(events[i].date))
                    {
                        sorted.Add(events[i]);
                    }
                }
            }
            listView1.Items.Clear();
            sorted.Reverse();
            for (int i=0; i<sorted.Count; i++)
            {
                ListViewItem item = new ListViewItem(sorted[i].date);
                item.SubItems.Add(sorted[i].time);
                item.SubItems.Add(sorted[i].text);
                listView1.Items.Add(item);
                AddImage(events[i].type.ToString(), item);
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked && comboBox1.Text != string.Empty)
                {
                    listView1.Items.Clear();
                    for (int i = 0; i < events.Count; i++)
                    {
                        if (events[i].type.ToString() == comboBox1.Text)
                        {
                            ListViewItem item = new ListViewItem(events[i].date);
                            item.SubItems.Add(events[i].time);
                            item.SubItems.Add(events[i].text);
                            listView1.Items.Add(item);
                            AddImage(events[i].type.ToString(), item);
                        }
                    }
                }
                else if (radioButton2.Checked)
                {
                    listView1.Items.Clear();
                    for(int i = 0; i < events.Count; i++)
                    {

                        ListViewItem item = new ListViewItem(events[i].date);                        
                        item.SubItems.Add(events[i].time);
                        item.SubItems.Add(events[i].text);
                        listView1.Items.Add(item);
                        AddImage(events[i].type.ToString(), item);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Проверьте ввод");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            FindForm form = new FindForm();
            form.Show();
        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            contextMenuStrip1.Show();
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EditEventForm form = new EditEventForm(this.text, events[listView1.FocusedItem.Index]);
                NumDeleg.nD(listView1.FocusedItem.Index);
                form.Show();
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AskForm form = new AskForm(events[listView1.FocusedItem.Index]);
                if (form.ShowDialog() == DialogResult.Yes)
                {
                    List<string> fromfile = new List<string>();
                    StreamReader reader = File.OpenText(@"C:\Users\User\source\repos\LW2\LW2\Users\" + text);
                    while (!reader.EndOfStream)
                    {
                        fromfile.Add(reader.ReadLine());
                    }
                    reader.Close();
                    File.Delete(@"C:\Users\User\source\repos\LW2\LW2\Users\" + text);
                    fromfile.Remove(fromfile[listView1.FocusedItem.Index]);
                    StreamWriter writer = File.AppendText(@"C:\Users\User\source\repos\LW2\LW2\Users\" + text);
                    for (int i = 0; i < fromfile.Count; i++)
                    {
                        writer.WriteLine(fromfile[i]);
                    }
                    writer.Close();
                    events.Remove(events[listView1.FocusedItem.Index]);
                    listView1.Items.Clear();
                    for (int i = 0; i < events.Count; i++)
                    {
                        ListViewItem item = new ListViewItem(events[i].date);
                        item.SubItems.Add(events[i].time);
                        item.SubItems.Add(events[i].text);
                        listView1.Items.Add(item);
                        AddImage(events[i].type.ToString(), item);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }        
    }
}
