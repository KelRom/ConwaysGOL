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
    public partial class OptionDialog : Form
    {
        public OptionDialog()
        {
            InitializeComponent();
        }

        public int TimeInterval
        {
            get 
            { 
                return (int)numericUpDownTimeInterval.Value; 
            }
            set
            {
                numericUpDownTimeInterval.Value = value;
            }
        }

        public int CellWidth
        {
            get 
            { 
                return (int)numericUpDownCellWidth.Value; 
            }
            set
            {
                numericUpDownCellWidth.Value = value;
            }
        }


        public int CellHeight
        {
            get
            {
                return (int)numericUpDownCellHeight.Value;
            }
            set
            {
                numericUpDownCellHeight.Value = value;
            }
        }
    }
}
