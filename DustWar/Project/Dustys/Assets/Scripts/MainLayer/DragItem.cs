using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Assets.GamePage;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Transform myTransform;

    private RectTransform myRectTransform;

    /// <summary>
    /// 用于event trigger对自身检测的开关
    /// </summary>
    private CanvasGroup canvasGroup;

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
    // 所属页面
    public Page m_OwnPage;
    void Start()
    {
        myTransform = this.transform;
        myRectTransform = this.transform as RectTransform;

        canvasGroup = GetComponent<CanvasGroup>();

        originalPosition = myTransform.position;
		Init ();
    }

	private void Init() {
		GetComponent<Image>().sprite = 
			m_Item.transform.Find ("Icon").GetComponent<Image>().sprite;
	}
    void Update()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;//让event trigger忽略自身，这样才可以让event trigger检测到它下面一层的对象,如包裹或物品格子等

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
            //移动至物品格子上
                if (curEnter.name == BagHangPage.m_sItemGrid)
                {
                    myTransform.position = curEnter.transform.position;
                    originalPosition = myTransform.position;
                    // 记录坐标
                    m_Item.m_Data.m_Index = curEnter.GetComponent<ItemGrid>().m_Index;

                    curEnter.GetComponent<Image>().color = lastEnterNormalColor;//当前格子恢复正常颜色

                }
                else
                {
                    //移动至包裹中的其它物品上
                    if (curEnter.name == eventData.pointerDrag.name && curEnter != eventData.pointerDrag)
                    {
                        // 交换坐标
                        int index = curEnter.GetComponent<DragItem>().m_Item.m_Data.m_Index;
                        curEnter.GetComponent<DragItem>().m_Item.m_Data.m_Index = m_Item.m_Data.m_Index;
                        m_Item.m_Data.m_Index = index;
                        Vector3 targetPostion = curEnter.transform.position;
                        curEnter.transform.position = originalPosition;
                        myTransform.position = targetPostion;
                        originalPosition = myTransform.position;
                    }
                    else
                    {
                        //拖拽至其它对象上面（包裹上的其它区域）
                        myTransform.position = originalPosition;
                    }
                }
                if (m_OwnPage == p)
                {
                    myTransform.parent = originalParentTransform;
                }

                CharacterPage cp = PageCtrl.Ins.Find<CharacterPage>(PageCtrl.Ins.topPage);
                if (m_OwnPage != cp && curEnter.name == CharacterPage.m_sItemGrid)
                {
                    if (curEnter.transform.parent.name == CharacterPage.m_sWeapon &&
                        m_Item.m_Data.m_Type == ITEMTYPE.WEAPON)
                    {
                        OnEquip();
                    }
                    if (curEnter.transform.parent.name == CharacterPage.m_sGuard &&
                        m_Item.m_Data.m_Type == ITEMTYPE.GUARD)
                    {
                        OnEquip();
                    }
                    if (curEnter.transform.parent.name == CharacterPage.m_sOrnament &&
                        m_Item.m_Data.m_Type == ITEMTYPE.ORNAMENT)
                    {
                        OnEquip();
                    }
                    if (curEnter.transform.parent.name == CharacterPage.m_sHorse &&
                        m_Item.m_Data.m_Type == ITEMTYPE.HORSE)
                    {
                        OnEquip();
                    }
                }
                Destroy(this.gameObject);
                cp.OnActiveBefre();
                m_OwnPage.OnActiveBefre();
            } 

            lastEnter.GetComponent<Image>().color = lastEnterNormalColor;//上一帧的格子恢复正常颜色
            canvasGroup.blocksRaycasts = true;//确保event trigger下次能检测到当前对象
            MainAreLayerCtrl.m_Instance.allowShowTips = true;//拖拽操作结束，允许显示物品信息
        }
    }



    public void OnPointerExit(PointerEventData eventData)
    {
		MainAreLayerCtrl.m_Instance.tips.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
		if (MainAreLayerCtrl.m_Instance.allowShowTips)
        {
            //鼠标悬在同一物品上，则已经显示的tips保持原样，避免闪烁
            if (MainAreLayerCtrl.m_Instance.tips.activeSelf)
                return;

            //显示提示信息
			MainAreLayerCtrl.m_Instance.ShowTips(myRectTransform, m_Item);
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

	public void OnEquip(){
        ResCtrl.Ins.m_CharacterCtrl.Equip(m_Item, m_Item.m_Data.m_Type, this.transform.localPosition);
        MainAreLayerCtrl.m_Instance.tips.SetActive(false);

        if (PageCtrl.Ins.Find<CharacterPage>() != null)
        {
            PageCtrl.Ins.Find<CharacterPage>().OnActiveBefre();
        }
        if (PageCtrl.Ins.Find<CharacterPage>().m_BagHangPage.gameObject.activeSelf)
        {
            BagHangPage p = PageCtrl.Ins.Find<CharacterPage>().m_BagHangPage;
            p.OnActiveBefre();
        }
	}
}
