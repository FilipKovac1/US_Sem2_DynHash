using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;

namespace GUI
{
    public partial class PropertyView : Form
    {
        public delegate void OnDispose(Property p);
        private OnDispose onDispose;
        private Property property;
        public PropertyView(Property p = null, OnDispose onDispose = null)
        {
            InitializeComponent();
            this.onDispose += onDispose;
            this.property = p;
            if (this.property != null)
            {
                tb_ID.Text = this.property.ID.ToString();
                tb_ID.Enabled = false;
                tb_CA.Text = this.property.CadastralArea;
                tb_CA.Enabled = false;
                tb_RN.Text = this.property.RN.ToString();
                tb_RN.Enabled = false;
                tb_Desc.Text = this.property.Description;
            } else
            {
                tb_ID.Text = "0";
                tb_ID.Enabled = true;
                tb_CA.Text = "Cadastral area";
                tb_CA.Enabled = true;
                tb_RN.Text = "0";
                tb_RN.Enabled = true;
                tb_Desc.Text = "Description";
            }
            this.ShowDialog();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (this.property == null)
            {
                this.property = new Property()
                {
                    ID = Int32.Parse(tb_ID.Text),
                    RN = Int32.Parse(tb_RN.Text),
                    CadastralArea = tb_CA.Text,
                };
            }
            this.property.Description = tb_Desc.Text;
            this.onDispose?.Invoke(this.property);
            this.Dispose();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
