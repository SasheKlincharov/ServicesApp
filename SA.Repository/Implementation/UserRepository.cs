using SA.Data;
using SA.Data.Entity;
using SA.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<SAUser>> GetAllUsers()
        {
            var users = new List<SAUser>();
            users = context.Users.ToList();

            return await Task.FromResult(users);
        }
    }
}
