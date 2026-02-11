using System;
using System.Collections.Generic;
using System.Text;

namespace DataProviderFactory;

internal enum DataProviderEnum { 
    SqlServer,
    #if PC
        OleDb,
    #endif
    Odbc,
    None
}
