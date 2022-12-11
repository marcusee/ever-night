using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
	public AudioClip click, melee , intro , fight;
	AudioSource source;

	void Start()
	{
		source = GetComponent<AudioSource>();
		source.Play(0);
//		audioData.Pause();
		source.loop = true;
		Debug.Log("started");
	}


	public void PlayMelee()
	{
		source.PlayOneShot(melee);
	}
	
	public void PlayClick()
	{
		source.PlayOneShot(click);
	}

	public void PlayFight()
	{
		source.clip = fight;
		source.Play(0);

	}

	public void PlayIntro()
	{
		source.clip = intro;
		source.Play(0);
	}
}