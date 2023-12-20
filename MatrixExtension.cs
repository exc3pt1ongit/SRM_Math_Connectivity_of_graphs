using Pastel;

namespace SRM_Connectivity_of_graphs
{
    public static class MatrixExtension
    {
        private static int[,] IdentityMatrix(int[,] matrix)
        {
            int size = matrix.GetLength(0);
            int[,] identityMatrix = new int[size, size];

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (i == j) identityMatrix[i, j] = 1;

            return identityMatrix;
        }

        private static int[,] MatrixCompletelyWithOnes(int[,] matrix)
        {
            int size = matrix.GetLength(0);
            int[,] identityMatrix = new int[size, size];

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    identityMatrix[i, j] = 1;

            return identityMatrix;
        }

        public static int[,] Power(this int[,] matrix, int power, bool flagShowLog = false)
        {
            if (power == 0) return new int[matrix.GetLength(0), matrix.GetLength(1)];
            if (power < 0) throw new ArgumentException("Power must be greater than or equal to 0.");

            var result = (int[,])matrix.Clone();
            for (int i = 1; i < power; i++)
            {
                result = MultiplyMatrices(result, matrix);
            }

            if (flagShowLog)
            {
                Console.WriteLine($"\n[ МАТРИЦЯ У СТЕПЕНI {power} ]".Pastel("#F8DDFA"));
                result.DrawMatrixInConsole();
            }

            return result;
        }

        public static void DrawMatrixInConsole(this int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j].ToString().Pastel("#FFFFFF") + " ");
                }

                Console.WriteLine();
            }
        }

        private static int[,] MultiplyMatrices(int[,] firstMatrix, int[,] secondMatrix)
        {
            var firstRows = firstMatrix.GetLength(0);
            var firstCols = firstMatrix.GetLength(1);
            var secondRows = secondMatrix.GetLength(0);
            var secondCols = secondMatrix.GetLength(1);

            if (firstCols != secondRows)
            {
                throw new ArgumentException("Matrix dimensions do not allow multiplication.");
            }

            int temp = 0;
            var result = new int[firstRows, secondCols];

            for (int i = 0; i < firstRows; i++)
            {
                for (int j = 0; j < secondCols; j++)
                {
                    result[i, j] = 0;
                    for (int k = 0; k < firstCols; k++)
                    {
                        result[i, j] += firstMatrix[i, k] * secondMatrix[k, j];
                    }
                }
            }

            return result;
        }

        public static int[,] BooleanTransform(this int[,] matrix)
        {
            int sizeX = matrix.GetLength(0);
            int sizeY = matrix.GetLength(1);
            int[,] booleanTransMatrix = new int[sizeX, sizeY];

            for (int i = 0; i < sizeX; i++)
                for (int j = 0; j < sizeY; j++)
                    matrix[i, j] = (matrix[i, j] > 0) ? 1 : 0;

            return booleanTransMatrix;
        }

        public static int[,] SumMatrix(this int[,] matrix1, int[,] matrix2)
        {
            if (matrix1.GetLength(0) != matrix2.GetLength(0) || matrix1.GetLength(1) != matrix2.GetLength(1))
                throw new ArgumentException("Both matrices must have the same dimensions.");

            var rows = matrix1.GetLength(0);
            var cols = matrix1.GetLength(1);
            var result = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }

            return result;
        }

        public static int[,] GetReachabilityMatrix(int[,] matrix)
        {
            int size = matrix.GetLength(0);
            int[,] reachabilityMatrix = IdentityMatrix(matrix);
            int[,] temporaryMatrix = new int[size, size];

            for (int i = 1; i < size; i++)
            {
                temporaryMatrix = reachabilityMatrix.SumMatrix(matrix.Power(i, true));
                reachabilityMatrix = temporaryMatrix;
            }

            SeparateStrings();

            Console.WriteLine("Матриця досяжностi (перед булевою трансформацiєю):".Pastel("#F8DDFA"));
            reachabilityMatrix.DrawMatrixInConsole();
            Console.WriteLine();

            reachabilityMatrix.BooleanTransform();

            return reachabilityMatrix;
        }

        public static int[,] GetReachabilityMatrixShort(int[,] matrix)
        {
            int size = matrix.GetLength(0);
            int[,] identityMatrix = IdentityMatrix(matrix);
            int[,] reachabilityMatrix = new int[size, size];
            int[,] temporaryMatrix = new int[size, size];

            for (int i = 1; i < size; i++)
            {
                temporaryMatrix = matrix.SumMatrix(identityMatrix).Power(size - 1);
                reachabilityMatrix = temporaryMatrix;
            }

            Console.WriteLine("[КОРОТКИЙ МЕТОД] Матриця досяжностi (перед булевою трансформацiєю):".Pastel("#F8DDFA"));
            reachabilityMatrix.DrawMatrixInConsole();
            Console.WriteLine();

            reachabilityMatrix.BooleanTransform();

            return reachabilityMatrix;
        }

        public static bool IsMatrixEqualsToFullOnes(this int[,] matrix)
        {
            int size = matrix.GetLength(0);
            int[,] fullOnesMatrix = MatrixCompletelyWithOnes(matrix);

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (matrix[i, j] != fullOnesMatrix[i, j])
                        return false;

            return true;
        }

        public static int[,] GetTransposeMatrix(int[,] matrix)
        {
            var rows = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            var result = new int[cols, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[j, i] = matrix[i, j];
                }
            }

            return result;
        }

        public static bool IsGraphStronglyConnected(this int[,] matrix)
        {
            return matrix.IsMatrixEqualsToFullOnes();
        }

        public static bool IsGraphUnilaterallyConnected(this int[,] matrix)
        {
            var transponedMatrix = GetTransposeMatrix(matrix);
            var sumMatrixWithTransponed = matrix.SumMatrix(transponedMatrix);

            SeparateStrings();

            Console.WriteLine("Сума матрицi досяжностi та її транспонованої: ".Pastel("#F8DDFA"));
            sumMatrixWithTransponed.DrawMatrixInConsole();

            SeparateStrings();

            sumMatrixWithTransponed.BooleanTransform();

            return sumMatrixWithTransponed.IsMatrixEqualsToFullOnes();
        }

        public static bool IsGraphWeaklyConnected(this int[,] matrix)
        {
            var transponedMatrix = GetTransposeMatrix(matrix);
            var sumMatrixWithTransponed = matrix.SumMatrix(IdentityMatrix(matrix)).SumMatrix(transponedMatrix);
            var multipliedMatrix = sumMatrixWithTransponed.Power(matrix.GetLength(0) - 1);

            SeparateStrings();

            Console.WriteLine("Сума одиничної, початкової та транспонованої матриць: ".Pastel("#F8DDFA"));
            sumMatrixWithTransponed.DrawMatrixInConsole();

            Console.WriteLine("\nВихiдна матриця (для перевiрки) в степенi (n-1): ".Pastel("#F8DDFA"));
            multipliedMatrix.DrawMatrixInConsole();

            SeparateStrings();

            multipliedMatrix.BooleanTransform();

            return multipliedMatrix.IsMatrixEqualsToFullOnes();
        }

        public static void SeparateStrings()
        {
            Console.WriteLine();
            Console.WriteLine("=======================================================================".Pastel("#F8DDFA"));
            Console.WriteLine();
        }
    }
}
