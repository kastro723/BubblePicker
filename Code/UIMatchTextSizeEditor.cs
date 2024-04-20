// Ver. 1.0.1
// Updated: 2024-04-20

using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;

[CustomEditor(typeof(UIMatchTextSize))]
public class UIMatchTextSizeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        UIMatchTextSize script = (UIMatchTextSize)target;

        if (GUILayout.Button("Fit"))
        {
            // ������Ʈ �ʱ�ȭ �˻� �� ó��
            if (script.GetText() == null)
            {
                script.SetText(script.GetComponentInChildren<TextMeshProUGUI>());
                if (script.GetText() == null)
                {
                    Debug.LogError("TextMeshProUGUI component is not found on the child objects.");
                    return;
                }
            }

            if (script.GetImage() == null)
            {
                script.SetImage(script.GetComponentInChildren<Image>());
                if (script.GetImage() == null)
                {
                    Debug.LogError("Image component is not found on the child objects.");
                    return;
                }
            }

            if (script.GetFitter() == null)
            {
                script.SetFitter(script.GetComponent<ContentSizeFitter>());
                if (script.GetFitter() == null)
                {
                    Debug.LogError("ContentSizeFitter component is not found.");
                    return;
                }
            }

            // ���� ĵ���� ������Ʈ �� Fit �޼��� ȣ��
            Canvas.ForceUpdateCanvases();
            script.Fit();
        }
    }
}
