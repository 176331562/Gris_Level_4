using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：游戏全局单例--------
 * -----------脚本创建时间：2024-11-01-----------
 */
public enum NowStoryLevel
{
    level1,
    level2
}

public enum NowPlayerModel
{
    stroy,
    controller
}

public class GrisGameSington : Sington<GrisGameSington>
{

    #region 属性
    //默认当前关卡为第一关
    [HideInInspector]
    public NowStoryLevel nowStoryLevel = NowStoryLevel.level1;

    //默认当前是剧情控制
    [HideInInspector]
    public NowPlayerModel nowPlayerModel = NowPlayerModel.stroy;

    //玩家移动速度
    [HideInInspector]
    public float moveSpeed = 3;

    //剧情移动速度
    [HideInInspector]
    public float stroySpeed = 1;


    //剧情移动点位
    [HideInInspector]
    public Dictionary<string, Vector3> stroyMovePointDic = new Dictionary<string, Vector3>();

    //剧情需要物体
    [HideInInspector]
    public Dictionary<string, GameObject> stroyObjDic = new Dictionary<string, GameObject>();
    #endregion

    #region 组件
    //获取玩家就能获取其他东西
    [HideInInspector]
    public Transform playerTrans;


    #endregion
     
    private void Awake()
    {
        FindPlayerObj();
    }


    /// <summary>
    /// 通过单例去获取玩家数据
    /// </summary>
    private void FindPlayerObj()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerTrans == null)
        {
            Debug.LogError("玩家数据获取失败");
        }
    }

    /// <summary>
    /// 每过一关就需要去找剧情移动点位
    /// </summary>
    public void StartFindMovePoint()
    {
        StartCoroutine(GetAllStroyMovePoint());
    }

    /// <summary>
    /// 每过一关就需要去找剧情物体
    /// </summary>
    public void StartFindStroyObj()
    {
        StartCoroutine(GetAllStroyObj());
    }

    /// <summary>
    /// 寻找点位的协程，防止主线程卡顿
    /// </summary>
    /// <returns></returns>
    IEnumerator GetAllStroyMovePoint()
    {
        stroyMovePointDic.Clear();

        GameObject[] gos = GameObject.FindGameObjectsWithTag("StroyMovePoint");

        for (int i = 0; i < gos.Length; i++)
        {
            if(!stroyMovePointDic.ContainsKey(gos[i].name))
            {
                stroyMovePointDic.Add(gos[i].name, gos[i].transform.position);
            }
        }
        yield return null;
    }

    /// <summary>
    /// 寻找当前场景里面的所有剧情物体
    /// </summary>
    /// <returns></returns>
    IEnumerator GetAllStroyObj()
    {
        stroyObjDic.Clear();

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Stroy");

        for (int i = 0; i < gos.Length; i++)
        {
            if (!stroyObjDic.ContainsKey(gos[i].name))
            {
                stroyObjDic.Add(gos[i].name, gos[i].gameObject);
            }
        }
        yield return null;
    }
}
