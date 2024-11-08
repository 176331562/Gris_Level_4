using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
        if(bossModel.playerDead)
        {
            return;
        }

        if(bossModel.hp != 0 && bossModel.hp > 0 && !bossModel.playerDead)
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

        if(bossModel.startMove && bossModel.targetPoint != Vector3.zero && bossModel.action != null)
        {
            if(Vector3.Distance(this.transform.position,bossModel.targetPoint) > 0.1f)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, bossModel.targetPoint, bossModel.moveSpeed * Time.deltaTime);
            }
            else
            {
                bossModel.startMove = false;

                bossModel.targetPoint = Vector3.zero;

                bossModel.action?.Invoke();
                bossModel.action = null;
            }
        }
    }

    public void Damage(int damage)
    {
        if(bossModel.hp > 0)
        {
            bossModel.hp -= damage;

            Debug.LogError(bossModel.hp);

            if (bossModel.hp <= 0)
            {
                if (!bossModel.isDead)
                {
                    bossModel.isDead = true;

                    BossDead();
                }
            }
        }           
    }

    private void BossDead()
    {
        AudioSington.Instance.StopPlay();

        GrisGameSington.Instance.nowPlayerModel = NowPlayerModel.stroy;

        bossView.Animator.SetBool("Sing", false);

        bossView.circleController.SingSang(false);

        bossView.Animator.SetBool("Walk", false);

        StartCoroutine(BossDeadator());
    }

    /// <summary>
    /// Boss被打败后
    /// </summary>
    /// <returns></returns>
    IEnumerator BossDeadator()
    {
        yield return new WaitForSeconds(1);

        bossView.PlayerTrans.GetComponent<GrisPlayer>().StroyEnd();

        bossView.Animator.Play("Cry");

        yield return new WaitForSeconds(2);

        bossView.Animator.Play("PlayerFly");



        MoveTo(true, this.transform.position + (this.transform.up * 5),()=> 
        {
            
        });

        Camera.main.GetComponent<CameraFollow>().ChangeSize(10);

        yield return new WaitForSeconds(2);

        bossView.ChangeSky.StartChang(true, new Color(0, 0, 0, 0), 1);
    }

    private void MoveTo(bool startMove,Vector3 targetPoint,UnityAction unityAction)
    {
        this.bossModel.startMove = startMove;

        this.bossModel.targetPoint = targetPoint;

        this.bossModel.action = unityAction;
    }

    private void MoveTo(bool startMove, Vector3 targetPoint)
    {
        this.bossModel.startMove = startMove;

        this.bossModel.targetPoint = targetPoint;
    }

    public void PlayerIsDead(bool b)
    {
        bossModel.playerDead = b;
    }
}
