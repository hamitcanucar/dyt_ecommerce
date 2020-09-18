using System.Threading.Tasks;
using dyt_ecommerce.Models.EmailManager;

namespace dyt_ecommerce.Services.Abstract
{
    public interface IEmailService
    {
        Task Send(EmailMessageModel message);
    }
}