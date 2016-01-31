using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

    Image img;

	// Use this for initialization
	void Start () {
        img = GetComponent<Image>();
        StartCoroutine(Fade());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator Fade()
    {
        float time = 0.0f;

        while (time < 5.0f)
        {
            time += Time.deltaTime;
            img.color = Color.Lerp(new Color(1.0f,1.0f,1.0f,0.0f), Color.white, time / 5.0f);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(2.0f);

        time = 0.0f;

        while (time < 5.0f)
        {
            time += Time.deltaTime;
            img.color = Color.Lerp(Color.white, new Color(1.0f, 1.0f, 1.0f, 0.0f), time / 5.0f);
            yield return new WaitForEndOfFrame();
        }
    }
}
