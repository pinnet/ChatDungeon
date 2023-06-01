using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GPTEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;
    private int m_ClickCount = 0;
    private TreeView tree;
    private const string m_ButtonPrefix = "button";


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

        var buttons = root.Query<Button>();
        buttons.ForEach(RegisterHandler);
        tree = root.Q<TreeView>();
        
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
                Debug.Log("SendButton");
                break;
            case "CopyButton":
                Debug.Log("CopyButton");
                break;
            default:
                Debug.Log("Default");
                break;
        }
       
    }
}
