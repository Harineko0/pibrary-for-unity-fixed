using System.Threading.Tasks;

namespace Pibrary.Auth
{
    public interface IGoogleAuthHandler
    {
        public Task<string> getIdToken();
    }
}