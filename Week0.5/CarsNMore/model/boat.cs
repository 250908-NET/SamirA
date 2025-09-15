namespace CarsNMore.model;
//Shane Harvey
//Kendell Rennie
//Kush Gandhi
//Matthew Schade

public class Boat : Vehicle
{
    public double maxKnotSpeed { get; set; }
    public double maxCargoCapacity{ get; set; }
    public bool hasEngine { get; set; } 
    public string shipUse { get; set; } //Cargo, Passenger, Cruise, Military, etc.
    public int crewSize {get; set;}
    
    public Boat() : base()
    {
        this.maxCargoCapacity = 1;
        this.maxKnotSpeed = 1.0;
        this.hasEngine = true;
        this.shipUse = "";
        this.crewSize = 1;
    }

    public Boat(double maxKnotSpeed, double maxCargoCapacity, bool hasEngine, string shipUse, int crewSize, double mileage, int newPassengerCapacity, string make, string model, double resaleValue, string VIN) : base(mileage, newPassengerCapacity, make, model, resaleValue, VIN)
    {
        this.maxKnotSpeed = maxKnotSpeed;
        this.maxCargoCapacity = maxCargoCapacity;
        this.hasEngine = hasEngine;
        this.shipUse = shipUse;
        this.crewSize = crewSize;
    }

    public string describeBoat()
    {
        return $"This is a {this.shipUse} ship that can go {this.maxKnotSpeed} knots, with a carrying capacity of {this.maxCargoCapacity} tons! \n There are {this.crewSize} crew on the ship";
    }

}