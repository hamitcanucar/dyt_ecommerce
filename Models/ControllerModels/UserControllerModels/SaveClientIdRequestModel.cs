using System.ComponentModel.DataAnnotations;

namespace dytsenayasar.Models.ControllerModels.UserControllerModels
{
    public class SaveClientIdRequestModel
    {
        [Required, MaxLength(255)]
        public string ClientId { get; set; }
    }
}