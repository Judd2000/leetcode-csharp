using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.DalLib.Models;

public class Car
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MakeId { get; set; }
    public string Color { get; set; }
    public byte[] TimeStamp { get; set; }
}
