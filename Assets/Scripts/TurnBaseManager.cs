using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TurnBase;

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
        states.Add(new TimeWaitState("TurnStart", .5f));
        states.Add(new DrawState("Draw"));
        states.Add(new PrepareState("Prepare"));
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
