using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------脚本创建者：sikaris----------------
 * -----------脚本作用：花--------
 * -----------脚本创建时间：2024-11-07-----------
 */
public class Follows : MonoBehaviour
{
    //是否已经开花
    private bool isOpen;

    //
    private Animator animator;

    //眼泪预制体
    private GameObject tearItem;

    //当前开花的下标
    private static int nowFollowIndex;

    //
    private Transform tearPoint;

    private void Start()
    {
        animator = this.GetComponent<Animator>();

        ResourcesSington.Instance.LoadAssetAync<GameObject>("Prefab/TearItem", (obj) => 
        {
            tearItem = obj;
        });

        tearPoint = this.transform.GetChild(0).transform;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("SingCircle"))
        {
            if (!isOpen)
            {
                animator.Play("Open");

                isOpen = true;

                Invoke("CreateTearItem", 2);

                ++nowFollowIndex;

            }
        }
    }

    private void CreateTearItem()
    {
        GameObject tearObj = GameObject.Instantiate(tearItem, tearPoint.transform.position, Quaternion.identity);

        tearObj.name = this.gameObject.name;
    }
}
