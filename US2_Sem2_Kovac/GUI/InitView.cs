using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class InitView : Form
    {
        public delegate void OnDispose(int MaxDepth, int BlockSize, string FilePath);
        private OnDispose onDispose { get; set; }
        public InitView(OnDispose action)
        {
            InitializeComponent();
            this.onDispose += action;
            this.ShowDialog();
        }

        private void btn_Continue_Click(object sender, EventArgs e)
        {
            onDispose?.Invoke(Int32.Parse(trieDepth.Text), Int32.Parse(blockSize.Text), dirPath.Text);
            this.Dispose();
        }
    }
}
