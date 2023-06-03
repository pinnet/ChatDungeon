using System;
using OpenAI_API;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Helpers.Events;
using Helpers.Interfaces;
using CandyCoded.env;
using OpenAI_API.Chat;
using OpenAI_API.Models;

public class OpenAIProvider : IAIProvider
{ 
    public event EventHandler<UIEventArgs> OnAnswerReceived;


    private OpenAIAPI m_OpenAIAPI;
    private string currentContext = null;
    private string currentQuestion = null;
    private List<ChatMessage> messages;

    public OpenAIProvider()
    {
        if (!env.TryParseEnvironmentVariable("OPENAI_API_KEY", out string apiKey)) throw new Exception("OPENAI_API_KEY not set");
        m_OpenAIAPI = new OpenAIAPI(apiKey);
        messages = new List<ChatMessage> { new ChatMessage(ChatMessageRole.System, "you are an Open AI chat bot") };
    }

    

    private async void GetResponse()
    {
        var msg = messages;
        var chatResult = await m_OpenAIAPI.Chat.CreateChatCompletionAsync(new ChatRequest(){
            Model = Model.ChatGPTTurbo,
            Temperature = 0.1,
            MaxTokens = 50,
            Messages = messages
        });

        if (chatResult == null || chatResult.Choices.Count == 0) throw new Exception("No response from OpenAI");

        ChatMessage response = new ChatMessage();
        response.Role = chatResult.Choices[0].Message.Role;
        response.Content = chatResult.Choices[0].Message.Content;
        messages.Add(response);
        OnAnswerReceived?.Invoke(this, CreateEventArgs(question: currentQuestion, answer: response.Content, context: currentContext));

    }


    public void AskQuestion(string question, string? context = null)
    {

        bool newContext = (context != null || context != currentContext);
        if (newContext) {
            currentContext = context;
            messages[0] = new ChatMessage(ChatMessageRole.System, currentContext);        
        }
        else { currentContext = null; }

        if (question == currentQuestion && !newContext) return;       
        currentQuestion = question;

        if (currentQuestion.Length > 100)
        {
            currentQuestion = currentQuestion.Substring(0, 100);
        }

        messages.Add(new ChatMessage(ChatMessageRole.User, currentQuestion));
        GetResponse();
    }

    public UIEventArgs CreateEventArgs(string question = null, string answer = null, string context = null)
    {
        UIEventArgs args = new UIEventArgs();
        if (question != null) args.Question = question;
        if (answer != null)   args.Answer = answer;
        if (context != null)  args.Context = context;
        return args;
    }
}
