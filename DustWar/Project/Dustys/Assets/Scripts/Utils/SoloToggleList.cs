using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SoloToggleList : MonoBehaviour
{
    public Toggle[] m_ToggleList;
    public delegate void ValChageCallBack(int Index);
    public ValChageCallBack m_ValChageCallBack;

    public int m_IndexSeleted;
    // 是否是单选
    public bool m_isUnique;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < m_ToggleList.Length; i++)
        {
            m_ToggleList[i].onValueChanged.AddListener(OnChageVal);
        }
    }

    public void Init(ValChageCallBack action)
    {
        m_ValChageCallBack = action;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetIndexSelected(int index)
    {
        for (int i = 0; i < m_ToggleList.Length; i++)
        {
            if (i == index)
            {
                m_ToggleList[i].isOn = true;
            }
            else
            {
                m_ToggleList[i].isOn = false;
            }
        }
    }

    private void OnChageVal(bool isOn)
    {
        for (int i = 0; i < m_ToggleList.Length; i++)
        {
            if (m_ToggleList[i].isOn)
            {
                m_IndexSeleted = i;
                m_ValChageCallBack(i);
            }
        }
    }
}
