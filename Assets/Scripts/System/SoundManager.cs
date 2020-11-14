using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField]
    private List<AudioClip> lsSounds = new List<AudioClip>();
    
    private void Awake()
    {
        instance = this;
    }
    
    public void PlaySound(SoundIndex soundIndex, bool isLoop = false)
    {
        EazySoundManager.PlaySound(lsSounds[(int)soundIndex], isLoop);
    }

    public void PauseSound(SoundIndex soundIndex)
    {
        Audio audio = EazySoundManager.GetSoundAudio(lsSounds[(int)soundIndex]);
        
        if (audio != null)
        {
            Debug.LogError(audio.Clip.name);
            audio.Stop();
        }
        
    }
}

public enum SoundIndex
{
    Countdown = 0,
    Gernade = 1,
    Enemy_kill,
    Boss_kill,
    Boss_walk,
    Player_walk,
    Click,
    Game_over,
  
}