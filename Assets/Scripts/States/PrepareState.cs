using UnityEngine;

namespace TurnBase {
    public class PrepareState : State {
        private float maxWaitTime = 30f;
        private float leftTime;
        private bool[] playerCheck;

        public PrepareState(string name, float maxWaitTime = 30f) {
            this.Name = name;
            this.maxWaitTime = maxWaitTime;
        }

        public override void Enter() {
            base.Enter();
            leftTime = maxWaitTime;
            //get players' number
            //and new bool[n]
        }

        public override void Update() {
            base.Update();
            if (playerAllCheck() || leftTime <= 0) {
                this.Exit();
            } else {
                leftTime -= Time.deltaTime;
            }
        }

        public bool playerAllCheck() {
            if (playerCheck == null) {
                return false;
            }
            
            foreach (var check in playerCheck) {
                if (!check) {
                    return false;
                }
            }

            return true;
        }
    }
}