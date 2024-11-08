using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：Boss攻击--------
 * -----------脚本创建时间：2024-11-07-----------
 */
public class OutSideCircle : MonoBehaviour
{
    //
    public bool isBoss;

    //
    private GrisPlayer grisPlayer;

    //
    private float atkTime;

    //
    private BossController bossController;

    private void Start()
    {
        grisPlayer = GrisGameSington.Instance.playerTrans.GetComponent<GrisPlayer>();       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isBoss && collision.CompareTag("Player"))
        {
            atkTime += Time.deltaTime;

            if (atkTime > 1f)
            {
                if(grisPlayer.PlayerHp(true, 2))
                {
                    bossController = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>();

                    bossController.PlayerIsDead(true);
                }

                atkTime = 0;
            }          
        }
        else if(!isBoss && collision.CompareTag("Boss"))
        {
            atkTime += Time.deltaTime;

            if(bossController == null)
            {
                bossController = collision.GetComponent<BossController>();
            }

            if (atkTime > 1f)
            {
                bossController.Damage(2);

                atkTime = 0;
            }
        }
    }
}
