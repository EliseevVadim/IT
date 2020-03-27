using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LW2
{
    public static class Test
    {
        public delegate void test(Event s);
        public static test callbackEventHandler;
    }
    public static class Finder
    {
        public delegate void timeToFind(string s);
        public static timeToFind finder;
    }
    public static class EditEv
    {
        public delegate void Editor(Event a);
        public static Editor editor;
    }
    public static class NumDeleg
    {
        public delegate void ND(int n);
        public static ND nD;
    }
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form = new Form1();
            Form2 form2 = new Form2();
            Registration registration = new Registration();
            OrganaizerForm organaizerForm = new OrganaizerForm();
            DialogResult result = Autorization(DialogResult.OK, DialogResult.Yes);
            if (result == DialogResult.Cancel)
            {
                form2.Close();
                form.Close();
            }
            else if (result == DialogResult.OK)
            {
                form2.Close();
            }
            else
            {
                DialogResult result1 = RegistrationCanc(DialogResult.Abort);
                if (result1 == DialogResult.OK)
                {
                    Autorization(DialogResult.Cancel, DialogResult.Yes);
                }
                else if (result1 == DialogResult.Abort)
                {
                    Autorization(DialogResult.No, DialogResult.Yes);
                }
            }            
        }
        static DialogResult Autorization (DialogResult result1, DialogResult result2)
        {
            Form1 form = new Form1();
            Form2 form2 = new Form2();
            DialogResult result3 = form.ShowDialog();
            if (result3 == DialogResult.Yes)
            {
                return DialogResult.Yes;
            }
            else if (result3 == DialogResult.OK) {
                DialogResult result4 = form2.ShowDialog();
                if (result4 == DialogResult.OK)
                {
                    return DialogResult.OK;
                }

                else if (result4 == DialogResult.Abort)
                {
                    return Autorization(result1, result2);
                }
                else
                {
                    return DialogResult.Cancel;
                }
            }
            else
            {
                return DialogResult.Cancel;
            }
        }
        static DialogResult RegistrationCanc(DialogResult result)
        {
            Form1 form = new Form1();
            Registration registration = new Registration();
            DialogResult result1 = registration.ShowDialog();
            if (result1 == DialogResult.OK)
            {
                return DialogResult.OK;
            }
            else if (result1 == DialogResult.Cancel)
            {
                return DialogResult.Cancel;
            }
            else if (result1 == DialogResult.Abort) 
            {
                if (form.DialogResult == DialogResult.Yes)
                {
                    return RegistrationCanc(DialogResult.No);
                }
                else
                {
                    return Autorization(DialogResult.Cancel, DialogResult.Yes);
                }
            }
            else
            {
                return RegistrationCanc(result);
            }
        }
    }
}
