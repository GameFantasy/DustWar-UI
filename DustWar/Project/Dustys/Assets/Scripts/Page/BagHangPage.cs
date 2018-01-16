using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Assets.GamePage;

public class BagHangPage : Page{

	public GameObject m_ItemPrefab;
	public RectTransform m_RectBagList;

    public static string m_sItemGrid = "ItemGrid";
    public static string m_sItem = "BagItem(Clone)";
    /// <summary>
    /// 是否允许显示信息提示
    /// </summary>
    public bool allowShowTips = true;

    public GameObject m_BagBG;
    public GameObject m_BagList;

    public List<GameObject> items;
    public List<GameObject> grids;

    void Awake()
    {
        //m_BagList = ResCtrl.Ins.m_CharacterCtrl.transform.Find("BagList");
    }

    void Start()
    {
        base.Start();
    }

    public override void OnActiveBefre()
    {
        GameObjUtils.Destory(m_RectBagList);
        foreach (Item i in ResCtrl.Ins.m_CharacterCtrl.m_BagList)
        {
            GameObject go = GameObjUtils.Create(m_RectBagList, m_ItemPrefab);
            go.transform.parent = m_RectBagList;
            go.GetComponent<DragItem>().m_Item = i;
            go.GetComponent<DragItem>().m_OwnPage = this;
        }
        StartCoroutine(SortItems());
	}

    /// <summary>
    /// 物品简单排序
    /// </summary>
    public IEnumerator SortItems()
    {
        yield return new WaitForEndOfFrame();
        items = FindObject.FindChildsInGameObject(m_BagList, m_sItem);
        grids = FindObject.FindChildsInGameObject(m_BagBG, m_sItemGrid);

        #region 包裹格子作简单排序，顺序为从左上起逐行至右下格子
#if flase
        List<GameObject> gridList = new List<GameObject>();
        gridList.AddRange(grids);
        gridList.Sort(delegate(GameObject x, GameObject y)
        {
            Vector3 xp = x.transform.position;
            Vector3 yp = y.transform.position;
            if (xp.y == yp.y && xp.x == yp.x)
            {
                return 0;
            }else if (xp.y == yp.y)
            {
                if (xp.x < yp.x)
                {
                    return -1;
                }
                return 1;
            }else if (xp.x == yp.x)
            {
                if (xp.y > yp.y)
                {
                    return -1;
                }
                return 1;
            }else if (xp.y > yp.y)
            {
                return -1;
            }
            return 1;
        });
#endif
        #endregion

        //物品与排列好的格子相对应
        for (int i = 0; i < items.Count; i++)
        {
            items[i].transform.position = grids[items[i].GetComponent<DragItem>().m_Item.m_Data.m_Index].transform.position;
            items[i].GetComponent<DragItem>().originalPosition = items[i].transform.position;
        }
    }
}

