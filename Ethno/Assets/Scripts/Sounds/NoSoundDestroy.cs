using UnityEngine;
using System.Collections;

public class NoSoundDestroy : MonoBehaviour {

	void Update () 
	{
		if (SoundManager.Instance.CanIPlaySound(gameObject))
			Destroy(gameObject);
	}
}
