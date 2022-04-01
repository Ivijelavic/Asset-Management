using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCappCoreWeb.Models
{
    public class MockUgovoriRepository : IUgovoriRepository
    {
        private List<Ugovori> _ugovori;

        public MockUgovoriRepository(List<Ugovori> ugovori)
        {
            _ugovori = ugovori;
        }

        public IEnumerable<Ugovori> GetAllUgovore()
        {
            return _ugovori;
        }

        public Ugovori GetUgovor(int Id)
        {
            return this._ugovori.FirstOrDefault(e => e.IDUgovora == Id);
        }
    }
}
