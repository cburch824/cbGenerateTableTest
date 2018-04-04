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
        int perCharacterSizeMultiplier;

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
        /// <param name="numOfDatapoints">The total number of datapoints in the x-axis of the set.</param>
        /// <param name="maxLeftColValue">Maximum value of the left column's data.</param>
        /// <param name="maxRightColValue">Maximum value of the right column's data.</param>
        public frmDrawTableWindow(int numOfDatapoints, int maxLeftColValue, int maxRightColValue, string headerLeftColumn, string headerRightColumn)
        {


            numberOfDatapoints = numOfDatapoints;
            //updates the max characters in the left column value if necessary
            maxCharactersInLeftColumn = (maxLeftColValue.ToString().Length >= headerLeftColumn.Length) ? maxLeftColValue.ToString().Length : headerLeftColumn.Length;
            maxCharactersInRightColumn = (maxRightColValue.ToString().Length >= headerRightColumn.Length) ? maxRightColValue.ToString().Length : headerRightColumn.Length;


            perCharacterSizeMultiplier = 8; //determines number of pixels added to graph per max number of values in columns (scales width and height of table window)

            tablePositionMin = 20;
            tableWindowSizeX = 200 + (maxCharactersInLeftColumn * perCharacterSizeMultiplier) + (maxCharactersInRightColumn * perCharacterSizeMultiplier);
            tableWindowSizeY = 200 + (numberOfDatapoints * perCharacterSizeMultiplier);

            this.Height = tableWindowSizeY + 40;

            bottomOfTableLeftColumn = new Point(0 + tablePositionMin, tableWindowSizeY - tablePositionMin);
            bottomOfTableRightColumn = new Point(tableWindowSizeX - tablePositionMin, tableWindowSizeY - tablePositionMin);

            tableHeaderLeftColumn = new Point(0 + tablePositionMin, tablePositionMin);
            tableHeaderRightColumn = new Point(tableHeaderLeftColumn.X + 40 + (maxCharactersInLeftColumn * perCharacterSizeMultiplier), tablePositionMin);

            middleOfTableTop = new Point((tableHeaderRightColumn.X - 10), tableHeaderLeftColumn.Y);
            middleOfTableBottom = new Point(middleOfTableTop.X, bottomOfTableLeftColumn.Y);


            firstTableDataPointLeftColumn = new Point(tableHeaderLeftColumn.X, tableHeaderLeftColumn.Y + tablePositionMin);
            firstTableDataPointRightColumn = new Point(middleOfTableTop.X + 20, tableHeaderRightColumn.Y + tablePositionMin);

            //tableScaleX = ((firstTableDataPointRightColumn.X - firstTableDataPointLeftColumn.X) / (maxLeftColValue + maxRightColValue) * perCharacterSizeMultiplier); //currently obsolete. May be needed when working in large scales.
            tableScaleY = (bottomOfTableLeftColumn.Y - firstTableDataPointLeftColumn.Y) / numOfDatapoints;


            tableImageBitmap = new Bitmap(this.Width, this.Height);
            using (Graphics g = Graphics.FromImage(tableImageBitmap))
                g.Clear(Color.White);

            //Draw the headers
            drawTableHeader(headerLeftColumn, headerRightColumn);

        }

        /// <summary>
        /// Draws the structure of the table, including the lines dividing headers and data points, and right and left data columns.
        /// </summary>
        public void drawTableBorder()
        {
            Point headerDataDividerLeftSide = new Point(tableHeaderLeftColumn.X, tableHeaderLeftColumn.Y + 20);
            Point headerDataDividerRightSide = new Point(tableHeaderRightColumn.X + (maxCharactersInRightColumn * perCharacterSizeMultiplier), tableHeaderLeftColumn.Y + 20);

            Pen borderPen = new Pen(Color.Black);
            using (Graphics graphicsObject = Graphics.FromImage(tableImageBitmap))
            {
                graphicsObject.DrawLine(borderPen, headerDataDividerLeftSide, headerDataDividerRightSide); //draw the line seperating the headers from the data columns
                graphicsObject.DrawLine(borderPen, middleOfTableTop, middleOfTableBottom); //draw the line extending down between the columns
            }

            borderPen.Dispose();
        }

        /// <summary>
        /// Save the table bitmap image to a jpeg file.
        /// </summary>
        /// <param name="filename">The filename of the saved image.</param>
        public void saveTable(string filename)
        {
            tableImageBitmap.Save(filename + ".jpg");
        }

        public void populateTableWithData(List<clsDoublePoint> dataList)
        {
            int pointNumber = 0;
            foreach(clsDoublePoint dataPoint in dataList)
            {
                drawTableData(dataPoint.XAxis, dataPoint.YAxis, pointNumber);
                pointNumber++; //keeps track of the current row we are writing on the table
                    
            }

        }

        /// <summary>
        /// Draws a single data point's left and right column data on the table.
        /// </summary>
        /// <param name="xValue">The value of the data point's left column (x-axis/independent data).</param>
        /// <param name="yValue">The value of the data point's right column (y-axis/dependent data)</param>
        /// <param name="pointNumber">The data point's position in the set, 0-indexed.</param>
       
        public void drawTableData(double xValue, double yValue, int pointNumber)
        {
            Font stringFont = new Font(FontFamily.GenericMonospace, 10, FontStyle.Regular);
            using (Graphics graphicsObject = Graphics.FromImage(tableImageBitmap))
            {
                //draw the left column datapoint
                int xValuePositionOffset = maxCharactersInLeftColumn - xValue.ToString().Length;
                graphicsObject.DrawString(xValue.ToString(), stringFont, Brushes.Black, new Point((int)(firstTableDataPointLeftColumn.X + (perCharacterSizeMultiplier * xValuePositionOffset)), firstTableDataPointLeftColumn.Y + (int)(pointNumber * tableScaleY)));
                //draw the right column datapoint
                int yValuePositionOffset = maxCharactersInRightColumn - yValue.ToString().Length;
                graphicsObject.DrawString(yValue.ToString(), stringFont, Brushes.Black, new Point((int)(firstTableDataPointRightColumn.X + (perCharacterSizeMultiplier * yValuePositionOffset)), firstTableDataPointRightColumn.Y + (int)(pointNumber * tableScaleY)));
            }
        }

        /// <summary>
        /// Draws the header strings for the table.
        /// </summary>
        /// <param name="headerLeftColumn">Header for the left column</param>
        /// <param name="headerRightColumn">Header for the right column</param>
        private void drawTableHeader(string headerLeft, string headerRight)
        {

            Font stringFont = new Font(FontFamily.GenericMonospace, 10, FontStyle.Regular);
            using (Graphics graphicsObject = Graphics.FromImage(tableImageBitmap))
            {
                graphicsObject.DrawString(headerLeft, stringFont, Brushes.Black, tableHeaderLeftColumn);
                graphicsObject.DrawString(headerRight, stringFont, Brushes.Black, tableHeaderRightColumn);
            }
        }
    }//close frmDrawTableWindow class
}//close cbGenerateTableTest namespace
