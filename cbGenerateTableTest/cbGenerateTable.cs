using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cbLibrary;
using System.Windows.Forms;
using System.Drawing;


namespace cbGenerateTableTest
{
    /// <summary>
    /// The cbGenerateTable class creates a table from a clsDoublePoint object and saves the table as a .jpg object with a specified name.
    /// </summary>
    public class cbGenerateTable
    {
        clsGenerateTable tableWindow;
        string fileName;

        
        public cbGenerateTable(List<clsDoublePoint> inputData, string fn)
        {
            fileName = fn;
            drawWindow(inputData);
            
            
        }

        private void drawWindow(List<clsDoublePoint> dL)
        {

            //This foreach determines the maximum x and y values in the set of plot points. 
            //  They are then sent to the clsDrawScatterplot constructor.
            double maxX = double.MinValue;
            double maxY = double.MinValue;
            foreach (clsDoublePoint plotPoint in dL)
            {
                maxX = Math.Max(maxX, plotPoint.XAxis);
                maxY = Math.Max(maxY, plotPoint.YAxis);
            }


            //Calls the constructor.
            tableWindow = new clsGenerateTable( new frmDrawTableWindow(dL.Count, 5, 5));
            tableWindow.tableWindow.Show();

            tableWindow.tableWindow.drawTableBorder();
            //tableWindow.tableWindow.drawScale();
            tableWindow.tableWindow.populateTableWithData(dL);

            tableWindow.tableWindow.saveTable(fileName);

            tableWindow.tableWindow.Close();
        }

    }//close cbGenerateTable

    class clsGenerateTable
    {
        public Application app;
        public frmDrawTableWindow tableWindow;

        public clsGenerateTable(frmDrawTableWindow dtw)
        {
            tableWindow = dtw;
        }



    }//close clsDrawTable
}//close cbGenerateTableTest
