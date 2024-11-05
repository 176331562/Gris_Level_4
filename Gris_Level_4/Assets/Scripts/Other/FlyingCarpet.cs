using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：飞毯--------
 * -----------脚本创建时间：2024-11-05-----------
 */
public class FlyingCarpet : MonoBehaviour
{
    //目标位置
    private Vector3 targetPoint;

    //是否面朝向右边
    private bool isRight;

    //用来判断当前是不是要翻面
    private float x;

    //组件
    private SpriteRenderer sr;

    //移动速度
    public float speed;

    //移动点位数组
    private Transform[] movePoints;

    //当前移动点位数组的下标
    private int moveIndex;

    private void Start()
    {
        x = this.transform.position.x;

        sr = this.GetComponent<SpriteRenderer>();

        Transform pointsTrans = GameObject.FindGameObjectWithTag("PointsFather").transform.Find("Flying Carpet_MovePoint").transform;

        movePoints = new Transform[pointsTrans.childCount];

        for (int i = 0; i < pointsTrans.childCount; i++)
        {
            movePoints[i] = pointsTrans.GetChild(i).transform;
        }
    }

    private void Update()
    {
        Fly();
    }

    /// <summary>
    /// 改变目标点位
    /// </summary>
    /// <param name="targetPoint"></param>
    public void MoveTo(Vector3 targetPoint)
    {
        this.targetPoint = targetPoint;
    }

    /// <summary>
    /// 飞毯移动
    /// </summary>
    private void Fly()
    {
        if (targetPoint != Vector3.zero)
        {
            if (Vector3.Distance(this.transform.position, targetPoint) >= 0.1f)
            {
                if (targetPoint.x < x)
                {
                    isRight = false;

                    sr.flipX = isRight;
                }
                else
                {
                    isRight = true;

                    sr.flipX = isRight;
                }
            }
            else
            {
                if (moveIndex < movePoints.Length)
                {
                    MoveTo(movePoints[moveIndex].transform.position);

                    moveIndex++;
                }
                else
                {
                    moveIndex = 0;

                    MoveTo(movePoints[moveIndex].transform.position);
                }

            }

            this.transform.position = Vector3.Lerp(this.transform.position, targetPoint, speed * Time.deltaTime);
        }
        else
        {
            MoveTo(movePoints[moveIndex].transform.position);
        }
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
