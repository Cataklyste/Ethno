using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundPlayerRandom : MonoBehaviour {

	[SerializeField]
	private List<string> listAudio;

	private List<string> listCopy;

	public float TimerInstantiate = 0.3f;
	private float _realTimer;

	[Range(0.0f, 100.0f)]
	public float ChanceDeJouer;

	void Start () {
		listCopy = new List<string>(listAudio);
		Shuffle();
	}
	
	void Update () {
		if (listCopy.Count == 0)
		{
			listCopy = new List<string>(listAudio);
			Shuffle();
		}
		else
		{
			if (_realTimer <= 0.0f)
			{
				float randTMP = Random.Range(0.0f, 100.0f);
				if (randTMP <= ChanceDeJouer)
				{
					if (SoundManager.Instance.CanIPlaySound(gameObject))
					{
						SoundManager.Instance.PlaySfx(gameObject, listCopy[0], 1.0f, true, 10.0f, false);
						listCopy.Remove(listCopy[0]);
					}
				}
				_realTimer = TimerInstantiate;
			}
			else
			{
				_realTimer -= Time.deltaTime;
			}
		}
	}

	void Shuffle()
	{
		for (int i = 0; i < listCopy.Count; i++)
		{
			string temp = listCopy[i];
			int randomIndex = Random.Range(i, listCopy.Count);
			listCopy[i] = listCopy[randomIndex];
			listCopy[randomIndex] = temp;
		}
	}
}
