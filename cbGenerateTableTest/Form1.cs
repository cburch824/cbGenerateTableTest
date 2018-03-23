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
    public partial class frmGenerateTableTestForm : Form
    {
        public frmGenerateTableTestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //User-defined values
            string headerXColumn = "Header X";
            string headerYColumn = "Header Y";
            int maximumYValue = 99999999;
            int numberOfDatapoints = 20;
            string tableFileName = "Basic Datalist Test"; //will be saved as a jpeg file

            //Random function used for seeding dataset
            Random randomYValueGenerator = new Random();

            //Generate a basic list of data
            List<clsDoublePoint> basicDataList = new List<clsDoublePoint>();
            for(int i = 1; i < numberOfDatapoints + 1; i++)
            {
                int randomlyGeneratedYValue = randomYValueGenerator.Next(maximumYValue);
                basicDataList.Add(new clsDoublePoint(i, randomlyGeneratedYValue));
            }

            //Use that basic datalist to generate and save a table
            cbGenerateTable generateBasicTable = new cbGenerateTable(basicDataList, headerXColumn, headerYColumn,  tableFileName);




        }

        private void frmGenerateTableTestForm_Load(object sender, EventArgs e)
        {

        }
    }
}
