using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using dytsenayasar.DataAccess.Entities;

namespace dytsenayasar.Models
{
    public class ContentModel : AEntityModel<Content, ContentModel>
    {
        public ContentModel(){}

        public ContentModel(Content entity)
        {
            SetValuesFromEntity(entity);
        }

        public string Name { get; set; }
        public string FileType { get; set; }
        public byte[] DataFiles { get; set; }
        public DateTime? CreatedOn { get; set; }
        public User User { get; set; }

        public ICollection<int> CategoryIds { get; set; }
        public IDictionary<int, string> Categories { get; set; }

        public override Content ToEntity()
        {
            var content = new Content
            {
                Name = Name,
                FileType = FileType,
                CreatedOn = CreatedOn ?? DateTime.MaxValue,
                DataFiles = DataFiles,
            };

            return content;
        }

        public override void SetValuesFromEntity(Content entity)
        {
            if (entity == null) return;

            base.SetValuesFromEntity(entity);

            ID = entity.ID;
            Name = entity.Name;
            FileType = entity.FileType;
            CreatedOn = entity.CreatedOn;
            DataFiles = entity.DataFiles;
        }
    }

    public static class ContentEntityExtentions
    {
        public static ContentModel ToModel(this Content entity)
        {
            var model = new ContentModel();
            model.SetValuesFromEntity(entity);
            return model;
        }
    }
}