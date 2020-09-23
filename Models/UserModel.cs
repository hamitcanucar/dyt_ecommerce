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

        public string PersonalId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Phone { get; set; }
        public GenderType? Gender { get; set; }
        public UserType? UserType { get; set; }
        public bool? Active { get; set; }

        public string Token { get; set; }

        public override void SetValuesFromEntity(User entity)
        {
            if(entity == null) return;

            base.SetValuesFromEntity(entity);

            ID = entity.ID;
            PersonalId = entity.PersonalId;
            Email = entity.Email;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            Phone = entity.Phone;
            BirthDay = entity.BirthDay;
            Gender = entity.Gender;
            UserType = entity.UserType;
            Active = entity.Active;
        }

        public override User ToEntity()
        {
            return new User
            {
                ID = ID,
                PersonalId = PersonalId,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Phone = Phone,
                BirthDay = BirthDay ?? DateTime.MinValue,
                Gender = Gender ?? GenderType.Male,
                UserType = UserType ?? dytsenayasar.DataAccess.Entities.UserType.User,
                Active = Active ?? false,
                Password = PersonalId.HashToSha256()
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