namespace Vehicles.Core;

public class AmphibiousCar : Vehicle, IDriveable, ISwimmable
{
    public AmphibiousCar(string make, string model, double initialOdometer = 0)
        : base(make, model, initialOdometer) { }

    public string Drive(double km)
    {
        if (km <= 0)
            throw new ArgumentException(Resource1.ErrDistanceInvalid);

        Odometer += km;
        return string.Format(Resource1.LogAmphiDrive, Make, km, Odometer);
    }

    public string Swim(double km)
    {
        if (km <= 0)
            throw new ArgumentException(Resource1.ErrDistanceInvalid);

        Odometer += km;
        return string.Format(Resource1.LogAmphiSwim, Make, km, Odometer);
    }

    public override string Move(double km) => Drive(km);
}
