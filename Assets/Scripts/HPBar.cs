using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HPBar : MonoBehaviour {

    public RectTransform bufferArea;
    public RectTransform fillArea;
    public Text text;

    public int HP = 100;
    public int MaxHP = 100;

    private float speed = 1f;

    /// <summary>
    /// 将子节点的Rect与父节点对齐, 并且重置
    /// </summary>
    public void reset() {
        var rect = GetComponent<RectTransform>();

        bufferArea.sizeDelta = rect.sizeDelta;
        bufferArea.FindChild("Buffer").GetComponent<RectTransform>().sizeDelta = rect.sizeDelta;
        fillArea.sizeDelta = rect.sizeDelta;
        fillArea.FindChild("Fill").GetComponent<RectTransform>().sizeDelta = rect.sizeDelta;

        speed = rect.sizeDelta.x * 1;

        updateText();
    }

    public void setHP(int hp) {
        HP = hp;
        checkHp();
        updateHpBar(true);
    }

    public void setMaxHP(int max) {
        MaxHP = max;
        checkHp();
        updateHpBar(true);
    }


    /// <summary>
    /// 受到伤害，缓冲血条会慢慢减少
    /// </summary>
    /// <param name="damage">伤害值</param>
    public void hurt(int damage) {
        HP -= damage;
        checkHp();
        updateHpBar(false);
    }

    /// <summary>
    /// 恢复，血条并不会有动画
    /// </summary>
    /// <param name="recovery">恢复值</param>
    public void recover(int recovery) {
        HP += recovery;
        checkHp();
        updateHpBar(true);
    }

    void Awake() {
        reset();
        bufferArea.gameObject.SetActive(true);
        fillArea.gameObject.SetActive(true);

        checkHp();
    }

    void Update() {
        if (bufferArea.sizeDelta.x > fillArea.sizeDelta.x) {
            bufferArea.sizeDelta = new Vector2(
                bufferArea.sizeDelta.x - Time.deltaTime * speed, bufferArea.sizeDelta.y);
        }

        if (bufferArea.sizeDelta.x < .1f) {
            bufferArea.sizeDelta = new Vector2(.1f, bufferArea.sizeDelta.y);
        }
    }

    /// <summary>
    /// 检查hp的有效性
    /// </summary>
    private void checkHp() {
        if (MaxHP < 0) {
            MaxHP = 0;
            HP = 0;
        }

        if (HP < 0) {
            HP = 0;
        }

        if (HP > MaxHP) {
            HP = MaxHP;
        }
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="isUpdateBuffer">是否直接更新缓冲</param>
    void updateHpBar(bool isUpdateBuffer) {
        float percent = (float) HP / MaxHP;
        var rect = GetComponent<RectTransform>();

        var newSize = new Vector2(rect.sizeDelta.x * percent, rect.sizeDelta.y);
        if (newSize.x < .1f) {
            newSize.x = .1f;
        }
        fillArea.sizeDelta = newSize;
        if (isUpdateBuffer) {
            bufferArea.sizeDelta = newSize;
        }

        updateText();
    }

    /// <summary>
    /// 更新绑定的文本
    /// </summary>
    void updateText() {
        if (text != null) {
            text.text = HP + " / " + MaxHP;
        }
    }
}
