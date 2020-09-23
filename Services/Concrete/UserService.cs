using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using dytsenayasar.Models;
using dytsenayasar.Util;
using dytsenayasar.Context;
using dytsenayasar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Collections.Generic;
using dytsenayasar.Services.Abstract;
using Microsoft.Extensions.Configuration;

namespace dytsenayasar.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<UserModel> Login(string pidOrEmail, string password)
        {
            var hashedPass = password.HashToSha256();

            var query = from u in _context.Users
                        where (u.PersonalId == pidOrEmail || u.Email == pidOrEmail) && u.Password == hashedPass
                        select u;

            var user = await query.AsNoTracking().FirstOrDefaultAsync();

            if (user == null)
                return null;

            var result = user.ToModel();

            if (user.Active)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["JwtKey"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]{
                        new Claim(JWTUser.ID, result.ID.ToString()),
                        new Claim(ClaimTypes.Role, result.UserType.ToString()),
                    }),
                    Issuer = "false", //_appSettings.Issuer,
                    Expires = DateTime.UtcNow.AddHours(1), //DateTime.UtcNow.AddMinutes(_appSettings.JwtValidityInMinutes),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                result.Token = tokenHandler.WriteToken(token);
            }

            return result;
        }

        public async Task<User> Register(User user)
        {
            //check unique columns
            var result = await _context.Users.AnyAsync(u => u.PersonalId == user.PersonalId || u.Email == user.Email);

            if (result)
            {
                return null;
            }

            user.Password = user.Password.HashToSha256();
            user.UserType = UserType.User;

            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Get(Guid id)
        {
            var query = from u in _context.Users
                        where Guid.Equals(u.ID, id)
                        select u;

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<User> Get(string pidOrEmail)
        {
            var query = from u in _context.Users
                        where u.PersonalId == pidOrEmail || u.Email == pidOrEmail
                        select u;

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<ICollection<User>> Get(ICollection<Guid> ids)
        {
            var query = from u in _context.Users
                        where ids.Contains(u.ID)
                        select u;

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<ICollection<User>> Find(UserFindParametersModel parameters, int limit = 20, int offset = 0)
        {
            return await CreateUserFindQuery(parameters)
                .ToPage(limit, offset)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<long> FindCount(UserFindParametersModel parameters)
        {
            return CreateUserFindQuery(parameters).LongCountAsync();
        }

        public IQueryable<User> CreateUserFindQuery(UserFindParametersModel parameters)
        {
            var query = from u in _context.Users
                select new {
                    User = u
                };

            if (parameters.Gender.HasValue)
            {
                query = query.Where(x => x.User.Gender == parameters.Gender.Value);
            }
            if (parameters.UserType.HasValue)
            {
                query = query.Where(x => x.User.UserType == parameters.UserType.Value);
            }
            if (parameters.BirthDate.HasValue)
            {
                query = query.Where(x => x.User.BirthDay >= parameters.BirthDate.Value);
            }
            
            if (!String.IsNullOrEmpty(parameters.Phone))
            {
                query = query.Where(x => EF.Functions.Like(x.User.Phone, $"%{parameters.Phone}%"));
            }
            if (!String.IsNullOrEmpty(parameters.SearchValue))
            {
                var pattern = $"%{parameters.SearchValue.ToLower()}%";
                query = query.Where(x =>
                    EF.Functions.Like(x.User.PersonalId.ToLower(), pattern)
                    || EF.Functions.Like(x.User.Email.ToLower(), pattern)
                    || EF.Functions.Like(x.User.FirstName.ToLower() + " " + x.User.LastName.ToLower(), pattern)
                );
            }

            return query.Select(x => x.User).Distinct();
        }

        public async Task<User> Update(Guid id, UserModel model, string password = null)
        {
            var result = await _context.Users.Where(u => Guid.Equals(u.ID, id)).FirstOrDefaultAsync();

            if (result == null)
                return new User { ID = Guid.Empty };

            result = await SetUpdateValues(result, model, password);
            if (result == null) return null;

            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Guid?> UpdateImage(Guid id, Guid imgId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.ID == id);
            if (user == null) return Guid.Empty;

            var oldImgId = user.Image;
            user.Image = imgId;
            await _context.SaveChangesAsync();
            return oldImgId;
        }

        private async Task<User> SetUpdateValues(User user, UserModel model, string password = null)
        {
            if ((!String.IsNullOrEmpty(model.PersonalId)) || !String.IsNullOrEmpty(model.Email))
            {
                //check unique columns
                var query = from u in _context.Users
                            where u.PersonalId == model.PersonalId || u.Email == model.Email
                            select new
                            {
                                u.PersonalId,
                                u.Email
                            };
                var conflicts = await query.AsNoTracking().ToListAsync();

                foreach (var c in conflicts)
                {
                    if (c.PersonalId == model.PersonalId && user.PersonalId != model.PersonalId)
                    {
                        return null;
                    }

                    if (c.Email == model.Email && user.Email != model.Email)
                    {
                        return null;
                    }
                }
            }

            user.PersonalId = (String.IsNullOrEmpty(model.PersonalId)) ? user.PersonalId : model.PersonalId;
            user.Email = (String.IsNullOrEmpty(model.Email)) ? user.Email : model.Email;
            user.Password = (String.IsNullOrEmpty(password)) ? user.Password : password.HashToSha256();
            user.FirstName = (String.IsNullOrEmpty(model.FirstName)) ? user.FirstName : model.FirstName;
            user.LastName = (String.IsNullOrEmpty(model.LastName)) ? user.LastName : model.LastName;
            user.BirthDay = model.BirthDay ?? user.BirthDay;
            user.Phone = (String.IsNullOrEmpty(model.Phone)) ? user.Phone : model.Phone;
            user.Gender = model.Gender ?? user.Gender;

            return user;
        }

        public async Task<bool> UpdatePassword(User user, string password)
        {
            _context.Users.Attach(user);
            user.Password = password.HashToSha256();
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> ActivateUser(User user)
        {
            _context.Users.Attach(user);
            user.Active = true;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<User> UpdatePassword(Guid id, string oldPassword, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.ID == id);
            if (user == null) return null;

            if (oldPassword.HashToSha256() != user.Password) return new User { ID = Guid.Empty };
            user.Password = newPassword.HashToSha256();
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> SaveClientId(Guid userId, string clientId)
        {
            var uc = new UserClient { UserId = userId, ClientId = clientId };

            _context.UserClients.RemoveRange(_context.UserClients.Where(x => x.UserId == userId || x.ClientIdHash == uc.ClientIdHash));
            _context.UserClients.Add(uc);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<ICollection<string>> GetClientIds(ICollection<Guid> userIds)
        {
            return await _context.UserClients
                .Where(x => userIds.Contains(x.UserId))
                .Select(x => x.ClientId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}