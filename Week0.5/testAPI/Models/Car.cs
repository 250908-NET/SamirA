namespace Models.Car;

public class Car
{
    // Fields
    public int Id { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }

    // Methods
    // Constructor
    public Car() {}

    public Car(int id, string make, string model, int year)
    {
        Id = id;
        Make = make;
        Model = model;
        Year = year;
    }

    public override string ToString()
    {
        return $"{Year} {Make} {Model} (ID: {Id})";
    }
}