using System;
using dytsenayasar.DataAccess.Entities;

namespace dytsenayasar.Models
{
    public class ContentModel : AEntityModelWithFile<Content, ContentModel>
    {
        public ContentModel(){}

        public ContentModel(Content entity)
        {
            SetValuesFromEntity(entity);
        }

        public ContentModel(Content entity, bool taken) : this(entity)
        {
            Taken = taken;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public int? AgeLimit { get; set; }
        public double? Price { get; set; }
        public DateTime? ValidityDate { get; set; }
        public bool Taken { get; set; }


        public override Content ToEntity()
        {
            var content = new Content
            {
                Title = Title,
                Description = Description,
                ValidityDate = ValidityDate ?? DateTime.MaxValue,
            };

            return content;
        }

        public override void SetValuesFromEntity(Content entity)
        {
            if (entity == null) return;

            base.SetValuesFromEntity(entity);

            ID = entity.ID;
            Title = entity.Title;
            Description = entity.Description;
            ValidityDate = entity.ValidityDate;

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

        public static ContentModel ToModel(this Content entity, string baseUrl)
        {
            var model = new ContentModel();
            model.SetValuesFromEntity(baseUrl, entity);
            return model;
        }
    }
}