namespace Vehicles.Core;

public class Car : Vehicle, IDriveable
{
    public Car(string make, string model, double initialOdometer = 0)
        : base(make, model, initialOdometer) { }

    public string Drive(double km)
    {
        if (km <= 0)
            throw new ArgumentException(Resource1.ErrDistanceInvalid);

        Odometer += km;
        return string.Format(Resource1.LogCarDrive, Make, km, Odometer);
    }

    public override string Move(double km) => Drive(km);
}
