using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using UOWPocketBookAPI.Core.IConfiguration;
using UOWPocketBookAPI.Core.IRepositories;
using UOWPocketBookAPI.Core.Repositories;

namespace UOWPocketBookAPI.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;        

        public IUserRepository UserRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context, ILoggerFactory logger)
        {           
            _context = context;
            _logger = logger.CreateLogger("logs");
            UserRepository = new UserRepository(_context, _logger);

        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();   
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
