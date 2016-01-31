using UnityEngine;
using System.Collections;

public class SignPreview : MonoBehaviour {

    public float displayTime = 2.0f;
    private float currentTime;
    private SpriteRenderer preview;
	// Use this for initialization
	void Start ()
    {
        preview = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (currentTime > 0.0f)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0.0f)
                preview.sprite = null;
        }
	}

    public void ResetTimer()
    {
        currentTime = displayTime;
    }
}
