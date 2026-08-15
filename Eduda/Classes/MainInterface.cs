using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Eduda
{
    public interface MainInterface 
    {
        DataTable AllData(string TableName);


       
        int MaxNo();
    }
}