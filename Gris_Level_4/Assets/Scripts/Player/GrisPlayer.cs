using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：人物移动脚本--------
 * -----------脚本创建时间：2024-10-08-----------
 */
public class GrisPlayer : MonoBehaviour
{

    #region 属性
    //剧情移动的目标点
    private Vector3 targetTrans;

    //是否需要剧情移动
    private bool isStroyMove = true;

    //是否需要翻面
    private bool filpX;

    //移动到点位后需要执行的委托
    private UnityAction moveToEvent;

    //玩家移动速度
    private float moveSpeed;

    //是否接地
    private bool isGround;

    //
    private bool isJump;
    #endregion

    #region 组件
    //玩家状态机
    private Animator playerAni;

    //渲染器
    private SpriteRenderer sr;

    //刚体
    private Rigidbody2D rig2D;

    #endregion



    private void OnEnable()
    {
        playerAni = this.GetComponent<Animator>();

        sr = this.GetComponent<SpriteRenderer>();

        rig2D = this.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
       
    }

  
    private void FixedUpdate()
    {
        StroyWalkTo(targetTrans, filpX);

        PlayerMove();
    }

    private void Update()
    {
        PlayerJump();
    }

    /// <summary>
    /// 开启剧情模式
    /// </summary>
    /// <param name="targetPoint">目标点</param>
    /// <param name="isFilpX">是否翻面</param>
    private void StroyWalkTo(Vector3 targetPoint,bool isFilpX)
    {
        if(GrisGameSington.Instance.nowPlayerModel == NowPlayerModel.stroy)
        {
            if (isStroyMove && targetPoint != Vector3.zero)
            {
                if (isFilpX != sr.flipX)
                {
                    sr.flipX = isFilpX;
                }

                if (Vector2.Distance(this.transform.position, targetPoint) >= 0.2f)
                {
                    playerAni.Play("Walk");

                    this.transform.position = Vector2.Lerp(this.transform.position, targetPoint, GrisGameSington.Instance.stroySpeed * 0.4f * Time.deltaTime);
                }
                else
                {
                    playerAni.Play("Idle");

                    rig2D.bodyType = RigidbodyType2D.Dynamic;

                    isStroyMove = false;

                    if (moveToEvent != null)
                    {
                        moveToEvent?.Invoke();

                        moveToEvent = null;
                    }
                }
            }
        }
        
    }

    /// <summary>
    /// 开启剧情模式
    /// </summary>
    /// <param name="targetPoint"></param>
    /// <param name="isFilpX"></param>
    public void StartStroy(Vector3 targetPoint, bool isFilpX)
    {
        targetTrans = targetPoint;

        filpX = isFilpX;

        isStroyMove = true;

        rig2D.bodyType = RigidbodyType2D.Kinematic;
    }


    /// <summary>
    /// 开启剧情模式
    /// </summary>
    /// <param name="targetPoint"></param>
    /// <param name="isFilpX"></param>
    /// <param name="willTodoAction"></param>
    public void StartStroy(Vector3 targetPoint, bool isFilpX,UnityAction willTodoAction)
    {
        targetTrans = targetPoint;

        filpX = isFilpX;

        isStroyMove = true;

        rig2D.bodyType = RigidbodyType2D.Kinematic;

        moveToEvent = willTodoAction;
    }

    private void PlayerMove()
    {
        //GrisGameSington.Instance.nowPlayerModel = NowPlayerModel.controller;

        if (GrisGameSington.Instance.nowPlayerModel == NowPlayerModel.controller)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");

            PlayerJump();

            if (horizontal != 0)
            {
                rig2D.velocity = new Vector2(horizontal * 200 * Time.deltaTime, rig2D.velocity.y);

                //如果在移动就得改变面朝向
                sr.flipX = horizontal < 0 ? true : false;

                //
                if (isGround && !isJump)
                {
                    playerAni.SetBool("Run", true);
                    playerAni.SetBool("isGround", true);
                }

            }
            else
            {
                if (!isJump)
                {
                    rig2D.velocity = Vector2.zero;
                }

                if (isGround)
                {
                    playerAni.SetBool("Run", false);
                    playerAni.SetBool("isGround", true);
                }
            }

            //判断当前是否进行地形碰撞
            if (rig2D.bodyType != RigidbodyType2D.Dynamic)
            {
                rig2D.bodyType = RigidbodyType2D.Dynamic;
            }
        }
        

        
    }

    //玩家跳跃
    private void PlayerJump()
    {
        if(GrisGameSington.Instance.nowPlayerModel == NowPlayerModel.controller)
        {
            //在地面上按下
            if (isGround && Input.GetKeyDown(KeyCode.Space))
            {
                rig2D.velocity = new Vector2(rig2D.velocity.x, rig2D.velocity.y + 8);

                isJump = true;
            }

            //如果已经按下了Jump键就判断rig的速度来进行状态转换
            if (isJump)
            {
                playerAni.SetFloat("JumpY", rig2D.velocity.y);
            }

            //如果跳跃了一定高度就判定已经离开地面
            if (rig2D.velocity.y > 0.5f && isJump)
            {
                isGround = false;

                playerAni.SetBool("isGround", isGround);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.ClosestPoint(this.transform.position).y < this.transform.position.y)
        {
            isGround = collision.collider.CompareTag("Ground") ? true : false;

            rig2D.velocity = new Vector2(rig2D.velocity.x, 0);

            playerAni.SetBool("isGround", isGround);

            isJump = false;
        }

        

        //Debug.LogError(isGround);
    }
}
