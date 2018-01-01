using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Head : MonoBehaviour {

	// 头像的显示
    private Image headImg;
	void Start () {
        headImg = this.transform.Find("Border/Head").GetComponent<Image>();
	}
    //设置头像，或者修改成文理穿过里啊处理
    void SetImage(Sprite _sprite)
    {
        headImg.sprite = _sprite;
    }
    void SetImage(Texture2D _texture)
    {
        Sprite spriteload = Sprite.Create(_texture, new Rect(0.0F, 0.0F, _texture.width, _texture.height), new Vector2(0.5F, 0.5F));
        headImg.sprite = spriteload;
    }
}
