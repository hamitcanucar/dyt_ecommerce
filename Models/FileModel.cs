using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using dytsenayasar.DataAccess.Entities;

namespace dytsenayasar.Models
{
    public class UserFileModel : AEntityModel<UserFile, UserFileModel>
    {
        public UserFileModel(){}

        public UserFileModel(UserFile entity)
        {
            SetValuesFromEntity(entity);
        }

        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] DataFiles { get; set; }
        public DateTime? CreatedOn { get; set; }
        public User User { get; set; }

        public ICollection<int> CategoryIds { get; set; }
        public IDictionary<int, string> Categories { get; set; }

        public override UserFile ToEntity()
        {
            var userFile = new UserFile
            {
                FileName = FileName,
                FileType = FileType,
                CreatedOn = CreatedOn ?? DateTime.MaxValue,
            };

            return userFile;
        }

        public override void SetValuesFromEntity(UserFile entity)
        {
            if (entity == null) return;

            base.SetValuesFromEntity(entity);

            ID = entity.ID;
            FileName = entity.FileName;
            FileType = entity.FileType;
            CreatedOn = entity.CreatedOn;
        }
    }

    public static class UserFileEntityExtentions
    {
        public static UserFileModel ToModel(this UserFile entity)
        {
            var model = new UserFileModel();
            model.SetValuesFromEntity(entity);
            return model;
        }
    }
}