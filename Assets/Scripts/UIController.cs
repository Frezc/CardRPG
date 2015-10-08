using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public LinearExpandGroup cardGroup;
    public RectTransform skillList;
    public LinearExpandGroup skillGroup;
    public HPBar hpBar;
    public Text deckNumberText;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// 显示技能面板
    /// </summary>
    public void ShowSkillGroup() {
        print("show skills");
        if (skillList) {
            skillList.gameObject.SetActive(true);
            skillList.GetComponent<CanvasRenderer>().SetAlpha(0);
//            Sequence sequence = DOTween.Sequence();

//            sequence.Append(skillList.DOMoveX());
            skillList.anchoredPosition = new Vector2(-20f, 0f);
            skillList.DOAnchorPos(new Vector2(0f, 0f), .5f);
        }
    }

    /// <summary>
    /// 隐藏技能面板
    /// </summary>
    public void HideSkillGroup() {
        if (skillList) {
            print("hide skills");
            skillList.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 切换面板显示状态
    /// </summary>
    public void ToggleSkillGroup() {
        if (skillList) {
            if (skillList.gameObject.activeSelf) {
                HideSkillGroup();
            } else {
                ShowSkillGroup();
            }
        }
    }


}
