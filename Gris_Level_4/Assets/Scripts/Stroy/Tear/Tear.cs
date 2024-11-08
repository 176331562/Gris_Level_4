using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：剧情_眼泪移动--------
 * -----------脚本创建时间：2024-11-01-----------
 */
public class Tear : MonoBehaviour
{
    //是否已经移动到了
    private bool isMoveTo;

    public bool IsMoveTo
    {
        get { return isMoveTo; }
    }

    //目标点
    private Vector3 targetPos;

    //开启剧情移动
    private bool startMove;

    //眼泪移动的速度
    public float speed;

    //到达点位后就去自己的点位
    private Vector3 lastPos;

    //移动点位
    private Transform[] movePointArray;

    //眼泪的数量
    private int tearNum;

    //当前眼泪的下标
    private static int nowTearIndex;

    //已经到达目标点的眼泪数量
    private static int isTargetNum;

    //持续时间
    private float timer;

    //
    private float curTime;

    //是否销毁
    private bool isDestroy;

    //是否一直跟随
    private bool isAlwaysFollow;

    //
    private int nowFollowIndex;

    //获取当前所有眼泪跟随的总数
    private static int nowFollowNums;

    public bool IsDestroy
    {
        get { return isDestroy; }
        set { isDestroy = value; }
    }

    private void Update()
    {
        StartWalk();

        IsMoveToEnd();

        if(isAlwaysFollow)
        {
            //Debug.LogError("GrisGameSington.Instance.followTearPoints[nowFollowIndex].position" + GrisGameSington.Instance.followTearPoints[nowFollowIndex].position);
            //Debug.LogError("nowFollowIndex" + nowFollowIndex);
            //Debug.LogError("-------------");

            this.transform.position = Vector3.Lerp(this.transform.position, GrisGameSington.Instance.followTearPoints[nowFollowIndex].position, speed * Time.deltaTime);
        }
    }

    //开启眼泪移动
    public void StartMoveTo(Vector3 targetPoint)
    {
        targetPos = targetPoint;

        startMove = true;
    }

    /// <summary>
    /// 开始移动
    /// </summary>
    /// <param name="targetPoint"></param>
    private void StartWalk()
    {
        if(startMove && targetPos != Vector3.zero)
        {
            if (Vector3.Distance(this.transform.position, targetPos) <= 0.1f)
            {
                startMove = false;

                targetPos = Vector3.zero;             
            }
            else
            {
                this.transform.position = Vector3.Lerp(this.transform.position, targetPos, speed * Time.deltaTime);              
            }
        }
    }

    public void MoveToLastPoint(bool isTrue,Vector3 lastPoint)
    {
        isMoveTo = true;

        startMove = true;

        targetPos = lastPoint;
    }

    /// <summary>
    /// 得到移动的点位
    /// </summary>
    /// <param name="targetArray"></param>
    public void GetMoveArray(Transform[] targetArray,int tearNum)
    {
        this.movePointArray = targetArray;

        ++nowTearIndex;

        isMoveTo = true;

        this.tearNum = tearNum;
    }

    /// <summary>
    /// 到达最后的指定位置
    /// </summary>
    private void IsMoveToEnd()
    {
        if(movePointArray != null && isMoveTo)
        {
            if (Vector3.Distance(this.transform.position, movePointArray[movePointArray.Length - tearNum - 1].transform.position) <= 0.1f)
            {
                StartMoveTo(movePointArray[movePointArray.Length - nowTearIndex].transform.position);
            }

            if (Vector3.Distance(this.transform.position, movePointArray[movePointArray.Length - nowTearIndex].transform.position) <= 0.1f)
            {
                Vector3 targetRot = new Vector3(0, 0, movePointArray[movePointArray.Length - nowTearIndex].transform.rotation.eulerAngles.z);

                this.transform.rotation = Quaternion.Euler(targetRot);

                ++isTargetNum;

                //Debug.LogError("已经到达的数量" + isTargetNum);

                isMoveTo = false;

                GrisGameSington.Instance.isTargetTearNum = isTargetNum;
            }
        }
    }

    /// <summary>
    /// 眼泪按照指定位置进行移动
    /// </summary>
    /// <param name="dir"></param>
    public void MoveTo(Vector3 dir)
    {
        if(!isDestroy)
        {
            this.transform.Translate(dir * speed * Time.deltaTime);

            Destroy(this.gameObject, 4);            
        }
    }

    private void OnDestroy()
    {
        isDestroy = true;

        //nowFollowNums = 0;

        //GrisGameSington.Instance.isFollowTearNum = 0;

        Debug.Log("销毁了");
    }

    public void AlwaysFollow(int nowIndex)
    {
        isAlwaysFollow = true;

        nowFollowIndex = nowIndex;

        ++nowFollowNums;

        GrisGameSington.Instance.isFollowTearNum = nowFollowNums;
    }
}
