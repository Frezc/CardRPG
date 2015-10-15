using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AutoExpandText : MonoBehaviour {

    private Text text;

	// Use this for initialization
	void Start () {
	    text = GetComponent<Text>();

        resize();
	}
    
	// Update is called once per frame
	void Update () {
	}

    /// <summary>
    /// 将文本框扩展到文字同等长度
    /// </summary>
    public void resize() {

        Font font = this.text.font;
        int fontSize = this.text.fontSize;
        string text = this.text.text;
        font.RequestCharactersInTexture(text, fontSize, FontStyle.Normal);
        CharacterInfo characterInfo;
        float width = 0f;
        for (int i = 0; i < text.Length; i++) {
            font.GetCharacterInfo(text[i], out characterInfo, fontSize);
            width += characterInfo.advance;
        }

        var rect = this.text.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(width, rect.sizeDelta.y);

        GetComponentInParent<MessageBox>().Resize();
    }
}
