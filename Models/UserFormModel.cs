using System;
using dytsenayasar.Util;
using dytsenayasar.DataAccess.Entities;
using static dytsenayasar.DataAccess.Entities.UserForm;

namespace dytsenayasar.Models
{
    public class UserFormModel : AEntityModel<UserForm, UserFormModel>
    {
        public UserFormModel()
        {
            ID = Guid.NewGuid();
        }

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

        public override void SetValuesFromEntity(UserForm entity)
        {
            if (entity == null) return;

            base.SetValuesFromEntity(entity);

            ID = entity.ID;
            AllergyFoods = entity.AllergyFoods;
            Anemia = entity.Anemia;
            BadHabits = entity.BadHabits;
            BestFoods = entity.BestFoods;
            BreakfastFoods = entity.BreakfastFoods;
            BreakfastTime = entity.BreakfastTime;
            Breastfeed = entity.Breastfeed;
            CallTimes = entity.CallTimes;
            CardiovascularDiseases = entity.CardiovascularDiseases;
            Chest = entity.Chest;
            ContinuousDrugs = entity.ContinuousDrugs;
            Diabetes = entity.Diabetes;
            DietType = entity.DietType;
            DinnerFoods = entity.DinnerFoods;
            DinnerTime = entity.DinnerTime;
            Drugs = entity.Drugs;
            Family = entity.Family;
            FavoriteBreakfastFoods = entity.FavoriteBreakfastFoods;
            FavoriteFruits = entity.FavoriteFruits;
            FavoriteMeatFoods = entity.FavoriteMeatFoods;
            FavoriteVegetablesFoods = entity.FavoriteVegetablesFoods;
            FoodsUntilSleep = entity.FoodsUntilSleep;
            Length = entity.Length;
            LunchFoods = entity.LunchFoods;
            LunchTime = entity.LunchTime;
            LungInfection = entity.LungInfection;
            MaxWeight = entity.MaxWeight;
            Methods = entity.Methods;
            MinWeight = entity.MinWeight;
            NoteToDietitian = entity.NoteToDietitian;
            Operation = entity.Operation;
            OralDiseases = entity.OralDiseases;
            IsOrderRegl = entity.IsOrderRegl;
            IsRegl = entity.IsRegl;
            References = entity.References;
            SleepPatterns = entity.SleepPatterns;
            SleepTime = entity.SleepTime;
            Sports = entity.Sports;
            StomachAndIntestineDiseases = entity.StomachAndIntestineDiseases;
            ThyroidDiseases = entity.ThyroidDiseases;
            ToiletFrequency = entity.ToiletFrequency;
            Parturition = entity.Parturition;
            Phone2 = entity.Phone2;
            Hospital = entity.Hospital;
            Waist = entity.Waist;
            WakeUpTime = entity.WakeUpTime;
            Weight = entity.Weight;
            Job = entity.Job;
        }

        public override UserForm ToEntity()
        {
            return new UserForm
            {
                ID = ID,
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
                Job = Job,
            };
        }
    }

    public static class UserFormEntityExtentions
    {
        public static UserFormModel ToModel(this UserForm userForm)
        {
            var model = new UserFormModel();
            model.SetValuesFromEntity(userForm);
            return model;
        }
    }
}