using UnityEngine;
using System.Collections;

public class SceneMgr : MonoBehaviour {
    public static SceneMgr m_Instance;

    void Awake()
    {
        m_Instance = this;
    }

    public void OnButton(TouchMgr.TYPE type)
    {
        switch (type)
        {
            case TouchMgr.TYPE.LEFT:
            {
                //Vector2 myRay = CameraMgr.m_Instance.m_SceneCamera.ScreenToWorldPoint(Input.mousePosition);
                //Vector2 mousePos2D = new Vector2(myRay.x, myRay.y);
                //RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector3.zero);
                //if (hit.collider != null)
                //{
                //    Debug.Log("点中物体");
                //    //do something  
                //   // print(hit.collider);

                //}
                //Collider2D[] col = Physics2D.OverlapPointAll(
                //    CameraMgr.m_Instance.m_SceneCamera.ScreenToWorldPoint(Input.mousePosition));

                //Debug.Log("点");
                //if (col.Length > 0)
                //{
                //    foreach (Collider2D c in col)
                //    {
                //        Debug.Log("点中物体" + c.gameObject.name);
                //        //do what you want
                //    }
                //}
                break;
            }
            case TouchMgr.TYPE.RIGHT:
            {
                break;
            }
        }
    }
}
