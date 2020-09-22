using System;
using dyt_ecommerce.Util;
using dytsenayasar.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dyt_ecommerce.DataAccess.Entities
{
    public class UserForm : AEntity
    {
        public enum DietTypes { GaininWeight, LoseWeight, HeartDisease, Gout,HighLipidCholesterol, Canser, Goiter, HormoneDisorder, Pregnancy, Trimethylaminuria, CeliacDisease, Allergy, KidneyFailure, Gastritis, LiverFailureOrHepatitis, ElderlyNutrition, BabyNutrition, PuerperalNutrition, Other}
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DietTypes DietType { get; set; }
        public string Job { get; set; }
        public string Phone2 { get; set; }
        public string CallTimes { get; set; }
        public string References { get; set; }
        


        //Habits
        public string WakeUpTime { get; set; }
        public string BreakfastTime { get; set; }
        public string LunchTime { get; set; }
        public string DinnerTime { get; set; }
        public string SleepTime { get; set; }
        public string BreakfastFoods { get; set; }
        public string LunchFoods { get; set; }
        public string DinnerFoods { get; set; }
        public string FoodsUntilSleep { get; set; }
        public string SleepPatterns { get; set; }
        public string Drugs { get; set; }
        public string Sports { get; set; }
        public string BadHabits { get; set; }
        public string ToiletFrequency{ get; set; }
        public string AllergyFoods { get; set; }
        public string BestFoods { get; set; }
        public string FavoriteBreakfastFoods { get; set; }
        public string FavoriteVegetablesFoods { get; set; }
        public string FavoriteMeatFoods { get; set; }
        public string FavoriteFruits { get; set; }

        //Genereal Health Status
        public string OralDiseases { get; set; }
        public string CardiovascularDiseases { get; set; }
        public string StomachAndIntestineDiseases { get; set; }
        public string ThyroidDiseases { get; set; }
        public string Anemia { get; set; }
        public string UrinaryInfection { get; set; }
        public string LungInfection { get; set; }
        public string ContinuousDrugs { get; set; }
        public string Diabetes { get; set; }
        public string Hospital { get; set; }
        public string Operation { get; set; }
        public bool? IsRegl { get; set; }
        public bool? IsOrderRegl { get; set; }
        public string Parturition { get; set; }
        public string Breastfeed { get; set; }

        //Personal
        public float Weight { get; set; }
        public int Length { get; set; }
        public int Chest { get; set; }
        public int Waist { get; set; }
        public float MaxWeight { get; set; }
        public float MinWeight { get; set; }
        public string Methods { get; set; }
        public string Family { get; set; }
        public string NoteToDietitian { get; set; }
        


        public class UserClientEntityConfiguration : EntityConfiguration<UserForm>
        {
            public UserClientEntityConfiguration() : base("user_form")
            {
            }

            public override void Configure(EntityTypeBuilder<UserForm> builder)
            {
                base.Configure(builder);

                builder.HasOne<User>(uc => uc.User)
                    .WithOne(u => u.Form)
                    .HasForeignKey<UserForm>(uc => uc.UserId);

                builder.Property(uc => uc.UserId);
            }
        }
    }
}