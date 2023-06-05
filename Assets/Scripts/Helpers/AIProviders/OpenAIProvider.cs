#nullable enable
using System;
using OpenAI_API;
using System.Collections.Generic;
using Helpers.Interfaces;
using CandyCoded.env;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using UnityEngine;
using UnityEditor;

namespace AIProviders
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "OpenAIProvider", menuName = "AIManager/OpenAIProvider", order = 1)]
    public class OpenAIProvider : BaseAIProvider
    {
        public override event EventHandler<UIEventArgs> OnAnswerReceived;
        [SerializeField] private OpenAIModelDataSO m_ModelData;

        private OpenAIAPI openAIAPI;
        private string? currentContext = null;
        private string? currentQuestion = null;
        private List<ChatMessage> messages;

        private int maxTokens = 50;
        private double temperature = 0.1;
        private Model model = Model.ChatGPTTurbo;
       
        
        public OpenAIProvider()
        {
            if (!env.TryParseEnvironmentVariable("OPENAI_API_KEY", out string apiKey)) throw new Exception("OPENAI_API_KEY not set");
            openAIAPI = new OpenAIAPI(apiKey);
            messages = new List<ChatMessage> { new ChatMessage(ChatMessageRole.System, String.Empty) };

            if (m_ModelData != null)
            {
                this.maxTokens = m_ModelData.maxTokens;
                this.temperature = m_ModelData.temperature;
                this.model = m_ModelData.model;
            } 
        }

        private async void GetResponse()
        {
            ChatResult? chatResult = null;
            try
            {
                chatResult = await openAIAPI.Chat.CreateChatCompletionAsync(new ChatRequest()
                {
                    Model = model,
                    Temperature = temperature,
                    MaxTokens = maxTokens,
                    Messages = messages
                });
            }
            catch (Exception e)
            {
                OnAnswerReceived?.Invoke(this,
                new UIEventArgs(status: e.Message)
                );
                return;
            }

            if (chatResult == null || chatResult.Choices.Count == 0)
            {
                OnAnswerReceived?.Invoke(this,
                new UIEventArgs(status: "Error empty chat result")
                );
                return;
            }

            ChatMessage response = new ChatMessage();
            response.Role = chatResult.Choices[0].Message.Role;
            response.Content = chatResult.Choices[0].Message.Content;
            messages.Add(response);

            var status = $"Tokens Used {ApproxTokenCount()}";

            OnAnswerReceived?.Invoke(this,
                new UIEventArgs(question: currentQuestion,answer: response.Content,context: currentContext,status:status )
                );
        }
        private int ApproxTokenCount()
        {
            int count = 0;
            foreach (var msg in messages)
            {
                count += msg.Content.Split(' ').Length;
            }
            return count * 3;
        }
        public override void AskQuestion(string question, string? context = null)
        {
            bool newContext = (context != null || context != currentContext);
            if (newContext)
            {
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
    }
}