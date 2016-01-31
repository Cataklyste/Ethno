using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Credit : MonoBehaviour {

	Image img;
	private bool haveClicked, isInCredit;
	public float fadeInFloat = 1.0f, fadeOutFloat = 1.0f;
	public BoxCollider Bed;


	void Start () {
		img = GetComponent<Image>();
	}
	
	void Update () {

		if (Input.GetMouseButtonDown(0) && !isInCredit)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit))
			{
				if (raycastHit.collider == Bed as Collider)
				{
					img.enabled = true;
					StartCoroutine(FadeIn());
					isInCredit = true;
				}
			}

		}
		else if(Input.GetMouseButtonDown(0) && !haveClicked)
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
		img.enabled = false;
	}
}
