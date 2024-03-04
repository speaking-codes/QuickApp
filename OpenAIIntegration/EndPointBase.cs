using DAL.Enums;
using DAL.Models;
using Newtonsoft.Json;
using OpenAI;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAIIntegration
{
    public interface IEndPointBaseOpenAI
    {
        Task CreateCustomer(Customer customer);
    }

    public class EndPointBaseOpenAI : IEndPointBaseOpenAI
    {
        private const string SecretKey = "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";
        private const string basePattern = "Per fini di test, devi fornirmi i seguenti dati Cognome, Nome, DataNascita, NumeroFigli. I dati restituiti devono essere in formato JSON ";
        private string baseMessageRequest = " La persona in questione è: {0} il suo stato civile è: {1}, svolge la professione di {2} con contratto {3}";
        private OpenAIClient OpenAi { get; set; }

        public EndPointBaseOpenAI()
        {
            OpenAi = new OpenAIClient(SecretKey);
        }

        public async Task CreateCustomer(Customer customer)
        {
            var messageRequest = String.Format(baseMessageRequest,
                                                customer.Gender.GetDefinition(),
                                                customer.MaritalStatus.MaritalStatusDescription,
                                                customer.ProfessionType.ProfessionTypeDescription,
                                                customer.ContractType.ContractTypeTitle);

            var messages = new List<Message>
            {
                new Message(Role.System,basePattern),
                new Message(Role.User, messageRequest)
            };

            var chatRequest = new ChatRequest(messages, "gpt-3.5-turbo", responseFormat: ChatResponseFormat.Json);
            var chatResponse = await OpenAi.ChatEndpoint.GetCompletionAsync(chatRequest);

            foreach (var item in chatResponse.Choices)
            {
                Console.WriteLine(item);
            }
        }
    }
}
