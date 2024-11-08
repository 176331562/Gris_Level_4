using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：转圈--------
 * -----------脚本创建时间：2024-11-08-----------
 */
public class RotCircle : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(this.transform.forward * 3 * Time.deltaTime);
    }
}
