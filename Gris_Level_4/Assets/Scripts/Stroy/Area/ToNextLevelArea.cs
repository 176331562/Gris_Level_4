using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：通往下一关--------
 * -----------脚本创建时间：2024-11-05-----------
 */
public class ToNextLevelArea : MonoBehaviour
{
    //是否已经触发了
    private bool isTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (!isTrigger/* && GrisGameSington.Instance.isFollowTearNum == 5*/)
            {
                isTrigger = true;

                GrisGameSington.Instance.nowPlayerModel = NowPlayerModel.stroy;

                GrisGameSington.Instance.playerTrans.GetComponent<GrisPlayer>().ToLevel2Stroy();
            }
        }
    }

}
