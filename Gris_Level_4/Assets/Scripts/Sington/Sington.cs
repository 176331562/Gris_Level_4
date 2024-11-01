using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：泛型单例--------
 * -----------脚本创建时间：2024-11-01-----------
 */
public abstract class Sington<T> : MonoBehaviour where T : Sington<T>,new()
{
    private static T instance;

    public static T Instance
    {
        get
        {            
            if(instance == null)
            {
                GameObject singtonObj = new GameObject(typeof(T).ToString());

                instance = singtonObj.AddComponent<T>();

                DontDestroyOnLoad(singtonObj);
            }

            return instance;
        }
    }
}
