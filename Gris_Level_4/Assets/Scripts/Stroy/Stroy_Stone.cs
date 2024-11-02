using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：剧情_眼泪石像--------
 * -----------脚本创建时间：2024-11-01-----------
 */
public class Stroy_Stone : MonoBehaviour
{
    //眼泪行径数组
    private Transform[] tearMoveArray;

    //剧情眼泪数量
    private int stroy_TearNum = 5;

    //眼泪预制体
    private GameObject tearObj;

    //眼泪预制体数组
    private GameObject[] tearObjs;

    void Start()
    {
        //第一个就是点位父物体
        FindTearMovePoint(this.transform.GetChild(0));

        //获取眼泪
        tearObj = ResourcesSington.Instance.LoadAsset<GameObject>("Prefab/Tear");

        tearObjs = new GameObject[stroy_TearNum];

        //Debug.LogError(tearObj);
        
        StartCoroutine(CreateTearItem());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 查找所有移动点
    /// </summary>
    /// <param name="pointFather"></param>
    private void FindTearMovePoint(Transform pointFather)
    {
        tearMoveArray = new Transform[pointFather.childCount];

        for (int i = 0; i < pointFather.childCount; i++)
        {
            tearMoveArray[i] = pointFather.GetChild(i).transform;
        }

        //Debug.LogError(tearMoveArray.Length);
    }


    /// <summary>
    /// 生成眼泪并进行移动
    /// </summary>
    /// <returns></returns>
    IEnumerator CreateTearItem()
    {
        GameObject gos = null;

        Tear tear = null;

        for (int i = 0; i < stroy_TearNum; i++)
        {
            gos = GameObject.Instantiate(tearObj, tearMoveArray[0].position, Quaternion.identity);
            gos.name = tearObj.name;

            tear = gos.GetComponent<Tear>();

            tear.GetMoveArray(tearMoveArray, stroy_TearNum);

            for (int j = 0; j < tearMoveArray.Length-stroy_TearNum; j++)
            {
                tear.StartMoveTo(tearMoveArray[j].position);

                yield return new WaitForSeconds(2);                
            }
            yield return new WaitForSeconds(2);
        }       
        yield return null;
    }
}
