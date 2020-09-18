using dyt_ecommerce.DataAccess.Entities;

namespace dyt_ecommerce.Models.ControllerModels
{
    public abstract class AControllerEntityModel<TEntityModel>
    {
        public abstract TEntityModel ToModel();
    }
}