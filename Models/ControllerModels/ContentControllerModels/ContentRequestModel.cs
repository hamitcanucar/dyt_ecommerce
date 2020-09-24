using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using dytsenayasar.DataAccess.Entities;

namespace dytsenayasar.Models.ControllerModels.ContentControllerModels
{
    public class ContentRequestModel : AControllerEntityModel<ContentModel>
    {
        [Required, MaxLength(128)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
        public DateTime? ValidityDate { get; set; }
        public ContentTypes? ContentType { get; set; }

        public ICollection<int> Categories { get; set; }

        public override ContentModel ToModel()
        {
            var contentModel = new ContentModel
            {
                Title = Title,
                Description = Description,
                ValidityDate = ValidityDate,
                ContentType = ContentType,
                CategoryIds = Categories
            };
            return contentModel;
        }
    }
}