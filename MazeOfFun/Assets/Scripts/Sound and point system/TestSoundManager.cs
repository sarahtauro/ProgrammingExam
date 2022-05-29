using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSoundManager : MonoBehaviour
{
    public static TestSoundManager Instance;
    [SerializeField] private AudioSource _musicSouce, _effectSource;

    private static float _volume;
    public static float Volume 
    { 
        get { return _volume; } 
        set 
        {
            if (value < 0)
                value = 0;
            else if (value > 1)
                value = 1;
            _volume = value;
            _AdjustVolume();
        } 
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlaySound(AudioClip clip)
    {
        _effectSource.PlayOneShot(clip);
    }

    private static void _AdjustVolume()
    {
        AudioListener.volume = Volume;
    }
}
