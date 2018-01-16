using UnityEngine;
using System.Collections;
using Assets.Utils;
using Assets.ResData;

public class ResCtrl : SingletonMono<ResCtrl>
{
    public CharacterCtrl m_CharacterCtrl;
    public PlayerMgr m_PlayerMgr;

    void Awake()
    {
        m_CharacterCtrl = GetComponentInChildren<CharacterCtrl>();
        m_PlayerMgr = GetComponentInChildren<PlayerMgr>();

        m_CharacterCtrl.InitData();
    }

	// Use this for initialization
	void Start () {
        // 读取配置数据
        GameDataSet.Ins = ResConvMgr.LoadGoFromFile<GameDataSet>(Application.dataPath + "/ResData" + "/ConfigData.dat");
        PoolResDataSet.Ins = ResConvMgr.LoadGoFromFile<PoolResDataSet>(Application.dataPath + "/ResData" + "/PoolData.dat");
        //Debug.Log(PoolResDataSet.Ins.m_PlayerDic[1].m_PlayerName);

        ResCtrl.Ins.m_CharacterCtrl.m_CharacterRes.Load();
        ResCtrl.Ins.m_PlayerMgr.Load();

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
