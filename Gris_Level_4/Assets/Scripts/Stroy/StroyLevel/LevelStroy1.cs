using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：剧情一脚本--------
 * -----------脚本创建时间：2024-11-01-----------
 */
public class LevelStroy1 : MonoBehaviour
{
    //玩家
    private Transform playerTrans;

    private void Start()
    {

        //查找剧情物体
        GrisGameSington.Instance.StartFindStroyObj();

        //先加载背景音乐
        ResourcesSington.Instance.LoadAssetAync<AudioClip>("AudioClip/BG1", (clip) => 
        {
            AudioSington.Instance.PlayMusic(clip, 1);
        });

        
        playerTrans = GrisGameSington.Instance.playerTrans;

        //开启剧情移动
        GrisGameSington.Instance.StartFindMovePoint();

        //获取需要的剧情点位，并进行移动
        playerTrans.GetComponent<GrisPlayer>().StartStroy(GrisGameSington.Instance.stroyMovePointDic["MovePoint_1"], true,GrisPlayer.StroyMoveType.Walk,()=> 
        {
            //如果没有剧情石像
            if (GrisGameSington.Instance.stroyObjDic["Gril1"].GetComponent<Stroy_Stone>() == null)
            {
                GrisGameSington.Instance.stroyObjDic["Gril1"].AddComponent<Stroy_Stone>();
            }
        });       
    }

    
}
