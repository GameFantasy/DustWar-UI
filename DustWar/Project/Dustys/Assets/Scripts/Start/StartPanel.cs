using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour {
    private Button btnFight;
    private Button btnSingle;
    private Button btnSetting;
    private Button btnExitGame;
	// Use this for initialization
	void Start () {
        transform.Find("Figth").GetComponent<Button>().onClick.AddListener(ClickFight);
        transform.Find("Single").GetComponent<Button>().onClick.AddListener(ClickSingle);
        transform.Find("Setting").GetComponent<Button>().onClick.AddListener(ClickSetting);
        transform.Find("Exit").GetComponent<Button>().onClick.AddListener(ClickExit);	
	}
    void ClickFight()
    { 
    }
    void ClickSingle()
    { 
    }
    void ClickSetting()
    { 
    }
    void ClickExit()
    {
        Application.Quit();
    }
}
