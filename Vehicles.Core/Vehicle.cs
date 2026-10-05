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

    public virtual string Move(double km)
    {
        if (km <= 0)
            throw new ArgumentException(Resource1.ErrDistanceInvalid);

        Odometer += km;
        return string.Format(Resource1.LogMoveResult, ToString(), km, Odometer);
    }

    public virtual string Honk() => string.Format(Resource1.LogHonk, ToString());

    public virtual string FlashLights() => string.Format(Resource1.LogFlash, ToString());

    public override string ToString() => $"{Make} {Model}";
}

