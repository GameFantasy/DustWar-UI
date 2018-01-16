using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class GameDataSet{
    // 背包列表
    public List<ItemData> m_BagList = new List<ItemData>();
    // 装备列表
    public List<ItemData> m_EquipList = new List<ItemData>();

    private static GameDataSet m_Ins;
    public static GameDataSet Ins
    {
        get
        {
            if (m_Ins == null)
            {
                m_Ins = new GameDataSet();
                return m_Ins;
            }
            return m_Ins;
        }
        set
        {
            m_Ins = value;
        }
    }
}
