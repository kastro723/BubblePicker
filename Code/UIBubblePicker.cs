// Ver. 2.0.3
// Updated: 2024-05-28

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIBubblePicker : MonoBehaviour
{

    public enum eMoveType { Up, Down } // bg가 늘어나는 방향

    public enum eTextSort { Left, Center, Right } // 텍스트 정렬 방향

    public ContentSizeFitter bgFitter;
    public TextMeshProUGUI text; // 텍스트 
    public Image bg; // 배경 이미지 좌표
    public Image picker; // picker 이미지 좌표
    public Transform pickerTrans; // pickerTrans(picker의 원점의 위치)

    public float pickerOffsetY = 0; // bg와 picker의 접합 높이 조정

    public float bgWidth; // image의 가로 길이
    public float textWidth; // text의 가로 길이

    public float textFontSize = 30;

    public float horizontalPadding = 0; // 좌, 우 padding
    public float verticalPadding = 0;  // 상, 하 padding
    public float paragraphSpacing = 0; // 엔터 간 간격
    public float lineSpacing = 0; //    줄 간 간격

    public Vector2 useIfEmptyTextDefaultSize; // 텍스트가 없을 때, image의 기본 사이즈 설정

    public eMoveType moveType = eMoveType.Up; // 기본은 Up으로 지정
    public eTextSort textSort = eTextSort.Left; // 기본은 Left로 지정






    private void Awake()
    {
        // 현재 게임 오브젝트의 부모에서 Canvas 컴포넌트를 찾음
        if (canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();
        }

        // "UI" 레이어에 있는 카메라를 찾음
        if (uiCamera == null)
        {
            Camera[] cameras = FindObjectsOfType<Camera>();
            foreach (Camera cam in cameras)
            {
                if (cam.gameObject.layer == LayerMask.NameToLayer("UI"))
                {
                    uiCamera = cam;
                    break;  // 적합한 카메라를 찾았으면 반복 중단
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

    // 튜토리얼 박스를 초기화하는 메서드
    public void Init(Canvas canvas, Camera uiCamera)
    {
        this.canvas = canvas;
        this.uiCamera = uiCamera;
        Debug.LogFormat("<color=cyan>UITutorialBox 초기화 : {0},{1}</color>", canvas, uiCamera);
    }


    //튜토리얼 박스를 표시하는 메서드 (Code)
    public void Show(string message, eMoveType moveType, eTextSort textSort)
    {
        this.moveType = moveType;
        this.textSort = textSort;

        Debug.Log($"Message: {message}, PickerTrans Position: {pickerTrans.position}, MoveType: {moveType}, TextSort: {textSort}");
        Debug.Log($"<color=yellow>[튜토리얼] : {message}, {moveType}, {textSort} </color>");

        float textHeight = this.text.preferredHeight;

        bg.rectTransform.sizeDelta = new Vector2(bgWidth, textHeight + verticalPadding * 2);

      

        if (moveType == eMoveType.Up)
        {

            picker.transform.rotation = Quaternion.Euler(0, 0, 0);
            bg.rectTransform.pivot = new Vector2(0.5f, 0f);

            picker.rectTransform.pivot = new Vector2(0.5f, 0f);

            Vector3 pickerWorldPosition = pickerTrans.position;  // 월드 좌표
            Vector3 pickerScreenPosition = Camera.main.WorldToScreenPoint(pickerWorldPosition);  // 스크린 좌표
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, pickerScreenPosition, canvas.worldCamera, out Vector2 localPosition);

            picker.rectTransform.localPosition = new Vector3(localPosition.x, localPosition.y, 0);
            bg.rectTransform.localPosition = new Vector3(0, localPosition.y + picker.rectTransform.rect.height - pickerOffsetY, 0);
        }
        else if (moveType == eMoveType.Down)
        {
            picker.transform.rotation = Quaternion.Euler(0, 0, 180);
            bg.rectTransform.pivot = new Vector2(0.5f, 1f);

            picker.rectTransform.pivot = new Vector2(0.5f, 0f);

            Vector3 pickerWorldPosition = pickerTrans.position;  // 월드 좌표
            Vector3 pickerScreenPosition = Camera.main.WorldToScreenPoint(pickerWorldPosition);  // 스크린 좌표
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

        // 첫 번째 호출(Show)이 레이아웃 변경을 초기화하고, 두 번째 호출(Fit)이 이 변경사항을 확정적으로 적용
        Fits();

    }

    public void Fit() 
    {
        Fits();
        Fits();
    }

    //튜토리얼 박스를 표시하는 메서드 (Editor)
    private void Fits()
    {
        Debug.Log($"Message: {text.text}, PickerTrans Position: {pickerTrans.position}, MoveType: {moveType}, TextSort: {textSort}");
        Debug.Log($"<color=yellow>[튜토리얼] : {text.text}, {moveType}, {textSort}</color>");

        float textHeight = this.text.preferredHeight;

        bg.rectTransform.sizeDelta = new Vector2(bgWidth, textHeight + verticalPadding * 2);

   

        

        if (moveType == eMoveType.Up)
        {
            picker.transform.rotation = Quaternion.Euler(0, 0, 0);
            bg.rectTransform.pivot = new Vector2(0.5f, 0f);

            picker.rectTransform.pivot = new Vector2(0.5f, 0f);

            Vector3 pickerWorldPosition = pickerTrans.position;  // 월드 좌표
            Vector3 pickerScreenPosition = Camera.main.WorldToScreenPoint(pickerWorldPosition);  // 스크린 좌표

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, pickerScreenPosition, canvas.worldCamera, out Vector2 localPosition);
            picker.rectTransform.localPosition = localPosition;
            bg.rectTransform.localPosition = new Vector3(0, localPosition.y + picker.rectTransform.rect.height - pickerOffsetY, 0);
        }
        else if (moveType == eMoveType.Down)
        {
            picker.transform.rotation = Quaternion.Euler(0, 0, 180);
            bg.rectTransform.pivot = new Vector2(0.5f, 1f);

            picker.rectTransform.pivot = new Vector2(0.5f, 0f);

            Vector3 pickerWorldPosition = pickerTrans.position;  // 월드 좌표
            Vector3 pickerScreenPosition = Camera.main.WorldToScreenPoint(pickerWorldPosition);  // 스크린 좌표

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
