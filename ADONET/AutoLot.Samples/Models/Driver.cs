using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Samples.Models;

[Table("Drivers", Schema="dbo")]
[EntityTypeConfiguration(typeof(DriverConfiguration))]
public class Driver : BaseEntity
{
    public Person PersonInfo { get; set; } = new();

    [InverseProperty(nameof(Car.Drivers))]
    // A driver can 'have' multiple cars.
    public IEnumerable<Car> Cars { get; set; } = [];

    [InverseProperty(nameof(CarDriver.DriverNavigation))] // both Car and Driver are inverse to CarDriver
    public IEnumerable<CarDriver> CarDrivers { get; set; } = [];
}
