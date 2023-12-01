// Abstract class representing a general vehicle
abstract class Vehicle
{
    // Properties
    public double Speed { get; set; }
    public int Capacity { get; set; }

    // Abstract method for movement
    public abstract void Move();
}

// Class representing a human with speed property and move method
class Human
{
    public double Speed { get; set; }

    public void Move()
    {
        Console.WriteLine("Walking...");
    }
}

// Derived class Car from Vehicle
class Car : Vehicle
{
    // Additional property for car
    public string Type { get; set; }

    // Implementation of Move method
    public override void Move()
    {
        Console.WriteLine($"Driving a {Type} with speed {Speed}.");
    }
}

// Derived class Bus from Vehicle
class Bus : Vehicle
{
    // Additional property for bus
    public int PassengerCount { get; set; }

    // Implementation of Move method
    public override void Move()
    {
        Console.WriteLine($"Moving a bus with speed {Speed} and {PassengerCount} passengers.");
    }
}

// Derived class Train from Vehicle
class Train : Vehicle
{
    // Additional property for train
    public string Route { get; set; }

    // Implementation of Move method
    public override void Move()
    {
        Console.WriteLine($"Traveling by train on route {Route} with speed {Speed}.");
    }
}

// Class representing a transport network
class TransportNetwork
{
    // List to store various vehicles
    private List<Vehicle> vehicles = new List<Vehicle>();

    // Method to add a vehicle to the network
    public void AddVehicle(Vehicle vehicle)
    {
        vehicles.Add(vehicle);
    }

    // Method to control the movement of all vehicles in the network
    public void ControlMovement()
    {
        foreach (var vehicle in vehicles)
        {
            vehicle.Move();
        }
    }
}

// Class representing a route with start and end points
class Route
{
    public string StartPoint { get; set; }
    public string EndPoint { get; set; }

    // Method to calculate the optimal route based on the type of transport
    public string CalculateOptimalRoute(Vehicle vehicle)
    {
        // Logic for calculating optimal route based on the type of transport
        return $"Optimal route for {vehicle.GetType().Name}: {StartPoint} to {EndPoint}";
    }
}

// Example Usage
class Program
{
    static void Main()
    {
        // Creating instances of different vehicles
        Car car = new Car { Speed = 60, Capacity = 4, Type = "Sedan" };
        Bus bus = new Bus { Speed = 40, Capacity = 30, PassengerCount = 15 };
        Train train = new Train { Speed = 80, Capacity = 200, Route = "City Express" };

        // Creating a transport network
        TransportNetwork network = new TransportNetwork();

        // Adding vehicles to the network
        network.AddVehicle(car);
        network.AddVehicle(bus);
        network.AddVehicle(train);

        // Controlling the movement of vehicles in the network
        network.ControlMovement();

        // Creating a route and calculating optimal routes for each vehicle
        Route route = new Route { StartPoint = "City A", EndPoint = "City B" };

        Console.WriteLine(route.CalculateOptimalRoute(car));
        Console.WriteLine(route.CalculateOptimalRoute(bus));
        Console.WriteLine(route.CalculateOptimalRoute(train));
    }
}
