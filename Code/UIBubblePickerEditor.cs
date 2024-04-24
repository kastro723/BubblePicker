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
            // 컴포넌트 초기화 검사 및 처리
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



            // 강제 캔버스 업데이트 후 Show 메서드 호출
            // 첫 번째 호출이 레이아웃 변경을 초기화하고, 두 번째 호출이 이 변경사항을 확정적으로 적용
            Canvas.ForceUpdateCanvases();
            script.Fit();
            script.Fit();
        }
    }
}
