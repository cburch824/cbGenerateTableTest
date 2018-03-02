using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using cbLibrary;

namespace cbGenerateTableTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //generate a basic list of data
            List<clsDoublePoint> basicDataList = new List<clsDoublePoint>();
            for(int i = 1; i < 21; i++)
            {
                basicDataList.Add(new clsDoublePoint(i, i));
            }

            //use that basic datalist to generate and save a table
            cbGenerateTable generateBasicTable = new cbGenerateTable(basicDataList, "Header X", "Header Y",  "Basic Datalist Test");




        }
    }
}
