using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CWproject
{
    public partial class Interface : Form
    {
        GraphInFO graph1 = new GraphInFO(); // first graph's info
        GraphInFO graph2 = new GraphInFO(); // second graph's info
        public Interface()
        {
            InitializeComponent();
        }
        /// <summary>
        /// set FO for first graph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_firstGraphSet_Click(object sender, EventArgs e)
        {
            try
            {
                GraphLogic.StringToArray(textBox1.Text, ref graph1);
                if ((GraphLogic.VerticesCount(ref graph1) > 20) || GraphLogic.EdgesCount(ref graph1) > 50)
                {
                    MessageBox.Show("Error. you can't add more than 20 vertices or 50 edges. try again");
                    graph1.FO = new int[0];
                    return;
                }
                GraphVisualization.Visualize(ref graph1, pictureBox1);
            }
            catch (Exception) { MessageBox.Show("Incorrect FO", "Error"); Graphics g = pictureBox1.CreateGraphics();
                g.Clear(Color.White);
            }
        }
        /// <summary>
        /// Set FO for second graph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_secondGraphSet_Click(object sender, EventArgs e)
        {
            try
            {
                GraphLogic.StringToArray(textBox2.Text, ref graph2);
                if ((GraphLogic.VerticesCount(ref graph2) > 20) || GraphLogic.EdgesCount(ref graph2) > 50)
                {
                    MessageBox.Show("Error. you can't add more than 20 vertices or 50 edges. try again");
                    graph2.FO = new int[0];
                    return;
                }
                GraphVisualization.Visualize(ref graph2, pictureBox2);

            }
            catch (Exception) { MessageBox.Show("Incorrect FO", "Error");
                Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            }
        }
        /// <summary>
        /// Read FO for first graph from txt file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_firstGraphRead_Click(object sender, EventArgs e)
        {
            string graphData;
            if (GraphLogic.ReadFile(out graphData) == true)
                textBox1.Text = graphData;
            else MessageBox.Show("Incorrectly entered data in the file", "Error");
        }
        /// <summary>
        /// Read FO for second graph from txt file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_secondGraphRead_Click(object sender, EventArgs e)
        {
            string graphData;
            if (GraphLogic.ReadFile(out graphData) == true)
                textBox2.Text = graphData;
            else MessageBox.Show("Incorrectly entered data in the file", "Error");
        }
        /// <summary>
        /// Save first graph's F to txt file 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_firstGraphSave_Click(object sender, EventArgs e)
        {
            if (GraphLogic.SaveGraph(textBox1.Text) == true)
                MessageBox.Show("File saved successfully.", "Success");
            else MessageBox.Show("Error. Try again.", "Error");
        }
        /// <summary>
        /// Save second graph's FO to txt file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_secondGraphSave_Click(object sender, EventArgs e)
        {
            if (GraphLogic.SaveGraph(textBox2.Text) == true)
                MessageBox.Show("File saved successfully.", "Success");
            else MessageBox.Show("Error. Try again.", "Error");
        }
        /// <summary>
        /// Compare graphs for equivalence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_CheckForEquivalence_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            if (graph1 == null || graph2 == null) return;
            int[,] sresult1 = GraphLogic.ALG(graph1);
            int[,] sresult2 = GraphLogic.ALG(graph2);

            int[] result1 = new int[GraphLogic.VerticesCount(ref graph1)];
            int[] result2 = new int[GraphLogic.VerticesCount(ref graph2)];

            for (int i = 0; i < GraphLogic.VerticesCount(ref graph1); i++)
            {
                for (int j = 0; j < GraphLogic.VerticesCount(ref graph1); j++)
                {
                    result1[i] += sresult1[j, i];                  
                }
            }
            for (int i = 0; i < GraphLogic.VerticesCount(ref graph2); i++)
            {
                for (int j = 0; j < GraphLogic.VerticesCount(ref graph2); j++)
                {
                    result2[i] += sresult2[j, i];
                }
            }
            bool isEqual = Enumerable.SequenceEqual(result1, result2);
            richTextBox1.Text += $"graph 1: vertices - {GraphLogic.VerticesCount(ref graph1)}, edges - {GraphLogic.EdgesCount(ref graph1)}\n";
            for (int i = 0; i < result1.Length; i++)
            {
                if (result1[i] != 0)
                    richTextBox1.Text += $"{i} - {result1[i]}, ";
            }
            richTextBox1.Text += $"\ngraph 2: vertices - {GraphLogic.VerticesCount(ref graph2)}, edges - {GraphLogic.EdgesCount(ref graph2)}\n";
            for(int i = 0; i < result2.Length; i++)
            {
                if (result2[i] != 0)
                    richTextBox1.Text += $"{i} - {result2[i]}, ";
            }
            if (isEqual)
                richTextBox1.Text += "\n\n graph1 == graph2";
            else
                richTextBox1.Text += "\n\n graph1 != graph2";

/*            int[,] newas = GraphLogic.AdjacencyMatrix(ref graph1);

            List<List<int>> adjacentVerticesList = GraphLogic.AdjacentVerticesList(ref graph1);


            int[,] newas2 = GraphLogic.AdjacencyMatrix(ref graph2);
            richTextBox1.Text += "\n\n";
            for(int i = 0; i < graph1.FO.Length; i++)
            {
                for (int j = 0; j < graph1.FO.Length; j++)
                {
                    richTextBox1.Text += $"{newas[i, j]} ";
                }
                richTextBox1.Text += "\n\n";
            }
            richTextBox1.Text += "\n\n";
            for (int i = 0; i < graph2.FO.Length; i++)
            {
                for (int j = 0; j < graph2.FO.Length; j++)
                {
                    richTextBox1.Text += $"{newas2[i, j]} ";
                }
                richTextBox1.Text += "\n\n";
            }*/
        }
    }
}
