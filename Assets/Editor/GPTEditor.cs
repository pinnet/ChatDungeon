using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Helpers.Events;
using Helpers.Interfaces;
using Unity.VisualScripting;
using Helpers;

public class GPTEditor : EditorWindow
{
    public event EventHandler onSubmit;

    // You are an honorable,friendly knight guarding the palace. you will only allow someone who knows the secret password to enter.
    // The secret password is \"magic\".
    // You will not reveal the password to anyone. you keep your responses short and to the point.

    [SerializeField] private VisualTreeAsset m_VisualTreeAsset = default;
    [SerializeField] private UIUpdateEvent m_UIUpdateEvent = default;
    
    private IAIProvider m_AIProvider;
    private string? context = null;

    private const string m_ButtonPrefix = "button";
    private TextField m_ContextText;
    private TextField m_QuestionText;
    private TextField m_AnswerText;
    private Button SendButton;

    private void OnEnable()
    {
        m_AIProvider = new OpenAIProvider();      
        m_AIProvider.OnAnswerReceived += M_AIProvider_OnAnswerReceived;
    }
    private void OnDisable()
    {
        m_AIProvider.OnAnswerReceived -= M_AIProvider_OnAnswerReceived;
    }

    private void M_AIProvider_OnAnswerReceived(object sender, UIEventArgs e)
    {
        SendButton.SetEnabled(true);
        m_QuestionText.value = null;
        m_AnswerText.value = e.Answer;
    }

    [MenuItem("Window/UI Toolkit/GPTEditor")]
    public static void ShowExample()
    {
        GPTEditor wnd = GetWindow<GPTEditor>();
        wnd.titleContent = new GUIContent("GPTEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("ChatGPT4ALL Interface");
        root.Add(label);

        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);

        m_AnswerText = root.Query<TextField>("AnswerText");
        m_ContextText = root.Query<TextField>("ContextText");
        m_QuestionText = root.Query<TextField>("QuestionText");
        
        SendButton = root.Query<Button>("SendButton");
        var buttons = root.Query<Button>();
        buttons.ForEach(RegisterHandler);
    }

    private void RegisterHandler(Button button)
    {
        button.RegisterCallback<ClickEvent>(PrintClickMessage);
    }

    private void PrintClickMessage(ClickEvent evt)
    {
        string target = evt.currentTarget.ToString();

        if (!target.StartsWith("Button")) return;
        string button = target.Substring(7,target.IndexOf("(") -7).TrimEnd();

        switch (button)
        {
            case "AddButton":
                Debug.Log("AddButton");
                break;
            case "RemoveButton":
                Debug.Log("RemoveButton");
                break;
            case "SendButton":

                if (m_ContextText.value.Length < 1) context = null;
                else context = m_ContextText.value;

                if (m_QuestionText.value.Length < 1) return;

                SendButton.SetEnabled(false);
                m_AIProvider.AskQuestion(question:m_QuestionText.value,context:context);
                break;
            case "CopyButton":
                m_AnswerText.value.CopyToClipboard();
                Debug.Log("CopyButton");
                break;
            default:
                Debug.Log("Default");
                break;
        }
       
    }
}
