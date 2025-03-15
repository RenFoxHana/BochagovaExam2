namespace BochagovaExam
{
    public class Graph
    {
        private int n;

        public Graph(int numberOfVertices)
        {
            n = numberOfVertices;
        }

        public double[,] Floyd(double[,] a)
        {
            double[,] d = new double[n, n];
            d = (double[,])a.Clone();
            for (int i = 1; i <= n; i++)
                for (int j = 0; j <= n - 1; j++)
                    for (int k = 0; k <= n - 1; k++)
                        if (d[j, k] > d[j, i - 1] + d[i - 1, k])
                            d[j,k] = d[j,i - 1] + d[i - 1,k];
            return d;
        }

        public void FindMinimumPath(double[,] adjacencyMatrix)
        {
            Console.WriteLine("Введите номера точек, в которых контейнеры отсутствуют через пробел:");
            string input = Console.ReadLine();
            bool[] excludedPoints = new bool[n];
            foreach (var point in input.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                if (int.TryParse(point, out int outPoint) && outPoint >= 1 && outPoint <= n)
                {
                    excludedPoints[outPoint - 1] = true;
                }
                else
                {
                    Console.WriteLine($"Точка {point} введена неверно. Она не будет учитываться.");
                }
            }

            double[,] distances = Floyd(adjacencyMatrix);

            //Перебор всех точек с исключением пустых
            int[] remainingPoints = new int[n];
            int remainingCount = 0;
            for (int i = 0; i < n; i++)
                if (!excludedPoints[i])
                    remainingPoints[remainingCount++] = i;

            if (remainingCount < 2)
            {
                Console.WriteLine("Недостаточно точек для построения маршрута.");
                return;
            }

            double minPathLength = double.MaxValue;
            int[] endPath = new int[remainingCount];

            int[] currentPoint = new int[remainingCount];
            for (int i = 0; i < remainingCount; i++)
            {
                currentPoint[i] = remainingPoints[i];
            }

            //Метод для нахождения пути с пропуском точек с рекурсией
            void FindPath(int point)
            {
                if (point == remainingCount - 1)
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
                    for (int i = point; i < remainingCount; i++)
                    {
                        int temp = currentPoint[point];
                        currentPoint[point] = currentPoint[i];
                        currentPoint[i] = temp;

                        FindPath(point + 1);

                        temp = currentPoint[point];
                        currentPoint[point] = currentPoint[i];
                        currentPoint[i] = temp;
                    }
                }
            }

            FindPath(0);

            Console.Write("Путь: ");
            foreach (var point in endPath)
                Console.Write((point + 1) + " ");
            Console.WriteLine($"Длина пути: {minPathLength:F2}");
        }
    }
}