using UnityEngine;
using System.Collections;

public enum ITEMTYPE
{
    DEFUALT,
    WEAPON,
    GUARD,
    ORNAMENT,
    HORSE,
};


[System.Serializable]
public class ItemData : GameData
{
    // 物品id, 只对应同种类型
    public int m_Id;
    // 在背包位置
    public int m_Index;
    public ITEMTYPE m_Type;
}
