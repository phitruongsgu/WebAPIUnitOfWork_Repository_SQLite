using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UOWPocketBookAPI.Core.IRepositories;
using UOWPocketBookAPI.Data;
using UOWPocketBookAPI.Models;

namespace UOWPocketBookAPI.Core.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context, ILogger logger) : base(context,logger)
        {
        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                return await dbSet.ToListAsync();
            }catch(Exception ex)
            {
                _logger.LogError(ex,"{Repo} All method error",typeof(UserRepository));
                return new List<User>();
            }
        }

        public override async Task<bool> Update(User user)
        {
            try
            {
                var existUser = await dbSet.Where(x => x.Id == user.Id).FirstOrDefaultAsync();  
                if(existUser == null)
                {
                    return await Add(user);
                }
                existUser.FirstName = user.FirstName;
                existUser.LastName = user.LastName;
                existUser.Email = user.Email;

                return true;
            }
            catch(Exception ex) {
                _logger.LogError(ex, "{Repo} Update method error", typeof(UserRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var existUser = await dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
                if(existUser != null)
                {
                    dbSet.Remove(existUser);
                    return true;
                }

                return false;
            }          
            catch(Exception ex) {
                _logger.LogError(ex, "{Repo} Delete method error", typeof(UserRepository));
                return false;
            }
}
    }
}
