// namespace declaration - a way to organize code
namespace CarsNMore.model;

// Every class contains memebers
// Members are either fields or methods

public class Vehicle
{
    // Fields - values and variables that the object/class contains
    //milage - double
    public double mileage { get; set; }

    // private double mileage;
    // public double GetMileage() { return mileage; }
    // public void SetMileage(double value) { mileage = value; }

    //passenger capacity - int
    public int passengerCapacity { get; set; }

    //make - string
    public string make { get; set; }

    //model - string
    public string model { get; set; }

    //resaleValue - double
    public double resaleValue { get; set; }

    //VIN - string
    public string VIN { get; set; }


    // Methods - functions or behaviors that the object/class can perform
    // Constructor - a special method that is called when an object is created that defines how to create the object
    public Vehicle()
    {
        mileage = 0;
        passengerCapacity = 1;
        make = "";
        model = "";
        resaleValue = 0;
        VIN = "00000000000000000";
    }

    public Vehicle(double mileage = 0, int passengerCapacity = 1, string make = "", string model = "", double resaleValue = 0, string VIN = "00000000000000000")
    {
        this.mileage = mileage;
        this.passengerCapacity = passengerCapacity;
        this.make = make;
        this.model = model;
        this.resaleValue = resaleValue;
        this.VIN = VIN;
    }

    public override string ToString(){
        return $"Vehicle Info:\nVehicle Passenger Capacity: {this.passengerCapacity}\nVehicle Mileage: {this.mileage}\nVehicle Make: {this.make}\nVehicle Model: {this.model}\nVehicle VIN: {this.VIN}\nResale: {this.resaleValue}\n";
    }

}