using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：BossController--------
 * -----------脚本创建时间：2024-11-07-----------
 */
public class BossController : MonoBehaviour
{
    //
    private BossModel bossModel;

    //
    private BossView bossView;

    void Start()
    {
        bossModel = this.GetComponent<BossModel>();

        bossView = this.GetComponent<BossView>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.LogError(bossView.PlayerTrans.position);

        if(bossModel.hp > 0)
        {
            if (Vector3.Distance(this.transform.position, bossView.PlayerTrans.position) <= bossModel.attackDis)
            {
                bossView.Animator.SetBool("Sing", true);

                bossView.Animator.SetBool("Walk", false);

                bossView.circleController.SingSang(true);
            }
            else
            {
                //说明玩家在右边
                if (bossView.PlayerTrans.position.x > this.transform.position.x)
                {
                    if (bossView.Sr.flipX != false)
                    {
                        bossView.Sr.flipX = false;
                    }
                }
                else
                {
                    if (bossView.Sr.flipX != true)
                    {
                        bossView.Sr.flipX = true;
                    }

                }

                bossView.Animator.SetBool("Sing", false);
                bossView.circleController.SingSang(false);
                this.transform.position = Vector2.Lerp(this.transform.position, /*new Vector2(bossView.PlayerTrans.position.x,this.transform.position.y)*/bossView.PlayerTrans.position, bossModel.moveSpeed * Time.deltaTime);

                bossView.Animator.SetBool("Walk", true);
            }
        }
    }

    public void Damage(int damage)
    {
        if(bossModel.hp > 0)
        {
            bossModel.hp -= damage;
        }
    }
}
