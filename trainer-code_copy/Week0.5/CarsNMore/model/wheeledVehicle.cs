namespace CarsNMore.model;

public class WheeledVehicle : Vehicle
{
    // Fields
    // number of wheels - int
    public int numberOfWheels { get; set; }
    // wheel size - int
    public int wheelSize { get; set; }
    // treadDepth - double
    public double treadDepth { get; set; }

    // Methods
    // Constructor
    public WheeledVehicle() : base()
    {
        numberOfWheels = 1;
        wheelSize = 0;
        treadDepth = 0;
    }

    public WheeledVehicle(int numberOfWheels, int wheelSize, double treadDepth, double mileage, int passengerCapacity, string make, string model, double resaleValue, string VIN) : base( mileage, passengerCapacity, make, model, resaleValue, VIN)
    {
        this.numberOfWheels = numberOfWheels;
        this.wheelSize = wheelSize;
        this.treadDepth = treadDepth;
    }

}