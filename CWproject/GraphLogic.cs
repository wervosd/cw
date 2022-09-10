using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CWproject
{
    public class GraphLogic
    {
        /// <summary>
        /// get count of graph's vertices
        /// </summary>
        /// <param name="graphData"></param>
        /// <returns></returns>
        public static int VerticesCount(ref GraphInFO graphData)
        {
            int verticesCount = 0;
            for (int i = 0; i < graphData.FO.Length; i++)
            {
                if (graphData.FO[i] == 0)
                    verticesCount++;
            }
            return verticesCount;
        }
        /// <summary>
        /// get count of graph's edges 
        /// </summary>
        /// <param name="graphData"></param>
        /// <returns></returns>
        public static int EdgesCount(ref GraphInFO graphData)
        {
            int edgesCount = 0;
            for (int i = 0; i < graphData.FO.Length; i++)
            {
                if (graphData.FO[i] != 0)
                    edgesCount++;
            }
            return edgesCount;
        }
        /// <summary>
        /// get list of Adjacent Vertices
        /// </summary>
        /// <param name="graphData"></param>
        /// <returns></returns>
        public static List<List<int>> AdjacentVerticesList(ref GraphInFO graphData)
        {
            List<List<int>> adjacentVerticiesList = new List<List<int>>();
            List<int> temp = new List<int>();
            for (int i = 0; i < graphData.FO.Length; i++)
            {
                if (graphData.FO[i] != 0)
                    temp.Add(graphData.FO[i]);
                else
                {
                    adjacentVerticiesList.Add(temp);
                    temp = new List<int>();
                }
            }
            return adjacentVerticiesList;
        }
        /// <summary>
        /// Convert string to FO format
        /// </summary>
        /// <param name="text"></param>
        /// <param name="graphData"></param>
        /// <returns></returns>
        public static bool StringToArray(string text, ref GraphInFO graphData)
        {
            try
            {
                string[] tempString = text.Split(' ');
                graphData.FO = new int[tempString.Length];
                for (int i = 0; i < tempString.Length; i++) graphData.FO[i] = int.Parse(tempString[i]);
            }
            catch (Exception) { return false; }
            return true;
        }
        /// <summary>
        /// Read file with graph's FO
        /// </summary>
        /// <param name="graphData"></param>
        /// <returns></returns>
        public static bool ReadFile(out string graphData)
        {
            graphData = null;
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Open FIle";
            openFile.InitialDirectory = @"C:\\";
            openFile.Filter = "txt files (*.txt)|*.txt";
            try
            {
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader sr = new StreamReader(openFile.FileName))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                            graphData = line;
                    }
                }
                else
                {
                    graphData = null;
                    return false;
                }
            }
            catch (Exception)
            {
                graphData = null;
                return false;
            }
            return true;
        }
        /// <summary>
        /// Save graph's FO to txt file
        /// </summary>
        /// <param name="graphData"></param>
        /// <returns></returns>
        public static bool SaveGraph(string graphData)
        {
            string filename;
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Title = "Save Graph's FO";
            saveFile.InitialDirectory = @"C:\\";
            saveFile.Filter = "txt files (*.txt)|*.txt";
            try
            {
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    filename = saveFile.FileName;
                    FileStream filestream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter writefile = new StreamWriter(filestream);
                    writefile.WriteLine(graphData);
                    writefile.Flush();
                    writefile.Close();
                    filestream.Close();
                }
                return true;
            }
            catch (Exception) { return false; }
        }
        /// <summary>
        /// get adjacency matrix for graph
        /// </summary>
        /// <param name="graphData"></param>
        /// <returns></returns>
        public static int[,] AdjacencyMatrix(ref GraphInFO graphData)
        {
            int size = GraphLogic.VerticesCount(ref graphData);
            int[,] Matrix = new int[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    Matrix[i, j] = 0;
            List<List<int>> list = AdjacentVerticesList(ref graphData);
            for (int i = 0; i < size; i++)
            {
                foreach (var a in list[i])
                {
                    Matrix[a - 1, i] = 1;
                }
            }
            return Matrix;
        }
        /// <summary>
        /// Get Weight matrix for graph
        /// </summary>
        /// <param name="graphData"></param>
        /// <returns></returns>
        public static int[,] DistanceMatrix(ref GraphInFO graphData)
        {
            int size = GraphLogic.VerticesCount(ref graphData);
            int[,] weightMatrix = new int[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    weightMatrix[i, j] = -1;
            int[,] a = AdjacencyMatrix(ref graphData);
            int k = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (a[j, i] == 1)
                    {
                        weightMatrix[j, i] = 1;
                        k++;
                    }
                }
            }
            return weightMatrix;
        }
        /// <summary>
        /// Floyd Warshall's algorithm to find all paths and solving our problem
        /// </summary>
        /// <param name="graphData"></param>
        /// <returns></returns>
        public static int[,] ALG(GraphInFO graphData)
        {
            int[,] distance = DistanceMatrix(ref graphData);
            int[,] Next = DistanceMatrix(ref graphData);

            int NOT_CONNECTED = -1;

            for (int i = 0; i < GraphLogic.VerticesCount(ref graphData); i++)
            {
                for (int j = 0; j < GraphLogic.VerticesCount(ref graphData); j++)
                {
                    if (distance[i, j] != -1)
                        Next[i, j] = j;
                    else Next[i, j] = -1;
                }
            }
            for (int k = 0; k < GraphLogic.VerticesCount(ref graphData); ++k)
            {
                for (int i = 0; i < GraphLogic.VerticesCount(ref graphData); ++i)
                {
                    if (distance[i, k] != NOT_CONNECTED)
                    {

                        for (int j = 0; j < GraphLogic.VerticesCount(ref graphData); ++j)
                        {
                            if (distance[k, j] != NOT_CONNECTED && (distance[i, j] == NOT_CONNECTED || distance[i, k] + distance[k, j] < distance[i, j]))
                            {
                                distance[i, j] = distance[i, k] + distance[k, j];
                                Next[i, j] = Next[i, k];

                            }

                        }

                    }

                }
            }
            List<List<int>> result = new List<List<int>>();
            int[,] temparray = new int[10, 10]; 
            int t = 0;

            for(int i = 0; i < GraphLogic.VerticesCount(ref graphData); i++)
            {
                
                for(int j = 0; j < GraphLogic.VerticesCount(ref graphData); j++)
                {
                    if(i != j)
                    {
                        temparray[t, distance[i, j]] += 1;
                    }
                }
                t++;
            }
            return temparray;
        }
    }
    /// <summary>
    /// Class for graph's info (FO-format)
    /// </summary>
    public class GraphInFO
    {
        public int[] FO { get; set; }
        public int[] FOW { get; set; }
        public GraphInFO(int[] fo)
        {
            FO = fo;
        }
        public GraphInFO(int[] fo, int[] fow)
        {
            FO = fo;
            FOW = fow;
        }
        public GraphInFO()
        {
            FO = new int[0];
            FOW = new int[0];
        }
    }
}
