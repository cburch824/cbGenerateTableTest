using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace cbLibrary
{
    class DrawScatterplot
    {
        clsDrawScatterplot window;
        string fileName;

        public DrawScatterplot(List<clsDoublePoint> plotList, string fn)
        {
            fileName = fn;
            drawWindow(plotList);

        }

        private void drawWindow(List<clsDoublePoint> pL)
        {
            //This foreach determines the maximum x and y values in the set of plot points. 
            //  They are then sent to the clsDrawScatterplot constructor.
            double maxX = double.MinValue;
            double maxY = double.MinValue;
            foreach (clsDoublePoint plotPoint in pL)
            {
                maxX = Math.Max(maxX, plotPoint.XAxis);
                maxY = Math.Max(maxY, plotPoint.YAxis);
            }


            //Calls the constructor.
            window = new clsDrawScatterplot(new frmDrawWindow(maxX, maxY));
            window.graphWindow.Show();

            window.graphWindow.drawGraphBorders();
            window.graphWindow.drawScale();
            window.graphWindow.plotPoints(pL);
            
            window.graphWindow.saveGraph(fileName);

            window.graphWindow.Close();
        } 


    }

    class clsDrawScatterplot
    {
        public Application app;
        public frmDrawWindow graphWindow;

        public clsDrawScatterplot(frmDrawWindow dw)
        {
            graphWindow = dw;
            

        }


        private void drawLineOnWindow(frmDrawWindow f1)
        {
            Graphics graphicsObject = f1.CreateGraphics();

            Pen outlinePen = Pens.Black;
            SolidBrush b1 = new SolidBrush(Color.Black);

            graphicsObject.DrawLine(outlinePen, graphWindow.Left, graphWindow.Top - 20, graphWindow.Right, graphWindow.Bottom + 20);
            graphicsObject.FillRectangle(b1, new Rectangle(0, 0, 200, 300));

            outlinePen.Dispose();
            graphicsObject.Dispose();



        }


    }//close clsDrawScatterplot class

}//close namespace
