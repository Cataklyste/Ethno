using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{

	Image img;

	private bool haveClicked;

	public float fadeInFloat = 5.0f, fadeOutFloat = 5.0f;

	// Use this for initialization
	void Start()
	{
		img = GetComponent<Image>();
		StartCoroutine(FadeIn());
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetMouseButtonDown(0) && !haveClicked)
			StartCoroutine(FadeOut());			
	}

	IEnumerator FadeIn()
	{
		float time = 0.0f;

		while (time < fadeInFloat)
		{
			time += Time.deltaTime;
			img.color = Color.Lerp(new Color(1.0f, 1.0f, 1.0f, 0.0f), Color.white, time / fadeInFloat);
			yield return new WaitForEndOfFrame();
		}


	}
	IEnumerator FadeOut()
	{
		float time = 0.0f;

		haveClicked = true;

		while (time < fadeOutFloat)
		{
			time += Time.deltaTime;
			img.color = Color.Lerp(Color.white, new Color(1.0f, 1.0f, 1.0f, 0.0f), time / fadeOutFloat);
			yield return new WaitForEndOfFrame();
		}
	}
}
