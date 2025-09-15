using CarsNMore.model;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Cars N More Starting...");
        Car myCar = new Car();
        Console.WriteLine("new car created");

        myCar.make = "Toyota";
        myCar.model = "Corolla";

        Console.WriteLine(myCar.describeCar());

        //Bicycle
        Bicycle bicycle = new Bicycle();
        bicycle.gearNumber = 5;
        Console.WriteLine(bicycle);
        //MotorBike: Bicycle Child
        MotorBike motorBike = new MotorBike();

        //Boat
        Boat myBoat = new Boat();
        myBoat.maxKnotSpeed = 15;
        myBoat.hasEngine = true;
        myBoat.shipUse = "Cargo";
        myBoat.maxCargoCapacity = 200;
        myBoat.crewSize = 20;

        Console.WriteLine(myBoat);
    }
}