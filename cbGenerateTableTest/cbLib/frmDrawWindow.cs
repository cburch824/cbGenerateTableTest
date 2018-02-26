using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cbLibrary

{
    public partial class frmDrawWindow : Form
    {

        int graphMin; //determines distance from border of form to the graph
        int graphSizeX; //determines X axis size of graph (in px)
        int graphSizeY; //determines Y axis size of graph (in px)

        Point graphBorderTopLeft; //a point describing the location of the top left corner of graph
        Point graphBorderTopRight;
        Point graphBorderBottomLeft;
        Point graphBorderBottomRight;

        Point graphOrigin;  //a point describing the location of the graph's origin

        double graphScaleX; //a double value used for determining point locations (through normalization scaling)
        double graphScaleY;

        double maxGraphValueX; //the maximum X value the graph contains
        double maxGraphValueY;

        Bitmap graphImageBitmap;


        /// <summary>
        /// Draws a graph scaled to the given maximum x and y values.
        /// </summary>
        /// <param name="maxXValue"></param>
        /// <param name="maxYValue"></param>
        public frmDrawWindow(double maxXValue, double maxYValue)
        {
            InitializeComponent();

            graphMin = 50;
            graphSizeX = 600;
            graphSizeY = 400;

            graphBorderTopLeft = new Point(graphMin, graphMin);
            graphBorderTopRight = new Point(graphMin + graphSizeX, graphMin);
            graphBorderBottomLeft = new Point(graphMin, graphMin + graphSizeY);
            graphBorderBottomRight = new Point(graphMin + graphSizeX, graphMin + graphSizeY);

            graphOrigin = graphBorderBottomLeft;

            maxGraphValueX = maxXValue;
            maxGraphValueY = maxYValue;

            graphScaleX = graphSizeX / maxGraphValueX;
            graphScaleY = graphSizeY / maxGraphValueY;

            graphImageBitmap = new Bitmap(this.Width, this.Height);
            using (Graphics g = Graphics.FromImage(graphImageBitmap))
                g.Clear(Color.White);

        }

        private void frmDrawWindow_Load(object sender, EventArgs e)
        {
           
        }

        
        public void drawGraphBorders()
        {

            //Graphics graphicsObject = this.CreateGraphics();
            Pen borderPen = new Pen(Color.Black);
            using (Graphics graphicsObject = Graphics.FromImage(graphImageBitmap))
            {
                graphicsObject.DrawLine(borderPen, graphBorderTopLeft, graphBorderTopRight);
                graphicsObject.DrawLine(borderPen, graphBorderBottomLeft, graphBorderBottomRight);
                graphicsObject.DrawLine(borderPen, graphBorderTopLeft, graphBorderBottomLeft);
                graphicsObject.DrawLine(borderPen, graphBorderTopRight, graphBorderBottomRight);
            }

            borderPen.Dispose();

        }

        public void drawScale()
        {
            
            Font scaleFont = new Font("Times New Roman", 12.0F);
            using (Graphics scaleGraphics = Graphics.FromImage(graphImageBitmap))
            {
                //draw x axis scale
                //draw 0 marker
                scaleGraphics.DrawString("|", scaleFont, Brushes.Black, graphBorderBottomLeft.X - 4, graphBorderBottomLeft.Y - 5);
                scaleGraphics.DrawString("0", scaleFont, Brushes.Black, graphBorderBottomLeft.X - 6, graphBorderBottomLeft.Y + 10);

                //draw halfway marker
                int halfwayX =(int)(maxGraphValueX / 2);
                string halfwayXString = halfwayX.ToString();
                int halfwayBetweenGraphX = ((graphBorderBottomRight.X - graphBorderBottomLeft.X) / 2) + graphBorderBottomLeft.X;

                scaleGraphics.DrawString("|", scaleFont, Brushes.Black, halfwayBetweenGraphX - 4, graphBorderBottomLeft.Y - 5);
                scaleGraphics.DrawString(halfwayXString, scaleFont, Brushes.Black, halfwayBetweenGraphX - 6, graphBorderBottomLeft.Y + 10);

                //draw end marker
                scaleGraphics.DrawString("|", scaleFont, Brushes.Black, graphBorderBottomRight.X - 4, graphBorderBottomLeft.Y - 5);
                scaleGraphics.DrawString(((int)maxGraphValueX).ToString(), scaleFont, Brushes.Black, graphBorderBottomRight.X - 6, graphBorderBottomLeft.Y + 10);


                //draw y axis scale
                //draw 0 marker
                scaleGraphics.DrawString("-", scaleFont, Brushes.Black, graphBorderBottomLeft.X - 6, graphBorderBottomLeft.Y -11);
                scaleGraphics.DrawString("0", scaleFont, Brushes.Black, graphBorderBottomLeft.X - 6 - 12, graphBorderBottomLeft.Y - 11);

                //draw halfway marker
                int halfwayBetweenGraphY = ((graphBorderBottomLeft.Y - graphBorderTopLeft.Y) / 2) + graphBorderTopLeft.Y;

                int halfwayY = (int)(maxGraphValueY / 2);
                string halfwayYString = halfwayY.ToString();

                scaleGraphics.DrawString("-", scaleFont, Brushes.Black, graphBorderBottomLeft.X - 6, halfwayBetweenGraphY - 11);
                scaleGraphics.DrawString(halfwayYString, scaleFont, Brushes.Black, graphBorderBottomLeft.X - 6 - 12, halfwayBetweenGraphY - 11);

                //draw end marker
                scaleGraphics.DrawString("-", scaleFont, Brushes.Black, graphBorderBottomLeft.X - 6, graphBorderTopLeft.Y - 11);
                scaleGraphics.DrawString(((int)maxGraphValueY).ToString(), scaleFont, Brushes.Black, graphBorderBottomLeft.X - 6 - 12 , graphBorderTopLeft.Y - 11);

            }
        }

        public void saveGraph(string filename)
        {
            graphImageBitmap.Save(filename + ".jpg");
        }

        public void testPlot()
        {
            Random randomInt = new Random();
            for (int xVal = 0; xVal < maxGraphValueX; xVal += 100)
            {
                double yVal = randomInt.NextDouble() * maxGraphValueY;
                drawPoint(xVal, yVal);
            }


        }

        public void drawPoint(double xValue, double yValue)
        {
            double graphValueX = xValue * graphScaleX;
            double graphValueY = yValue * graphScaleY;

            float graphCircleRadius = 2.0F;

            
            Pen circlePen = new Pen(Color.Black);
            using (Graphics graphicsObject = Graphics.FromImage(graphImageBitmap))
                graphicsObject.DrawCircle(circlePen, graphOrigin.X + (float)graphValueX, graphOrigin.Y - (float)graphValueY, graphCircleRadius);

            
            circlePen.Dispose();

        }



        public void plotPoints(List<clsDoublePoint> plotList)
        {
            foreach (clsDoublePoint plotObject in plotList)
            {
                drawPoint(plotObject.XAxis, plotObject.YAxis);
            }
            
        }



        #region Table Drawing Methods



        #endregion

    }//close class


    public static class GraphicsExtensions
    {
        public static void DrawCircle(this Graphics g, Pen pen,
                                      float centerX, float centerY, float radius)
        {
            g.DrawEllipse(pen, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        }

        public static void FillCircle(this Graphics g, Brush brush,
                                      float centerX, float centerY, float radius)
        {
            g.FillEllipse(brush, centerX - radius, centerY - radius,
                          radius + radius, radius + radius);
        }
    }






}
