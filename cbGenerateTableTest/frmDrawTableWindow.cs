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
    public partial class frmDrawTableWindow : Form
    {
        public frmDrawTableWindow()
        {
            InitializeComponent();
        }

        private void frmDrawTableWindow_Load(object sender, EventArgs e)
        {

        }


        int numberOfDatapoints;
        int maxCharactersInLeftColumn; //used to avoid overflow
        int maxCharactersInRightColumn;

        Point tableHeaderLeftColumn;
        Point tableHeaderRightColumn;
        Point firstTableDataPointLeftColumn;
        Point firstTableDataPointRightColumn;
        Point bottomOfTableLeftColumn;
        Point bottomOfTableRightColumn;
        Point middleOfTableTop;
        Point middleOfTableBottom;

        int tablePositionMin;
        int tableWindowSizeX;
        int tableWindowSizeY;

        double tableScaleY;
        double tableScaleX;

        Bitmap tableImageBitmap;

        /// <summary>
        /// Draws the window for the table and sets the scale for the table using input values.
        /// </summary>
        /// <param name="numOfDatapoints">The total number of datapoints along each axis</param>
        /// <param name="maxLeftColValue">Maximum value of the left column's data.</param>
        /// <param name="maxRightColValue">Maximum value of the right column's data.</param>
        public frmDrawTableWindow(int numOfDatapoints, int maxLeftColValue, int maxRightColValue)
        {

            numberOfDatapoints = numOfDatapoints;
            maxCharactersInLeftColumn = (maxLeftColValue.ToString()).Length; //determines the max characters in the left column
            maxCharactersInRightColumn = (maxRightColValue.ToString()).Length; //determines the max characters in the right column

            int perCharacterSizeMultiplier = 2; //determines number of pixels added to graph per max number of values in columns (scales width and height of table window)

            tablePositionMin = 20;
            tableWindowSizeX = 200 + (maxCharactersInLeftColumn * perCharacterSizeMultiplier) + (maxCharactersInRightColumn * perCharacterSizeMultiplier);
            tableWindowSizeY = 200 + (numberOfDatapoints * perCharacterSizeMultiplier);

            tableHeaderLeftColumn = new Point(0 + tablePositionMin, 0 + tablePositionMin);
            tableHeaderRightColumn = new Point(tableWindowSizeX - tablePositionMin, 0 + tablePositionMin);

            firstTableDataPointLeftColumn = new Point(tableHeaderLeftColumn.X, tableHeaderLeftColumn.Y + tablePositionMin);
            firstTableDataPointRightColumn = new Point(tableHeaderRightColumn.X, tableHeaderRightColumn.Y + tablePositionMin);

            bottomOfTableLeftColumn = new Point(0 + tablePositionMin, tableWindowSizeY - tablePositionMin);
            bottomOfTableRightColumn = new Point(tableWindowSizeX - tablePositionMin, tableWindowSizeY - tablePositionMin);

            middleOfTableTop = new Point((tableHeaderLeftColumn.X + tableHeaderRightColumn.X) / 2, tableHeaderLeftColumn.Y);
            middleOfTableBottom = new Point(middleOfTableTop.X, bottomOfTableLeftColumn.Y);

            tableScaleX = ((firstTableDataPointRightColumn.X - firstTableDataPointLeftColumn.X) / (maxLeftColValue + maxRightColValue) * perCharacterSizeMultiplier);
            tableScaleY = ((bottomOfTableLeftColumn.Y - firstTableDataPointLeftColumn.Y) / (numOfDatapoints  * perCharacterSizeMultiplier));


            tableImageBitmap = new Bitmap(this.Width, this.Height);
            using (Graphics g = Graphics.FromImage(tableImageBitmap))
                g.Clear(Color.White);

        }

        public void drawTableBorder()
        {
            Pen borderPen = new Pen(Color.Black);
            using (Graphics graphicsObject = Graphics.FromImage(tableImageBitmap))
            {
                graphicsObject.DrawLine(borderPen, tableHeaderLeftColumn, tableHeaderRightColumn); //draw the line seperating the headers from the data columns
                graphicsObject.DrawLine(borderPen, middleOfTableTop, middleOfTableBottom); //draw the line extending down between the columns
            }

            borderPen.Dispose();
        }

        public void saveTable(string filename)
        {
            tableImageBitmap.Save(filename + ".jpg");
        }

        public void populateTableWithData(List<clsDoublePoint> dataList)
        {
            int pointNumber = 1;
            foreach(clsDoublePoint dataPoint in dataList)
            {
                drawTableData(dataPoint.XAxis, dataPoint.YAxis, pointNumber);
                pointNumber++; //keeps track of the current row we are writing on the table
                    
            }

        }

        public void drawTableData(double xValue, double yValue, int pointNumber)
        {
            Pen stringPen = new Pen(Color.Black);
            Font stringFont = new Font(FontFamily.GenericMonospace, 10, FontStyle.Regular);
            using (Graphics graphicsObject = Graphics.FromImage(tableImageBitmap))
            {
                //draw the left column datapoint
                graphicsObject.DrawString(xValue.ToString(), stringFont, Brushes.Black, new Point((int)(firstTableDataPointLeftColumn.X), (int)(pointNumber * tableScaleY)));
                //todo: draw the right column datapoint
            }
        }

    }//close frmDrawTableWindow class
}//close cbGenerateTableTest namespace
