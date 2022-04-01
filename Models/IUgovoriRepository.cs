using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCappCoreWeb.Models
{
    public interface IUgovoriRepository
    {
        Ugovori GetUgovor(int Id);
        IEnumerable<Ugovori> GetAllUgovore();      
    }
}
