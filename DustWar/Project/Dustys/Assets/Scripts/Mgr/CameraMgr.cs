using UnityEngine;
using System.Collections;

public class CameraMgr : MonoBehaviour {
	public static CameraMgr m_Instance;
    public Camera m_UICamera;
    public Camera m_SceneCamera;

	void Awake()
	{
		m_Instance = this;
	}
}
