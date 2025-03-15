namespace BochagovaExam
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] adjacencyMatrix = new double[10, 10];

            // Матрица смежности графа
            adjacencyMatrix = new double[,]
            {
                { 0,   0.94, Double.MaxValue, Double.MaxValue, Double.MaxValue, Double.MaxValue,1.88, Double.MaxValue, Double.MaxValue, Double.MaxValue },
                { 0.94, 0,   0.66, Double.MaxValue, Double.MaxValue, Double.MaxValue, 1.2,   Double.MaxValue, Double.MaxValue, Double.MaxValue },
                { Double.MaxValue, 0.66, 0,   1.04, Double.MaxValue, 1.7,   Double.MaxValue, Double.MaxValue, Double.MaxValue, Double.MaxValue },
                { Double.MaxValue, Double.MaxValue, 1.04, 0, Double.MaxValue,  0.77,  Double.MaxValue, Double.MaxValue, Double.MaxValue, Double.MaxValue },
                { Double.MaxValue, Double.MaxValue, Double.MaxValue, Double.MaxValue, 0,   1.92, Double.MaxValue, Double.MaxValue, Double.MaxValue, Double.MaxValue },
                { Double.MaxValue, Double.MaxValue, 1.7,   0.77, 1.92, 0, Double.MaxValue, Double.MaxValue, Double.MaxValue,  1.52 },
                { 1.88, 1.2,   Double.MaxValue,   Double.MaxValue, Double.MaxValue, Double.MaxValue, 0,   0.53, Double.MaxValue, Double.MaxValue },
                { Double.MaxValue, Double.MaxValue,   Double.MaxValue, Double.MaxValue, Double.MaxValue, Double.MaxValue, 0.53, 0,   1.54, Double.MaxValue },
                { Double.MaxValue, Double.MaxValue, Double.MaxValue, Double.MaxValue, Double.MaxValue, Double.MaxValue, Double.MaxValue, 1.54, 0,     0.86},
                { Double.MaxValue, Double.MaxValue, Double.MaxValue, Double.MaxValue, Double.MaxValue, 1.52, Double.MaxValue, Double.MaxValue, 0.86, 0 }
            };

            int numberOfVertices = adjacencyMatrix.GetLength(0);
            Graph graph = new Graph(numberOfVertices);

            graph.FindMinimumPath(adjacencyMatrix);
        }
    }
}