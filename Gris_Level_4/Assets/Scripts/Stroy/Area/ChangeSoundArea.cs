using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：改变背景音乐--------
 * -----------脚本创建时间：2024-11-04-----------
 */
public class ChangeSoundArea : MonoBehaviour
{
    //是否已经进入了
    private bool isEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isEnter)
        {
            if (collision.CompareTag("Player"))
            {
                isEnter = true;

                ResourcesSington.Instance.LoadAssetAync<AudioClip>("AudioClip/BG2", (clip) =>
                {
                    Debug.LogError(clip.name);

                    AudioSington.Instance.ChangeSound(true, clip);
                });
            }
        }
        
    }
}
