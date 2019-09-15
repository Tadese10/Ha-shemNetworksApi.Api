using Ha_shemNetworksApiCommon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ha_shemNetworksApi.Api.ServiceInterfaces
{
   public interface IUserService
    {
        User Authenticate(string username, string password);
        (bool, string, User) RegisterNewUser(User user);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
