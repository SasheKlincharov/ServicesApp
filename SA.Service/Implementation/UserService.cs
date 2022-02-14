using Microsoft.AspNetCore.Mvc.Rendering;
using SA.Repository.Interface;
using SA.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<List<SelectListItem>> GetAllUsers()
        {
            var allUsers = await this.userRepository.GetAllUsers();

            var result = new List<SelectListItem>();

            foreach(var user in allUsers)
            {
                result.Add(new SelectListItem() { Text = user.UserName, Value = user.Id });
            }

            return await Task.FromResult(result);
        }
    }
}
