using System;
using Microsoft.Bot.Builder.FormFlow;

namespace CaaS.Models.BotModels
{
    [Serializable]
    public class CasoQuery
    {
        [Prompt("Por favor ingresa la {&}")]
        [Optional]
        public string Descripcion { get; set; }

        [Prompt("¿Cerca de que lugar?")]
        public string Lugar { get; set; }
    }

}

