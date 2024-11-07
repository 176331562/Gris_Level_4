using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：唱歌圈圈Model--------
 * -----------脚本创建时间：2024-11-07-----------
 */
public class SingCircleModel : MonoBehaviour
{
    //外圈的尺寸
    public float outSideScale;

    //内圈的尺寸
    public float insideScale;

    //外圈的移动速度
    public float outSideSpeed;

    //内圈的移动速度
    public float insideSpeed;

    //内圈的旋转速度
    public float insideRotSpeed;

    //是否开始缩放
    [HideInInspector]
    public bool startChange;

    //内圈碰撞器半径
    public float insideColRad = 1.86f;

    //
    public float insideColSpeed;
}
