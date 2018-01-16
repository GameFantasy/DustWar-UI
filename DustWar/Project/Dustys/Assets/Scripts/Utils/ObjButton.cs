using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class ObjButton : MonoBehaviour {

    [FormerlySerializedAs("onClick")]
    public Button.ButtonClickedEvent m_OnRightClick = new Button.ButtonClickedEvent();
    public Button.ButtonClickedEvent m_OnLeftClick = new Button.ButtonClickedEvent();
}
