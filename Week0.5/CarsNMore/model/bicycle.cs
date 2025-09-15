namespace CarsNMore.model;
/*
//Team Members: Matthew Sims, Charles Trangay, Stephen A, Vishesh Kumar, Faraz M

TODO: 🔲✅
    ✅Fields {
     gearNumber: int, 
     bikeType: string, 
     hasHandBrake: bool,
     bikeLockCode: string
    }
    🔲Methods
    🔲Constructor
    

Inheritance Tree:
    /Vehicle {milage, passengerCapacity, make, model, resaleValue, VIN}
        /Wheeled Vehicle {numberOfWheels, wheelSize, treadDepth}
           /Bicycle

*/
public class Bicycle : WheeledVehicle  {
    //Fields
    public int gearNumber {get; set;}
    public bool hasHandBrake {get; set;}
    public string bikeType {get; set;}
    public string bikeLockCode {get; set;}

    //Constructor
    public Bicycle() : base() {
        gearNumber = 1;
        hasHandBrake = true;
        bikeType = "";
        bikeLockCode = "";
    }
    public Bicycle(int gearNumber, bool hasHandBrake, string bikeType, string bikeLockCode, int wheelSize, double mileage, int newPassengerCapacity, string make, string model, double resaleValue, string VIN, double treadDepth) : base(2, wheelSize, treadDepth, mileage, newPassengerCapacity, make, model, resaleValue, VIN) {
        this.gearNumber = gearNumber;
        this.hasHandBrake = hasHandBrake;
        this.bikeType = bikeType;
        this.bikeLockCode = bikeLockCode;
    }

    //Methods
    public override string ToString() {
        return $"Bike Info: \nGear Number: {this.gearNumber}\nHandel Brakes: {this.hasHandBrake}\nBike Type: {this.bikeType}\nBike Lock Code: {this.bikeLockCode}\nBike Make: {this.make}\nBike Model: {this.model}\nBike VIN: {this.VIN}\nResale: {this.resaleValue}\n";
    }
}