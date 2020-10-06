using System;
using dytsenayasar.DataAccess.Entities;

namespace dytsenayasar.Models
{
    public class UserScaleModel : AEntityModel<UserScale, UserScaleModel>
    {
        public UserScaleModel()
        {
            ID = Guid.NewGuid();
        }

        public float Weigt { get; set; }
        public int Cheest { get; set; }
        public int Waist { get; set; }
        public int UpperArm { get; set; }
        public int Hip { get; set; }
        public int Leg { get; set; }

        public override void SetValuesFromEntity(UserScale entity)
        {
            if (entity == null) return;

            base.SetValuesFromEntity(entity);

            ID = entity.ID;
            Weigt = entity.Weigt;
            Cheest = entity.Cheest;
            Waist = entity.Waist;
            UpperArm = entity.UpperArm;
            Hip = entity.Hip;
            Leg = entity.Leg;

        }

        public override UserScale ToEntity()
        {
            return new UserScale
            {
                ID = ID,
                Weigt = Weigt,
                Cheest = Cheest,
                Waist = Waist,
                UpperArm = UpperArm,
                Hip = Hip,
                Leg = Leg,
            };
        }
    }

    public static class UserScaleEntityExtentions
    {
        public static UserScaleModel ToModel(this UserScale userScale)
        {
            var model = new UserScaleModel();
            model.SetValuesFromEntity(userScale);
            return model;
        }
    }
}