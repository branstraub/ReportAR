using CaaS.Models.BotModels;

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

    [LuisModel("YourModelId", "YourSubscriptionKey")]
    public class RootLuisDialog : LuisDialog<object>
    {
        private const string EntityGeographyCity = "builtin.geography.city";

        private const string EntityHotelName = "Hotel";

        private const string EntityAirportCode = "AirportCode";

        private IList<string> titleOptions = new List<string> { "“Very stylish, great stay, great staff”", "“good hotel awful meals”", "“Need more attention to little things”", "“Lovely small hotel ideally situated to explore the area.”", "“Positive surprise”", "“Beautiful suite and resort”" };

        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry, I did not understand '{result.Query}'. Type 'help' if you need assistance.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("SearchHotels")]
        public async Task Search(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync($"Welcome to the Hotels finder! We are analyzing your message: '{message.Text}'...");

            var hotelsQuery = new CasoQuery();

            EntityRecommendation cityEntityRecommendation;

            if (result.TryFindEntity(EntityGeographyCity, out cityEntityRecommendation))
            {
                cityEntityRecommendation.Type = "Destination";
            }

            //var hotelsFormDialog = new FormDialog<CasoQuery>(hotelsQuery, this.BuildHotelsForm, FormOptions.PromptInStart, result.Entities);

            //context.Call(hotelsFormDialog, this.ResumeAfterHotelsFormDialog);
        }

        [LuisIntent("ShowHotelsReviews")]
        public async Task Reviews(IDialogContext context, LuisResult result)
        {
            EntityRecommendation hotelEntityRecommendation;

            if (result.TryFindEntity(EntityHotelName, out hotelEntityRecommendation))
            {
                await context.PostAsync($"Looking for reviews of '{hotelEntityRecommendation.Entity}'...");

                var resultMessage = context.MakeMessage();
                resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                resultMessage.Attachments = new List<Attachment>();

                for (int i = 0; i < 5; i++)
                {
                    var random = new Random(i);
                    ThumbnailCard thumbnailCard = new ThumbnailCard()
                    {
                        Title = this.titleOptions[random.Next(0, this.titleOptions.Count - 1)],
                        Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris odio magna, sodales vel ligula sit amet, vulputate vehicula velit. Nulla quis consectetur neque, sed commodo metus.",
                        Images = new List<CardImage>()
                        {
                            new CardImage() { Url = "https://upload.wikimedia.org/wikipedia/en/e/ee/Unknown-person.gif" }
                        },
                    };

                    resultMessage.Attachments.Add(thumbnailCard.ToAttachment());
                }

                await context.PostAsync(resultMessage);
            }

            context.Wait(this.MessageReceived);
        }


        //private async Task ResumeAfterHotelsFormDialog(IDialogContext context, IAwaitable<CasoQuery> result)
        //{
        //    try
        //    {
        //        var searchQuery = await result;

        //        var hotels = await this.GetHotelsAsync(searchQuery);

        //        await context.PostAsync($"I found {hotels.Count()} hotels:");

        //        var resultMessage = context.MakeMessage();
        //        resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        //        resultMessage.Attachments = new List<Attachment>();

        //        foreach (var hotel in hotels)
        //        {
        //            HeroCard heroCard = new HeroCard()
        //            {
        //                Title = hotel.Name,
        //                Subtitle = $"{hotel.Rating} starts. {hotel.NumberOfReviews} reviews. From ${hotel.PriceStarting} per night.",
        //                Images = new List<CardImage>()
        //                {
        //                    new CardImage() { Url = hotel.Image }
        //                },
        //                Buttons = new List<CardAction>()
        //                {
        //                    new CardAction()
        //                    {
        //                        Title = "More details",
        //                        Type = ActionTypes.OpenUrl,
        //                        Value = $"https://www.bing.com/search?q=hotels+in+" + HttpUtility.UrlEncode(hotel.Location)
        //                    }
        //                }
        //            };

        //            resultMessage.Attachments.Add(heroCard.ToAttachment());
        //        }

        //        await context.PostAsync(resultMessage);
        //    }
        //    catch (FormCanceledException ex)
        //    {
        //        string reply;

        //        if (ex.InnerException == null)
        //        {
        //            reply = "You have canceled the operation.";
        //        }
        //        else
        //        {
        //            reply = $"Oops! Something went wrong :( Technical Details: {ex.InnerException.Message}";
        //        }

        //        await context.PostAsync(reply);
        //    }
        //    finally
        //    {
        //        context.Done<object>(null);
        //    }
        //}

      
    }
}
