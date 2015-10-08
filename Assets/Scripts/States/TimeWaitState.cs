using UnityEngine;

namespace TurnBase {
    public class TimeWaitState : State {

        private float waitTime = 0.3f;
        private float waited = 0f;

        public TimeWaitState(string name, float waitTime = .3f) {
            this.Name = name;
            this.waitTime = waitTime;
        }

        public virtual void Enter() {
            base.Enter();
            waited = 0f;
        }

        public virtual void Update() {
            if (waited > waitTime) {
                this.Exit();
            } else {
                waited += Time.deltaTime;
            }
        }
    }
    
}