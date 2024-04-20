// Ver. 1.0.1
// Updated: 2024-04-20

using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(ContentSizeFitter))]
public class UIMatchTextSize : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;
    [SerializeField] private Transform picker;

    public float pickerOffsetY = 0; // image와 picker가 붙을 때 offset

    public float imageWidth; // image의 가로 길이

    public float horizontalPadding = 10; // 좌, 우 padding
    public float verticalPadding = 10;  // 상, 하 padding
    public float paragraphSpacing = 0; // 줄 간 간격
    
    public Vector2 useIfEmptyTextDefaultSize; // 텍스트가 없을 때, image의 기본 사이즈 설정

    public enum MoveType { Down, Up } // 이미지가 늘어나는 방향
    public enum TextSort { Left, Center, Right } // 텍스트 정렬 방향

    [SerializeField] private MoveType moveType;
    [SerializeField] private TextSort textSort;

    private ContentSizeFitter fitter;

    private void Awake()
    {
        fitter = GetComponent<ContentSizeFitter>();
    }

    public void Fit()
    {
        if (string.IsNullOrEmpty(this.text.text))
        {
            image.rectTransform.sizeDelta = useIfEmptyTextDefaultSize;
            return;
        }

        float height = text.preferredHeight + verticalPadding * 2;
        image.rectTransform.sizeDelta = new Vector2(imageWidth + horizontalPadding * 2, height);

        Vector3 pickerTopCenter = picker.position;
        picker.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1f);

        if (moveType == MoveType.Up)
        {
            picker.rotation = Quaternion.Euler(0, 0, 0);
            image.rectTransform.pivot = new Vector2(0.5f, 0f);
            image.rectTransform.position = new Vector3(image.rectTransform.position.x, pickerTopCenter.y - pickerOffsetY, pickerTopCenter.z);
        }
        else if (moveType is MoveType.Down)
        {
            picker.rotation = Quaternion.Euler(0, 0, 180);
            image.rectTransform.pivot = new Vector2(0.5f, 1f);
            image.rectTransform.position = new Vector3(image.rectTransform.position.x, pickerTopCenter.y + pickerOffsetY, pickerTopCenter.z);
        }

        text.paragraphSpacing = paragraphSpacing;
        text.rectTransform.anchoredPosition = new Vector2(0, 0);

        switch (textSort)
        {
            case TextSort.Left:
                text.rectTransform.anchorMin = new Vector2(0f, 0.5f);
                text.rectTransform.anchorMax = new Vector2(0f, 0.5f);
                text.rectTransform.pivot = new Vector2(0f, 0.5f);
                break;
            case TextSort.Center:
                text.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                text.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                text.rectTransform.pivot = new Vector2(0.5f, 0.5f);
                break;
            case TextSort.Right:
                text.rectTransform.anchorMin = new Vector2(1f, 0.5f);
                text.rectTransform.anchorMax = new Vector2(1f, 0.5f);
                text.rectTransform.pivot = new Vector2(1f, 0.5f);
                break;
        }

        text.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, text.preferredHeight);
        text.margin = new Vector4(horizontalPadding, verticalPadding, horizontalPadding, verticalPadding);

        Canvas.ForceUpdateCanvases();
        fitter.SetLayoutHorizontal();
        fitter.SetLayoutVertical();
    }


    public TextMeshProUGUI GetText()
    {
        return text;
    }

    public void SetText(TextMeshProUGUI text)
    {
        this.text = text;
    }

    public Image GetImage()
    {
        return image;
    }

    public void SetImage(Image image)
    {
        this.image = image;
    }

    public ContentSizeFitter GetFitter()
    {
        return fitter;
    }

    public void SetFitter(ContentSizeFitter fitter)
    {
        this.fitter = fitter;
    }
}
