using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageBox : LinearExpandGroup {

    /// <summary>
    /// 保存的最大消息数量
    /// </summary>
    public int maxMsg = 50;

    private Queue<string> msgList = new Queue<string>();

    /// <summary>
    /// 扩展MsgBox的宽度使之包含所有文本
    /// </summary>
    public void Resize() {
        var texts = GetComponentsInChildren<RectTransform>();

        var rect = GetComponent<RectTransform>();
        var maxWidth = rect.sizeDelta.x;
        foreach (var rectTransform in texts) {
            if (rectTransform.sizeDelta.x + 12 > maxWidth) {
                maxWidth = rectTransform.sizeDelta.x + 12;
            }
        }
        rect.sizeDelta = new Vector2(maxWidth, rect.sizeDelta.y);
    }

    /// <summary>
    /// 向
    /// </summary>
    /// <param name="msg"></param>
    public void addMessage(string msg) {
        if (msgList.Count >= maxMsg) {
            msgList.Dequeue();
            msgList.Enqueue(msg);
        }
    }
}
