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
    }

   /// <summary>
   /// 开启剧情模式
   /// </summary>
   /// <param name="targetPoint">目标点</param>
   /// <param name="isFilpX">是否翻面</param>
    private void StroyWalkTo(Vector3 targetPoint,bool isFilpX)
    {
        if (isStroyMove && targetPoint != Vector3.zero)
        {
            if(isFilpX != sr.flipX)
            {
                sr.flipX = isFilpX;
            }
            
            if (Vector2.Distance(this.transform.position, targetPoint) >= 0.2f)
            {
                playerAni.Play("Walk");

                this.transform.position = Vector2.Lerp(this.transform.position, targetPoint, GrisGameSington.Instance.stroySpeed*0.4f * Time.deltaTime);
            }
            else
            {
                playerAni.Play("Idle");

                rig2D.bodyType = RigidbodyType2D.Dynamic;

                isStroyMove = false;

                if(moveToEvent != null)
                {
                    moveToEvent?.Invoke();

                    moveToEvent = null;
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
}
