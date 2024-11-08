using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：人物移动脚本--------
 * -----------脚本创建时间：2024-10-08-----------
 */
public class GrisPlayer : MonoBehaviour
{


    #region 属性
    //故事模式人物移动情况
    public enum StroyMoveType
    {
        Walk,
        None
    }


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

    //是否正在跳跃状态
    private bool isJump;

    //故事模式时是Run还是无状态移动
    private bool startRun;

    //眼泪跟随人物的点位
    [HideInInspector]
    public Transform[] followPoints;

    //默认人物平移移动
    private StroyMoveType moveType = StroyMoveType.None;

    //行走音频
    private AudioClip runClip, jumpClip, fallClip,singClip;

    private float runTime,jumpTime;

    public int hp;
    #endregion

    #region 组件
    //玩家状态机
    private Animator playerAni;

    //渲染器
    private SpriteRenderer sr;

    //刚体
    private Rigidbody2D rig2D;

    //
    private AudioSource audioSource;

    private SingCircleController circleController;
    #endregion



    private void OnEnable()
    {
        playerAni = this.GetComponent<Animator>();

        sr = this.GetComponent<SpriteRenderer>();

        rig2D = this.GetComponent<Rigidbody2D>();

        audioSource = this.GetComponent<AudioSource>();
    }

    void Start()
    {
        followPoints = new Transform[this.transform.Find("FollowPoints").childCount];

        for (int i = 0; i < this.transform.Find("FollowPoints").childCount; i++)
        {
            followPoints[i] = this.transform.Find("FollowPoints").GetChild(i).transform;
        }

        GrisGameSington.Instance.followTearPoints = followPoints;

        runClip = ResourcesSington.Instance.LoadAsset<AudioClip>("AudioClip/Move");

        jumpClip = ResourcesSington.Instance.LoadAsset<AudioClip>("AudioClip/Jump");

        fallClip = ResourcesSington.Instance.LoadAsset<AudioClip>("AudioClip/Land");

        singClip = ResourcesSington.Instance.LoadAsset<AudioClip>("AudioClip/Sing");

        //GrisGameSington.Instance.nowPlayerModel = NowPlayerModel.controller;

        circleController = this.transform.Find("SingCicle").GetComponent<SingCircleController>();
    }

  
    private void FixedUpdate()
    {
        StroyWalkTo(targetTrans, moveType, filpX);

        if(hp > 0)
        {
            PlayerMove();
        }
    }

    private void Update()
    {
        if(hp > 0)
        {
            PlayerJump();
        }
        
    }


    #region 人物方法
    //人物移动
    private void PlayerMove()
    {
        

        if (GrisGameSington.Instance.nowPlayerModel == NowPlayerModel.controller)
        {
            if(Input.GetKey(KeyCode.K) && isGround)
            {
                playerAni.SetBool("Sing", true);

                circleController.SingSang(true);

                if (audioSource.clip == null)
                {
                    audioSource.clip = singClip;
                }
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }

                return;
            }
            else
            {
                playerAni.SetBool("Sing", false);

                circleController.SingSang(false);

                audioSource.Stop();

                float horizontal = Input.GetAxisRaw("Horizontal");

                if (horizontal == 0 && !isJump)
                {
                    rig2D.drag = 5;
                }
                else
                {
                    rig2D.drag = 1;
                }

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



                        if (runTime < 0.5f)
                        {


                            runTime += Time.deltaTime;
                        }
                        else
                        {
                            AudioSource.PlayClipAtPoint(runClip, this.transform.position);

                            runTime = 0;
                        }
                    }
                }
                else
                {
                    if (!isJump && rig2D.velocity.y >= 0)
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



    }

    //玩家跳跃
    private void PlayerJump()
    {
        if (GrisGameSington.Instance.nowPlayerModel == NowPlayerModel.controller)
        {
            //
            if(!isGround && rig2D.velocity.y < 0)
            {
                playerAni.SetFloat("JumpY", rig2D.velocity.y);
            }


            //在地面上按下
            if (isGround && Input.GetKeyDown(KeyCode.Space))
            {
                rig2D.velocity = new Vector2(rig2D.velocity.x, rig2D.velocity.y +12);

                isJump = true;
            }
            else if (!isGround && !Input.GetKeyDown(KeyCode.Space))
            {
                if (rig2D.velocity.y < 0)
                {
                    playerAni.SetFloat("JumpY", rig2D.velocity.y);
                }
            }

            //如果已经按下了Jump键就判断rig的速度来进行状态转换
            if (isJump)
            {
                playerAni.SetFloat("JumpY", rig2D.velocity.y);

                if (jumpTime < 3f)
                {
             

                    jumpTime += Time.deltaTime;
                }
                else
                {
                    AudioSource.PlayClipAtPoint(jumpClip, this.transform.position);

                    jumpTime = 0;
                }

                //AudioSource.PlayClipAtPoint(jumpClip, this.transform.position);
            }

            //如果跳跃了一定高度就判定已经离开地面
            if (rig2D.velocity.y > 0.5f && isJump)
            {
                isGround = false;

                playerAni.SetBool("isGround", isGround);
            }
        }
    }

    public bool PlayerHp(bool isDamage,int damage)
    {
        if(isDamage &&hp > 0)
        {
            hp -= damage;

            Debug.LogError("hp");

            if(hp <= 0)
            {
                PlayerDead();

                return true;
            }
        }
        return false;
    }
    #endregion



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.ClosestPoint(this.transform.position).y < this.transform.position.y)
        {
            isGround = collision.collider.CompareTag("Ground") ? true : false;

            rig2D.velocity = new Vector2(rig2D.velocity.x, 0);

            playerAni.SetBool("isGround", isGround);

            isJump = false;

            if(isGround &&GrisGameSington.Instance.nowPlayerModel == NowPlayerModel.controller)
            {
                playerAni.SetFloat("JumpY", -8);
            }
        }
    }


    #region 剧情模式
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
    public void StartStroy(Vector3 targetPoint, bool isFilpX,StroyMoveType stroyMoveType ,UnityAction willTodoAction)
    {
        targetTrans = targetPoint;

        this.moveType = stroyMoveType;

        filpX = isFilpX;

        isStroyMove = true;

        rig2D.bodyType = RigidbodyType2D.Kinematic;

        moveToEvent = willTodoAction;
    }

    /// <summary>
    /// 开启剧情模式
    /// </summary>
    /// <param name="targetPoint"></param>
    /// <param name="isFilpX"></param>
    /// <param name="willTodoAction"></param>
    public void StartStroy(Vector3 targetPoint, bool isFilpX, StroyMoveType stroyMoveType)
    {
        targetTrans = targetPoint;

        this.moveType = stroyMoveType;

        filpX = isFilpX;

        isStroyMove = true;

        rig2D.bodyType = RigidbodyType2D.Kinematic;
    }

    /// <summary>
    /// 开启剧情模式
    /// </summary>
    /// <param name="targetPoint">目标点</param>
    /// <param name="isFilpX">是否翻面</param>
    private void StroyWalkTo(Vector3 targetPoint,StroyMoveType stroyMoveType,bool isFilpX)
    {
        if (GrisGameSington.Instance.nowPlayerModel == NowPlayerModel.stroy)
        {
            if (isStroyMove && targetPoint != Vector3.zero)
            {
                if (isFilpX != sr.flipX)
                {
                    sr.flipX = isFilpX;
                }

                if (Vector2.Distance(this.transform.position, targetPoint) >= 0.2f)
                {
                    if(stroyMoveType == StroyMoveType.Walk)
                    {
                        playerAni.Play("Walk");
                    }                   

                    this.transform.position = Vector2.Lerp(this.transform.position, targetPoint, GrisGameSington.Instance.stroySpeed * 0.4f * Time.deltaTime);
                }
                else
                {
                    if (stroyMoveType == StroyMoveType.Walk)
                    {
                        playerAni.Play("Idle");
                    }
                   
                    isStroyMove = false;

                    if (moveToEvent != null)
                    {
                        moveToEvent?.Invoke();

                        isStroyMove = false;

                        targetPoint = Vector3.zero;

                        moveToEvent = null;
                    }
                }
            }
        }

    }


    //完成第一关剧情模式收尾并进入第二场景
    public void ToLevel2Stroy()
    {
        StartCoroutine(ToLevel2StroyMethod());
    }

    IEnumerator ToLevel2StroyMethod()
    {
        ResourcesSington.Instance.LoadAssetAync<AudioClip>("AudioClip/BG4", (clip) =>
        {
            AudioSington.Instance.PlayMusic(clip, 1);
        }); 

        AsyncOperation rr = SceneManager.LoadSceneAsync("Level2");

        rr.allowSceneActivation = false;

        ChangeColorArea changeColorArea = GameObject.Find("Area/RenderColors").GetComponent<ChangeColorArea>();

        playerAni.SetBool("Run", false);

        yield return new WaitForSeconds(1);

        playerAni.Play("Cry");

        yield return new WaitForSeconds(1);

        StartStroy(this.transform.position + (this.transform.up * 6), false, StroyMoveType.None);

        CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
        cameraFollow.ChangeSize(8.17f);
        cameraFollow.MoveTo(this.transform.position + new Vector3(10, 8f, -10), () =>
        {
            
           
            playerAni.Play("PlayerFly");
            
        });

        yield return new WaitForSeconds(4);

        changeColorArea.StartChange();

        yield return new WaitForSeconds(34);

        if(changeColorArea.isDown)
        {
            Debug.LogError("完成");

            AudioSington.Instance.StopPlay();

            Vector3 targetPoint = new Vector3(GrisGameSington.Instance.playerTrans.position.x, GrisGameSington.Instance.playerTrans.position.y + 3.5f, -10);
            cameraFollow.MoveToPlayer(true);

            cameraFollow.ChangeSize(5);

            StartStroy(this.transform.position + (-this.transform.up * 6), false, StroyMoveType.None);

            yield return new WaitForSeconds(6);

            playerAni.Play("Cry Back");

            yield return new WaitForSeconds(2);

            AudioSington.Instance.StopPlay();

            rr.allowSceneActivation = true;
        }
    }

    /// <summary>
    /// 结局
    /// </summary>
    public void StroyEnd()
    {
        isGround = true;

        circleController.SingSang(true);

        playerAni.SetBool("Run", false);
        playerAni.SetBool("Walk", false);
        playerAni.SetBool("Sing", true);

    }

    public void PlayerDead()
    {
        hp = 0;

        AudioSington.Instance.StopPlay();
        GrisGameSington.Instance.nowPlayerModel = NowPlayerModel.stroy;

        circleController.SingSang(false);

        playerAni.SetBool("Run", false);
        playerAni.SetBool("Walk", false);
        playerAni.SetBool("Sing", false);

        StartCoroutine(PlayerDeadator());
    }

    IEnumerator PlayerDeadator()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync("StartScene");

        yield return ao;

        ao.allowSceneActivation = false;

        playerAni.Play("Cry");

        yield return new WaitForSeconds(2);

        StartStroy(this.transform.position + (this.transform.up * 5), sr.flipX, StroyMoveType.None, () => { });


        playerAni.Play("PlayerFly");

        yield return new WaitForSeconds(5);

        ao.allowSceneActivation = true;
    }
    #endregion
}

