using System.ComponentModel.DataAnnotations;
using dytsenayasar.DataAccess.Entities;

namespace dytsenayasar.Models.ControllerModels.ContentControllerModels
{
    public class CategoryRequestModel : AControllerEntityModel<Category>
    {
        [Required, MaxLength(32)]
        public string Name_en { get; set; }

        [Required, MaxLength(32)]
        public string Name_tr { get; set; }

        public override Category ToModel()
        {
            return new Category
            {
                Name_en = Name_en,
                Name_tr = Name_tr
            };
        }
    }
}