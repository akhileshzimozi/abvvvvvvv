using Blabyap.Common.Model;
using Blabyap.Common.Model.NetworkModel;
using System.Threading.Tasks;

namespace Blabyap.Services
{
    public interface IRestServices
    {

        Task<PostTokenResponse> PostCVApiIDUrl(CVData vdata);
        
        Task<ResponseResultCV> GetCVApiUrl();

        


    }
}
  