using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：眼泪预制体--------
 * -----------脚本创建时间：2024-11-04-----------
 */
public class TearItem : MonoBehaviour
{

    //缩放速度
    private float speed=3;

    //内圈Trans
    private Transform insideTrans;

    //目标大小
    public float targetScale;

    //组件
    private SpriteRenderer sr;

    //默认颜色
    private Color srColor;

    //渐隐的值
    public float fadeValue;

    //渐隐速度
    public float fadeSpeed;

    //生成的图像
    private GameObject reloadObj;


    private void Start()
    {
        insideTrans = this.transform.Find("Out_SideCircle").transform;

        sr = insideTrans.GetComponent<SpriteRenderer>();

        srColor = sr.color;

        ResourcesSington.Instance.LoadAssetAync<GameObject>("Prefab/"+ this.gameObject.name, (obj) => 
        {
            reloadObj = obj;
        });
    }


    private void Update()
    {
        if (insideTrans.localScale.x <= targetScale)
        {
            insideTrans.localScale += Vector3.one * 0.1f * speed * Time.deltaTime;            
        }
      

        if(sr.color.a > fadeValue)
        {
            sr.color -= new Color(0, 0, 0, fadeSpeed * Time.deltaTime * 0.24f);
        }
        else
        {
            insideTrans.localScale = Vector2.zero;

            sr.color = srColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Instantiate(reloadObj,this.transform.position,Quaternion.identity);

            Destroy(this.gameObject);
        }
    }

    
}
