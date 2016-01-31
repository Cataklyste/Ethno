using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour {
	
	public AudioSource[] Source;
	public int DurationFade;
	
	private float[] Volume;
	
	void Awake ()
	{
		if (Source.Length != 0)
		{
			Volume = new float[Source.Length];
			for (int i = 0; i < Source.Length ; i++)
				Volume[i] = Source[i].volume;

			StartCoroutine (RoutineFade ());
		}	



	}
	
	IEnumerator RoutineFade ()
	{
		float multiplier;
		float time = 0;
		
		while (time < DurationFade) 
		{
			for (int i = 0; i < Source.Length; i++) 
			{
				multiplier = Volume [i] / DurationFade;
				Source [i].volume -= multiplier * Time.deltaTime;
			}
			
			time += Time.deltaTime;
			
			yield return null;
		}
		for (int i = 0; i < Source.Length; i++)
			Source [i].volume = 0;
	}
}
