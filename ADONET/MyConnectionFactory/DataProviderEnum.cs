using System;
using System.Collections.Generic;
using System.Text;

namespace MyConnectionFactory
{
    internal enum DataProviderEnum { 
        SqlServer,
        #if PC
            OleDb,
        #endif
        Odbc,
        None
    }
}
