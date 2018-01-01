using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hp : MonoBehaviour
{
    private Slider slider;

    // 血条的显示
    void Start()
    {
        slider = this.transform.Find("Slider").GetComponent<Slider>();
    }
    //调用的方法
    void ChangeHp(float _value)
    {
        slider.value = _value;
    }
}
