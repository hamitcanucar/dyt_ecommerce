using System;
using System.ComponentModel.DataAnnotations;
using dytsenayasar.DataAccess.Entities;
using static dytsenayasar.DataAccess.Entities.UserForm;

namespace dytsenayasar.Models.ControllerModels.UserControllerModels
{
    public class UserScaleRequestModel : AControllerEntityModel<UserScale>
    {
        public float Weigt { get; set; }
        public int Cheest { get; set; }
        public int Waist { get; set; }
        public int UpperArm { get; set; }
        public int Hip { get; set; }
        public int Leg { get; set; }

        public override UserScale ToModel()
        {
            return new UserScale
            {
                Cheest = Cheest,
                Hip = Hip,
                Leg = Leg,
                UpperArm = UpperArm,
                Waist = Waist,
                Weigt = Weigt
            };
        }
    }
}