using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Vehicles.Core;

namespace Vehicles.WpfApp;

public partial class MainWindow : Window
{
    public ObservableCollection<Vehicle> Vehicles { get; } = new();

    public MainWindow()
    {
        InitializeComponent();
        VehiclesListBox.ItemsSource = Vehicles;

        Vehicles.Add(new Car("Volvo", "V60", 0));
        Vehicles.Add(new Boat("Bella", "600", 0));
        Vehicles.Add(new AmphibiousCar("Amphi", "X", 0));

        VehiclesListBox.SelectedIndex = 0;
    }

    private Vehicle? SelectedVehicle => VehiclesListBox.SelectedItem as Vehicle;

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
        if (SelectedVehicle == null)
        {
            SelectedTitleText.Text = Resource1.DefaultSelectPrompt;
            OdometerText.Text = Resource1.OdometerEmpty;
            DriveButton.IsEnabled = false;
            SwimButton.IsEnabled = false;
            RemoveButton.IsEnabled = false;
            return;
        }

        SelectedTitleText.Text = string.Format(Resource1.SelectedFormat, SelectedVehicle.Make, SelectedVehicle.Model);
        OdometerText.Text = string.Format(Resource1.OdometerFormat, SelectedVehicle.Odometer);

        DriveButton.IsEnabled = SelectedVehicle is IDriveable;
        SwimButton.IsEnabled = SelectedVehicle is ISwimmable;
        RemoveButton.IsEnabled = true;
    }

    private void VehiclesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UpdateDetails();
    }

    private void Drive_Click(object sender, RoutedEventArgs e)
    {
        if (SelectedVehicle is IDriveable driver)
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
        if (SelectedVehicle is ISwimmable swimmer)
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

    private void Remove_Click(object sender, RoutedEventArgs e)
    {
        if (SelectedVehicle != null)
        {
            Vehicles.Remove(SelectedVehicle);
        }
    }

    private void AddCar_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var car = new Car(NewMakeBox.Text, NewModelBox.Text);
            Vehicles.Add(car);
            VehiclesListBox.SelectedItem = car;
            AddToLog(string.Format(Resource1.LogAddedCar, car));
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message, Resource1.ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private void AddBoat_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var boat = new Boat(NewMakeBox.Text, NewModelBox.Text);
            Vehicles.Add(boat);
            VehiclesListBox.SelectedItem = boat;
            AddToLog(string.Format(Resource1.LogAddedBoat, boat));
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message, Resource1.ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private void AddAmphibian_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var amphi = new AmphibiousCar(NewMakeBox.Text, NewModelBox.Text);
            Vehicles.Add(amphi);
            VehiclesListBox.SelectedItem = amphi;
            AddToLog(string.Format(Resource1.LogAddedAmphi, amphi));
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message, Resource1.ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}