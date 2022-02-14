using SA.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Repository.Interface
{
    public interface IUserRepository
    {
        Task<List<SAUser>> GetAllUsers();
    }
}
