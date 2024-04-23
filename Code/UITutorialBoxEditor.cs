// Ver. 2.0.0
// Updated: 2024-04-24

using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;

[CustomEditor(typeof(UITutorialBox))]
public class UITutorialBoxEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        UITutorialBox script = (UITutorialBox)target;

        if (GUILayout.Button("Show"))
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
                script.SetCanvas(script.GetComponentInParent<Canvas>());
                if (script.GetFitter() == null)
                {
                    Debug.LogError("Canvas component is not found.");
                    return;
                }
            }
       


            // 강제 캔버스 업데이트 후 Show 메서드 호출
            Canvas.ForceUpdateCanvases();

            script.Show(script.picker.transform.position,script.moveType,script.textSort,script.text.text);
        }
    }
}
