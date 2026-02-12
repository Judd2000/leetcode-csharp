using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLot.DalLib.BulkImport;

public interface IMyDataReader<T> : IDataReader
{
    List<T> Records { get; set; }
}
