using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TouchMgr : MonoBehaviour {
    public static TouchMgr m_Instance;

    public float m_CameraMoveSpeed;

    public enum TYPE
    {
        LEFT,
        RIGHT,
    }

    void Awake()
    {
        m_Instance = this;
    }

	// Update is called once per frame
	void Update () {
        //Debug.Log(Input.mousePosition);
        MoveMap();
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
#if IPHONE || ANDROID
			if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
#else
            if (EventSystem.current.IsPointerOverGameObject())
#endif      
            {
                //Debug.Log("当前触摸在UI上");
            }
            else
            {
                if (Input.GetMouseButtonDown(0)) // 鼠标左键一直按住也就是滑动
                {
                    OnButton(TYPE.LEFT);
                    //Debug.Log("拖拽");
                }
                if (Input.GetMouseButtonDown(1)) // 鼠标右键一直按住也就是滑动
                {
                    OnButton(TYPE.RIGHT);
                    //Debug.Log("拖拽");
                }
            }
        }
	}

    private void MoveMap()
    {
        bool doMove = false;
        //Debug.Log(Input.mousePosition);
        if (Input.mousePosition.y < 20 && Input.mousePosition.y >= 0)
        {
            if (CameraMgr.m_Instance.m_SceneCamera.transform.localPosition.y > -7.2f)
            {
                CameraMgr.m_Instance.m_SceneCamera.transform.Translate(Vector3.down * Time.deltaTime * m_CameraMoveSpeed);
                doMove = true;
            }
        }
        if (Input.mousePosition.y < Screen.height && Input.mousePosition.y >= Screen.height - 20)
        {
            if (CameraMgr.m_Instance.m_SceneCamera.transform.localPosition.y < 7.2f)
            {
                CameraMgr.m_Instance.m_SceneCamera.transform.Translate(Vector3.up * Time.deltaTime * m_CameraMoveSpeed);
                doMove = true;
            }
        }
        if (Input.mousePosition.x < 20 && Input.mousePosition.x >= 0)
        {
            if (CameraMgr.m_Instance.m_SceneCamera.transform.localPosition.x > -13f){
                CameraMgr.m_Instance.m_SceneCamera.transform.Translate(Vector3.left * Time.deltaTime * m_CameraMoveSpeed);
                doMove = true;
            }
        }
        if (Input.mousePosition.x < Screen.width && Input.mousePosition.x >= Screen.width - 20)
        {
            if(CameraMgr.m_Instance.m_SceneCamera.transform.localPosition.x < 13f){
                CameraMgr.m_Instance.m_SceneCamera.transform.Translate(Vector3.right * Time.deltaTime * m_CameraMoveSpeed);
                doMove = true;
            }
        }
    }

    private void OnButton(TYPE type)
    {
        SceneMgr.m_Instance.OnButton(type);
    }
}
