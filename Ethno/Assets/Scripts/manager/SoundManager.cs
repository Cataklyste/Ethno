using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SoundManager : SingletonScript<SoundManager>
{

	private List<AudioClip> _audioClips;
	//private Dictionary<GameObject, AudioSource> _audioSources = new Dictionary<GameObject, AudioSource>();
	private Dictionary<GameObject, Coroutine> _corutineDictionary = new Dictionary<GameObject, Coroutine>();
	

	// Use this for initialization
	void Awake()
	{
		_audioClips = Resources.LoadAll<AudioClip>("Audio/").ToList();
	}

	public void PlaySfx(GameObject gameObj, string soundName, float volume = 1f, bool spacial = false, float maxDistance = 500f, bool loop = false)
	{
		AudioSource audioSource = gameObj.AddComponent<AudioSource>();
		audioSource.volume = volume;
		audioSource.spread = 360;
		audioSource.maxDistance = maxDistance;
		audioSource.spatialBlend = Convert.ToInt32(spacial);
		audioSource.rolloffMode = AudioRolloffMode.Linear;

		audioSource.loop = loop;

		AudioClip audioclip = _audioClips.Find(c =>
		{
			if (c.name == soundName)
			{
				return c;
			}
			return false;
		});

		if (audioclip == null)
			return;

		audioSource.Stop();
		audioSource.clip = audioclip;

		audioSource.Play();

		if (_corutineDictionary.ContainsKey(gameObj))
			StopSfx(gameObj);

		_corutineDictionary.Add(gameObj, StartCoroutine(PlaySound(gameObj, audioSource)));
	}

	public void StopSfx(GameObject gameObj)
	{
		if (_corutineDictionary.ContainsKey(gameObj))
		{
			StopCoroutine(_corutineDictionary[gameObj]);
			if (gameObj)
				Destroy(gameObj.GetComponent<AudioSource>());
			_corutineDictionary.Remove(gameObj);
		}
	}

	IEnumerator PlaySound(GameObject gameObj, AudioSource audioSource)
	{
		while (audioSource.loop)
		{
			yield return null;
		}

		yield return new WaitForSeconds(audioSource.clip.length);

		StopSfx(gameObj);
	}

	public bool CanIPlaySound(GameObject GO)
	{
		if (_corutineDictionary.ContainsKey(GO))
		{
			return false;
		}
		return true;
	}
}

