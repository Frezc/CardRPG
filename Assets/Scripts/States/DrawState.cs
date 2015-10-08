namespace TurnBase {
    public class DrawState : TimeWaitState {
        public DrawState(string name, float waitTime = 1f) : base(name, waitTime) {

        }

        public override void Enter() {
            base.Enter();
            //draw
        }
    }
}