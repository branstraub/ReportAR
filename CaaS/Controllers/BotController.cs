using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaaS.DataClassImplementations;
using CaaS.Dialogs;
using CaaS.Interfaces;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Threading.Tasks;

namespace CaaS.Controllers
{

    [Serializable]
    public class BotController : Controller
    {
        private readonly IReportesRepository _reportesRepository;
        private readonly IOngsRepository _ongsRepository;

        public BotController()
        {
            _reportesRepository = new ReportesRepository();
            _ongsRepository = new OngsRepository();
        }

        // GET: Bot
        public async Task<ActionResult> Index(Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {

                await Conversation.SendAsync(activity, () => new RootLuisDialog(_reportesRepository, _ongsRepository));
            }
            else
            {
                this.HandleSystemMessage(activity);
            }

            return new HttpStatusCodeResult(201);
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}