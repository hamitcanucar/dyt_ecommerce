using System;
using System.ComponentModel.DataAnnotations;
using dyt_ecommerce.DataAccess.Entities;
using dytsenayasar.DataAccess.Entities;

namespace dyt_ecommerce.Models.ControllerModels.UserControllerModels
{
    public class UpdateUserRequestModel : AControllerEntityModel<UserModel>
    {
        [MinLength(11), MaxLength(11)]
        public string PersonalId { get; set; }

        [EmailAddress, MaxLength(64)]
        public string Email { get; set; }

        [MinLength(4), MaxLength(128)]
        public string Password { get; set; }

        [MaxLength(64)]
        public string FirstName { get; set; }

        [MaxLength(64)]
        public string LastName { get; set; }

        public DateTime? BirthDay { get; set; }

        [MaxLength(32)]
        public string Phone { get; set; }

        public GenderType? Gender { get; set; }

        public UserType? UserType { get; set; }
        public bool? Active { get; set; }

        public override UserModel ToModel()
        {
            return new UserModel
            {
                PersonalId = PersonalId,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Phone = Phone,
                BirthDay = BirthDay,
                Gender = Gender,
                UserType = UserType,
                Active = Active
            };
        }
    }
}