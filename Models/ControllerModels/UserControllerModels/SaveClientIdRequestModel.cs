using System.ComponentModel.DataAnnotations;

namespace SlibraryWebapi.Models.ControllerModels.UserControllerModels
{
    public class SaveClientIdRequestModel
    {
        [Required, MaxLength(255)]
        public string ClientId { get; set; }
    }
}