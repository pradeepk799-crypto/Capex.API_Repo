using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Infrastructure.Interfaces
{
    public interface IDBLogger
    {
        void AddErrorLog(string errorLog);
    }
}
