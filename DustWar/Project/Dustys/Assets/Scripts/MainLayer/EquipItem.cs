using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Assets.GamePage;

public class EquipItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform myTransform;

    private RectTransform myRectTransform;

    /// <summary>
    /// 拖拽操作前的有效位置，拖拽到有效位置时更新
    /// </summary>
    public Vector3 originalPosition;

    /// <summary>
    /// 拖拽操作前的有效位置，拖拽到有效位置时更新
    /// </summary>
    public Transform originalParentTransform;

    /// <summary>
    /// 记录上一帧所在物品格子
    /// </summary>
    private GameObject lastEnter = null;

    /// <summary>
    /// 记录上一帧所在物品格子的正常颜色
    /// </summary>
    private Color lastEnterNormalColor;

    /// <summary>
    /// 拖拽至新的物品格子时，该物品格子的高亮颜色
    /// </summary>
    private Color highLightColor = Color.cyan;

	// 实际所属物品
	public Item m_Item;
	// 没有物品是显示
    public Item m_DefaultEquipItem;
    // 所属页面
    public Page m_OwnPage;

    void Start()
    {
        myTransform = this.transform;
        myRectTransform = this.transform as RectTransform;


        originalPosition = myTransform.position;
		Init ();
    }

	public void Init() {
		if (m_Item != null) {
			this.transform.Find ("Icon").GetComponent<Image> ().sprite = 
			m_Item.transform.Find ("Icon").GetComponent<Image> ().sprite;
		} else {
			this.transform.Find ("Icon").GetComponent<Image> ().sprite = 
				m_DefaultEquipItem.transform.Find ("Icon").GetComponent<Image> ().sprite;
		}
	}
    void Update()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        lastEnter = eventData.pointerEnter;
        lastEnterNormalColor = lastEnter.GetComponent<Image>().color;

        originalPosition = myTransform.position;//拖拽前记录起始位置
        originalParentTransform = myTransform.parent;

        myTransform.parent = MainAreLayerCtrl.m_Instance.m_TopRoot;

        gameObject.transform.SetAsLastSibling();//保证当前操作的对象能够优先渲染，即不会被其它对象遮挡住

        MainAreLayerCtrl.m_Instance.allowShowTips = false;//拖拽开始，屏蔽物品信息显示
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(myRectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            myRectTransform.position = globalMousePos;

        }

        GameObject curEnter = eventData.pointerEnter;

        bool inItemGrid = EnterItemGrid(curEnter);
        if (inItemGrid)
        {
            Image img = curEnter.GetComponent<Image>();

            if (lastEnter != curEnter)
            {
                lastEnter.GetComponent<Image>().color = lastEnterNormalColor;
                lastEnter = curEnter;//记录当前物品格子以供下一帧调用
            }

            //当前格子设置高亮
            img.color = highLightColor;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject curEnter = eventData.pointerEnter;

        //拖拽到的空区域中（如包裹外），恢复原位
        if (curEnter == null)
        {
            myTransform.position = originalPosition;
        }
        else
        {
            if (PageCtrl.Ins.Find<CharacterPage>().m_BagHangPage.gameObject.activeSelf)
            {
                BagHangPage p = PageCtrl.Ins.Find<CharacterPage>().m_BagHangPage;
                if (m_OwnPage != p && curEnter.name == BagHangPage.m_sItemGrid)
                {
                    OnUnEquip();
                }
                p.OnActiveBefre();
                //Debug.Log("拖动人物栏");
            }
            if (PageCtrl.Ins.Find<CharacterPage>(PageCtrl.Ins.topPage) != null)
            {
                CharacterPage p = PageCtrl.Ins.Find<CharacterPage>(PageCtrl.Ins.topPage);
            }

            myTransform.position = originalPosition;
            myTransform.parent = originalParentTransform;
            lastEnter.GetComponent<Image>().color = lastEnterNormalColor;//上一帧的格子恢复正常颜色
            MainAreLayerCtrl.m_Instance.allowShowTips = true;//拖拽操作结束，允许显示物品信息
        }
    }

    /// <summary>
    /// 判断鼠标指针是否指向包裹中的物品格子
    /// </summary>
    /// <param name="go">鼠标指向的对象</param>
    /// <returns></returns>
    bool EnterItemGrid(GameObject go)
    {
        if (go == null)
        {
            return false;
        }
        return go.name == BagHangPage.m_sItemGrid;

    }


    public void OnPointerExit(PointerEventData eventData)
    {
		MainAreLayerCtrl.m_Instance.tips.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //鼠标悬在同一物品上，则已经显示的tips保持原样，避免闪烁
        if (eventData.lastPress == eventData.pointerEnter)
        {
            return;
        }

		//显示提示信息
		if (m_Item != null) {
			MainAreLayerCtrl.m_Instance.ShowTips (myRectTransform, m_Item);
		}
    }

	public void OnUnEquip(){
		if (m_Item != null) {
            ResCtrl.Ins.m_CharacterCtrl.UnEquip(m_Item.m_Data.m_Type, this.transform.position);
			this.transform.Find ("Icon").GetComponent<Image> ().sprite = 
			m_DefaultEquipItem.transform.Find ("Icon").GetComponent<Image> ().sprite;
			m_Item = null;
            MainAreLayerCtrl.m_Instance.tips.SetActive(false);

            if (PageCtrl.Ins.Find<CharacterPage>().m_BagHangPage.gameObject.activeSelf)
            {
                PageCtrl.Ins.Find<CharacterPage>().m_BagHangPage.OnActiveBefre();
            }
		}
	}
}
