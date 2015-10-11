using UnityEngine;

namespace TurnBase {
    public class TimeWaitState : State {

        private float waitTime = 0.3f;
        private float waited = 0f;

        public TimeWaitState(TurnBaseManager manager, string name, float waitTime = .3f) : base(manager) {
            this.Name = name;
            this.waitTime = waitTime;
        }

        public override void Enter() {
            base.Enter();
            waited = 0f;
        }

        public override void Update() {
            if (waited > waitTime) {
                this.Exit();
            } else {
                waited += Time.deltaTime;
            }
        }
    }
    
}