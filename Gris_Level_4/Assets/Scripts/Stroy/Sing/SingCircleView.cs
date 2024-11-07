using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：唱歌圈圈View--------
 * -----------脚本创建时间：2024-11-07-----------
 */
public class SingCircleView : MonoBehaviour
{
    //外圈缩放
    private Transform outsideScale;
    public Transform OutSideScale { get => outsideScale; set => outsideScale = value; }

    //内圈缩放
    private Transform insideScale;
    public Transform InsideScale { get => insideScale; set => insideScale = value; }

    //
    private CircleCollider2D circleCollider;
    public CircleCollider2D CircleCollider { get => circleCollider; set => circleCollider = value; }

    private void Start()
    {
        outsideScale = this.transform;

        insideScale = this.transform.Find("OutSideCircle").transform;

        circleCollider = insideScale.GetComponent<CircleCollider2D>();
    }
}
