using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Samples.Models;

[Table("Inventory", Schema = "dbo")]
[Index(nameof(MakeId), Name = "IX_Inventory_MakeId")]
[EntityTypeConfiguration(typeof(CarConfiguration))] // need to add this reference to CarConfiguration as well
public class Car : BaseEntity
{
    private string _color;
    [Required, StringLength(50)]
    public string Color {
        get => _color;
        set => _color = value;
    }
    [Required, StringLength(50)]
    public string PetName { get; set; } = string.Empty;
    public int MakeId { get; set; }

    [ForeignKey(nameof(MakeId))] // tell EF Core that the foreign key prop is MakeId
    public Make MakeNavigation { get; set; }

    // Use nullable private backing field with useDefault Value to get the default database value assigned when 'null'.
    private bool? _isDrivable; // if this is not nullable, the 'default value' will actually override any other values when going to the database.

    public bool IsDrivable { 
        get => _isDrivable ?? true;
        set => _isDrivable = value;
    }

    public Radio RadioNavigation { get; set; }

    [InverseProperty(nameof(Driver.Cars))] // describe that the other end of the many-many relationship is Driver.
    public IEnumerable<Driver> Drivers { get; set; } = [];

    public DateTime? DateBuilt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public string Display { get; set; }

    [InverseProperty(nameof(CarDriver.CarNavigation))]
    public IEnumerable<CarDriver> CarDrivers { get; set; } = [];
}
