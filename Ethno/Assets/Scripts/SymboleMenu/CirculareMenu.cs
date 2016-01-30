﻿using UnityEngine;
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

    public int playerValue { get; private set; }

	// Use this for initialization
	void Start ()
    {
        playerValue = 0;
        rectTransf = transform as RectTransform;

        buttons = new List<MenuButton>();

        for (int i = 0; i < 0; i++ )
        {
            GameObject go = GameObject.Instantiate(ButtonPrefab);
            go.transform.SetParent(rectTransf);
            go.GetComponent<MenuButton>().SetValue(1 << i);
            buttons.Add(go.GetComponent<MenuButton>());
        }

        gameObject.SetActive(false);
	}

    void OnEnable()
    {
        if (buttons != null && buttons.Count > 0)
        {
            Vector2 orientation = Vector2.up * buttonDistance;
            float angle = 360.0f / buttons.Count;

            if (buttons.Count % 2 == 0)
                orientation = Rotate(orientation, angle / 2.0f);

            for (int i = 0; i < buttons.Count; i++)
            {
                RectTransform rect = buttons[i].transform as RectTransform;
                rect.offsetMax = Vector2.one * buttonSize;
                rect.offsetMin = Vector2.zero;
                rect.anchoredPosition = rectTransf.anchoredPosition + orientation;
                orientation = Rotate(orientation, angle);
            }
        }
    }

    public Vector2 Rotate(this Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    public void AddButton(Sprite img, int value)
    {
        if (buttons == null)
            buttons = new List<MenuButton>();

        GameObject go = GameObject.Instantiate(ButtonPrefab);
        go.transform.SetParent(rectTransf);
        go.GetComponent<MenuButton>().SetImage(img);
        go.GetComponent<MenuButton>().SetValue(value);
        buttons.Add(go.GetComponent<MenuButton>());
    }

    public void AddValue(int value)
    {
        playerValue = playerValue | value;
    }

    public void RemoveValue(int value)
    {
        playerValue = playerValue & (int.MaxValue ^ value);
    }
}