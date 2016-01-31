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
    private int tangramNumber;
	private bool isStarted = false;


	void OnEnable()
	{
		image = GetComponent<Image>();
	}

	void Start ()
    {
		PlaceHolder = transform.GetChild(0) as RectTransform;
        PlaceHolder.GetComponent<Image>().sprite = image.sprite;
		rectTransf = transform as RectTransform;
		menu = rectTransf.parent.GetComponent<CirculareMenu>();

		isStarted = true;
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

	public void SetTangramNumber(int tangramNumber)
	{
        this.tangramNumber = tangramNumber;
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
            
            StartCoroutine(TranslateImageToMiddle());
			PlayTangramSound();
        }
        else
        {
            image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            menu.RemoveValue(value);
            StartCoroutine(TranslateImageToButton());
		}
    }

	private void PlayTangramSound()
	{
		switch (tangramNumber)
		{
			case 1:
				SoundManager.Instance.PlaySfx(gameObject, "Sfx_Tangram_01_C");
				Debug.Log("C");
				break;
			case 2:
				SoundManager.Instance.PlaySfx(gameObject, "Sfx_Tangram_02_C#");
				Debug.Log("C#");
				break;
			case 3:
				SoundManager.Instance.PlaySfx(gameObject, "Sfx_Tangram_03_D");
				Debug.Log("D");
				break;
			case 4:
				SoundManager.Instance.PlaySfx(gameObject, "Sfx_Tangram_04_D#");
				Debug.Log("D#");
				break;
			case 5:
				SoundManager.Instance.PlaySfx(gameObject, "Sfx_Tangram_05_E");
				Debug.Log("E");
				break;
			case 6:
				SoundManager.Instance.PlaySfx(gameObject, "Sfx_Tangram_06_F");
				Debug.Log("F");
				break;
			case 7:
				SoundManager.Instance.PlaySfx(gameObject, "Sfx_Tangram_07_F#");
				Debug.Log("F#");
				break;
			case 8:
				SoundManager.Instance.PlaySfx(gameObject, "Sfx_Tangram_08_G");
				Debug.Log("G");
				break;
			case 9:
				SoundManager.Instance.PlaySfx(gameObject, "Sfx_Tangram_09_G#");
				Debug.Log("G#");
				break;
			case 10:
				SoundManager.Instance.PlaySfx(gameObject, "Sfx_Tangram_10_A");
				Debug.Log("A");
				break;
			case 11:
				SoundManager.Instance.PlaySfx(gameObject, "Sfx_Tangram_11_A#");
				Debug.Log("A#");
				break;
			case 12:
				SoundManager.Instance.PlaySfx(gameObject, "Sfx_Tangram_12_B");
				Debug.Log("B");
				break;
			default:
				SoundManager.Instance.PlaySfx(gameObject, "Sfx_Tangram_01_C");
				Debug.Log("Default");
				Debug.Log(tangramNumber);
				break;
		}
	}

    IEnumerator TranslateImageToMiddle()
    {
		PlaceHolder.SetParent(rectTransf.parent);

	    Vector2 positionTarget = -Vector2.up*50;

		Vector2 direction = positionTarget - PlaceHolder.anchoredPosition;
        direction.Normalize();

        while (PlaceHolder.anchoredPosition != positionTarget && translateToMiddle)
        {
            float distance = Vector2.Distance(PlaceHolder.anchoredPosition, positionTarget);
            if (distance > 1.0f)
                PlaceHolder.Translate(direction * Time.deltaTime * distance * speed);
            else
                PlaceHolder.anchoredPosition = positionTarget;

            yield return new WaitForEndOfFrame();
        }
		Debug.Log("nice");
		menu.AddValue(value);
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

	public void resetImagePos()
	{
		if (!isStarted) return;

		if (PlaceHolder == null || rectTransf == null)
			return;

		translateToMiddle = false;
		PlaceHolder.SetParent(rectTransf);
		PlaceHolder.anchoredPosition = Vector2.zero;
		SoundManager.Instance.StopSfx(gameObject);
	}
}
