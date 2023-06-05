using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(AIManagerSO))]
[CanEditMultipleObjects]
public class AIManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        AIManagerSO aiManager = (AIManagerSO)target;
        if (GUILayout.Button("Open Chat Dialog Editor"))
        {
            MenuCommand command = new MenuCommand(target);
            AIEditorWindow.CreateWindow(command);
        }
    }

}
