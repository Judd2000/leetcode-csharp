using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Samples.Models;


[Table("Makes", Schema="dbo")]
public class Make : BaseEntity
{
    [Required, StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [InverseProperty(nameof(Car.MakeNavigation))]
    public IEnumerable<Car> Cars { get; set; } = [];
}
