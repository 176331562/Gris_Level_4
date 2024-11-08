using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartPanelController : MonoBehaviour
{
    private StartPanelView startPanelView;

    private void Start()
    {
        startPanelView = this.GetComponent<StartPanelView>();

        startPanelView.StartBtn.onClick.AddListener(() => 
        {
            SceneManager.LoadScene("Level1");
        });

        startPanelView.QuitBtn.onClick.AddListener(() => 
        {
            Application.Quit();
        });
    }
}
