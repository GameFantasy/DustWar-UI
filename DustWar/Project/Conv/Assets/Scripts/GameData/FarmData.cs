using UnityEngine;
using System.Collections;

[System.Serializable]
public class FarmData : BuildingData
{
    public enum FARMTYPE
    {
        LV1,
        LV2,
        LV3,
    }
    public enum CROP
    {
        SOYA, // 大豆
        WHEAT,// 小麦
    }
    public FARMTYPE m_FarmType;
    public CROP m_Crop;

    public int m_Population;
    public int m_PopulationUpLimit;

    // 各种类型确认时间, 每次更改耕作信息就重置
    public int m_DoneTime;
    // 产出增长时间
    public int m_ProduceTime;
}
