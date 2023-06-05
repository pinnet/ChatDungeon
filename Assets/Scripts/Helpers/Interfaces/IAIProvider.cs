#nullable enable
using System;
using UnityEditor;

namespace Helpers.Interfaces
{
    public interface IAIProvider
    {
        public abstract event System.EventHandler<UIEventArgs> OnAnswerReceived;
        public abstract void AskQuestion(string question,string? context = null);
    }
}
