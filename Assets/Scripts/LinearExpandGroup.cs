using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
    public bool Center = true;

    float minLength = 0f;

    public void AddChild(RectTransform child) {
        var parentTransform = GetComponent<RectTransform>();
        //这样会把孙结点也包含进来
//        var childrenTransforms = GetComponentsInChildren<RectTransform>();
        var childrenTransforms = new List<RectTransform>();
        var transform = this.transform;
        for (int i = 0; i < transform.childCount; i++) {
            var achild = transform.GetChild(i);
            if (achild.gameObject.activeSelf) {
                childrenTransforms.Add(achild.GetComponent<RectTransform>());
            }
        }

        var childrenLength = 0f;
        var expectLen = 0f;

        //设置居中或不居中时的锚点
        var anchor = .5f;
        if (!Center) {
            anchor = 0f;
        }

        if (align == Alignment.Horizontal) {
            for (int i = 0; i < childrenTransforms.Count; i++) {
                childrenLength += childrenTransforms[i].sizeDelta.x;
            }

            //加入新子节点后的总宽度
            expectLen = (childrenTransforms.Count + 2) * padding
                              + childrenLength + child.sizeDelta.x;

            resize(expectLen);

            if (reverse) {
                child.anchorMin = new Vector2(1, anchor);
                child.anchorMax = new Vector2(1, anchor);
                child.anchoredPosition = new Vector2(
                    - (expectLen - padding - child.sizeDelta.x / 2),
                    parentTransform.sizeDelta.y);
            } else {
                child.anchorMin = new Vector2(0, anchor);
                child.anchorMax = new Vector2(0, anchor);
                child.anchoredPosition = new Vector2(
                    expectLen - padding - child.sizeDelta.x / 2,
                    parentTransform.sizeDelta.y);
            }
        } else {
            // 第一项为Content本身 所以去除
            for (int i = 0; i < childrenTransforms.Count; i++) {
                childrenLength += childrenTransforms[i].sizeDelta.y;
            }

            //加入新子节点后的总宽度
            expectLen = (childrenTransforms.Count + 2) * padding
                              + childrenLength + child.sizeDelta.y;

            resize(expectLen);
            
            if (reverse) {
                child.anchorMin = new Vector2(anchor, 1);
                child.anchorMax = new Vector2(anchor, 1);
                child.anchoredPosition = new Vector2(
                    parentTransform.sizeDelta.x,
                    - (expectLen - padding - child.sizeDelta.y / 2));
            } else {
                child.anchorMin = new Vector2(anchor, 0);
                child.anchorMax = new Vector2(anchor, 0);
                child.anchoredPosition = new Vector2(
                    parentTransform.sizeDelta.x,
                    expectLen - padding - child.sizeDelta.y / 2);
            }
        }
        

        child.SetParent(GetComponent<Transform>(), false);
        
    }

    /// <summary>
    /// 当删除子节点后调用，整理位置
    /// </summary>
    public void MoveToAlign() {
        var parentTransform = GetComponent<RectTransform>();
        //        var childrenTransforms = GetComponentsInChildren<RectTransform>();

        var childrenTransforms = new List<RectTransform>();
        var transform = this.transform;
        for (int i = 0; i < transform.childCount; i++) {
            var achild = transform.GetChild(i);
            if (achild.gameObject.activeSelf) {
                childrenTransforms.Add(achild.GetComponent<RectTransform>());
            }
        }

        var childrenLength = 0f;
        var expectLen = 0f;

        if (align == Alignment.Horizontal) {
            // 第一项为Content本身 所以去除
            for (int i = 0; i < childrenTransforms.Count; i++) {
                childrenLength += childrenTransforms[i].sizeDelta.x;
            }

            //加入新子节点后的总宽度
            expectLen = (childrenTransforms.Count + 1) * padding
                              + childrenLength;

            //将所有卡片移动到正确的位置
            for (int i = 0; i < childrenTransforms.Count; i++) {
                childrenTransforms[i].DOAnchorPos(new Vector2(
                    reverse
                        ? -(padding + childrenTransforms[i].sizeDelta.x / 2 +
                            (padding + childrenTransforms[i].sizeDelta.x) * i)
                        : padding + childrenTransforms[i].sizeDelta.x / 2 +
                          (padding + childrenTransforms[i].sizeDelta.x) * i,
                    childrenTransforms[i].anchoredPosition.y),
                    .8f);
            }
        } else {
            // 第一项为Content本身 所以去除
            for (int i = 0; i < childrenTransforms.Count; i++) {
                childrenLength += childrenTransforms[i].sizeDelta.y;
            }

            //加入新子节点后的总gao度
            expectLen = (childrenTransforms.Count + 1) * padding
                              + childrenLength;

            //将所有卡片移动到正确的位置
            for (int i = 0; i < childrenTransforms.Count; i++) {
                childrenTransforms[i].DOAnchorPos(new Vector2(
                    childrenTransforms[i].anchoredPosition.x,
                    reverse
                        ? -(padding + childrenTransforms[i].sizeDelta.y / 2 +
                            (padding + childrenTransforms[i].sizeDelta.y) * i)
                        : padding + childrenTransforms[i].sizeDelta.y / 2 +
                          (padding + childrenTransforms[i].sizeDelta.y) * i),
                    .8f);
            }
        }

        resize(expectLen);
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
        var nObj3 = Instantiate(test).GetComponent<RectTransform>();
        AddChild(nObj3);
        var nObj4 = Instantiate(test).GetComponent<RectTransform>();
        AddChild(nObj4);
    }
    
}
