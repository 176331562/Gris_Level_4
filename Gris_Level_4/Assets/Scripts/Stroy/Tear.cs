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

    private void Update()
    {
        StartWalk();

        IsMoveToEnd();
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

                //if(isMoveTo)
                //{
                //    isMoveTo = false;
                //}
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


    private void IsMoveToEnd()
    {
        if(movePointArray != null && isMoveTo)
        {
            if (Vector3.Distance(this.transform.position, movePointArray[movePointArray.Length - tearNum - 1].transform.position) <= 0.1f)
            {
                StartMoveTo(movePointArray[movePointArray.Length - nowTearIndex].transform.position);

                if(Vector3.Distance(this.transform.position, movePointArray[movePointArray.Length - nowTearIndex].transform.position) <= 0.1f)
                {
                    isMoveTo = false;
                }
            }
        }
    }
}
