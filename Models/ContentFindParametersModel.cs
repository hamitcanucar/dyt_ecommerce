using System;
using System.Collections.Generic;
using dytsenayasar.DataAccess.Entities;

namespace dytsenayasar.Models
{
    public class ContentFindParametersModel
    {
        public ICollection<int> Categories { get; set; }
        public  Guid? CreatorId { get; set; }
        public DateTime? MinValidity { get; set; }
        public DateTime? MaxValidity { get; set; }
        public ICollection<ContentType> ContentType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}