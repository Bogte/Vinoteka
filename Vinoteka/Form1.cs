using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vinoteka
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void artikliToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Artikli artikli = new Artikli();
            artikli.ShowDialog();
        }

        private void lagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Musterija musterija = new Musterija();
            musterija.ShowDialog();
        }

        private void lagerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Lager lager = new Lager();
            lager.ShowDialog();
        }
    }
}
