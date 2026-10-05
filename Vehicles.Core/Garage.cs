using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Vehicles.Core;

public class Garage<T> where T : Vehicle
{
    public string Name { get; }
    public ObservableCollection<T> Vehicles { get; } = new();

    public event Action<string>? LogAction;

    public Garage(string name)
    {
        Name = name;
    }

    public void Add(T vehicle)
    {
        if (vehicle == null) throw new ArgumentNullException(nameof(vehicle));

        foreach (var other in Vehicles)
        {
            LogAction?.Invoke(other.Honk());
        }

        Vehicles.Add(vehicle);
    }

    public bool Remove(T vehicle)
    {
        if (vehicle == null) return false;

        bool removed = Vehicles.Remove(vehicle);
        if (removed)
        {
            foreach (var remaining in Vehicles)
            {
                LogAction?.Invoke(remaining.FlashLights());
            }
        }
        return removed;
    }
}
