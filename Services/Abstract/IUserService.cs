using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dytsenayasar.Models;
using dytsenayasar.DataAccess.Entities;

namespace dytsenayasar.Services.Abstract
{
    public interface IUserService
    {
        Task<User> Register(User user);
        Task<UserModel> Login(string pidOrEmail, string password);
        Task<User> Get(Guid id);
        Task<User> Get(string pidOrEmail);
        Task<ICollection<User>> Get(ICollection<Guid> ids);
        Task<User> Update(Guid id, UserModel user, string password = null);
        Task<UserForm> UserForm(UserForm userForm, Guid id);
        Task<bool> ActivateUser(User user);
        Task<bool> UpdatePassword(User user, string password);
        Task<Guid?> UpdateImage(Guid id, Guid imgId);
        Task<User> UpdatePassword(Guid id, string oldPassword, string newPassword);
        Task<ICollection<User>> Find(UserFindParametersModel parameters, int limit = 20, int offset = 0);
        Task<long> FindCount(UserFindParametersModel parameters);
        Task<bool> SaveClientId(Guid userId, string clientId);
        Task<ICollection<string>> GetClientIds(ICollection<Guid> userIds);
        IQueryable<UserMembershipModel> CreateUserMembershipTableQuery();
        
    }
}