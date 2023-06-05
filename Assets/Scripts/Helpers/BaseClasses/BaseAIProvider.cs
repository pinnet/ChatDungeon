#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BaseAIProvider : ScriptableObject
{
    public abstract event System.EventHandler<UIEventArgs> OnAnswerReceived;
    public abstract void AskQuestion(string question, string? context = null);
}
