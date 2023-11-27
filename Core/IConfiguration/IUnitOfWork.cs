using System.Threading.Tasks;
using UOWPocketBookAPI.Core.IRepositories;

namespace UOWPocketBookAPI.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        Task CompleteAsync();
    }
}
