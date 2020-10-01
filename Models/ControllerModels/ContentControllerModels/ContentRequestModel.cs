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
        public string Name { get; set; }

        [MaxLength(255)]
        public string FileType { get; set; }
        public byte[] DataFiles { get; set; }
        public DateTime? CreatedOn { get; set; }
        
        public User User { get; set; }

        public override ContentModel ToModel()
        {
            var contentModel = new ContentModel
            {
                Name = Name,
                FileType = FileType,
                CreatedOn = CreatedOn,
            };
            return contentModel;
        }
    }
}