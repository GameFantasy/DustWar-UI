using UnityEngine;
using System.Collections;
using System;
using Assets.ResData;

public class Item : MonoBehaviour{
    public ItemData m_Data;
    public CommonResData m_ResData;

    public void Awake()
    {
        string s = this.gameObject.name;
        s = s.Split('(')[0];
        m_Data.m_Name = s;
    }
}
