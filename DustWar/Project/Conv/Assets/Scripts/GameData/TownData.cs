using UnityEngine;
using System.Collections;

[System.Serializable]
public class TownData : BuildingData
{
    // 建成时间
    public int m_DoneTime;
    // 人口增长时间
    public int m_PeopleGrowTime;
    public bool m_isGrow = false;
    public int m_PreGrowTime;
    // 是否到达人口上限
    public bool m_IsMax;
    public bool m_IsLvMax;

    public int m_Population;
    public int m_PopulationUpLimit;
    // 村庄等级
    public int m_Level;
}
