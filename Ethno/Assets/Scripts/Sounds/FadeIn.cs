using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour {
	
	public AudioSource Source;
	public int DurationFade;
    public string sourceStartName;
    public string sourceName;

	private float Volume;
	
	IEnumerator RoutineFade ()
	{
		float multiplier;
		float time = 0;
		
		while (time < DurationFade) 
		{
			multiplier = Volume / DurationFade;
			Source.volume += multiplier * Time.deltaTime;
			
			time += Time.deltaTime;
			
			yield return null;
		}

		Source.volume = Volume;
	}

    void OnTriggerEnter()
    {
        Source.clip = Resources.Load<AudioClip>("Audio/" + sourceName);

        Volume = Source.volume;
        Source.volume = 0;
        Source.Play();


        StartCoroutine(RoutineFade());
    }
}
