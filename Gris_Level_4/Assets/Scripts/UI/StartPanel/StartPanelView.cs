using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartPanelView : MonoBehaviour
{
   
    public Button StartBtn { get; set; }

    public Button SettingBtn { get; set; }

    public Button QuitBtn { get; set; }



    private void Awake()
    {
        StartBtn = this.transform.Find("Btns/StartButton").GetComponent<Button>();

        SettingBtn = this.transform.Find("Btns/SettingButton").GetComponent<Button>();

        QuitBtn = this.transform.Find("Btns/QuitButton").GetComponent<Button>();
    }


}
