using UnityEngine;
using System.Collections;
using Assets.GamePage;

public class CharacterPage : Page {// Use this for initialization

    public static string m_sItemGrid = "Icon";
    public static string m_sWeapon = "Weapon";
    public static string m_sGuard = "Guard";
    public static string m_sOrnament = "Ornament";
    public static string m_sHorse = "Horse";

    public BagHangPage m_BagHangPage;
    public MainAreLayerCtrl.CharaterData m_Data;

	public  override void OnActiveBefre (){
		MatchEquipItem ("Weapon");
		MatchEquipItem ("Guard");
		MatchEquipItem ("Ornament");
		MatchEquipItem ("Horse");
	}

	private void MatchEquipItem(string name){
		EquipItem ei = transform.Find ("Equip").Find (name).GetComponent<EquipItem>();
        ei.m_Item = ResCtrl.Ins.m_CharacterCtrl.m_TranEquip.Find(name).GetComponentInChildren<Item>();
        ei.Init();
	}

    PageHeler m_Bag = new PageHeler();
	void Start () {
        base.Start ();
        m_Data = _owner.m_Para as MainAreLayerCtrl.CharaterData;

        m_BagHangPage = transform.Find("BagRodMask").Find("BagRod").GetComponentInChildren<BagHangPage>();
        m_Bag.m_IsRegister = false;
        m_Bag.m_CanDrag = false;
        m_Bag.m_LocalPos = new Vector3(-25.4433f, -159.3776f, 0);
        m_BagHangPage._owner = m_Bag;

        if (m_Data.m_ActiveBag)
        {
            m_BagHangPage.gameObject.SetActive(true);
            transform.Find("BagRodMask").Find("BagRod").GetComponent<Animator>().SetTrigger("Pop");
            m_BagHangPage.OnActiveBefre();
        }
        else
        {
            m_BagHangPage.gameObject.SetActive(false);
        }
	}

    public void OnBagPage()
    {
        if (!m_Data.m_ActiveBag)
        {
            m_Data.m_ActiveBag = true;
            m_BagHangPage.gameObject.SetActive(true);
            transform.Find("BagRodMask").Find("BagRod").GetComponent<Animator>().SetTrigger("Pop");
            m_BagHangPage.OnActiveBefre();
        }
        else
        {
            m_Data.m_ActiveBag = false;
            transform.Find("BagRodMask").Find("BagRod").GetComponent<Animator>().SetTrigger("Push");
            StartCoroutine(WaitPlayOver());
        }
    }

    IEnumerator WaitPlayOver() 
    {
    //播放fumo_left_hand的动画所需要的时间 为9秒,过后启动第二个动画
        yield return new WaitForSeconds(1); 
        m_BagHangPage.gameObject.SetActive(false);
    }
}
