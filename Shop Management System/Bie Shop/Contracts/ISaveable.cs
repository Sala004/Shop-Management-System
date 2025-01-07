using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bie_Shop.Contracts
{
    internal interface ISaveable
    {
        string ConvertToStringForSaving();
    }
}
