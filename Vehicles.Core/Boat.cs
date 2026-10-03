namespace Vehicles.Core;

public class Boat : Vehicle, ISwimmable
{
    public Boat(string make, string model, double initialOdometer = 0)
        : base(make, model, initialOdometer) { }

    public string Swim(double km)
    {
        if (km <= 0)
            throw new ArgumentException(Resource1.ErrDistanceInvalid);

        Odometer += km;
        return string.Format(Resource1.LogBoatSwim, Make, km, Odometer);
    }

    public override string Move(double km) => Swim(km);
}
