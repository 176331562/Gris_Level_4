using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：Boss区域--------
 * -----------脚本创建时间：2024-11-07-----------
 */
public class BossArea : MonoBehaviour
{
    //
    private bool isEnter;

    public Color color;

    //
    private GameObject bossObj;

    private void Start()
    {
        ResourcesSington.Instance.LoadAssetAync<GameObject>("Prefab/BossGris", (obj) => 
        {
            bossObj = obj;
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (!isEnter && GrisGameSington.Instance.isFollowTearNum % 5 == 0)
            {
                isEnter = true;

                ResourcesSington.Instance.LoadAssetAync<AudioClip>("AudioClip/Boss", (clip) =>
                {
                    AudioSington.Instance.PlayMusic(clip, 1);
                });


                GameObject.Find("Area/BlackSky").GetComponent<ChangeSky>().StartChang(true, color, 3, () =>
                 {
                     LoadBoss();
                 });
            }

            //isEnter = true;

            //ResourcesSington.Instance.LoadAssetAync<AudioClip>("AudioClip/Boss", (clip) =>
            //{
            //    AudioSington.Instance.PlayMusic(clip, 1);
            //});


            //GameObject.Find("Area/BlackSky").GetComponent<ChangeSky>().StartChang(true, color, 3, () =>
            //{
            //    LoadBoss();
            //});
        }
    }

    private void LoadBoss()
    {
        GameObject boss = GameObject.Instantiate(bossObj, this.transform.position, Quaternion.identity);
    }
}
