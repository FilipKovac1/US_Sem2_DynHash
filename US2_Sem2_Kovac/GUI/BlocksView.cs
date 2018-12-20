using System;
using System.Data;
using System.Windows.Forms;
using DynHash;
using Model;

namespace GUI
{
    public partial class BlocksView : Form
    {
        private DynHash<Property> prop;
        private DynHash<PropertyByCadastral> propCA;

        public BlocksView(DynHash<Property> p_Id, DynHash<PropertyByCadastral> p_Ca)
        {
            InitializeComponent();

            this.prop = p_Id;
            this.propCA = p_Ca;

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

        private DataRow GetRowProperty(Property p, DataTable dt)
        {
            DataRow dr = dt.NewRow();

            dr[0] = p.ID;
            dr[1] = p.CadastralArea;
            dr[2] = p.RN;
            dr[3] = p.Description;

            return dr;
        }

        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID"),
                new DataColumn("Cadastral"),
                new DataColumn("RN"),
                new DataColumn("Desc"),
            });

            return dt;
        }

        private void dg_ID_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataTable dt = this.GetDataTable();
                foreach (Property p in this.prop.GetBlockRecords(Int32.Parse(dg_ID.Rows[e.RowIndex].Cells[0].Value.ToString())))
                    dt.Rows.Add(this.GetRowProperty(p, dt));

                this.dg_Rec.DataSource = dt;
            }
        }

        private void dg_CA_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataTable dt = this.GetDataTable();
                foreach (PropertyByCadastral p in this.propCA.GetBlockRecords(Int32.Parse(dg_CA.Rows[e.RowIndex].Cells[0].Value.ToString())))
                    dt.Rows.Add(this.GetRowProperty(p.Property, dt));

                this.dg_Rec.DataSource = dt;
            }
        }
    }
}
