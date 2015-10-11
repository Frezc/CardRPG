namespace Model {

    public class BReload : Buff {

        public BReload(Character character) : base(character) {
            name = "Reload";
            inherent = true;
            duration = -1;
        }

        public override void UpdateDescription() {
            description = "抽牌时有"+chance+"%的几率再抽一张。";
            if (Addition) {
                description += "效果发动后有10%的几率再抽一张。";
            }
        }

        public override void OnDrawEnter(TurnBaseManager turnBaseManager) {
            //修改drawstate中的抽卡数量，然后抽牌
        }
    }
}