using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager 
{
    // AudioSource
    // AudioClip
    // AudioListener

    public void Play(Define.Sound soundType, string path, float pitch = 1.0f)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";
        if(soundType == Define.Sound.Bgm)
        {
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);
        }
        //bgm�� ��츸 loop
    }
}
