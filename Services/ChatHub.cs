using Microsoft.AspNetCore.SignalR;
using MySecureWebApi.DTOs;

namespace MySecureWebApi.Services;

public class ChatHub : Hub
{
    private static readonly List<UserMessage> MessagesHistory = [];
    
    public async Task PostMessage(string message)
    {
        var senderId = Context.ConnectionId;
        Console.WriteLine($"Message from {senderId}: {message}");
        var userMessage = new UserMessage
        {
            Sender = senderId,
            Content = message,
            SentTime = DateTime.UtcNow
        };
        MessagesHistory.Add(userMessage);
        await Clients.All.SendAsync("ReceiveMessage", senderId, message, userMessage.SentTime);
    }

    public async Task RetrieveMessageHistory()
    {
        Console.WriteLine($"Retrieving message history for connection {Context.ConnectionId}, [{MessagesHistory.Count}] messages...");
        await Clients.Caller.SendAsync("MessageHistory", MessagesHistory);
    }
        
}