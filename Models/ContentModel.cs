using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

        public ContentModel(string baseUrl, Content entity)
        {
            SetValuesFromEntity(baseUrl, entity);
        }

        public ContentModel(Content entity, bool taken) : this(entity)
        {
            Taken = taken;
        }

        public ContentModel(Content entity, bool taken, string baseUrl) : this(baseUrl, entity)
        {
            Taken = taken;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ValidityDate { get; set; }
        public ContentTypes? ContentType { get; set; }
        public bool Taken { get; set; }

        public ICollection<int> CategoryIds { get; set; }
        public IDictionary<int, string> Categories { get; set; }

        public override Content ToEntity()
        {
            var content = new Content
            {
                Title = Title,
                Description = Description,
                UploadDate = ValidityDate ?? DateTime.MaxValue,
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
            ValidityDate = entity.UploadDate;
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