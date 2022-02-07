using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConwaysGOL
{
    public partial class RandomBySeed : Form
    {
        public RandomBySeed()
        {
            InitializeComponent();
        }

       public int Seed
        {
            get
            {
                return (int)numericUpDownSeed.Value;
            }
            set
            {
                numericUpDownSeed.Value = value;
            }
        }
    }
}
