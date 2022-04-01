using MVCappCoreWeb.EfDbLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCappCoreWeb.Models
{
    public  class SQLUgovoriRepository
    {
        private readonly OrbisDbContext _context;
        public SQLUgovoriRepository(OrbisDbContext context)
        {
            _context = context;
        }
        //public IEnumerable<Ugovori> GetAllUgovori()
        //{
        //    return _context.ugovori;
        //}
        //public Ugovori GetUgovori(int Id)
        //{
        //    return _context.ugovori.Find(Id);
        //}

    }
}
