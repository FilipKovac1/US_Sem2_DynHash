using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DynHash;
using Model;

namespace GUI
{
    public partial class BlocksView : Form
    {
        public BlocksView(DynHash<Property> p_Id, DynHash<PropertyByCadastral> p_Ca)
        {
            InitializeComponent();

            DataTable tableID = new DataTable();
            DataTable tableCA = new DataTable();

            tableID.Columns.AddRange(this.GetColumns());
            tableCA.Columns.AddRange(this.GetColumns());

            p_Id.GetBlocks((block, address, linked) => tableID.Rows.Add(GetRow(block, address, linked, tableID)));
            p_Ca.GetBlocks((block, address, linked) => tableCA.Rows.Add(GetRow(block, address, linked, tableCA)));

            this.dg_ID.DataSource = tableID;
            this.dg_CA.DataSource = tableCA;

            this.ShowDialog();
        }

        private DataRow GetRow<T>(Block<T> block, int address, bool linked, DataTable table) where T : IRecord<T>
        {
            DataRow dr = table.NewRow();

            dr[0] = address;
            dr[1] = block.Records.Count;
            dr[2] = linked ? "Linked" : "Normal";

            return dr;
        }

        private DataColumn[] GetColumns()
        {
            return new DataColumn[] {
                new DataColumn("Block Adress"),
                new DataColumn("Objects Count", typeof(int)),
                new DataColumn("Block Type"),
            };
        }
    }
}
