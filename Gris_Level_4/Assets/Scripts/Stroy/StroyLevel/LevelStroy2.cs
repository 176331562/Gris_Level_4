using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：第二关剧情脚本--------
 * -----------脚本创建时间：2024-11-07-----------
 */
public class LevelStroy2 : MonoBehaviour
{

    private void OnEnable()
    {
        GrisGameSington.Instance.nowStoryLevel = NowStoryLevel.level2;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
