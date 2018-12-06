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
using DynHash;

namespace GUI
{
    public partial class MainForm : Form
    {
        private DynHash<PropertyByID> PropertiesByID { get; set; }
        private DynHash<PropertyByCadastral> PropertiesByCadastral { get; set; }

        private BindingSource BindSourceById = new BindingSource();
        private BindingSource BindSourceByCA = new BindingSource();

        private string FilePath { get; set; }

        public MainForm()
        {
            bool init = false;
            new InitView((maxDepth, blockSize, filePath) => {
                this.PropertiesByCadastral = new DynHash<PropertyByCadastral>(maxDepth, blockSize, filePath + "/byId.bin");
                this.PropertiesByID = new DynHash<PropertyByID>(maxDepth, blockSize, filePath + "/byCa.bin");
                this.FilePath = filePath;
                init = true;
            });
            if (init) {
                InitializeComponent();
                this.dg_ID.DataSource = BindSourceById;
                this.dg_CA.DataSource = BindSourceByCA;
                this.FormClosing += (sender, e) => { this.Save(); }; // save actual work to the file
                this.cb_SearchBy.SelectedIndex = 0;
            } else
                Environment.Exit(1);
        }

        private void btn_Create_Click(object sender, EventArgs e)
        {

        }

        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.PropertiesByCadastral.Destruct();
            this.PropertiesByID.Destruct();

            Random ids = new Random();
            int ca = 100;
            int prop = 100;
            int id = 0;
            Property property;
            for (int i = 1; i <= ca; i++)
            {
                for (int j = 1; j <= prop; j++)
                {
                    id = ids.Next(ca * prop);
                    property = new Property()
                    {
                        ID = id,
                        RN = j,
                        CadastralArea = "CA " + i,
                        Description = "Nejaky text " + i + " " + j
                    };
                    if (PropertiesByID.Add(new PropertyByID(property)))
                        PropertiesByCadastral.Add(new PropertyByCadastral(property));
                }
            }

            this.ReloadGrids();
        }

        private void Save()
        {
            this.PropertiesByCadastral.Save(this.FilePath + "/exportCA.txt");
            this.PropertiesByID.Save(this.FilePath + "/exportID.txt");
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Save();
            MessageBox.Show("Data were saved");
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.PropertiesByID.Load(this.FilePath + "/exportID.txt", new PropertyByID(new Property()));
            this.PropertiesByCadastral.Load(this.FilePath + "/exportCA.txt", new PropertyByCadastral(new Property()));

            MessageBox.Show("Data has been load");
            this.ReloadGrids();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.PropertiesByCadastral.Destruct();
            this.PropertiesByID.Destruct();
            this.Save();
            this.ReloadGrids();
        }

        private void ReloadGrids()
        {
            DataTable tableCA = new DataTable();
            DataTable tableID = new DataTable();

            DataColumn[] cols = new DataColumn[]
            {
                new DataColumn("ID", typeof(int)),
                new DataColumn("Cadastral Area", typeof(string)),
                new DataColumn("RN", typeof(int)),
                new DataColumn("Description", typeof(string)),
            };

            tableCA.Columns.AddRange(cols);
            tableID.Columns.AddRange(this.CloneColumns(cols));

            // Add rows.
            this.PropertiesByID.GetAll(new PropertyByID(new Property()), (p) => tableID.Rows.Add(this.CreateRow(p.Property, tableID)));
            this.PropertiesByCadastral.GetAll(new PropertyByCadastral(new Property()), (p) => tableCA.Rows.Add(this.CreateRow(p.Property, tableCA)));

            BindSourceByCA.DataSource = tableCA;
            BindSourceById.DataSource = tableID;
        }

        private DataColumn[] CloneColumns(DataColumn[] from)
        {
            DataColumn[] ret = new DataColumn[from.Length];
            for (int i = 0; i < from.Length; i++)
                ret[i] = new DataColumn(from[i].ColumnName, from[i].DataType);
            return ret;
        }

        private DataRow CreateRow(Property p, DataTable table)
        {
            DataRow ret = table.NewRow();
            ret[0] = p.ID;
            ret[1] = p.CadastralArea;
            ret[2] = p.RN;
            ret[3] = p.Description == null ? "" : p.Description;
            return ret;
        }

        private void dg_ID_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                PropertyByID p = this.PropertiesByID.Find(new PropertyByID(new Property(Int32.Parse(dg_ID.Rows[e.RowIndex].Cells[0].Value.ToString()))));
                if (p != null)
                    new PropertyView(p.Property, (property) => {
                        this.PropertiesByID.Update(p);
                        this.PropertiesByCadastral.Update(new PropertyByCadastral(p.Property));
                    });
                else
                    MessageBox.Show("Something weird happened ;)");
            }
        }

        private void dg_CA_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                PropertyByCadastral p = this.PropertiesByCadastral.Find(new PropertyByCadastral(new Property(Int32.Parse(dg_ID.Rows[e.RowIndex].Cells[0].Value.ToString()))));
                if (p != null)
                    new PropertyView(p.Property, (property) => {
                        this.PropertiesByCadastral.Update(p);
                        this.PropertiesByID.Update(new PropertyByID(p.Property));
                    });
                else
                    MessageBox.Show("Something weird happened ;)");
            }
        }
    }
}
