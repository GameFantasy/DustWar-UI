using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MessageBox : MonoBehaviour
{

    // 主要是通用的提示框，使用单例调用
    private Text textShow;
    private Button btnOk;
    public static UI_MessageBox _instance = null;
    public static UI_MessageBox GetInstace()
    {
        if (_instance = null)
        {
            _instance = new UI_MessageBox();
        }
        return _instance;
    }
    void Start()
    {
        textShow = this.transform.Find("Bg/Message").GetComponent<Text>();
        btnOk = this.transform.Find("Bg/Ok").GetComponent<Button>();
        btnOk.onClick.AddListener(OnClickOk);
    }
    public void SetMessage(string _Content)
    {
        textShow.text = _Content;

    }
    void OnClickOk()
    {
        this.gameObject.SetActive(false);
    }
}
