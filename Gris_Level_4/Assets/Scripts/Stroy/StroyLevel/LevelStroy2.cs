using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：第二关剧情脚本--------
 * -----------脚本创建时间：2024-11-07-----------
 */
public class LevelStroy2 : MonoBehaviour
{

    //


    private void OnEnable()
    {


        GrisGameSington.Instance.nowStoryLevel = NowStoryLevel.level2;

        GrisGameSington.Instance.isFollowTearNum = 0;

        GrisGameSington.Instance.nowPlayerModel = NowPlayerModel.controller;

        GrisGameSington.Instance.playerTrans = GameObject.FindGameObjectWithTag("Player").transform;

        ResourcesSington.Instance.LoadAssetAync<AudioClip>("AudioClip/BG6", (clip) =>
        {
            AudioSington.Instance.PlayMusic(clip, 1);
        });
    }

    private void Update()
    {
        Debug.LogError(GrisGameSington.Instance.isFollowTearNum);
    }
}
