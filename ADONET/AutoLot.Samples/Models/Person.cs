using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.Samples.Models;

[Owned]
public class Person : BaseEntity
{
    [Required, StringLength(50)]
    public string FirstName { get; set; }

    [Required, StringLength(50)]
    public string LastName { get; set; }
}
