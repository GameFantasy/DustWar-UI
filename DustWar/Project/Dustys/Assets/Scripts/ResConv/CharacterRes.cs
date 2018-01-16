using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using Assets.ResData;

public class CharacterRes
{
    
    public int m_Gold;
    public int m_Population;

    #region 资源存储相关
    public void Save()
    {
        SaveBagList();
        SaveEquip();
    }

    public void Load()
    {
        LoadBagList();
        LoadEquip();
    }

    private void SaveBagList()
    {
        for (int i = 0; i < ResCtrl.Ins.m_CharacterCtrl.m_BagList.Count; i++)
        {
            GameDataSet.Ins.m_BagList.Add(ResCtrl.Ins.m_CharacterCtrl.m_BagList[i].m_Data);
        }
    }
    private void LoadBagList()
    {
        for (int i = 0; i < ResCtrl.Ins.m_CharacterCtrl.m_BagList.Count; i++)
        {
            GameObject.Destroy(ResCtrl.Ins.m_CharacterCtrl.m_BagList[i].gameObject);
        }
        ResCtrl.Ins.m_CharacterCtrl.m_BagList.Clear();

        for (int i = 0; i < GameDataSet.Ins.m_BagList.Count; i++)
        {
            GameObject Preb0 = Resources.Load("Item" + "/" + GameDataSet.Ins.m_BagList[i].m_Name) as GameObject;
            GameObject Preb1 = GameObject.Instantiate(Preb0) as GameObject;
            Preb1.transform.parent = ResCtrl.Ins.m_CharacterCtrl.m_TranBagList;
            Preb1.GetComponent<Item>().m_Data = GameDataSet.Ins.m_BagList[i];
            Preb1.GetComponent<Item>().m_ResData = DeepCopy.Do<CommonResData>(PoolResDataSet.Ins.m_EquipDic[GameDataSet.Ins.m_BagList[i].m_Id]);
            ResCtrl.Ins.m_CharacterCtrl.m_BagList.Add(Preb1.GetComponent<Item>());
        }
    }

    private void SaveEquip()
    {
        List<Item> list = ResCtrl.Ins.m_CharacterCtrl.m_EquipList;
        List<ItemData> listData = GameDataSet.Ins.m_EquipList;
        for (int i = 0; i < list.Count; i++)
        {
            listData.Add(list[i].m_Data);
        }
    }
    private void LoadEquip()
    {
        List<Item> list = ResCtrl.Ins.m_CharacterCtrl.m_EquipList;
        List<ItemData> listData = GameDataSet.Ins.m_EquipList;
        for (int i = 0; i < list.Count; i++)
        {
            GameObject.Destroy(list[i].gameObject);
        }
        list.Clear();
        for (int i = 0; i < listData.Count; i++)
        {
            GameObject Preb0 = Resources.Load("Item" + "/" + listData[i].m_Name) as GameObject;
            GameObject Preb1 = GameObject.Instantiate(Preb0) as GameObject;
            string sType = ResCtrl.Ins.m_CharacterCtrl.GetType(Preb1.GetComponent<Item>().m_Data.m_Type);
            Preb1.transform.parent = ResCtrl.Ins.m_CharacterCtrl.m_TranEquip.Find(sType);
            list.Add(Preb1.GetComponent<Item>());
        }
    }
    #endregion
}
