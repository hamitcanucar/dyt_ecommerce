using System;
using System.ComponentModel.DataAnnotations;
using dytsenayasar.DataAccess.Entities;
using static dytsenayasar.DataAccess.Entities.UserForm;

namespace dytsenayasar.Models.ControllerModels.UserControllerModels
{
    public class UserFormRequestModel : AControllerEntityModel<UserForm>
    {
        public DietTypes DietType { get; set; }

        [MaxLength(64)]
        public string Job { get; set; }

        [MaxLength(32)]
        public string Phone2 { get; set; }

        [MaxLength(32)]
        public string CallTimes { get; set; }

        [MaxLength(64)]
        public string References { get; set; }



        //Habits
        [MaxLength(64)]
        public string WakeUpTime { get; set; }

        [MaxLength(64)]
        public string BreakfastTime { get; set; }

        [MaxLength(64)]
        public string LunchTime { get; set; }

        [MaxLength(64)]
        public string DinnerTime { get; set; }

        [MaxLength(64)]
        public string SleepTime { get; set; }

        [MaxLength(64)]
        public string BreakfastFoods { get; set; }

        [MaxLength(64)]
        public string LunchFoods { get; set; }

        [MaxLength(64)]
        public string DinnerFoods { get; set; }

        [MaxLength(64)]
        public string FoodsUntilSleep { get; set; }

        [MaxLength(64)]
        public string SleepPatterns { get; set; }

        [MaxLength(64)]
        public string Drugs { get; set; }

        [MaxLength(64)]
        public string Sports { get; set; }

        [MaxLength(64)]
        public string BadHabits { get; set; }

        [MaxLength(64)]
        public string ToiletFrequency { get; set; }

        [MaxLength(64)]
        public string AllergyFoods { get; set; }

        [MaxLength(64)]
        public string BestFoods { get; set; }

        [MaxLength(64)]
        public string FavoriteBreakfastFoods { get; set; }

        [MaxLength(64)]
        public string FavoriteVegetablesFoods { get; set; }

        [MaxLength(64)]
        public string FavoriteMeatFoods { get; set; }

        [MaxLength(64)]
        public string FavoriteFruits { get; set; }

        //Genereal Health Status
        [MaxLength(64)]
        public string OralDiseases { get; set; }

        [MaxLength(64)]
        public string CardiovascularDiseases { get; set; }

        [MaxLength(64)]
        public string StomachAndIntestineDiseases { get; set; }

        [MaxLength(64)]
        public string ThyroidDiseases { get; set; }

        [MaxLength(64)]
        public string Anemia { get; set; }

        [MaxLength(64)]
        public string UrinaryInfection { get; set; }

        [MaxLength(64)]
        public string LungInfection { get; set; }

        [MaxLength(64)]
        public string ContinuousDrugs { get; set; }

        [MaxLength(64)]
        public string Diabetes { get; set; }

        [MaxLength(64)]
        public string Hospital { get; set; }

        [MaxLength(64)]
        public string Operation { get; set; }

        [MaxLength(64)]
        public bool? IsRegl { get; set; }

        [MaxLength(64)]
        public bool? IsOrderRegl { get; set; }

        [MaxLength(64)]
        public string Parturition { get; set; }

        [MaxLength(64)]
        public string Breastfeed { get; set; }

        //Personal
        public float Weight { get; set; }
        public int Length { get; set; }
        public int Chest { get; set; }
        public int Waist { get; set; }
        public float MaxWeight { get; set; }
        public float MinWeight { get; set; }

        [MaxLength(64)]
        public string Methods { get; set; }

        [MaxLength(64)]
        public string Family { get; set; }


        public string NoteToDietitian { get; set; }

        public override UserForm ToModel()
        {
            return new UserForm
            {
                AllergyFoods = AllergyFoods,
                Anemia = Anemia,
                BadHabits = BadHabits,
                BestFoods = BestFoods,
                BreakfastFoods = BreakfastFoods,
                BreakfastTime = BreakfastTime,
                Breastfeed = Breastfeed,
                CallTimes = CallTimes,
                CardiovascularDiseases = CardiovascularDiseases,
                Chest = Chest,
                ContinuousDrugs = ContinuousDrugs,
                Diabetes = Diabetes,
                DietType = DietType,
                DinnerFoods = DinnerFoods,
                DinnerTime = DinnerTime,
                Drugs = Drugs,
                Family = Family,
                FavoriteBreakfastFoods = FavoriteBreakfastFoods,
                FavoriteFruits = FavoriteFruits,
                FavoriteMeatFoods = FavoriteMeatFoods,
                FavoriteVegetablesFoods = FavoriteVegetablesFoods,
                FoodsUntilSleep = FoodsUntilSleep,
                Length = Length,
                LunchFoods = LunchFoods,
                LunchTime = LunchTime,
                LungInfection = LungInfection,
                MaxWeight = MaxWeight,
                Methods = Methods,
                MinWeight = MinWeight,
                NoteToDietitian = NoteToDietitian,
                Operation = Operation,
                OralDiseases = OralDiseases,
                IsOrderRegl = IsOrderRegl,
                IsRegl = IsRegl,
                References = References,
                SleepPatterns = SleepPatterns,
                SleepTime = SleepTime,
                Sports = Sports,
                StomachAndIntestineDiseases = StomachAndIntestineDiseases,
                ThyroidDiseases = ThyroidDiseases,
                ToiletFrequency = ToiletFrequency,
                Parturition = Parturition,
                Phone2 = Phone2,
                Hospital = Hospital,
                Waist = Waist,
                WakeUpTime = WakeUpTime,
                Weight = Weight,
                Job = Job
            };
        }
    }
}