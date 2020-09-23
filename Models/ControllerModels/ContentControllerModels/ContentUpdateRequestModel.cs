using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using dytsenayasar.DataAccess.Entities;

namespace dytsenayasar.Models.ControllerModels.ContentControllerModels
{
    public class ContentUpdateRequestModel : AControllerEntityModel<ContentModel>
    {
        [Required, MaxLength(128)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [Range(0, 100)]
        public int? AgeLimit { get; set; }

        [Range(0.0d, double.MaxValue)]
        public double? Price { get; set; }
        public DateTime? ValidityDate { get; set; }
        // public ContentType? ContentType { get; set; }

        public ICollection<int> Categories { get; set; }

        public override ContentModel ToModel()
        {
            var contentModel = new ContentModel
            {
                Title = Title,
                Description = Description,
                ValidityDate = ValidityDate,
                // ContentType = ContentType,
                CategoryIds = Categories
            };

            return contentModel;
        }
    }
}