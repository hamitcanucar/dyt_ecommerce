using System;
using dyt_ecommerce.DataAccess.Entities;
using dytsenayasar.DataAccess.Entities;

namespace dyt_ecommerce.Models
{
    public class UserMembershipModel
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}