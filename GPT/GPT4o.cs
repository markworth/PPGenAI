using GPT.Request;
using System.ClientModel;
using OpenAI.Chat;
using Azure.AI.OpenAI;
using Newtonsoft.Json;

namespace GPT
{
    public class GPT4o
    {
        public static async Task<ChatCompletion?> GetAI(GPTRequest request)
        {
            var azure_endpoint = "https://markeastus.openai.azure.com";
            var openai_api_key = "d423876445f84cccaf6a2122aaab13b2";
            var openai_api_version = "2024-05-01-preview";
            var azure_deployment = "gpt-4o";

            var azureClient = new AzureOpenAIClient(
                new Uri(azure_endpoint),
                new ApiKeyCredential(openai_api_key));

            var chatClient = azureClient.GetChatClient(azure_deployment);
            var completion = await chatClient.CompleteChatAsync(request.messages);
            return completion?.Value;
        }
    }
}
