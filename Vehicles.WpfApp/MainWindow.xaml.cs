using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Vehicles.Core;

namespace Vehicles.WpfApp;

public partial class MainWindow : Window
{
    public Garage<Vehicle> CarGarage { get; } = new("Autode garaaž");
    public Garage<Vehicle> BoatGarage { get; } = new("Paatide garaaž");

    private Vehicle? selectedVehicle;

    public MainWindow()
    {
        InitializeComponent();

        CarGarage.LogAction += AddToLog;
        BoatGarage.LogAction += AddToLog;

        CarGarageBox.ItemsSource = CarGarage.Vehicles;
        BoatGarageBox.ItemsSource = BoatGarage.Vehicles;

        CarGarage.Add(new Car("Volvo", "V60", 120));
        CarGarage.Add(new Car("Audi", "A6", 50));

        BoatGarage.Add(new Boat("Bella", "600", 45));
        BoatGarage.Add(new AmphibiousCar("Amphi", "X", 10));

        CarGarageBox.SelectedIndex = 0;
    }

    private double ReadDistance()
    {
        if (!double.TryParse(DistanceBox.Text.Trim().Replace(',', '.'),
                             NumberStyles.Any,
                             CultureInfo.InvariantCulture,
                             out double km))
        {
            throw new ArgumentException(Resource1.ErrParseDistance);
        }
        return km;
    }

    private void AddToLog(string msg)
    {
        LogBox.AppendText($"{msg}\n");
        LogBox.ScrollToEnd();
    }

    private void UpdateDetails()
    {
        if (selectedVehicle == null)
        {
            SelectedTitleText.Text = Resource1.DefaultSelectPrompt;
            OdometerText.Text = Resource1.OdometerEmpty;
            MoveButton.IsEnabled = false;
            DriveButton.IsEnabled = false;
            SwimButton.IsEnabled = false;
            return;
        }

        SelectedTitleText.Text = string.Format(Resource1.SelectedFormat, selectedVehicle.Make, selectedVehicle.Model);
        OdometerText.Text = string.Format(Resource1.OdometerFormat, selectedVehicle.Odometer);

        MoveButton.IsEnabled = true;
        DriveButton.IsEnabled = selectedVehicle is IDriveable;
        SwimButton.IsEnabled = selectedVehicle is ISwimmable;
    }

    private void CarGarageBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CarGarageBox.SelectedItem is Vehicle v)
        {
            selectedVehicle = v;
            BoatGarageBox.SelectedIndex = -1;
            UpdateDetails();
        }
    }

    private void BoatGarageBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (BoatGarageBox.SelectedItem is Vehicle v)
        {
            selectedVehicle = v;
            CarGarageBox.SelectedIndex = -1;
            UpdateDetails();
        }
    }

    private void Move_Click(object sender, RoutedEventArgs e)
    {
        if (selectedVehicle == null) return;
        try
        {
            double km = ReadDistance();
            string msg = selectedVehicle.Move(km);
            AddToLog(msg);
            UpdateDetails();
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message, Resource1.ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private void Drive_Click(object sender, RoutedEventArgs e)
    {
        if (selectedVehicle is IDriveable driver)
        {
            try
            {
                double km = ReadDistance();
                string msg = driver.Drive(km);
                AddToLog(msg);
                UpdateDetails();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, Resource1.ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

    private void Swim_Click(object sender, RoutedEventArgs e)
    {
        if (selectedVehicle is ISwimmable swimmer)
        {
            try
            {
                double km = ReadDistance();
                string msg = swimmer.Swim(km);
                AddToLog(msg);
                UpdateDetails();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, Resource1.ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

    private void AddCarToCarGarage_Click(object sender, RoutedEventArgs e)
    {
        var car = new Car("BMW", $"Serie {CarGarage.Vehicles.Count + 1}");
        AddToLog(string.Format(Resource1.LogAddedCar, car));
        CarGarage.Add(car);
        CarGarageBox.SelectedItem = car;
    }

    private void AddAmphiToCarGarage_Click(object sender, RoutedEventArgs e)
    {
        var amphi = new AmphibiousCar("Gibbs", $"Quad {CarGarage.Vehicles.Count + 1}");
        AddToLog(string.Format(Resource1.LogAddedAmphi, amphi));
        CarGarage.Add(amphi);
        CarGarageBox.SelectedItem = amphi;
    }

    private void RemoveCarGarage_Click(object sender, RoutedEventArgs e)
    {
        if (CarGarageBox.SelectedItem is Vehicle v)
        {
            CarGarage.Remove(v);
            selectedVehicle = null;
            UpdateDetails();
        }
    }

    private void AddBoatToBoatGarage_Click(object sender, RoutedEventArgs e)
    {
        var boat = new Boat("Yamaha", $"Cruiser {BoatGarage.Vehicles.Count + 1}");
        AddToLog(string.Format(Resource1.LogAddedBoat, boat));
        BoatGarage.Add(boat);
        BoatGarageBox.SelectedItem = boat;
    }

    private void AddAmphiToBoatGarage_Click(object sender, RoutedEventArgs e)
    {
        var amphi = new AmphibiousCar("Amphi", $"WaterCar {BoatGarage.Vehicles.Count + 1}");
        AddToLog(string.Format(Resource1.LogAddedAmphi, amphi));
        BoatGarage.Add(amphi);
        BoatGarageBox.SelectedItem = amphi;
    }

    private void RemoveBoatGarage_Click(object sender, RoutedEventArgs e)
    {
        if (BoatGarageBox.SelectedItem is Vehicle v)
        {
            BoatGarage.Remove(v);
            selectedVehicle = null;
            UpdateDetails();
        }
    }
}