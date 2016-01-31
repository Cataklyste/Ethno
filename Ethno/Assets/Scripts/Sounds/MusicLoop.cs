using UnityEngine;
using System.Collections;

public class MusicLoop : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		SoundManager.Instance.PlaySfx(gameObject, "M_V2_Start");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (SoundManager.Instance.CanIPlaySound(gameObject))
		{
			SoundManager.Instance.PlaySfx(gameObject, "M_V2_Loop", 1.0f, false, 500.0f, true);
		}
	}
}
