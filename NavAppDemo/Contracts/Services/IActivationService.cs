using System.Threading.Tasks;

namespace NavAppDemo.Contracts.Services
{
    public interface IActivationService
    {
        Task ActivateAsync(object activationArgs);
    }
}
