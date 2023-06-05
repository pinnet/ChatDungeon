using Helpers.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "AIManager", menuName = "AIManager/AIManager")]
public class AIManagerSO : ScriptableObject
{
    public event System.EventHandler<UIEventArgs> OnAnswerReceived;

    [SerializeField] private BaseAIProvider m_AIProvider;
    private BaseAIProvider provider;

    private void Awake()
    {
        provider = Resources.Load<BaseAIProvider>(m_AIProvider.ToShortString()); 
    }

    private void OnEnable()
    {
        provider.OnAnswerReceived += Provider_OnAnswerReceived;
    }
    private void OnDisable()
    {
        provider.OnAnswerReceived -= Provider_OnAnswerReceived;
    }

    private void Provider_OnAnswerReceived(object sender, UIEventArgs e)
    {
        OnAnswerReceived?.Invoke(this, new UIEventArgs(question:e.Question, answer:e.Answer,context:e.Context,status:e.Status));
    }

    public void AskQuestion(string question, string context = null)
    {
        provider.AskQuestion(question, context);
    }
}
