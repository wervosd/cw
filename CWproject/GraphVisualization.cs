using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CWproject
{
    public class GraphVisualization
    {
        /// <summary>
        /// find vertices coords
        /// </summary>
        /// <param name="graphData"></param>
        /// <returns></returns>
        public static List<PointF> FindVerticesCoords(ref GraphInFO graphData)
        {
            float verticesCount = GraphLogic.VerticesCount(ref graphData);
            List<PointF> vCoords = new List<PointF>();
            float angle = 0.0f;
            float anglePart = 360 / verticesCount;
            for (int i = 0; i < verticesCount; i++)
            {
                float x = (float)(170 * Math.Cos(angle * Math.PI / 180F) + 200);
                float y = (float)(170 * Math.Sin(angle * Math.PI / 180F) + 170);
                angle += anglePart;

                vCoords.Add(new PointF(x, y));
            }
            return vCoords;
        }
        /// <summary>
        /// Visualize graph
        /// </summary>
        /// <param name="graphData"></param>
        /// <param name="pic"></param>
        public static void Visualize(ref GraphInFO graphData, PictureBox pic)
        {
            int verticesCount = GraphLogic.VerticesCount(ref graphData);
            Graphics g = pic.CreateGraphics();
            List<PointF> VertexPointsList = new List<PointF>();
            float angle = 0.0f;
            if (verticesCount == 0)
            {
                g.Clear(Color.White);
                return;
            }

            float anglePart = 360 / verticesCount;
            for (int i = 0; i < verticesCount; i++)
            {
                float x = (float)(170 * Math.Cos(angle * Math.PI / 180F) + 200);
                float y = (float)(170 * Math.Sin(angle * Math.PI / 180F) + 170);
                angle += anglePart;
                VertexPointsList.Add(new PointF(x, y));
            }

            g.Clear(Color.White);
            Pen pen = new Pen(Brushes.Black, 2.0f);
            List<List<int>> adjacentVerticesList = GraphLogic.AdjacentVerticesList(ref graphData);
            for (int i = 0; i < adjacentVerticesList.Count; i++)
            {
                foreach (var Vertex in adjacentVerticesList[i])
                {
                    Point startLine = new Point((int)VertexPointsList[Vertex - 1].X + 30, (int)VertexPointsList[Vertex - 1].Y + 30);
                    Point endLine = new Point((int)VertexPointsList[i].X + 30, (int)VertexPointsList[i].Y + 30);
                    g.DrawLine(pen, endLine, startLine);

                    Point endPoint = new Point((int)VertexPointsList[Vertex - 1].X + 30, (int)VertexPointsList[Vertex - 1].Y + 30);
                    Point startPoint = new Point((int)VertexPointsList[i].X + 30, (int)VertexPointsList[i].Y + 30);

                    double ugol = Math.Atan2(startPoint.X - endPoint.X, startPoint.Y - endPoint.Y);
                    Point secondEndPoint = new Point(Convert.ToInt32(endPoint.X + 15 * Math.Sin(ugol)), Convert.ToInt32(endPoint.Y + 15 * Math.Cos(ugol)));

                    g.DrawLine(pen, secondEndPoint.X, secondEndPoint.Y, Convert.ToInt32(secondEndPoint.X + 15 * Math.Sin(0.3 + ugol)), Convert.ToInt32(secondEndPoint.Y + 15 * Math.Cos(0.3 + ugol)));
                    g.DrawLine(pen, secondEndPoint.X, secondEndPoint.Y, Convert.ToInt32(secondEndPoint.X + 15 * Math.Sin(ugol - 0.3)), Convert.ToInt32(secondEndPoint.Y + 15 * Math.Cos(ugol - 0.3)));

                }
            }

            Point tm;
            Point point;
            float startAngle = 0.0F;
            float CirAngle = 360.0F;
            pen = new Pen(Color.Black);
            for (int i = 0; i < verticesCount; i++)
            {
                point = new Point((int)VertexPointsList[i].X, (int)VertexPointsList[i].Y);
                SolidBrush dotBrush = new SolidBrush(Color.Black);
                SolidBrush redBrush = new SolidBrush(Color.White);
                tm = new Point(point.X + 22, point.Y + 21);
                Rectangle rect = new Rectangle(point.X + 14, point.Y + 14, 30, 30);
                Font font = new Font("Arial", 10);

                g.FillPie(redBrush, rect, startAngle, CirAngle);
                g.DrawArc(pen, rect, startAngle, CirAngle);
                g.DrawString((i + 1).ToString(), font, dotBrush, tm);
            }
        }
    }

}
