using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：音频单例--------
 * -----------脚本创建时间：2024-11-01-----------
 */
public class AudioSington : Sington<AudioSington>
{
    //组件
    private AudioSource audioSource;

    //音量
    private float volmue;

    private void Awake()
    {
        if(audioSource == null)
        {
            audioSource = this.gameObject.AddComponent<AudioSource>();

            volmue = audioSource.volume;
            
        }
    }

    /// <summary>
    /// 播放音乐
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="volmue"></param>
    public void PlayMusic(AudioClip audioClip,float volmue)
    {
        audioSource.clip = audioClip;

        audioSource.volume = volmue;

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
