using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    #region Struct

    [System.Serializable]
    public struct AudioPair
    {
        public AudioType type;
        public SoundSetting soundBank;
    }

    #endregion

    #region Enum

    public enum AudioType
    {
        Accelerating,
        Breaking,
        Turning,
        Crash,
        NewCustomer,
        DropOff,
        FuelUp,
        NoMoney,
        Won,
        Lost,
        UIButton

    }

    #endregion

    public static SoundManager instance { get; private set; }        // Getter

    [SerializeField] private AudioMixer mainMixer;

    [SerializeField] private AudioSource backgroundPlayer;
    [SerializeField] private AudioSource sfxPlayer;
    [SerializeField] private AudioSource carEnginePlayer;


    [SerializeField] private List<AudioPair> audioPairs;
    private Dictionary<AudioType, SoundSetting> _audioPairDict;

    [SerializeField] private float defaultvolume = 50f;

    #region Getters
    public AudioSource CarEnginePlayer ()
    { return this.carEnginePlayer; }

    #endregion


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        
    }

    private void OnEnable()
    {
        _audioPairDict = new Dictionary<AudioType, SoundSetting>();

        // Go through our list
        for (int i = 0; i < audioPairs.Count; i++)
        {

            if (!_audioPairDict.ContainsKey(audioPairs[i].type))
                _audioPairDict[audioPairs[i].type] = audioPairs[i].soundBank;
            else
                Debug.LogWarning($"{GetType()}: Unable to add audio pair of type '{audioPairs[i].type}' - We already have added that data to our dictionary");
        }
    }

    public void PlayShotSound(AudioType type)
    {
        if (_audioPairDict.TryGetValue(type, out SoundSetting setting))
            setting.PlayRandomSound();
        else
            Debug.LogWarning($"{GetType()}: Unable to Play audio type '{type}' - We do not have that type in our dictionary");
    }

    public void PlayShotSound(AudioClip clip, float volume = 1)
    {
        sfxPlayer.PlayOneShot(clip, volume);
    }

    public void PlayASound(AudioClip clip, float volume = 1)
    {
        sfxPlayer.clip = clip;
        sfxPlayer.volume = volume;
        sfxPlayer.Play();
    }

    public void StopASound()
    {
        sfxPlayer.Stop();
    }



}
