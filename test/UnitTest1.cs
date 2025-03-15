namespace BochagovaExam.Tests
{
    public class GraphTests
    {
        private double[,] _adjacencyMatrix;
        private Graph _graph;

        [Fact]
        public void Test1()
        {
            int numberOfVertices = 10;
            double[,] adjacencyMatrix = new double[,]
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

            Graph _graph = new Graph(numberOfVertices);

            string input = "2 4";
            bool[] excludedPoints = new bool[numberOfVertices];
            foreach (var point in input.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                if (int.TryParse(point, out int outPoint))
                {
                    excludedPoints[outPoint - 1] = true;
                }
            }

            double[,] distances = _graph.Floyd(adjacencyMatrix);

            int[] remainingPoints = new int[numberOfVertices];
            int remainingCount = 0;
            for (int i = 0; i < numberOfVertices; i++)
            {
                if (!excludedPoints[i])
                {
                    remainingPoints[remainingCount++] = i;
                }
            }

            double minPathLength = double.MaxValue;
            int[] endPath = new int[remainingCount];
            int[] currentPoint = new int[remainingCount];
            for (int i = 0; i < remainingCount; i++)
            {
                currentPoint[i] = remainingPoints[i];
            }

            void FindPath(int j)
            {
                if (j == remainingCount - 1)
                {
                    double pathLength = 0;
                    for (int i = 0; i < remainingCount - 1; i++)
                    {
                        pathLength += distances[currentPoint[i], currentPoint[i + 1]];
                    }

                    if (pathLength < minPathLength)
                    {
                        minPathLength = pathLength;
                        Array.Copy(currentPoint, endPath, remainingCount);
                    }
                }
                else
                {
                    for (int i = j; i < remainingCount; i++)
                    {
                        int temp = currentPoint[j];
                        currentPoint[j] = currentPoint[i];
                        currentPoint[i] = temp;

                        FindPath(j + 1);

                        temp = currentPoint[j];
                        currentPoint[j] = currentPoint[i];
                        currentPoint[i] = temp;
                    }
                }
            }

            FindPath(0);

            Assert.Equal(minPathLength, 9.83);
        }
    }
}