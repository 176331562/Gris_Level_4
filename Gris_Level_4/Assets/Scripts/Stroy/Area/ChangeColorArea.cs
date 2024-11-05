using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：改变区域颜色--------
 * -----------脚本创建时间：2024-11-05-----------
 */
public class ChangeColorArea : MonoBehaviour
{
    //首先先获取所有颜色的默认值
    private Dictionary<string, ColorChild> colorDic = new Dictionary<string, ColorChild>();

    //
    public float speed;

    //完成
    public bool isDown;

    private void Start()
    {
        StartCoroutine(GetAllColorValues());

        
    }

    //查找子物体的组件和透明度
    IEnumerator GetAllColorValues()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            ColorChild colorChild = new ColorChild();

            colorChild.childCount = this.transform.GetChild(i).childCount;

            Transform[] transforms = new Transform[colorChild.childCount];

            colorChild.childs = new Transform[this.transform.GetChild(i).GetComponentsInChildren<Transform>().Length];
            colorChild.srs = new SpriteRenderer[this.transform.GetChild(i).GetComponentsInChildren<SpriteRenderer>().Length];
            colorChild.sms = new SpriteMask[this.transform.GetChild(i).GetComponentsInChildren<SpriteMask>().Length];
            colorChild.srAlphaValue = new float[colorChild.srs.Length];
            colorChild.smAlphaCutOffValue = new float[colorChild.sms.Length];
            colorChild.scales = new Vector3[this.transform.GetChild(i).childCount];

            int srsIndex = 0;
            int smsIndex = 0;

            for (int j = 0; j < this.transform.GetChild(i).childCount; j++)
            {
                Transform transform = this.transform.GetChild(i).GetChild(j);

                transforms[j] = this.transform.GetChild(i).GetChild(j);

                colorChild.scales[j] = transforms[j].localScale;

                transforms[j].localScale = Vector3.zero;

                colorChild.childs[j] = transforms[j].GetComponent<Transform>();

                if (transform.GetComponent<SpriteRenderer>() != null)
                {
                    colorChild.hasSr = true;

                    colorChild.srs[srsIndex] = transform.GetComponent<SpriteRenderer>();

                    colorChild.srAlphaValue[srsIndex] = transform.GetComponent<SpriteRenderer>().color.a;

                    transform.GetComponent<SpriteRenderer>().color = new Color(transform.GetComponent<SpriteRenderer>().color.r, 
                        transform.GetComponent<SpriteRenderer>().color.g, transform.GetComponent<SpriteRenderer>().color.b,0);

                    ++srsIndex;
                }

                if (transform.GetComponent<SpriteMask>() != null)
                {
                    colorChild.hasSm = true;

                    colorChild.sms[smsIndex] = transform.GetComponent<SpriteMask>();                    

                    colorChild.smAlphaCutOffValue[smsIndex] = transform.GetComponent<SpriteMask>().alphaCutoff;

                    ++smsIndex;
                    transform.GetComponent<SpriteMask>().alphaCutoff = 1;
                }
            }

            if(!colorDic.ContainsKey(this.transform.GetChild(i).name))
            {
                colorDic.Add(this.transform.GetChild(i).name, colorChild);
            }
        }

        yield return null;
    }

    IEnumerator StartChangeColor()
    {
        colorDic["SortingGroupBG"].timer = 7.5f;
        colorDic["SortingGroupColor"].timer = 7.5f;
        colorDic["Others"].timer = 15;

        while (true)
        {
            if (colorDic["SortingGroupBG"].ChangeAlpha())
            {
                if (colorDic["SortingGroupColor"].ChangeAlpha())
                {             
                    if (colorDic["Others"].ChangeAlpha())
                    {
                        Debug.Log("跳出循环");

                        isDown = true;

                        yield break;
                    }
                }
            }
            yield return null;            
        }       
    }

    public void StartChange()
    {
        StartCoroutine(StartChangeColor());
    }
}

public class ColorChild
{
    //子物体数量
    public int childCount;

    //子物体
    public Transform[] childs;

    //
    public SpriteRenderer[] srs;

    //
    public SpriteMask[] sms;

    //
    public Vector3[] scales;

    //是否有sr
    public bool hasSr;

    //是否有sm
    public bool hasSm;

    //sr的透明度
    public float[] srAlphaValue;

    //sm的透明度
    public float[] smAlphaCutOffValue;

    private Vector3 targetPos = Vector3.zero;

    public float timer;

    public bool ChangeAlpha()
    {
        if(timer > 0)
        {
            //先放大
            for (int i = 0; i < scales.Length; i++)
            {
                if (childs[i].localScale.x < scales[i].x)
                {
                    targetPos.x = Mathf.Lerp(childs[i].localScale.x, scales[i].x, 1 * Time.deltaTime);
                }

                if (childs[i].localScale.y < scales[i].y)
                {
                    targetPos.y = Mathf.Lerp(childs[i].localScale.y, scales[i].y, 1 * Time.deltaTime);
                }

                if (childs[i].localScale.z < scales[i].z)
                {
                    targetPos.z = Mathf.Lerp(childs[i].localScale.z, scales[i].z, 1 * Time.deltaTime);
                }

                childs[i].transform.localScale = targetPos;
            }
            //改透明度
            for (int i = 0; i < srs.Length; i++)
            {

                if (srAlphaValue[i] - srs[i].color.a >= 0.05f)
                {
                    srs[i].color = new Color(srs[i].color.r, srs[i].color.g, srs[i].color.b, Mathf.Lerp(srs[i].color.a, srAlphaValue[i], 0.2f * Time.deltaTime));
                }
            }

            //改sm值
            for (int i = 0; i < sms.Length; i++)
            {
                if (sms[i].alphaCutoff > smAlphaCutOffValue[i])
                {
                    sms[i].alphaCutoff = Mathf.Lerp(sms[i].alphaCutoff, smAlphaCutOffValue[i], 3 * Time.deltaTime);
                }

            }

            timer -= Time.deltaTime;
        }
        else
        {
            return true;
        }
        return false;
    }
}
