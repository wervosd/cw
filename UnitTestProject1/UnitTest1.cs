using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CWproject;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        GraphInFO graphData = new GraphInFO();
        GraphInFO graphData1 = new GraphInFO();
        [TestMethod]
        public void TestVerticesCount()
        {
            graphData.FO = new int[] { 3, 4, 5, 0, 3, 5, 6, 0, 1, 2, 0, 1, 6, 0, 1, 2, 0, 2, 4, 0 };
            int count = GraphLogic.VerticesCount(ref graphData);
            Assert.AreEqual(6, count);
        }
        [TestMethod]
        public void TestEdgesCount()
        {
            graphData1.FO = new int[] { 3, 4, 0, 4, 5, 0, 1, 5, 0, 1, 2, 0, 2, 3, 0 };
            int count = GraphLogic.EdgesCount(ref graphData1);
            Assert.AreEqual(10, count);
        }
        [TestMethod]
        public void TestEqual()
        {
            graphData.FO = new int[] { 3, 4, 5, 0, 3, 5, 6, 0, 1, 2, 0, 1, 6, 0, 1, 2, 0, 2, 4, 0 };
            graphData1.FO = new int[] { 4, 5, 0, 3, 4, 0, 2, 5, 6, 0, 1, 2, 6, 0, 1, 3, 0, 3, 4, 0 };
            int[,] sresult1 = GraphLogic.ALG(graphData);
            int[,] sresult2 = GraphLogic.ALG(graphData1);
            int[] result1 = new int[GraphLogic.VerticesCount(ref graphData)];
            int[] result2 = new int[GraphLogic.VerticesCount(ref graphData1)];
            for (int i = 0; i < GraphLogic.VerticesCount(ref graphData); i++)
                for (int j = 0; j < GraphLogic.VerticesCount(ref graphData); j++)
                    result1[i] += sresult1[j, i];
            for (int i = 0; i < GraphLogic.VerticesCount(ref graphData1); i++)
                for (int j = 0; j < GraphLogic.VerticesCount(ref graphData1); j++)
                    result2[i] += sresult2[j, i];
            bool isEqual = Enumerable.SequenceEqual(result1, result2);
            Assert.IsTrue(isEqual);
        }
        [TestMethod]
        public void TestNotEqual()
        {
            graphData.FO = new int[] { 2, 5, 0, 1, 0, 1, 4, 0, 6, 0, 3, 0, 2, 0 };
            graphData1.FO = new int[] { 3, 4, 0, 4, 5, 0, 1, 5, 0, 1, 2, 0, 2, 3, 0 };
            int[,] sresult1 = GraphLogic.ALG(graphData);
            int[,] sresult2 = GraphLogic.ALG(graphData1);
            int[] result1 = new int[GraphLogic.VerticesCount(ref graphData)];
            int[] result2 = new int[GraphLogic.VerticesCount(ref graphData1)];
            for (int i = 0; i < GraphLogic.VerticesCount(ref graphData); i++)
                for (int j = 0; j < GraphLogic.VerticesCount(ref graphData); j++)
                    result1[i] += sresult1[j, i];
            for (int i = 0; i < GraphLogic.VerticesCount(ref graphData1); i++)
                for (int j = 0; j < GraphLogic.VerticesCount(ref graphData1); j++)
                    result2[i] += sresult2[j, i];
            bool isEqual = Enumerable.SequenceEqual(result1, result2);
            Assert.IsFalse(isEqual);
        }
        [TestMethod]
        public void TestReadFile()
        {
            string str = "";
            Assert.IsTrue(GraphLogic.ReadFile(out str));
        }
        [TestMethod]
        public void TestSaveFile()
        {
            string str = "2 5 0 1 0 1 4 0 6 0 3 0 2 0";
            Assert.IsTrue(GraphLogic.SaveGraph(str));
        }
        [TestMethod]
        public void TestAdjacencyMatrix()
        {
            graphData.FO = new int[] { 2, 5, 0, 1, 0, 1, 4, 0, 6, 0, 3, 0, 2, 0 };
            int[,] expectedMatrix = new int[,] {
            {0, 1, 1, 0, 0, 0 },
            {1, 0, 0, 0, 0, 1 },
            {0, 0, 0, 0, 1, 0 },
            {0, 0, 1, 0, 0, 0 },
            {1, 0, 0, 0, 0, 0 },
            {0, 0, 0, 1, 0, 0 }};
            int[,] actualMatrix = GraphLogic.AdjacencyMatrix(ref graphData);
            CollectionAssert.AreEqual(expectedMatrix, actualMatrix);
        }
    }
}
