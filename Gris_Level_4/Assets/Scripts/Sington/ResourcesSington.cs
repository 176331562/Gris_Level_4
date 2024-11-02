using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：Resources加载单例--------
 * -----------脚本创建时间：2024-11-01-----------
 */
public class ResourcesSington : Sington<ResourcesSington>
{

    //已经加载过的资源
    private Dictionary<string, object> resDic = new Dictionary<string, object>();


    public T LoadAsset<T>(string assetName) where T : Object
    {
        if(!resDic.ContainsKey(assetName))
        {
            T t = Resources.Load<T>(assetName);

            resDic.Add(assetName, t);

            return t;
        }
        else
        {
            return resDic[assetName] as T;
        }       
    }

    public T LoadAssets<T>(string assetName,UnityAction<T> callBack) where T : Object
    {
        if (!resDic.ContainsKey(assetName))
        {
            T t = Resources.Load<T>(assetName);

            resDic.Add(assetName, t);

            callBack?.Invoke(t);

            return t;
        }
        else
        {
            callBack?.Invoke(resDic[assetName] as T);

            return resDic[assetName] as T;
        }
    }

    /// <summary>
    /// 异步加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetName"></param>
    /// <param name="callBack"></param>
    public void LoadAssetAync<T>(string assetName, UnityAction<T> callBack) where T : Object
    {
        StartCoroutine(LoadAsset<T>(assetName, callBack));
    }

    /// <summary>
    /// 异步加载资源协程
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetName"></param>
    /// <param name="callBack"></param>
    /// <returns></returns>
    IEnumerator LoadAsset<T>(string assetName,UnityAction<T> callBack) where T : Object
    {
        if(!resDic.ContainsKey(assetName))
        {
            ResourceRequest rr = Resources.LoadAsync<T>(assetName);

            yield return rr;

            rr.completed += (ee) => 
            {
                //Debug.LogError(12);

                callBack(rr.asset as T);
            };    
        }
        else
        {
            callBack(resDic[assetName] as T);
        }

    }
}
