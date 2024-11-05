using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：特殊区域进入后改变摄像机size--------
 * -----------脚本创建时间：2024-11-05-----------
 */
public class ChangeCameraArea : MonoBehaviour
{
    //退出区域后要还原size
    private float beforeSize;

    //进入区域后要改变的size
    public float changeSize;

    //防止进入反复获取
    private CameraFollow cameraFollow;

    private void Start()
    {
        cameraFollow = Camera.main.GetComponent<CameraFollow>();

        beforeSize = Camera.main.orthographicSize;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            cameraFollow.ChangeSize(changeSize);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cameraFollow.ChangeSize(beforeSize);
        }
    }
}
