using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using dytsenayasar.DataAccess.Entities;

namespace dytsenayasar.Models.ControllerModels.ContentControllerModels
{
    public class UserFileRequestModel : AControllerEntityModel<UserFileModel>
    {
        [Required, MaxLength(128)]
        public string FileName { get; set; }

        [MaxLength(255)]
        public string FileType { get; set; }
        public DateTime? CreatedOn { get; set; }
        
        public User User { get; set; }

        public override UserFileModel ToModel()
        {
            var userFileModel = new UserFileModel
            {
                FileName = FileName,
                FileType = FileType,
                CreatedOn = CreatedOn,
            };
            return userFileModel;
        }
    }
}