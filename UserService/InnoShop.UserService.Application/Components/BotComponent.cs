using Azure;
using Azure.AI.OpenAI;
using InnoShop.UserService.Application.ComponentInterfaces;
using Microsoft.Extensions.Configuration;
using OpenAI.Chat;

namespace InnoShop.UserService.Application.Components;

public class BotComponent : IBotComponent
{
    private readonly AzureOpenAIClient _azureClient;
    private readonly string _deploymentName;

    public BotComponent(IConfiguration configuration)
    {
        var endpoint = configuration["AzureOpenAI:Endpoint"];
        var key = configuration["AzureOpenAI:ApiKey"];
        _deploymentName = configuration["AzureOpenAI:DeploymentName"]!;

        if (string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(key) || string.IsNullOrEmpty(_deploymentName))
        {
            throw new ArgumentNullException("Azure Open AI configuration is missing or incomplete.");
        }

        _azureClient = new(new Uri(endpoint), new AzureKeyCredential(key));
    }

    public async Task<string> GetResponseAsync(string prompt)
    {
        if (prompt.StartsWith("/bot "))
        {
            var chatClient = _azureClient.GetChatClient(_deploymentName);

            var completion = await chatClient.CompleteChatAsync(
                [
                    new SystemChatMessage("You are very helpful assistant that talks like a good companion."),
                new UserChatMessage(prompt),
            ]);

            return completion.Value.Content[0].Text;
        }

        return null!;
    }
}