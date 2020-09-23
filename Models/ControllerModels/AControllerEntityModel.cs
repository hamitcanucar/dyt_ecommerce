using dytsenayasar.DataAccess.Entities;

namespace dytsenayasar.Models.ControllerModels
{
    public abstract class AControllerEntityModel<TEntityModel>
    {
        public abstract TEntityModel ToModel();
    }
}