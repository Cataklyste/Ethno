using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class MenuButton : Button {

    private Image image;
    private RectTransform rectTransf;

    private RectTransform PlaceHolder;
    private bool translateToMiddle = false;
    private CirculareMenu menu;
    [SerializeField]
    private float speed;

    private int value;

	void Start ()
    {
        PlaceHolder = transform.GetChild(0) as RectTransform;
        rectTransf = transform as RectTransform;
        menu = rectTransf.parent.GetComponent<CirculareMenu>();
	}

    public void SetImage(Sprite img)
    {
        image = GetComponent<Image>();
        image.sprite = img;
    }

    public void SetValue(int value)
    {
        this.value = value;
    }

    public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (eventData.button != UnityEngine.EventSystems.PointerEventData.InputButton.Left)
            return;

        translateToMiddle = !translateToMiddle;

        if (translateToMiddle)
        {
            image.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            menu.AddValue(value);
            StartCoroutine(TranslateImageToMiddle());
        }
        else
        {
            image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            menu.RemoveValue(value);
            StartCoroutine(TranslateImageToButton());
        }

    }

    IEnumerator TranslateImageToMiddle()
    {
        PlaceHolder.SetParent(rectTransf.parent);

        Vector2 direction = Vector2.zero - PlaceHolder.anchoredPosition;
        direction.Normalize();

        while (PlaceHolder.anchoredPosition != Vector2.zero && translateToMiddle)
        {
            float distance = Vector2.Distance(PlaceHolder.anchoredPosition, Vector2.zero);
            if (distance > 1.0f)
                PlaceHolder.Translate(direction * Time.deltaTime * distance * speed);
            else
                PlaceHolder.anchoredPosition = Vector2.zero;

            yield return new WaitForEndOfFrame();
        }

    }

    IEnumerator TranslateImageToButton()
    {


        Vector2 centerPos = rectTransf.anchoredPosition;

        Vector2 direction = centerPos - PlaceHolder.anchoredPosition;
        direction.Normalize();

        while (PlaceHolder.anchoredPosition != centerPos && !translateToMiddle)
        {
            float distance = Vector2.Distance(PlaceHolder.anchoredPosition, centerPos);
            if (distance > 5.0f)
                PlaceHolder.Translate(direction * Time.deltaTime * distance * speed);
            else
                PlaceHolder.anchoredPosition = centerPos;

            yield return new WaitForEndOfFrame();
        }
        PlaceHolder.SetParent(rectTransf);
    }

}
