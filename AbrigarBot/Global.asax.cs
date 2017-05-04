using System;
using System.Collections.Generic;
using System.Web.Http;
using Autofac;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.History;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading;
using System.ComponentModel;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using HolaHugo;

namespace TamberoBot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DebugActivityLogger>().AsImplementedInterfaces().InstancePerDependency();
            builder.Update(Conversation.Container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }

    public class DebugActivityLogger : IActivityLogger
    {
        public async Task LogAsync(IActivity activity)
        {
            //Mas adelante se puede borrar esto
            Debug.WriteLine($"From:{activity.From.Id} - To:{activity.Recipient.Id} - Message:{activity.AsMessageActivity()?.Text}");

            }
    }

   
}
