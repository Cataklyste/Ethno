using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CirculareMenu : MonoBehaviour {

    [SerializeField]
    private GameObject ButtonPrefab;

    private RectTransform rectTransf;
    private List<MenuButton> buttons;

    [SerializeField]
    private int buttonSize = 75;
    [SerializeField]
    private int buttonDistance = 200;

    private int nbButtonPressed = 0;

    public int playerValue { get; private set; }

    public IA ia;

	// Use this for initialization
	void Start ()
    {
        playerValue = 0;
        rectTransf = transform as RectTransform;

        buttons = new List<MenuButton>();

        //for (int i = 0; i < 3; i++ )
        //{
        //    GameObject go = GameObject.Instantiate(ButtonPrefab);
        //    go.transform.SetParent(rectTransf);
        //    go.GetComponent<MenuButton>().SetValue(1 << i);
        //    buttons.Add(go.GetComponent<MenuButton>());
        //}

        gameObject.SetActive(false);
	}

    void OnEnable()
    {
		

		if (buttons != null && buttons.Count > 3)
        {
            Vector2 orientation = Vector2.up * buttonDistance;
            float angle = 360.0f / buttons.Count;

            if (buttons.Count % 2 == 0)
                orientation = Rotate(orientation, angle / 2.0f);

            for (int i = 0; i < buttons.Count; i++)
            {
                RectTransform rect = buttons[i].transform as RectTransform;
                rect.gameObject.SetActive(true);
                rect.offsetMax = Vector2.one * buttonSize;
                rect.offsetMin = Vector2.zero;
                rect.anchoredPosition = rectTransf.anchoredPosition + orientation;
                orientation = Rotate(orientation, angle);
            }
        }
    }

    void OnDisable()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            RectTransform rect = buttons[i].transform as RectTransform;
			rect.gameObject.SetActive(false);
        }
    }

	public void SUPER()
	{
		for (int i = 0; i < buttons.Count; i++)
			buttons[i].resetImagePos();
	}

	public Vector2 Rotate(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    public void AddButton(Sprite img, int value, int numberTangram)
    {
        if (buttons == null)
            buttons = new List<MenuButton>();

        GameObject go = GameObject.Instantiate(ButtonPrefab);
        go.transform.SetParent(rectTransf);
        go.GetComponent<MenuButton>().SetImage(img);
        go.GetComponent<MenuButton>().SetValue(value);
        go.GetComponent<MenuButton>().SetTangramNumber(numberTangram);
		buttons.Add(go.GetComponent<MenuButton>());
    }

    public void AddValue(int value)
    {
        nbButtonPressed++;
        playerValue = playerValue | value;

        if (nbButtonPressed == 4)
        {
            ia.Answer(playerValue);
			nbButtonPressed = 0;
			playerValue = 0;
		}
    }

    public void RemoveValue(int value)
    {
        nbButtonPressed--;
        playerValue = playerValue & (int.MaxValue ^ value);
    }
}
