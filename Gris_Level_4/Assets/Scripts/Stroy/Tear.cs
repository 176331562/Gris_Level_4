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

    private void Update()
    {
        StartWalk();
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
            if (Vector3.Distance(this.transform.position, targetPos) >= 0.1f)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, targetPos, speed * Time.deltaTime);
            }
            else
            {
                startMove = false;

                targetPos = Vector3.zero;
            }
        }
    }
}
