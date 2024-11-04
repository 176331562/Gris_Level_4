using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：摄像机跟随移动--------
 * -----------脚本创建时间：2024-11-03-----------
 */
public class CameraFollow : MonoBehaviour
{
    //目标点
    private Vector3 targetPoint;

    //当前的视野大小
    private float nowSize;

    //移动速度
    private float moveSpeed = 3;

    //数值改变速度
    private float changeSpeed = 3;

    //是否可以开始移动
    private bool startMove;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        //开始移动到目标点
        if (GrisGameSington.Instance.nowPlayerModel == NowPlayerModel.controller)
        {
            Vector3 targetTrans = new Vector3(GrisGameSington.Instance.playerTrans.position.x, GrisGameSington.Instance.playerTrans.position.y + 2.2f, -10);

            targetPoint = targetTrans;
        }


        if (targetPoint != Vector3.zero)
        {
            //if(Vector3.Distance(this.transform.position,targetPoint) >= 0.1f)
            //{

            //}           
            this.transform.position = Vector3.Lerp(this.transform.position, targetPoint, moveSpeed * Time.deltaTime);
        }

        if (Camera.main.orthographicSize != nowSize && nowSize != 0)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, nowSize, changeSpeed * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
       
    }

    /// <summary>
    /// 改变目标点
    /// </summary>
    /// <param name="targetPoint"></param>
    public void MoveTo(Vector3 targetPoint)
    {
        this.targetPoint = targetPoint;

        startMove = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="targetSize"></param>
    public void ChangeSize(float targetSize)
    {
        this.nowSize = targetSize;
    }
}
