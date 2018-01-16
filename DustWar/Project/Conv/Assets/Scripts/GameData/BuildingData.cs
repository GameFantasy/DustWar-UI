using UnityEngine;
using System.Collections;

[System.Serializable]
public class BuildingData : GameData
{
    public float m_PosX;
    public float m_PosY;

    [System.Serializable]
    public enum TYPE
    {
        FARM,
        TOWN,
        TOWER
    }

    public TYPE m_Type;
    // 数据所属位置
}
