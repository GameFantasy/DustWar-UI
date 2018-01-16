using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System;

public class CharacterCtrl : MonoBehaviour {
    public List<Item> m_BagList = new List<Item>();
    public List<Item> m_EquipList = new List<Item>();
	public Transform m_TranBagList;

	public Transform m_TranEquip;
    public CharacterRes m_CharacterRes = new CharacterRes();

    public PlayerCtrl m_Player;
    
	void Awake()
	{
	}

    void Start()
    {
	}

    public void InitData()
    {
        //Item[] its = transform.Find("BagList").GetComponentsInChildren<Item>();
        //foreach (Item i in its)
        //{
        //    m_BagList.Add(i);
        //}
        
    }


    void Update()
    {

    }

	public string GetType(ITEMTYPE type){
		string ret = "";
		switch (type) {
		case ITEMTYPE.WEAPON:
			ret = "Weapon";
			break;
		case ITEMTYPE.GUARD:
			ret = "Guard";
			break;
		case ITEMTYPE.ORNAMENT:
			ret = "Ornament";
			break;
		case ITEMTYPE.HORSE:
			ret = "Horse";
			break;
		}
		return ret;
	}

	public void Equip(Item pi, ITEMTYPE type, Vector3 local){
		string sType = GetType(type);
		// 已经装备了, 卸下装备在装备
		Item it = m_TranEquip.Find(sType).GetComponentInChildren<Item> ();
		if (it != null) {
			UnEquip(type, local);
		}
		// 没有装备
		foreach (Item i in m_BagList) {
			if(i == pi){
                i.transform.parent = m_TranEquip.Find(sType);
                m_EquipList.Add(i);
                m_BagList.Remove(i);
				break;
			}
        }
        m_Player.SetAttribute();
    }

	public void UnEquip(ITEMTYPE type, Vector3 local){
		string sType = GetType(type);
        Item i = m_TranEquip.Find(sType).GetComponentInChildren<Item>();
        RefreshIndex(i);
        m_BagList.Add(i);
        m_EquipList.Remove(i);
		i.transform.parent = m_TranBagList;
		// 从背包换装备
        if (PageCtrl.Ins.Find<CharacterPage>().m_BagHangPage.gameObject.activeSelf)
        {
            PageCtrl.Ins.Find<CharacterPage>().m_BagHangPage.OnActiveBefre();
        }
        m_Player.SetAttribute();
    }

    // 更新背包坐标，防止放重
    private void RefreshIndex(Item it0)
    {
        for (int i = 0; i < m_BagList.Count; i++ )
        {
            if (m_BagList[i].m_Data.m_Index == it0.m_Data.m_Index)
            {
                it0.m_Data.m_Index++;
                i = 0;
            }
        }
    }

    public Item GetWeapon() {
        string sType = GetType(ITEMTYPE.WEAPON);
        // 已经装备了, 卸下装备在装备
        Item it = m_TranEquip.Find(sType).GetComponentInChildren<Item>();
        return it;
    }

    public Item GetGuard() {
        string sType = GetType(ITEMTYPE.GUARD);
        // 已经装备了, 卸下装备在装备
        Item it = m_TranEquip.Find(sType).GetComponentInChildren<Item>();
        return it;
    }
}
