using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：BossView--------
 * -----------脚本创建时间：2024-11-07-----------
 */
public class BossView : MonoBehaviour
{
    //组件
    public SpriteRenderer Sr { get; set; }

    //玩家位置
    public Transform PlayerTrans { get; set; }

    //组件  
    public Animator Animator { get; set; }

    //
    public SingCircleController circleController { get; set; }

    //
    public ChangeSky ChangeSky { get; set; }

    private void Start()
    {
        Sr = this.GetComponent<SpriteRenderer>();

        PlayerTrans = GrisGameSington.Instance.playerTrans;

        Animator = this.GetComponent<Animator>();

        circleController = this.transform.Find("SingCicle").GetComponent<SingCircleController>();

        ChangeSky = GameObject.Find("Area/BlackSky").GetComponent<ChangeSky>();
    }

}
