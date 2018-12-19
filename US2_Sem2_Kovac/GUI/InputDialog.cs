using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class InputDialog : Form
    {
        public delegate void OnSubmit(string Value);
        private OnSubmit onSubmit { get; set; }

        public InputDialog(string text, string defValue, OnSubmit onSubmit = null)
        {
            InitializeComponent();
            this.label1.Text = text;
            this.textBox1.Text = defValue;
            this.onSubmit = onSubmit;
            this.ActiveControl = this.textBox1;
            this.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.onSubmit?.Invoke(this.textBox1.Text);
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
