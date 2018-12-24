using Blabyap.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blabyap.Services
{
  public interface IChatClientService
    {
        Task Connect();
        event EventHandler<CVChat> OnMessageReceived;
        Task Send(CVChat chatMsg);
        
    }
}
