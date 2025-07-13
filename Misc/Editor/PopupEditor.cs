using UnityEditor;
using UnityEngine;

public class PopupEditor : EditorWindow
{
    float TimeToLive;
    string popupText;

    public void ShowPopup(float time, string text)
    {
        TimeToLive = time;
        popupText = text;
    }

    void OnGUI()
    {
        TimeToLive -= Time.deltaTime;
        EditorGUILayout.LabelField(popupText);
        if (TimeToLive < 0)
        {
            Close();
        }
    }
}


