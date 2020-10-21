using System;
using dytsenayasar.Util;
using dytsenayasar.DataAccess.Entities;

namespace dytsenayasar.Models
{
    public class UserModel : AEntityModel<User, UserModel>
    {
         public UserModel()
        {
            ID = Guid.NewGuid();
        }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType? UserType { get; set; }

        public string Token { get; set; }

        public override void SetValuesFromEntity(User entity)
        {
            if(entity == null) return;

            base.SetValuesFromEntity(entity);

            ID = entity.ID;
            Email = entity.Email;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            UserType = entity.UserType;
        }

        public override User ToEntity()
        {
            return new User
            {
                ID = ID,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                UserType = UserType ?? dytsenayasar.DataAccess.Entities.UserType.User,
            };
        }
    }

    public static class UserEntityExtentions
    {
        public static UserModel ToModel(this User user)
        {
            var model = new UserModel();
            model.SetValuesFromEntity(user);
            return model;
        }
    }
}