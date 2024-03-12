
public class Gauss
{
    public static void SwapRows(float[,] matrix, int row1, int row2)
    {
        int cols = matrix.GetLength(1);
        for (int i = 0; i < cols; i++)
        {
            float temp = matrix[row1, i];
            matrix[row1, i] = matrix[row2, i];
            matrix[row2, i] = temp;
        }
    }

    public static float[,] AddVectorToMatrix(float[,] matrix, float[] vector)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        float[,] result = new float[rows, cols + 1];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[i, j] = matrix[i, j];
            }
            result[i, cols] = vector[i];
        }
        return result;
    }


    public static float[] GaussWithoutMainElement(float[,] matrix, float[] vector)
    {
        float[,] fullMatrix = AddVectorToMatrix(matrix, vector);
        int rows = fullMatrix.GetLength(0);
        int cols = fullMatrix.GetLength(1);

        for (int i = 0; i < rows - 1; i++)
        {
            int firstNonZero = i;
            while (firstNonZero < rows && fullMatrix[firstNonZero, i] == 0)
            {
                firstNonZero++;
            }

            if (firstNonZero == rows)
            {
                continue;
            }

            SwapRows(fullMatrix, i, firstNonZero);

            for (int j = i + 1; j < rows; j++)
            {
                float factor = fullMatrix[j, i] / fullMatrix[i, i];
                for (int k = i; k < cols; k++)
                {
                    fullMatrix[j, k] -= factor * fullMatrix[i, k];
                }
            }
        }

        float[] solution = new float[rows];
        for (int i = rows - 1; i >= 0; i--)
        {
            float sum = 0;
            for (int j = i + 1; j < cols - 1; j++)
            {
                sum += fullMatrix[i, j] * solution[j];
            }
            solution[i] = (fullMatrix[i, cols - 1] - sum) / fullMatrix[i, i];
        }
        return solution;
    }

    public static float[] GaussWithMainElement(float[,] matrix, float[] vector)
    {
        float[,] fullMatrix = AddVectorToMatrix(matrix, vector);
        int rows = fullMatrix.GetLength(0);
        int cols = fullMatrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            int maxRow = i;
            float maxVal = Math.Abs(fullMatrix[i, i]);

            for (int j = i + 1; j < rows; j++)
            {
                float absVal = Math.Abs(fullMatrix[j, i]);
                if (absVal > maxVal)
                {
                    maxVal = absVal;
                    maxRow = j;
                }
            }

            if (maxRow != i)
            {
                SwapRows(fullMatrix, i, maxRow);
            }

            for (int j = i + 1; j < rows; j++)
            {
                float factor = fullMatrix[j, i] / fullMatrix[i, i];
                for (int k = i; k < cols; k++)
                {
                    fullMatrix[j, k] -= factor * fullMatrix[i, k];
                }
            }
        }

        float[] solution = new float[rows];
        for (int i = rows - 1; i >= 0; i--)
        {
            float sum = 0;
            for (int j = i + 1; j < cols - 1; j++)
            {
                sum += fullMatrix[i, j] * solution[j];
            }
            solution[i] = (fullMatrix[i, cols - 1] - sum) / fullMatrix[i, i];
        }
        return solution;
    }

}

    class Program
{

    static void Main(string[] args)
    {
        float[,] matrix = {
            {2.83333f, 5, 1},
            {1.7f, 3, 7},
            {8, 1, 1} 
        };

        float[] vector = { 11.66666f, 13.4f, 18 };

        //float[] solution = Gauss.GaussWithoutMainElement(matrix, vector);
        float[] solution = Gauss.GaussWithMainElement(matrix, vector);

        Console.WriteLine("Решение СЛАУ:");
        for (int i = 0; i < solution.Length; i++)
        {
            Console.WriteLine("x{0} = {1}", i + 1, solution[i]);
        }
    }
}