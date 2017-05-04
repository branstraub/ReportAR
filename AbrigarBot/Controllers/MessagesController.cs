using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Threading;
using System.Text.RegularExpressions;
using HolaHugo;

namespace TamberoBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => new TamberoDialog());
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            return null;
        }

        [LuisModel("17c38c57-7e9b-4fbf-b1a9-ebf3ed9a07f4", "00c5c678c9e34addac0c10ecbbcd09c4")]
        [Serializable]
        public class TamberoDialog : LuisDialog<object>
        {


            [LuisIntent("")]
            [LuisIntent("None")]
            public async Task None(IDialogContext context, LuisResult result)
            {

                string message = $"Perdón, no te entendí '{result.Query}'.";

                await context.PostAsync(message);
                var message2 = context.MakeMessage();

                var attachment = GetHeroCardHelp();
                message2.Attachments.Add(attachment);

                await context.PostAsync(message2);

                context.Wait(this.MessageReceived);
            }


            [LuisIntent("Hola")]
            public async Task Hola(IDialogContext context, LuisResult result)
            {

                string message = $"Buenas, ¿en que te puedo ayudar?";

                await context.PostAsync(message);
                var message2 = context.MakeMessage();

                var attachment = GetHeroCardHelp();
                message2.Attachments.Add(attachment);

                await context.PostAsync(message2);

                context.Wait(this.MessageReceived);
            }

            [LuisIntent("SaberSobreMi")]
            public async Task SaberSobreMi(IDialogContext context, LuisResult result)
            {

                string message = $"Buenas, la campaña del frio es una iniciativa de FundacionSi, junto con Microsoft queremos lograr que ninguna persona tenga que sufrir de estar en situacion de calle durante este periodo";

                await context.PostAsync(message);
                var message2 = context.MakeMessage();

                var attachment = GetHeroCardMore();
                message2.Attachments.Add(attachment);

                await context.PostAsync(message2);

                context.Wait(this.MessageReceived);
            }

            private Attachment GetHeroCardMore()
            {
                var heroCard = new HeroCard
                {
                    Title = "¿Queres saber mas?",
                    Subtitle = "Seguinos en nuestras redes sociales",
                    //Images = new List<CardImage> { new CardImage("https://sec.ch9.ms/ch9/7ff5/e07cfef0-aa3b-40bb-9baa-7c9ef8ff7ff5/buildreactionbotframework_960.jpg") },
                    Buttons = new List<CardAction>
                    {
                        new CardAction(ActionTypes.OpenUrl, "Facebook", value: "http://fundacionsi.org.ar"),
                         new CardAction(ActionTypes.OpenUrl, "Twitter", value: "http://fundacionsi.org.ar"),
                          new CardAction(ActionTypes.OpenUrl, "Pagina Web", value: "http://fundacionsi.org.ar"),

                    }
                };

                return heroCard.ToAttachment();
            }

            [LuisIntent("QueOngs")]
            public async Task QueOngs(IDialogContext context, LuisResult result)
            {
                string message = $"La Organizaciones que apoyan las campaña del frio son:";

                await context.PostAsync(message);
                var message2 = context.MakeMessage();


                var reply = context.MakeMessage();

                reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                reply.Attachments = GetHeroCardOngs(GetOngs());

                await context.PostAsync(reply);

                context.Wait(this.MessageReceived);
            }

            private IEnumerable<OngModel> GetOngs()
            {
                var html = "";
                var url = @"https://reportar.azurewebsites.net/home/getongs";

                var request = (HttpWebRequest) WebRequest.Create(url);

                using (var response = (HttpWebResponse) request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }

                dynamic jsonDecoded = JsonConvert.DeserializeObject<IEnumerable<OngModel>>(html);

                return jsonDecoded;
            }

            private static Attachment GetHeroCardHelp()
            {
                var heroCard = new HeroCard
                {
                    Title = "Acciones disponibles",
                    Subtitle = "Elije una de las opciones debajo",
                    //Images = new List<CardImage> { new CardImage("https://sec.ch9.ms/ch9/7ff5/e07cfef0-aa3b-40bb-9baa-7c9ef8ff7ff5/buildreactionbotframework_960.jpg") },
                    Buttons = new List<CardAction>
                    {
                        new CardAction(ActionTypes.PostBack, "Quiero reportar un caso", value: "Quiero reportar un caso"),
                        new CardAction(ActionTypes.PostBack, "¿Qué organizaciones participan?",
                            value: "¿Qué organizaciones participan?"),
                        new CardAction(ActionTypes.PostBack, "¿Qué es la campaña del frio?",
                            value: "¿Qué es la campaña del frio?"),

                    }
                };

                return heroCard.ToAttachment();
            }

            private static List<Attachment> GetHeroCardOngs(IEnumerable<OngModel> ongs)
            {

                var listCards = new List<Attachment>();

                foreach (var ong in ongs)
                {
                    listCards.Add(new HeroCard
                        {
                            Title = ong.Nombre,
                            Subtitle = $"Misión: {ong.Mision}",
                            Text = $"Telefono: {ong.Telefono}",

                            Buttons = new List<CardAction>()
                            {
                                new CardAction()
                                {
                                    Title = "Más Información",
                                    Type = ActionTypes.OpenUrl,
                                    Value = ong.WebUrl
                                },
                                new CardAction()
                                {
                                    Title = "Enviar Mail",
                                    Type = ActionTypes.OpenUrl,
                                    Value = "mailto:" + ong.Mail
                                }
                            }
                        }.ToAttachment()
                    );
                }


                return listCards;

            }


        }
    }

}