using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace BlabyApAzure.API
{
    [HubName("blabyapChatHUb")]
    public class chathub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}