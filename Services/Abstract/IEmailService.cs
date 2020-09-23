using System.Threading.Tasks;
using dytsenayasar.Models.EmailManager;

namespace dytsenayasar.Services.Abstract
{
    public interface IEmailService
    {
        Task Send(EmailMessageModel message);
    }
}