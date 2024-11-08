using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：眼泪生成的图像--------
 * -----------脚本创建时间：2024-11-04-----------
 */
public class TearAsset : MonoBehaviour
{
    //隐藏时间
    public float fadeTime;

    //
    private SpriteRenderer sr;

    //图像隐藏完后生成眼泪
    private GameObject tearObj;

    //获取眼泪脚本
    private Tear tear;

    //
    private static int followIndex;

    //人物身上跟随的点位
    private Transform followTrans;

    //
    private GrisPlayer grisPlayer;

    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();

        ResourcesSington.Instance.LoadAssetAync<GameObject>("Prefab/Tear", (obj) => 
        {
            tearObj = obj;            
        });

        grisPlayer = GrisGameSington.Instance.playerTrans.GetComponent<GrisPlayer>();

       
    }

    private void Update()
    {
        if(fadeTime > 0)
        {
            if(sr.color.a > 0)
            {
                sr.color -= new Color(0, 0, 0, fadeTime * Time.deltaTime * 0.1f);

                fadeTime -= Time.deltaTime;

                if(sr.color.a <= 0)
                {
                    Destroy(this.gameObject);

                    CreateTearObj();
                }
            }
        }
    }

    private void CreateTearObj()
    {
        GameObject obj = Instantiate(tearObj, this.transform.position, Quaternion.identity);

        tear = obj.GetComponent<Tear>();

        tear.AlwaysFollow(followIndex);

        ++followIndex;
    }

    private void OnDestroy()
    {
        if(followIndex == 5)
        {
            followIndex = 0;

            //GrisGameSington.Instance.isFollowTearNum = 0;
        }
    }


}
