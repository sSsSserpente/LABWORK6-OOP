class Quaternion
{
    // Quaternion components
    public double W { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    // Constructor
    public Quaternion(double w, double x, double y, double z)
    {
        W = w;
        X = x;
        Y = y;
        Z = z;
    }

    // Overloaded addition operator for quaternions
    public static Quaternion operator +(Quaternion q1, Quaternion q2)
    {
        return new Quaternion(q1.W + q2.W, q1.X + q2.X, q1.Y + q2.Y, q1.Z + q2.Z);
    }

    // Overloaded subtraction operator for quaternions
    public static Quaternion operator -(Quaternion q1, Quaternion q2)
    {
        return new Quaternion(q1.W - q2.W, q1.X - q2.X, q1.Y - q2.Y, q1.Z - q2.Z);
    }

    // Overloaded multiplication operator for quaternions
    public static Quaternion operator *(Quaternion q1, Quaternion q2)
    {
        double w = q1.W * q2.W - q1.X * q2.X - q1.Y * q2.Y - q1.Z * q2.Z;
        double x = q1.W * q2.X + q1.X * q2.W + q1.Y * q2.Z - q1.Z * q2.Y;
        double y = q1.W * q2.Y - q1.X * q2.Z + q1.Y * q2.W + q1.Z * q2.X;
        double z = q1.W * q2.Z + q1.X * q2.Y - q1.Y * q2.X + q1.Z * q2.W;

        return new Quaternion(w, x, y, z);
    }

    // Method to calculate the norm of the quaternion
    public double Norm()
    {
        return Math.Sqrt(W * W + X * X + Y * Y + Z * Z);
    }

    // Method to calculate the conjugate of the quaternion
    public Quaternion Conjugate()
    {
        return new Quaternion(W, -X, -Y, -Z);
    }

    // Method to calculate the inverse of the quaternion
    public Quaternion Inverse()
    {
        double normSquared = W * W + X * X + Y * Y + Z * Z;

        if (normSquared == 0)
        {
            throw new InvalidOperationException("Cannot invert a quaternion with zero norm.");
        }

        double inverseFactor = 1 / normSquared;

        return new Quaternion(W * inverseFactor, -X * inverseFactor, -Y * inverseFactor, -Z * inverseFactor);
    }

    // Overloaded equality operator for quaternions
    public static bool operator ==(Quaternion q1, Quaternion q2)
    {
        return q1.Equals(q2);
    }

    // Overloaded inequality operator for quaternions
    public static bool operator !=(Quaternion q1, Quaternion q2)
    {
        return !(q1 == q2);
    }

    // Override Equals method for quaternion comparison
    public override bool Equals(object obj)
    {
        if (!(obj is Quaternion))
        {
            return false;
        }

        Quaternion otherQuaternion = (Quaternion)obj;

        return W == otherQuaternion.W && X == otherQuaternion.X && Y == otherQuaternion.Y && Z == otherQuaternion.Z;
    }

    // Override GetHashCode method for quaternion comparison
    public override int GetHashCode()
    {
        return W.GetHashCode() ^ X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
    }

    // Method to convert quaternion to a 3x3 rotation matrix
    public double[,] ToRotationMatrix()
    {
        double[,] matrix = new double[3, 3];

        matrix[0, 0] = 1 - 2 * (Y * Y + Z * Z);
        matrix[0, 1] = 2 * (X * Y - W * Z);
        matrix[0, 2] = 2 * (X * Z + W * Y);

        matrix[1, 0] = 2 * (X * Y + W * Z);
        matrix[1, 1] = 1 - 2 * (X * X + Z * Z);
        matrix[1, 2] = 2 * (Y * Z - W * X);

        matrix[2, 0] = 2 * (X * Z - W * Y);
        matrix[2, 1] = 2 * (Y * Z + W * X);
        matrix[2, 2] = 1 - 2 * (X * X + Y * Y);

        return matrix;
    }

    // Method to convert a 3x3 rotation matrix to a quaternion
    public static Quaternion FromRotationMatrix(double[,] matrix)
    {
        if (matrix.GetLength(0) != 3 || matrix.GetLength(1) != 3)
        {
            throw new ArgumentException("Invalid matrix dimensions for rotation matrix.");
        }

        double trace = matrix[0, 0] + matrix[1, 1] + matrix[2, 2];

        if (trace > 0)
        {
            double s = 0.5 / Math.Sqrt(trace + 1);
            double w = 0.25 / s;
            double x = (matrix[2, 1] - matrix[1, 2]) * s;
            double y = (matrix[0, 2] - matrix[2, 0]) * s;
            double z = (matrix[1, 0] - matrix[0, 1]) * s;

            return new Quaternion(w, x, y, z);
        }
        else if (matrix[0, 0] > matrix[1, 1] && matrix[0, 0] > matrix[2, 2])
        {
            double s = 2 * Math.Sqrt(1 + matrix[0, 0] - matrix[1, 1] - matrix[2, 2]);
            double w = (matrix[2, 1] - matrix[1, 2]) / s;
            double x = 0.25 * s;
            double y = (matrix[0, 1] + matrix[1, 0]) / s;
            double z = (matrix[0, 2] + matrix[2, 0]) / s;

            return new Quaternion(w, x, y, z);
        }
        else if (matrix[1, 1] > matrix[2, 2])
        {
            double s = 2 * Math.Sqrt(1 + matrix[1, 1] - matrix[0, 0] - matrix[2, 2]);
            double w = (matrix[0, 2] - matrix[2, 0]) / s;
            double x = (matrix[0, 1] + matrix[1, 0]) / s;
            double y = 0.25 * s;
            double z = (matrix[1, 2] + matrix[2, 1]) / s;

            return new Quaternion(w, x, y, z);
        }
        else
        {
            double s = 2 * Math.Sqrt(1 + matrix[2, 2] - matrix[0, 0] - matrix[1, 1]);
            double w = (matrix[1, 0] - matrix[0, 1]) / s;
            double x = (matrix[0, 2] + matrix[2, 0]) / s;
            double y = (matrix[1, 2] + matrix[2, 1]) / s;
            double z = 0.25 * s;

            return new Quaternion(w, x, y, z);
        }
    }
}

class Program
{
    static void Main()
    {
        // Example usage

        // Creating quaternions
        Quaternion q1 = new Quaternion(1, 2, 3, 4);
        Quaternion q2 = new Quaternion(5, 6, 7, 8);

        // Performing arithmetic operations
        Quaternion sum = q1 + q2;
        Quaternion difference = q1 - q2;
        Quaternion product = q1 * q2;

        // Displaying results
        Console.WriteLine("Quaternion 1: " + FormatQuaternion(q1));
        Console.WriteLine("Quaternion 2: " + FormatQuaternion(q2));
        Console.WriteLine("Sum: " + FormatQuaternion(sum));
        Console.WriteLine("Difference: " + FormatQuaternion(difference));
        Console.WriteLine("Product: " + FormatQuaternion(product));

        // Calculating norm, conjugate, and inverse
        Console.WriteLine("Norm of Quaternion 1: " + q1.Norm());
        Console.WriteLine("Conjugate of Quaternion 1: " + FormatQuaternion(q1.Conjugate()));
        Console.WriteLine("Inverse of Quaternion 1: " + FormatQuaternion(q1.Inverse()));

        // Comparing quaternions
        Console.WriteLine("Quaternion 1 is equal to Quaternion 2: " + (q1 == q2));
        Console.WriteLine("Quaternion 1 is not equal to Quaternion 2: " + (q1 != q2));

        // Converting quaternion to rotation matrix and back
        double[,] rotationMatrix = q1.ToRotationMatrix();
        Quaternion fromMatrix = Quaternion.FromRotationMatrix(rotationMatrix);

        Console.WriteLine("Rotation Matrix from Quaternion 1: ");
        DisplayMatrix(rotationMatrix);

        Console.WriteLine("Quaternion from Rotation Matrix: " + FormatQuaternion(fromMatrix));
    }

    // Helper method to format quaternion for display
    static string FormatQuaternion(Quaternion q)
    {
        return $"({q.W}, {q.X}, {q.Y}, {q.Z})";
    }

    // Helper method to display a 3x3 matrix
    static void DisplayMatrix(double[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
