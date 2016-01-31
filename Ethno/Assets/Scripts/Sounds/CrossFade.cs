using UnityEngine;
using System.Collections;

public class CrossFade : MonoBehaviour {

    public GameObject fadeIn;
    public GameObject fadeOut;

    public float timeCrossFade;
    public float fadeInBaseVolume;
    public float fadeOutBaseVolume;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator FadeIn()
    {
        if (!fadeIn.activeInHierarchy)
            fadeIn.SetActive(true);
        float time = 0.0f;
        float multiplier;
        fadeInBaseVolume = fadeIn.GetComponent<AudioSource>().volume;
        fadeIn.GetComponent<AudioSource>().volume = 0.0f;
        while (time < timeCrossFade)
        {
            multiplier = 1.0f / timeCrossFade;
            fadeIn.GetComponent<AudioSource>().volume = multiplier * time;

            time += Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        if (!fadeOut.activeInHierarchy)
            fadeOut.SetActive(true);
        float time = 0.0f;
        float multiplier;
        fadeOutBaseVolume = fadeOut.GetComponent<AudioSource>().volume;

        while (time < timeCrossFade)
        {
            multiplier = 1.0f / timeCrossFade;
            fadeOut.GetComponent<AudioSource>().volume = 1 - multiplier * time;

            time += Time.deltaTime;

            yield return null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(FadeIn());
            StartCoroutine(FadeOut());
        }
    }
}
