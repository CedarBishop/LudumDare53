using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random; // double check that this is the one required rather than System.Random

[CreateAssetMenu(fileName ="SoundSetting",menuName = "Settings/Sound")]
public class SoundSetting : ScriptableObject
{

	#region Variables

	[Header("Settings")]
	[SerializeField] private AudioClip[] _sounds;
	[Space()]
	[Range(0,1)][SerializeField] private float _defaultVolume = 1;

	#endregion

	#region Play Sound

	public void PlayRandomSound()
	{
		// Get a random Clip and play it
		int clipIndex = Random.Range(0, _sounds.Length);
        PlayShotSound(clipIndex);
	}

	public void PlayShotSound(int index)
	{
		index = Mathf.Clamp(index, 0, _sounds.Length - 1);
		SoundManager.instance.PlayShotSound(_sounds[index], _defaultVolume);
	}

    public void PlayASound()
	{
		//Debug.Log(this);
		SoundManager.instance.PlayASound(_sounds[0], _defaultVolume);
    }

    public void StopASound()
    {
        SoundManager.instance.StopASound();
    }


    #endregion

}
