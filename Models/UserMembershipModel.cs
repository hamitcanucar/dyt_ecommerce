using System;
using dytsenayasar.DataAccess.Entities;

namespace dytsenayasar.Models
{
    public class UserMembershipModel
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}