using System;
using System.ComponentModel.DataAnnotations;
using dytsenayasar.DataAccess.Entities;
using static dytsenayasar.DataAccess.Entities.UserForm;

namespace dytsenayasar.Models.ControllerModels.UserControllerModels
{
    public class UserFormRequestModel : AControllerEntityModel<UserForm>
    {
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
        public string ToiletFrequency { get; set; }
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