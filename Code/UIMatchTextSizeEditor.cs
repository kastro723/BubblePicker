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
            // 컴포넌트 초기화 검사 및 처리
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

            // 강제 캔버스 업데이트 후 Fit 메서드 호출
            Canvas.ForceUpdateCanvases();
            script.Fit();
        }
    }
}
