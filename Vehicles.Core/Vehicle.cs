namespace Vehicles.Core;

public abstract class Vehicle
{
    public string Make { get; }
    public string Model { get; }
    public double Odometer { get; protected set; }

    protected Vehicle(string make, string model, double initialOdometer = 0)
    {
        if (string.IsNullOrWhiteSpace(make))
            throw new ArgumentException(Resource1.ErrMakeEmpty);
        if (string.IsNullOrWhiteSpace(model))
            throw new ArgumentException(Resource1.ErrModelEmpty);
        if (initialOdometer < 0)
            throw new ArgumentException(Resource1.ErrOdoNegative);

        Make = make.Trim();
        Model = model.Trim();
        Odometer = initialOdometer;
    }

    public abstract string Move(double km);

    public override string ToString() => $"{Make} {Model}";
}
