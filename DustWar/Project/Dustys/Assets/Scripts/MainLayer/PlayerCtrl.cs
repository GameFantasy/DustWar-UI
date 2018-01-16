using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.ResData;

public class PlayerCtrl : MonoBehaviour {
    public RectTransform m_CanvaRect;
    public Camera m_UICamera;

    public Animator m_Anim;

    // 是否到达
    public bool m_isOver;
    // 是否真的到达（分段寻路）
    public bool m_isAllOver;

    public Vector3 m_CurPos;
    public Vector3 m_TarPos;
    public Vector3 m_vForwardNormalized;

    public int m_CurCuitPointIndex;

    public RectTransform m_RectPointMap;
    public GameObject m_Map;

    // Use this for initialization
    void Awake()
    {
    }

    void Start()
    {
        MainAreLayerCtrl.m_Instance.m_PlayerCtrl = this;
    }

    #region 装备影响属性
    public void SetAttribute() {
        
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
    }
    
}
