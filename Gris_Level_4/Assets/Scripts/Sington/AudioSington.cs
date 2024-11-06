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

    //开始改变音量
    private bool startChangeVolume;

    //目标音量
    private float targetVolume;

    //目标音频
    private AudioClip targetClip;

    private void Awake()
    {
        if(audioSource == null)
        {
            audioSource = this.gameObject.AddComponent<AudioSource>();

            volmue = audioSource.volume;
            
        }
    }

    private void Update()
    {
        if(startChangeVolume)
        {
            if(audioSource.volume > 0)
            {
                audioSource.volume -= Time.deltaTime;
            }
            else if(audioSource.volume <= 0)
            {
                audioSource.volume = 0;

                audioSource.clip = targetClip;

                audioSource.volume = volmue;

                audioSource.Play();

                startChangeVolume = false;
            }          
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

        audioSource.loop = true;

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    /// <summary>
    /// 播放音乐
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="volmue"></param>
    public void PlayMusic(AudioClip audioClip, float volmue,bool isLoop)
    {
        audioSource.clip = audioClip;

        audioSource.volume = volmue;

        audioSource.loop = isLoop;

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void ChangeSound(bool isStartChange,AudioClip targetClip)
    {
        this.startChangeVolume = isStartChange;

        this.targetClip = targetClip;


    }
}
