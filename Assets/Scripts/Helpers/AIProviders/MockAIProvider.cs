#pragma warning disable 0067, 0414

using Helpers.Interfaces;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "MockAIProvider", menuName = "AIManager/MockAIProvider", order = 1)]
public class MockAIProvider : BaseAIProvider
{
    public override event EventHandler<UIEventArgs> OnAnswerReceived;

    public override void AskQuestion(string question, string context = null)
    {
       OnAnswerReceived?.Invoke(this, new UIEventArgs(question:question,answer:$"Answer to {question}:",context:context));
    }
}
