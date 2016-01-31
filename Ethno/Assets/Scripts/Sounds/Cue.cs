using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CueType { GRASS, SNOW, SAND, DIRT }

public class Cue : MonoBehaviour {

	public GameObject kamikaze;

	public float volume = 1f;

	public float TimerInstantiate = 0.3f;
	private float _realTimer;

	public CueType Type;

	public bool isActive;
	public bool isMoving;

	[SerializeField]
	private List<string> listAudio;

	private List<string> listCopy;

	void Start () 
	{
		_realTimer = TimerInstantiate;
		listCopy = new List<string>(listAudio);
		Shuffle();
	}
	
	void Update () 
	{
		if (!isActive || !isMoving) return;

		if (listCopy.Count == 0)
		{
			listCopy = new List<string>(listAudio);
			Shuffle();
		}
		else
		{
			if (_realTimer <= 0.0f)
			{
				GameObject tmp = Instantiate(kamikaze) as GameObject;
				SoundManager.Instance.PlaySfx(tmp, listCopy[0], volume);
				listCopy.Remove(listCopy[0]);
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
