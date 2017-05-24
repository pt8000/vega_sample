using System.Threading.Tasks;
using VegaApp.Core;

namespace VegaApp.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VegaDbContext _context;
        public UnitOfWork(VegaDbContext _context)
        {
            this._context = _context;
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}