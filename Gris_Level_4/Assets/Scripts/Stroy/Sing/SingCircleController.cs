using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：唱歌圈圈Controller--------
 * -----------脚本创建时间：2024-10-08-----------
 */
public class SingCircleController : MonoBehaviour
{
    //
    private SingCircleModel circleModel;

    //
    private SingCircleView circleView;

    void Start()
    {
        circleModel = this.GetComponent<SingCircleModel>();

        circleView = this.GetComponent<SingCircleView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(circleModel.startChange)
        {
            if(circleView.OutSideScale.localScale.x < circleModel.outSideScale)
            {
                circleView.OutSideScale.localScale += new Vector3(circleModel.outSideSpeed, circleModel.outSideSpeed, 0) * Time.deltaTime;
            }
            else
            {
                circleView.CircleCollider.enabled = true;

                if (circleView.InsideScale.localScale.x < circleModel.insideScale)
                {
                    circleView.InsideScale.localScale += new Vector3(circleModel.insideSpeed, circleModel.insideSpeed, 0) * Time.deltaTime;                   
                }
                
            }
        }
        else
        {
            if (circleView.InsideScale.localScale.x > 0)
            {
                circleView.InsideScale.localScale -= new Vector3(circleModel.insideSpeed, circleModel.insideSpeed, 0) * Time.deltaTime;               
            }
            else
            {
                circleView.CircleCollider.enabled = false;
            }

            if(circleView.OutSideScale.localScale.x > 0)
            {
                if (circleView.OutSideScale.localScale.x > 0)
                {
                    circleView.OutSideScale.localScale -= new Vector3(circleModel.outSideSpeed*2, circleModel.outSideSpeed*2, 0) * Time.deltaTime;
                }
            }
        }
    }

    public void SingSang(bool startSing)
    {
        circleModel.startChange = startSing;
    }
}
