using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class LinearExpandGroup : MonoBehaviour {

    public enum Alignment {
        Horizontal,
        Vertical
    }

    public float padding = 8f;

    public Alignment align = Alignment.Horizontal;
    public bool reverse = false;
    public RectTransform test;


    float minLength = 0f;

    public void AddChild(RectTransform child) {
        var parentTransform = GetComponent<RectTransform>();
        var childrenTransforms = GetComponentsInChildren<RectTransform>();

        var childrenLength = 0f;
        var expectLen = 0f;
        if (align == Alignment.Horizontal) {
            // 第一项为Content本身 所以去除
            for (int i = 1; i < childrenTransforms.Length; i++) {
                childrenLength += childrenTransforms[i].sizeDelta.x;
            }

            //加入新子节点后的总宽度
            expectLen = (childrenTransforms.Length + 1) * padding
                              + childrenLength + child.sizeDelta.x;

            resize(expectLen);

            if (reverse) {
                child.anchorMin = new Vector2(1, 0.5f);
                child.anchorMax = new Vector2(1,0.5f);
                child.anchoredPosition = new Vector2(
                    - (expectLen - padding - child.sizeDelta.x / 2),
                    parentTransform.sizeDelta.y);
            } else {
                child.anchorMin = new Vector2(0, 0.5f);
                child.anchorMax = new Vector2(0, 0.5f);
                child.anchoredPosition = new Vector2(
                    expectLen - padding - child.sizeDelta.x / 2,
                    parentTransform.sizeDelta.y);
            }
        } else {
            // 第一项为Content本身 所以去除
            for (int i = 1; i < childrenTransforms.Length; i++) {
                childrenLength += childrenTransforms[i].sizeDelta.y;
            }

            //加入新子节点后的总宽度
            expectLen = (childrenTransforms.Length + 1) * padding
                              + childrenLength + child.sizeDelta.y;

            resize(expectLen);

            if (reverse) {
                child.anchorMin = new Vector2(.5f, 1);
                child.anchorMax = new Vector2(.5f, 1);
                child.anchoredPosition = new Vector2(
                    parentTransform.sizeDelta.x,
                    - (expectLen - padding - child.sizeDelta.y / 2));
            } else {
                child.anchorMin = new Vector2(.5f, 0);
                child.anchorMax = new Vector2(.5f, 0);
                child.anchoredPosition = new Vector2(
                    parentTransform.sizeDelta.x,
                    expectLen - padding - child.sizeDelta.y / 2);
            }
        }
        

        child.SetParent(GetComponent<Transform>(), false);
        
    }

    public void FadeInAddChild(RectTransform child) {
        this.AddChild(child);
        var image = child.GetComponent<Image>();
        image.DOFade(0, 1).From();
        image.DOFade(1, .5f);
    }

    /// <summary>
    /// 重新调整控件的尺寸
    /// </summary>
    /// <param name="newWidth"></param>
    void resize(float newLen) {
        var parentTransform = GetComponent<RectTransform>();
        if (align == Alignment.Horizontal) {
            parentTransform.sizeDelta = new Vector2(
                newLen > minLength ? newLen : minLength,
                parentTransform.sizeDelta.y);
        } else {
            parentTransform.sizeDelta = new Vector2(
                parentTransform.sizeDelta.x,
                newLen > minLength ? newLen : minLength);
        }
    }

    void Start() {
        if (minLength == 0f) {
            minLength = align == Alignment.Horizontal
                ? GetComponent<RectTransform>().sizeDelta.x
                : GetComponent<RectTransform>().sizeDelta.y;
        }

        var nObj = Instantiate(test).GetComponent<RectTransform>();
        FadeInAddChild(nObj);
        var nObj1 = Instantiate(test).GetComponent<RectTransform>();
        FadeInAddChild(nObj1);
        var nObj2 = Instantiate(test).GetComponent<RectTransform>();
        FadeInAddChild(nObj2);
//        var nObj3 = Instantiate(test).GetComponent<RectTransform>();
//        AddChild(nObj3);
//        var nObj4 = Instantiate(test).GetComponent<RectTransform>();
//        AddChild(nObj4);
    }
    
}
