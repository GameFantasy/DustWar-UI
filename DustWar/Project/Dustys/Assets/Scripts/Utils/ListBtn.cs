using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListBtn : MonoBehaviour {
    public ScaleButton[] m_LlistBtn;
    // Use this for initialization
    void Start()
    {
        m_LlistBtn = transform.GetComponentsInChildren<ScaleButton>();
    }

    
}
