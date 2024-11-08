using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：改变背景天空--------
 * -----------脚本创建时间：2024-11-07-----------
 */
public class ChangeSky : MonoBehaviour
{
    //
    public bool startChange;

    //
    private Color targetColor;

    //
    private SpriteRenderer sr;

    //
    public float speed;

    //
    private UnityAction action;


    private void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (startChange)
        {
            if(targetColor != Color.black)
            {
                sr.color = Color.Lerp(sr.color, targetColor, speed * Time.deltaTime);
            }

            if(sr.color == targetColor)
            {
                startChange = false;

                if(action != null)
                {
                    action?.Invoke();

                    action = null;
                }
            }
        }
    }

    public void StartChang(bool startChange,Color targetColor,float speed)
    {
        this.targetColor = targetColor;

        this.startChange = startChange;

        this.speed = speed;


    }

    public void StartChang(bool startChange, Color targetColor, float speed,UnityAction unityAction)
    {
        this.targetColor = targetColor;

        this.startChange = startChange;

        this.speed = speed;

        this.action = unityAction;
    }


}
