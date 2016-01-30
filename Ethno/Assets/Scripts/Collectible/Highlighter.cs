using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Highlighter : MonoBehaviour {

    private SpriteRenderer spRenderer;

    [SerializeField]
    private Color color1;
    [SerializeField]
    private Color color2;

    private float time = 0.0f;
    private int timeScaler = 1;

	// Use this for initialization
	void Start () {
        spRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        time += timeScaler * Time.deltaTime;

        if (time >= 1.0f)
        {
            timeScaler = -1;
            time = 1.0f;
	    }
        if (time <= 0.0f)
        {
            timeScaler = 1;
            time = 0.0f;
	    }

        spRenderer.color = Color.Lerp(color1, color2, time);
	}
}
