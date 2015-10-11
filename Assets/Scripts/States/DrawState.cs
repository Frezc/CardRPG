namespace TurnBase {
    public class DrawState : TimeWaitState {
        public DrawState(TurnBaseManager manager, string name, float waitTime = 1f) : base(manager, name, waitTime) {

        }

        public override void Enter() {
            base.Enter();
            //draw
        }
    }
}