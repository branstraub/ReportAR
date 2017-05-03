using CaaS.DataClassImplementations;
using CaaS.Interfaces;
using CaaS.Models;

namespace CaaS.Dialogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.FormFlow;
    using Microsoft.Bot.Builder.Luis;
    using Microsoft.Bot.Builder.Luis.Models;
    using Microsoft.Bot.Connector;

    [Serializable]
    [LuisModel("17c38c57-7e9b-4fbf-b1a9-ebf3ed9a07f4", "00c5c678c9e34addac0c10ecbbcd09c4")]
    public class RootLuisDialog : LuisDialog<object>
    {

        private readonly IReportesRepository _reportesRepository;
        private readonly IOngsRepository _ongsRepository;

      

        public RootLuisDialog(IReportesRepository reportesRepository, IOngsRepository ongsRepository) 
        {
            _ongsRepository = ongsRepository;
            _reportesRepository = reportesRepository;
        }


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

       
        [LuisIntent("QueOngs")]
        public async Task QueOngs(IDialogContext context, LuisResult result)
        {
            string message = $"La Organizaciones que apoyan las campaña del frio son:";

            await context.PostAsync(message);
            var message2 = context.MakeMessage();


            var reply = context.MakeMessage();

            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            reply.Attachments = GetHeroCardOngs(_ongsRepository.GetOngs());

            await context.PostAsync(reply);

            context.Wait(this.MessageReceived);
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
                    new CardAction(ActionTypes.PostBack, "Quiero reportar un caso", value: "Quiero reportar un caso") ,
                    new CardAction(ActionTypes.PostBack, "¿Qué organizaciones participan?", value: "¿Qué organizaciones participan?") ,
                    new CardAction(ActionTypes.PostBack, "¿Qué es la campaña del frio?", value: "¿Qué es la campaña del frio?") ,

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
                                Value = "mailto:"+ong.Mail
                            }
                        }
                }.ToAttachment()
            );
            }


          return listCards;

        }



    }
}
