// Ver. 2.0.2
// Updated: 2024-04-25

using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(UIBubblePicker))]
public class UIBubblePickerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        UIBubblePicker script = (UIBubblePicker)target;

        if (GUILayout.Button("Fit"))
        {
            // ������Ʈ �ʱ�ȭ �˻� �� ó��
            if (script.GetText() == null)
            {
                script.SetText(script.text.GetComponent<TextMeshProUGUI>());
                if (script.GetText() == null)
                {
                    Debug.LogError("TextMeshProUGUI component is not found on the child objects.");
                    return;
                }
            }

            if (script.GetBg() == null)
            {
                script.SetBg(script.bg.GetComponent<Image>());
                if (script.GetBg() == null)
                {
                    Debug.LogError("Bg component is not found on the child objects.");
                    return;
                }
            }

            if (script.GetFitter() == null)
            {
                script.SetFitter(script.bgFitter.GetComponent<ContentSizeFitter>());
                if (script.GetFitter() == null)
                {
                    Debug.LogError("ContentSizeFitter component is not found.");
                    return;
                }
            }
            if (script.GetCanvas() == null)
            {
                script.SetCanvas(script.GetComponent<Canvas>());
                if (script.GetCanvas() == null)
                {
                    Debug.LogError("Canvas component is not found.");
                    return;
                }
            }



            // ���� ĵ���� ������Ʈ �� Show �޼��� ȣ��
            // ù ��° ȣ���� ���̾ƿ� ������ �ʱ�ȭ�ϰ�, �� ��° ȣ���� �� ��������� Ȯ�������� ����
            Canvas.ForceUpdateCanvases();
            script.Fit();
            script.Fit();
        }
    }
}
