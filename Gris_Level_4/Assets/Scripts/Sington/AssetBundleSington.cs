using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：ab包加载--------
 * -----------脚本创建时间：2024-11-01-----------
 */
public class AssetBundleSington : Sington<AssetBundleSington>
{
    //存储已经加载过的ab包
    private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();

    //
    private string abPath = Application.streamingAssetsPath + "/";

   

    public void LoadAssetAsync<T>(string abName, string assetName, UnityAction<T> callBack) where T : Object
    {
        StartCoroutine(LoadAbAsset<T>(abName, assetName, callBack));
    }

    /// <summary>
    /// 异步加载AB包
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="abName"></param>
    /// <param name="assetName"></param>
    /// <param name="callBack"></param>
    /// <returns></returns>
    IEnumerator LoadAbAsset<T>(string abName,string assetName,UnityAction<T> callBack) where T : Object
    {
        if(!abDic.ContainsKey(abName))
        {
            AssetBundleCreateRequest abc = AssetBundle.LoadFromFileAsync(abPath + abName);

            yield return abc;

            AssetBundle bundle = abc.assetBundle;
            abDic.Add(abName, bundle);
            yield return bundle;

            AssetBundleRequest abr = bundle.LoadAssetAsync<T>(assetName);

            yield return abr;

            if (abr.asset != null)
            {               
                callBack?.Invoke(abr.asset as T);
            }
        }
        else
        {
            AssetBundleRequest abr = abDic[abName].LoadAssetAsync<T>(assetName);

            yield return abr;

            if (abr.asset != null)
            {
                callBack?.Invoke(abr.asset as T);
            }
        }
    }
}
