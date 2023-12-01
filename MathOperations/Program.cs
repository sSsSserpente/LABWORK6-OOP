class MathOperations
{
    // Overloaded method for addition with two numbers
    public static T Add<T>(T a, T b)
    {
        dynamic dynamicA = a;
        dynamic dynamicB = b;
        return dynamicA + dynamicB;
    }

    // Overloaded method for addition with arrays
    public static T[] Add<T>(T[] array1, T[] array2)
    {
        if (array1.Length != array2.Length)
        {
            throw new ArgumentException("Arrays must have the same length for addition.");
        }

        T[] result = new T[array1.Length];

        for (int i = 0; i < array1.Length; i++)
        {
            result[i] = Add(array1[i], array2[i]);
        }

        return result;
    }

    // Overloaded method for addition with matrices
    public static T[,] Add<T>(T[,] matrix1, T[,] matrix2)
    {
        int rows = matrix1.GetLength(0);
        int columns = matrix1.GetLength(1);

        if (rows != matrix2.GetLength(0) || columns != matrix2.GetLength(1))
        {
            throw new ArgumentException("Matrices must have the same dimensions for addition.");
        }

        T[,] result = new T[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                result[i, j] = Add(matrix1[i, j], matrix2[i, j]);
            }
        }

        return result;
    }

    // Additional overloaded methods for subtraction, multiplication, etc. can be added similarly
}

class Program
{
    static void Main()
    {
        // Example usage
        int num1 = 5;
        int num2 = 10;
        int sum1 = MathOperations.Add(num1, num2);
        Console.WriteLine($"Sum of {num1} and {num2}: {sum1}");

        double[] array1 = { 1.5, 2.5, 3.5 };
        double[] array2 = { 0.5, 1.5, 2.5 };
        double[] sum2 = MathOperations.Add(array1, array2);
        Console.WriteLine("Sum of arrays: [" + string.Join(", ", sum2) + "]");

        int[,] matrix1 = { { 1, 2 }, { 3, 4 } };
        int[,] matrix2 = { { 5, 6 }, { 7, 8 } };
        int[,] sum3 = MathOperations.Add(matrix1, matrix2);
        Console.WriteLine("Sum of matrices:");
        for (int i = 0; i < sum3.GetLength(0); i++)
        {
            for (int j = 0; j < sum3.GetLength(1); j++)
            {
                Console.Write(sum3[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
