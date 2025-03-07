﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

    //移动到目标点后就执行
    private UnityAction moveToAction;

    //
    private bool moveToPlayer;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        //开始移动到目标点
        if (GrisGameSington.Instance.nowPlayerModel == NowPlayerModel.controller || moveToPlayer)
        {
            Vector3 targetTrans = new Vector3(GrisGameSington.Instance.playerTrans.position.x, GrisGameSington.Instance.playerTrans.position.y + 3.5f, -10);

            targetPoint = targetTrans;
        }
       

        if (targetPoint != Vector3.zero)
        {               
            this.transform.position = Vector3.Lerp(this.transform.position, targetPoint, moveSpeed * Time.deltaTime);

            if(Vector3.Distance(this.transform.position,targetPoint) <= 0.1f)
            {
                if(moveToAction != null)
                {
                    moveToAction?.Invoke();

                    moveToAction = null;
                }
            }
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
    /// 改变目标点
    /// </summary>
    /// <param name="targetPoint"></param>
    public void MoveTo(Vector3 targetPoint,UnityAction moveToAction)
    {
        this.targetPoint = targetPoint;

        startMove = true;

        this.moveToAction = moveToAction;
    }

    //
    public void MoveToPlayer(bool b)
    {
        this.moveToPlayer = b;
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
