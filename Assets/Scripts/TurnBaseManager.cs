using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TurnBase;

/// <summary>
/// 回合中各个阶段调用的接口
/// </summary>
public abstract class ITurnBaseEvents {
    public virtual void OnTurnStartEnter(TurnBaseManager turnBaseManager) { }

    public virtual void OnDrawEnter(TurnBaseManager turnBaseManager) { }

    public virtual void OnPrepareEnter(TurnBaseManager turnBaseManager) { }

    public virtual void OnExecutiveEnter(TurnBaseManager turnBaseManager) { }

    public virtual void OnAttack(TurnBaseManager turnBaseManager) { }

    public virtual void OnAttacked(TurnBaseManager turnBaseManager) { }

    public virtual void OnTurnEndEnter(TurnBaseManager turnBaseManager) { }
}

public class TurnBaseManager : MonoBehaviour {

    public UIController uiController;

    private int turnOrder = 0;
    private List<State> states = new List<State>();
    private int currentStateIndex = 0;

    public void reset() {
        turnOrder = 0;
        currentStateIndex = 0;
    }

	// Use this for initialization
	void Start () {
	    //初始化流程
        states.Add(new TimeWaitState(this, "TurnStart", .5f));
        states.Add(new DrawState(this, "Draw"));
        states.Add(new PrepareState(this, "Prepare"));
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
