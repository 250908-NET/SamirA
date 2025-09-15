namespace CarsNMore.model;

public class Car : WheeledVehicle
{
    // Fields
    // number of doors - int
    public int numberOfDoors { get; set; }
    // isElectric - bool
    public bool isElectric { get; set; } = false;
    // hasMirrors - bool
    public bool hasMirrors { get; set; } = true;
    // Methods
    // Constructor
    public Car() : base()
    {
        numberOfDoors = 4;
        isElectric = false;
        hasMirrors = true;
    }

    public Car(int numberOfDoors, bool isElectric, bool hasMirrors, int numberOfWheels, int wheelSize, double treadDepth, double mileage, int newPassengerCapacity, string make, string model, double resaleValue, string VIN) : base(numberOfWheels, wheelSize, treadDepth, mileage, newPassengerCapacity, make, model, resaleValue, VIN)
    {
        this.numberOfDoors = numberOfDoors;
        this.isElectric = isElectric;
        this.hasMirrors = true;
    }

    public string describeCar()
    {
        return $"My car is a {this.make} {this.model}"; // string interpolation
        string concat = "My car is a " + this.make + " " + this.model; // string concatenation
    }
}