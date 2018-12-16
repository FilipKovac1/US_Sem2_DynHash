using DynHash;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GUI
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void btn_Test_Click(object sender, EventArgs e)
        {
            string FilePath = dirPath.Text;

            DynHash<Property> dh = new DynHash<Property>(Int32.Parse(trieDepth.Text), Int32.Parse(blockSize.Text), FilePath + "/block_test.bin", new DHObjectReader(FilePath + "/properties_test.bin"));
            dh.Destruct();
            Random ids = new Random(100);
            int ca = Int32.Parse(caCount.Text);
            int prop = Int32.Parse(propCount.Text);
            List<Int32> idArray = new List<Int32>(ca + prop);
            int id = 0;
            for (int i = 1; i <= ca; i++)
            {
                for (int j = 1; j <= prop; j++)
                {
                    id = ids.Next(ca * prop);
                    if (dh.Add(new Property()
                        {
                            ID = id,
                            RN = j,
                            CadastralArea = "CA " + i,
                            Description = "Nejaky text " + i + " " + j
                        }
                    ))
                        idArray.Add(id);
                }
            }
            bool notGood = false;
            foreach (int i in idArray)
            {
                if (dh.Find(new Property(i)) == null)
                {
                    notGood = true;
                    break;
                }
            }
            if (notGood)
                MessageBox.Show("There is a problem");
            else
                MessageBox.Show("Its all good");

            dh.Save(FilePath + "/export_test.txt");

            this.Dispose();
        }

        private void btn_Load_Click(object sender, EventArgs e)
        {
            DynHash<Property> dh = new DynHash<Property>(0, 0, dirPath.Text + "/block_test.bin", new DHObjectReader(dirPath.Text + "/properties_test.bin"));
            dh.Load(dirPath.Text + "/export_test.txt", new Property());
            Console.WriteLine(dh.ToString());
        }
    }
}
