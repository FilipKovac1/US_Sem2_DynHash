using System;
using System.Data;
using System.Windows.Forms;
using Model;
using DynHash;

namespace GUI
{
    public partial class MainForm : Form
    {
        private DynHash<Property> PropertiesByID { get; set; }
        private DynHash<PropertyByCadastral> PropertiesByCadastral { get; set; }
        private DynHash<Person> Persons { get; set; }

        private BindingSource BindSourceById = new BindingSource();
        private BindingSource BindSourceByPe = new BindingSource();

        private string FilePath { get; set; }

        public MainForm()
        {
            bool init = false;
            new InitView((maxDepth, blockSize, filePath) =>
            {
                DHObjectReader propertyReader = new DHObjectReader(filePath + "/properties.bin");
                this.PropertiesByCadastral = new DynHash<PropertyByCadastral>(maxDepth, blockSize, filePath + "/ppCa.bin", propertyReader);
                this.PropertiesByID = new DynHash<Property>(maxDepth, blockSize, filePath + "/ppId.bin", propertyReader);
                this.Persons = new DynHash<Person>(maxDepth, blockSize, filePath + "/peID.bin", new DHObjectReader(filePath + "/people.bin"));
                this.FilePath = filePath;
                init = true;
            });
            if (init)
            {
                InitializeComponent();
                this.cb_Type.SelectedIndex = 0;
                this.dg_ID.DataSource = BindSourceById;
                this.dg_Person.DataSource = BindSourceByPe;
                this.FormClosing += (sender, e) => { this.Save(); }; // save actual work to the file
            }
            else
                Environment.Exit(1);
        }

        private void btn_Create_Click(object sender, EventArgs e)
        {
            if (this.cb_Type.SelectedIndex == 1)
            {
                new PropertyView(null, (property) =>
                {
                    if (property.ID > 0 && property.CadastralArea.Length > 0 && property.RN > 0)
                    {
                        if (this.PropertiesByID.Add(property))
                        {
                            MessageBox.Show("Property was created and saved successfuly");
                            this.ReloadGridsProperty();
                        }
                        else
                            MessageBox.Show("Property with this id already exists");
                    }
                    else
                        MessageBox.Show("Mandatory fields were not filled properly");

                });
            }
            else
            {
                new PersonView(null, (person) =>
                {
                    if (this.Persons.Add(person))
                    {
                        this.ReloadGridsPerson();
                        MessageBox.Show("Person was created successfuly");
                    }
                    else
                        MessageBox.Show("Person was not created successfuly");
                });
            }
        }

        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.PropertiesByCadastral.Destruct();
            this.PropertiesByID.Destruct();
            this.Persons.Destruct();

            new InputDialog("Number of objects to generate", 100.ToString(), (value) =>
            {
                Random ids = new Random();
                if (!Int32.TryParse(value, out int count))
                    MessageBox.Show("Wrong data type used");
                else
                {
                    int id = 0;
                    Property property;
                    for (int j = 1; j <= count; j++)
                    {
                        id = ids.Next(count) + 1;
                        property = new Property()
                        {
                            ID = id,
                            RN = j,
                            CadastralArea = "ZA",
                            Description = "Nejaky text " + id + " " + j
                        };
                        PropertiesByID.Add(property);
                    }

                    this.ReloadGridsProperty();

                    for (int i = 1; i <= count; i++)
                        this.Persons.Add(new Person(i.ToString(), ("Meno" + i), ("Druhe" + i)));

                    this.ReloadGridsPerson();
                }
            });
        }

        private void Save()
        {
            this.PropertiesByCadastral.Save(this.FilePath + "/exportCA.txt");
            this.PropertiesByID.Save(this.FilePath + "/exportID.txt");
            this.Persons.Save(this.FilePath + "/exportPerson.txt");
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Save();
            MessageBox.Show("Data were saved");
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.PropertiesByID.Load(this.FilePath + "/exportID.txt", new Property());
            this.PropertiesByCadastral.Load(this.FilePath + "/exportCA.txt", new PropertyByCadastral(new Property()));
            this.Persons.Load(this.FilePath + "/exportPerson.txt", new Person());

            MessageBox.Show("Data has been load");
            this.ReloadGridsProperty();
            this.ReloadGridsPerson();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.PropertiesByCadastral.Destruct();
            this.PropertiesByID.Destruct();
            this.Persons.Destruct();
            this.Save();
            this.ReloadGridsProperty();
            this.ReloadGridsPerson();
        }

        private void ReloadGridsPerson()
        {
            DataTable tablePe = new DataTable();
            tablePe.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID", typeof(string)),
                new DataColumn("Firstname", typeof(string)),
                new DataColumn("Lastname", typeof(string)),
            });

            this.Persons.GetAll(new Person(), (p) => tablePe.Rows.Add(this.CreateRowPerson(p, tablePe)));
            this.BindSourceByPe.DataSource = tablePe;
        }

        private void ReloadGridsProperty()
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
            this.PropertiesByID.GetAll(new Property(), (p) => tableID.Rows.Add(this.CreateRow(p, tableID)));
            this.PropertiesByCadastral.GetAll(new PropertyByCadastral(new Property()), (p) => tableCA.Rows.Add(this.CreateRow(p.Property, tableCA)));

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

        private DataRow CreateRowPerson(Person p, DataTable table)
        {
            DataRow ret = table.NewRow();
            ret[0] = p.ID;
            ret[1] = p.Firstname;
            ret[2] = p.Lastname;
            return ret;
        }

        private void dg_ID_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Property p = this.PropertiesByID.Find(new Property(Int32.Parse(dg_ID.Rows[e.RowIndex].Cells[0].Value.ToString())));
                if (p != null)
                    this.UpdatePropertyDialog(p);
                else
                    MessageBox.Show("Something weird happened ;)");
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (cb_Type.SelectedIndex == 1)
            {
                if (Int32.TryParse(tb_Search.Text, out int ID))
                {
                    Property p = this.PropertiesByID.Find(new Property(ID));
                    if (p != null)
                        this.UpdatePropertyDialog(p);
                    else
                        MessageBox.Show("Could not find property with ID " + ID);
                }
                else
                {
                    string[] cad = tb_Search.Text.Split('/');
                    if (cad.Length > 1 && Int32.TryParse(cad[1].TrimEnd(), out int RN))
                    {
                        PropertyByCadastral p = this.PropertiesByCadastral.Find(new PropertyByCadastral(new Property(RN, cad[0].TrimEnd())));
                        if (p != null)
                            this.UpdatePropertyDialog(p.Property);
                        else
                            MessageBox.Show("Could not find property by parameters Cadastral Area (" + cad[0] + ") and RN (" + cad[1] + ")");
                    }
                    else
                        MessageBox.Show("Wrong parameters for search (use just number as Property ID or Name of cadastral area/rn");
                }
            }
            else
            {
                Person p = this.Persons.Find(new Person(tb_Search.Text, "", ""));
                if (p != null)
                {
                    new PersonView(p.Clone(), (person) =>
                    {
                        if (!p.CompareFull(person))
                        {
                            this.Persons.Update(person);
                            this.ReloadGridsPerson();
                        }
                        else
                            MessageBox.Show("Nothing changed");
                    });
                }
                else
                    MessageBox.Show("Cannot find person");
            }
        }

        private void UpdatePropertyDialog(Property p)
        {
            new PropertyView(p.Clone(), (property) =>
            {
                if (!p.CompareFull(property))
                {
                    this.PropertiesByID.Update(property);
                    this.ReloadGridsProperty();
                }
                else
                    MessageBox.Show("Nothing changed");
            });
        }

        private void dg_Person_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Person p = this.Persons.Find(new Person(dg_Person.Rows[e.RowIndex].Cells[0].Value.ToString(), "", ""));
                if (p != null)
                    new PersonView(p.Clone(), (person) =>
                    {
                        if (!p.CompareFull(person))
                        {
                            this.Persons.Update(person);
                            this.ReloadGridsPerson();
                        }
                        else
                            MessageBox.Show("Nothing changed");
                    });
                else
                    MessageBox.Show("Something weird happened ;)");
            }
        }

        private void showBlocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new BlocksView(PropertiesByID, PropertiesByCadastral);
        }
    }
}
