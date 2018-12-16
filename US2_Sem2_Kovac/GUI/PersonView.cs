using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class PersonView : Form
    {
        public delegate void OnDispose(Person p);
        private OnDispose onDispose;
        private Person person;

        public PersonView(Person p, OnDispose onDispose)
        {
            this.person = p;
            this.onDispose = onDispose;
            InitializeComponent();
            if (p != null)
            {
                tb_ID.Text = p.ID;
                tb_ID.Enabled = false;
                tb_CA.Text = p.Firstname;
                tb_RN.Text = p.Lastname;
            } else
            {
                tb_ID.Text = "";
                tb_ID.Enabled = true;
                tb_CA.Text = "";
                tb_RN.Text = "";
            }
            this.ShowDialog();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (this.person == null)
                this.person = new Person(tb_ID.Text, "", "");
            this.person.Firstname = tb_CA.Text;
            this.person.Lastname = tb_RN.Text;
            onDispose?.Invoke(this.person);
            this.Dispose();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
