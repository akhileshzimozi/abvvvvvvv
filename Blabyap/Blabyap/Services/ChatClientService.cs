using Blabyap.Common.Model;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blabyap.Services
{
   public class ChatClientService : IChatClientService
    {
        private readonly HubConnection _connection;
        private readonly IHubProxy _proxy;

        public event EventHandler<CVChat> OnMessageReceived;

        public ChatClientService()
        {
            _connection = new HubConnection("https://blabychathub.azurewebsites.net/");
            _proxy = _connection.CreateHubProxy("Chat");
        }
        public async Task Connect()
        {
            await _connection.Start();

            _proxy.On("GetMessage", (string chatMsg,string candidateEmail)=> OnMessageReceived(this, new CVChat
            {
                ChatMsg = chatMsg,
              
            }));
        }


    
        public Task Send(CVChat chatMsg)
        {
           return _proxy.Invoke("Join",  chatMsg.ChatMsg);
        }
    }
}
