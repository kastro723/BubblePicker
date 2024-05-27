// Ver. 2.0.3
// Updated: 2024-05-28

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIBubblePicker : MonoBehaviour
{

    public enum eMoveType { Up, Down } // bg�� �þ�� ����

    public enum eTextSort { Left, Center, Right } // �ؽ�Ʈ ���� ����

    public ContentSizeFitter bgFitter;
    public TextMeshProUGUI text; // �ؽ�Ʈ 
    public Image bg; // ��� �̹��� ��ǥ
    public Image picker; // picker �̹��� ��ǥ
    public Transform pickerTrans; // pickerTrans(picker�� ������ ��ġ)

    public float pickerOffsetY = 0; // bg�� picker�� ���� ���� ����

    public float bgWidth; // image�� ���� ����
    public float textWidth; // text�� ���� ����

    public float textFontSize = 30;

    public float horizontalPadding = 0; // ��, �� padding
    public float verticalPadding = 0;  // ��, �� padding
    public float paragraphSpacing = 0; // ���� �� ����
    public float lineSpacing = 0; //    �� �� ����

    public Vector2 useIfEmptyTextDefaultSize; // �ؽ�Ʈ�� ���� ��, image�� �⺻ ������ ����

    public eMoveType moveType = eMoveType.Up; // �⺻�� Up���� ����
    public eTextSort textSort = eTextSort.Left; // �⺻�� Left�� ����






    private void Awake()
    {
        // ���� ���� ������Ʈ�� �θ𿡼� Canvas ������Ʈ�� ã��
        if (canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();
        }

        // "UI" ���̾ �ִ� ī�޶� ã��
        if (uiCamera == null)
        {
            Camera[] cameras = FindObjectsOfType<Camera>();
            foreach (Camera cam in cameras)
            {
                if (cam.gameObject.layer == LayerMask.NameToLayer("UI"))
                {
                    uiCamera = cam;
                    break;  // ������ ī�޶� ã������ �ݺ� �ߴ�
                }
            }
        }
    }


    public void Start()
    {
        if (canvas == null || uiCamera == null)
        {
            Debug.LogError("Canvas or Camera is not set properly.");
            return;
        }
    }

    private Canvas canvas;
    private Camera uiCamera;

    // Ʃ�丮�� �ڽ��� �ʱ�ȭ�ϴ� �޼���
    public void Init(Canvas canvas, Camera uiCamera)
    {
        this.canvas = canvas;
        this.uiCamera = uiCamera;
        Debug.LogFormat("<color=cyan>UITutorialBox �ʱ�ȭ : {0},{1}</color>", canvas, uiCamera);
    }


    //Ʃ�丮�� �ڽ��� ǥ���ϴ� �޼��� (Code)
    public void Show(string message, eMoveType moveType, eTextSort textSort)
    {
        this.moveType = moveType;
        this.textSort = textSort;

        Debug.Log($"Message: {message}, PickerTrans Position: {pickerTrans.position}, MoveType: {moveType}, TextSort: {textSort}");
        Debug.Log($"<color=yellow>[Ʃ�丮��] : {message}, {moveType}, {textSort} </color>");

        float textHeight = this.text.preferredHeight;

        bg.rectTransform.sizeDelta = new Vector2(bgWidth, textHeight + verticalPadding * 2);

      

        if (moveType == eMoveType.Up)
        {

            picker.transform.rotation = Quaternion.Euler(0, 0, 0);
            bg.rectTransform.pivot = new Vector2(0.5f, 0f);

            picker.rectTransform.pivot = new Vector2(0.5f, 0f);

            Vector3 pickerWorldPosition = pickerTrans.position;  // ���� ��ǥ
            Vector3 pickerScreenPosition = Camera.main.WorldToScreenPoint(pickerWorldPosition);  // ��ũ�� ��ǥ
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, pickerScreenPosition, canvas.worldCamera, out Vector2 localPosition);

            picker.rectTransform.localPosition = new Vector3(localPosition.x, localPosition.y, 0);
            bg.rectTransform.localPosition = new Vector3(0, localPosition.y + picker.rectTransform.rect.height - pickerOffsetY, 0);
        }
        else if (moveType == eMoveType.Down)
        {
            picker.transform.rotation = Quaternion.Euler(0, 0, 180);
            bg.rectTransform.pivot = new Vector2(0.5f, 1f);

            picker.rectTransform.pivot = new Vector2(0.5f, 0f);

            Vector3 pickerWorldPosition = pickerTrans.position;  // ���� ��ǥ
            Vector3 pickerScreenPosition = Camera.main.WorldToScreenPoint(pickerWorldPosition);  // ��ũ�� ��ǥ
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, pickerScreenPosition, canvas.worldCamera, out Vector2 localPosition);

            picker.rectTransform.localPosition = new Vector3(localPosition.x, localPosition.y, 0);
            bg.rectTransform.localPosition = new Vector3(0, localPosition.y - picker.rectTransform.rect.height + pickerOffsetY, 0);

        }

        text.paragraphSpacing = paragraphSpacing;
        text.lineSpacing = lineSpacing;
        text.fontSize = textFontSize;
        text.rectTransform.anchoredPosition = new Vector2(0, 0);

        switch (textSort)
        {
            case eTextSort.Left:
                text.rectTransform.anchorMin = new Vector2(0f, 0.5f);
                text.rectTransform.anchorMax = new Vector2(0f, 0.5f);
                text.rectTransform.pivot = new Vector2(0f, 0.5f);
                break;
            case eTextSort.Center:
                text.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                text.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                text.rectTransform.pivot = new Vector2(0.5f, 0.5f);
                break;
            case eTextSort.Right:
                text.rectTransform.anchorMin = new Vector2(1f, 0.5f);
                text.rectTransform.anchorMax = new Vector2(1f, 0.5f);
                text.rectTransform.pivot = new Vector2(1f, 0.5f);
                break;
        }

        text.rectTransform.sizeDelta = new Vector2(textWidth, text.preferredHeight);
        text.margin = new Vector4(horizontalPadding, verticalPadding, horizontalPadding, verticalPadding);
        text.text = message;

        Canvas.ForceUpdateCanvases();
        bgFitter.SetLayoutHorizontal();
        bgFitter.SetLayoutVertical();

        // ù ��° ȣ��(Show)�� ���̾ƿ� ������ �ʱ�ȭ�ϰ�, �� ��° ȣ��(Fit)�� �� ��������� Ȯ�������� ����
        Fits();

    }

    public void Fit() 
    {
        Fits();
        Fits();
    }

    //Ʃ�丮�� �ڽ��� ǥ���ϴ� �޼��� (Editor)
    private void Fits()
    {
        Debug.Log($"Message: {text.text}, PickerTrans Position: {pickerTrans.position}, MoveType: {moveType}, TextSort: {textSort}");
        Debug.Log($"<color=yellow>[Ʃ�丮��] : {text.text}, {moveType}, {textSort}</color>");

        float textHeight = this.text.preferredHeight;

        bg.rectTransform.sizeDelta = new Vector2(bgWidth, textHeight + verticalPadding * 2);

   

        

        if (moveType == eMoveType.Up)
        {
            picker.transform.rotation = Quaternion.Euler(0, 0, 0);
            bg.rectTransform.pivot = new Vector2(0.5f, 0f);

            picker.rectTransform.pivot = new Vector2(0.5f, 0f);

            Vector3 pickerWorldPosition = pickerTrans.position;  // ���� ��ǥ
            Vector3 pickerScreenPosition = Camera.main.WorldToScreenPoint(pickerWorldPosition);  // ��ũ�� ��ǥ

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, pickerScreenPosition, canvas.worldCamera, out Vector2 localPosition);
            picker.rectTransform.localPosition = localPosition;
            bg.rectTransform.localPosition = new Vector3(0, localPosition.y + picker.rectTransform.rect.height - pickerOffsetY, 0);
        }
        else if (moveType == eMoveType.Down)
        {
            picker.transform.rotation = Quaternion.Euler(0, 0, 180);
            bg.rectTransform.pivot = new Vector2(0.5f, 1f);

            picker.rectTransform.pivot = new Vector2(0.5f, 0f);

            Vector3 pickerWorldPosition = pickerTrans.position;  // ���� ��ǥ
            Vector3 pickerScreenPosition = Camera.main.WorldToScreenPoint(pickerWorldPosition);  // ��ũ�� ��ǥ

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, pickerScreenPosition, canvas.worldCamera, out Vector2 localPosition);
            picker.rectTransform.localPosition = localPosition;
            bg.rectTransform.localPosition = new Vector3(0, localPosition.y - picker.rectTransform.rect.height + pickerOffsetY, 0);

        }

        text.paragraphSpacing = paragraphSpacing;
        text.lineSpacing = lineSpacing;
        text.fontSize = textFontSize;
        text.rectTransform.anchoredPosition = new Vector2(0, 0);

        switch (textSort)
        {
            case eTextSort.Left:
                text.rectTransform.anchorMin = new Vector2(0f, 0.5f);
                text.rectTransform.anchorMax = new Vector2(0f, 0.5f);
                text.rectTransform.pivot = new Vector2(0f, 0.5f);
                break;
            case eTextSort.Center:
                text.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                text.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                text.rectTransform.pivot = new Vector2(0.5f, 0.5f);
                break;
            case eTextSort.Right:
                text.rectTransform.anchorMin = new Vector2(1f, 0.5f);
                text.rectTransform.anchorMax = new Vector2(1f, 0.5f);
                text.rectTransform.pivot = new Vector2(1f, 0.5f);
                break;
        }

        text.rectTransform.sizeDelta = new Vector2(textWidth, text.preferredHeight);
        text.margin = new Vector4(horizontalPadding, verticalPadding, horizontalPadding, verticalPadding);

        Canvas.ForceUpdateCanvases();
        bgFitter.SetLayoutHorizontal();
        bgFitter.SetLayoutVertical();

    }

    public TextMeshProUGUI GetText()
    {
        return text;
    }

    public void SetText(TextMeshProUGUI text)
    {
        this.text = text;
    }

    public Image GetBg()
    {
        return bg;
    }

    public void SetBg(Image bg)
    {
        this.bg = bg;
    }

    public ContentSizeFitter GetFitter()
    {
        return bgFitter;
    }

    public void SetFitter(ContentSizeFitter fitter)
    {
        this.bgFitter = fitter;
    }

    public Canvas GetCanvas()
    {
        return canvas;
    }

    public void SetCanvas(Canvas canvas)
    {
        this.canvas = canvas;
    }

}
