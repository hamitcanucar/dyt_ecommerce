using System;
using System.Collections.Generic;
using dytsenayasar.DataAccess.Entities;

namespace dytsenayasar.Models
{
    public class ContentFindParametersModel
    {
        public  Guid? CreatorId { get; set; }
        public DateTime? MinValidity { get; set; }
        public DateTime? MaxValidity { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}