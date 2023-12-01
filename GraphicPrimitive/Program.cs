// Abstract class representing a graphic primitive
abstract class GraphicPrimitive
{
    // Coordinates
    public int X { get; set; }
    public int Y { get; set; }

    // Abstract method to draw the graphic primitive
    public abstract void Draw();

    // Method to move the graphic primitive
    public void Move(int x, int y)
    {
        X += x;
        Y += y;
        Console.WriteLine($"Moved to new coordinates: ({X}, {Y})");
    }

    // Method to scale the graphic primitive
    public virtual void Scale(float factor)
    {
        Console.WriteLine("Scaling not supported for this primitive.");
    }
}

// Derived class Circle
class Circle : GraphicPrimitive
{
    public int Radius { get; set; }

    public override void Draw()
    {
        Console.WriteLine($"Drawing Circle at coordinates ({X}, {Y}) with radius {Radius}");
    }

    public override void Scale(float factor)
    {
        Radius = (int)(Radius * factor);
        Console.WriteLine($"Circle scaled. New radius: {Radius}");
    }
}

// Derived class Rectangle
class Rectangle : GraphicPrimitive
{
    public int Width { get; set; }
    public int Height { get; set; }

    public override void Draw()
    {
        Console.WriteLine($"Drawing Rectangle at coordinates ({X}, {Y}) with width {Width} and height {Height}");
    }

    public override void Scale(float factor)
    {
        Width = (int)(Width * factor);
        Height = (int)(Height * factor);
        Console.WriteLine($"Rectangle scaled. New width: {Width}, New height: {Height}");
    }
}

// Derived class Triangle
class Triangle : GraphicPrimitive
{
    public override void Draw()
    {
        Console.WriteLine($"Drawing Triangle at coordinates ({X}, {Y})");
    }
}

// Derived class Group
class Group : GraphicPrimitive
{
    private List<GraphicPrimitive> primitives;

    public Group()
    {
        primitives = new List<GraphicPrimitive>();
    }

    // Method to add a graphic primitive to the group
    public void AddPrimitive(GraphicPrimitive primitive)
    {
        primitives.Add(primitive);
    }

    public override void Draw()
    {
        Console.WriteLine($"Drawing Group at coordinates ({X}, {Y})");
        foreach (var primitive in primitives)
        {
            primitive.Draw();
        }
    }

    public override void Move(int x, int y)
    {
        base.Move(x, y);
        foreach (var primitive in primitives)
        {
            primitive.Move(x, y);
        }
    }
}

// GraphicsEditor class
class GraphicsEditor
{
    private List<GraphicPrimitive> primitives;

    public GraphicsEditor()
    {
        primitives = new List<GraphicPrimitive>();
    }

    // Method to add a graphic primitive to the editor
    public void AddPrimitive(GraphicPrimitive primitive)
    {
        primitives.Add(primitive);
    }

    // Method to draw all graphic primitives in the editor
    public void DrawAll()
    {
        foreach (var primitive in primitives)
        {
            primitive.Draw();
        }
    }

    // Method to move all graphic primitives in the editor
    public void MoveAll(int x, int y)
    {
        foreach (var primitive in primitives)
        {
            primitive.Move(x, y);
        }
    }
}

class Program
{
    static void Main()
    {
        // Example usage

        // Create graphic primitives
        Circle circle = new Circle { X = 10, Y = 20, Radius = 5 };
        Rectangle rectangle = new Rectangle { X = 30, Y = 40, Width = 8, Height = 12 };
        Triangle triangle = new Triangle { X = 50, Y = 60 };

        // Create a group and add primitives to it
        Group group = new Group();
        group.AddPrimitive(circle);
        group.AddPrimitive(rectangle);
        group.AddPrimitive(triangle);

        // Create a graphics editor and add primitives to it
        GraphicsEditor editor = new GraphicsEditor();
        editor.AddPrimitive(circle);
        editor.AddPrimitive(rectangle);
        editor.AddPrimitive(triangle);
        editor.AddPrimitive(group);

        // Draw all primitives in the editor
        editor.DrawAll();

        // Move all primitives in the editor
        editor.MoveAll(5, 5);

        // Scale a specific primitive
        circle.Scale(1.5f);

        // Draw all primitives in the editor after scaling
        editor.DrawAll();
    }
}
