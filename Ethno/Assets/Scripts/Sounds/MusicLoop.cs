using UnityEngine;
using System.Collections;

public class MusicLoop : MonoBehaviour {

    public string startSourceName;
    public string sourceName;

	// Use this for initialization
	void Start ()
	{
        SoundManager.Instance.PlaySfx(gameObject, startSourceName);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (SoundManager.Instance.CanIPlaySound(gameObject))
		{
            SoundManager.Instance.PlaySfx(gameObject, sourceName, 1.0f, false, 500.0f, true);
		}
	}
}
