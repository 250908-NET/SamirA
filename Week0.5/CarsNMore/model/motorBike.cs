namespace CarsNMore.model;


public class MotorBike : Bicycle  {

    //fields
    public string motorType {get; set;}
    public double gasTankSize {get; set;}

    //method
    //constuctor
    public MotorBike() : base() {
        motorType = "";
        gasTankSize = 0;
    }
    public MotorBike(string motorType, double gasTankSize, int gearNumber, bool hasHandBrake, string bikeType, string bikeLockCode, int wheelSize, double mileage, int newPassengerCapacity, string make, string model, double resaleValue, string VIN, double treadDepth) : base(gearNumber, hasHandBrake, bikeType, bikeLockCode, wheelSize, mileage, newPassengerCapacity, make, model, resaleValue, VIN, treadDepth) {
        this.motorType = motorType;
        this.gasTankSize = gasTankSize;
    }   
}