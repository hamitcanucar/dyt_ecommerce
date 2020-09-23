using System;
using System.ComponentModel.DataAnnotations;
using dytsenayasar.DataAccess.Entities;

namespace dytsenayasar.Models.ControllerModels.UserControllerModels
{
    public class RegisterRequestModel : AControllerEntityModel<User>
    {
        [Required, MinLength(11), MaxLength(11)]
        public string PersonalId { get; set; }

        [EmailAddress, Required, MaxLength(64)]
        public string Email { get; set; }

        [Required, MinLength(4), MaxLength(128)]
        public string Password { get; set; }

        [MaxLength(64)]
        public string FirstName { get; set; }

        [MaxLength(64)]
        public string LastName { get; set; }

        public DateTime? BirthDay { get; set; }

        [MaxLength(32)]
        public string Phone { get; set; }

        public GenderType? Gender { get; set; }

        public override User ToModel()
        {
            return new User
            {
                PersonalId = PersonalId,
                Email = Email,
                Password = Password,
                FirstName = FirstName,
                LastName = LastName,
                Phone = Phone,
                BirthDay = BirthDay ?? default(DateTime),
                Gender = Gender ?? GenderType.Male,
            };
        }
    }
}