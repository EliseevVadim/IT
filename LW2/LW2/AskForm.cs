using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LW2
{
    public partial class AskForm : Form
    {
        Event @event;
        public AskForm()
        {
            InitializeComponent();
        }
        public AskForm(Event e)
        {
            InitializeComponent();
            @event = e;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void AskForm_Load(object sender, EventArgs e)
        {
            label2.Text = @event.date +" "+ @event.time+" " +"<<"+ @event.text+">>";
        }
    }
}
