using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.GamePage;

public class MainAreLayerCtrl : MonoBehaviour {

	public static MainAreLayerCtrl m_Instance;

    public PlayerCtrl m_PlayerCtrl;

    public GameObject m_ArriveInfoNode;
    //public WaypointCircuitSearch m_WaypointCircuitSearch;

    public RectTransform m_PointMap;
    public GameObject m_UIBuildingInfo;

    public RectTransform m_PageRoot;
    public RectTransform m_PagePool;
    public RectTransform m_TopRoot;

    public Text m_GoldText;
    public Text m_PopulationText;
	/// <summary>
	/// 是否允许显示信息提示
	/// </summary>
	public bool allowShowTips = true;
	/// <summary>
	/// 信息提示
	/// </summary>
	public GameObject tips;

    PageHeler m_Character = new PageHeler();
    public class CharaterData
    {
        public bool m_ActiveBag;
    }
    void Awake()
    {
        m_Instance = this;
    }
	// Use this for initialization
	void Start () {
		tips.SetActive(false);
        PageCtrl.Ins.Init("Page", m_PageRoot, m_PagePool);

        PageCtrl.Ins.Register<CharacterPage>();
        CharaterData data = new CharaterData();
        data.m_ActiveBag = false;
        m_Character.m_Para = data as object;
        
        m_Character.m_LocalPos = new Vector3(-205.56f, -32.37f, 0);
	}
	
	// Update is called once per frame
	void Update () {
        ShowResources();
	}

    public void SaveRes()
    {
        ResConvMgr.Save();
    }

    public void LoadRes()
    {
        ResConvMgr.Load();
    }

    private void ShowResources()
    {
        m_GoldText.text = "$" + ResCtrl.Ins.m_CharacterCtrl.m_CharacterRes.m_Gold;
        m_PopulationText.text = ResCtrl.Ins.m_CharacterCtrl.m_CharacterRes.m_Population + "人";
    }
    
    public void OnCharacterPage()
    {
        PageCtrl.Ins.Show<CharacterPage>(m_Character);
	}

	public void ShowTips(RectTransform transform, Item item) {
		if (!tips.activeSelf) {
			// 先转屏幕坐标，再转相对坐标
			tips.transform.localPosition = TransfromUtils.WorldToLocalInRect(
                transform.position, CameraMgr.m_Instance.m_UICamera, m_TopRoot);
			tips.SetActive (true);
		}
        tips.GetComponentInChildren<Text>().text = item.m_Data.m_Name;
        //// 保持在最底部
        tips.transform.parent = this.transform;
        tips.transform.parent = m_TopRoot;
	}
}
