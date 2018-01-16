using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ImageNum : MonoBehaviour 
{
    private LinkedList<int> m_listNumbers = new LinkedList<int>();
    public List<Image> m_listNumberImg;
    public Color m_Color;
    public bool m_isRight;

    private static readonly string NUMBERS_PATH_PREFIX = "numbers/miao-shu-zi_{0}";

    void Awake()
    {
    }

    void OnDestroy()
    {
        if (null != m_listNumberImg)
        {
            m_listNumberImg.Clear();
            m_listNumberImg = null;
        }
        m_listNumbers.Clear();
        m_listNumbers = null;
    }

    public void SetVal(int dwForceValue) {
        m_listNumbers.Clear();
        if (!m_isRight) {
            // 左对齐
            int dwLowByte = 0;
            int dwCurrValue = dwForceValue;
            while (true) {
                dwLowByte = dwCurrValue % 10;
                dwCurrValue = dwCurrValue / 10;
                m_listNumbers.AddLast(dwLowByte);
                if (0 == dwCurrValue) {
                    break;
                }
            }
            int index = 0;
            var node = m_listNumbers.Last;
            while (true) {
                //Debug.Log(string.Format(NUMBERS_PATH_PREFIX, node.Value));
                Object[] _atlas = Resources.LoadAll("numbers/miao-shu-zi");
                m_listNumberImg[index].sprite = _atlas[node.Value + 1] as Sprite;
                m_listNumberImg[index].color = new Color(m_Color.r, m_Color.g, m_Color.b);
                ++index;
                if (null == node.Previous) {
                    break;
                }
                node = node.Previous;
            }
            for (int i = 0; i < m_listNumberImg.Count; ++i) {
                if (i < index) {
                    m_listNumberImg[i].transform.localPosition = Vector3.zero;
                    m_listNumberImg[i].transform.localPosition += Vector3.right * 80 * i;
                    m_listNumberImg[i].gameObject.SetActive(true);
                } else {
                    if (m_listNumberImg[i] != null)
                        m_listNumberImg[i].gameObject.SetActive(false);
                }
            }
        } else {
            // 右对齐
            int dwLowByte = 0;
            int dwCurrValue = dwForceValue;
            int index = 0;
            while (true) {
                dwLowByte = dwCurrValue % 10;
                dwCurrValue = dwCurrValue / 10;

                m_listNumberImg[index].gameObject.SetActive(true);

                Object[] _atlas = Resources.LoadAll("numbers/miao-shu-zi");
                m_listNumberImg[index].sprite = _atlas[dwLowByte + 1] as Sprite;
                m_listNumberImg[index].color = new Color(m_Color.r, m_Color.g, m_Color.b);
                ++index;

                if (0 == dwCurrValue) {
                    break;
                }
            }
            for (int i = 0; i < m_listNumberImg.Count; ++i) {
                if (i < index) {
                    m_listNumberImg[i].transform.localPosition = Vector3.zero;
                    m_listNumberImg[i].transform.localPosition -= Vector3.right * 120 * i;
                    m_listNumberImg[i].gameObject.SetActive(true);
                } else {
                    if (m_listNumberImg[i] != null)
                        m_listNumberImg[i].gameObject.SetActive(false);
                }
            }
        }
    }


    public void Init()
    {
        m_listNumberImg = new List<Image>();
        Transform thisTransform = transform;
        int iChildCount = thisTransform.childCount;
        for (int i = 0; i < iChildCount; ++i)
        {
            m_listNumberImg.Add(thisTransform.GetChild(i).GetComponent<Image>());
        }
    }
}
