using System;

namespace Model {

    /// <summary>
    /// 普通的攻击
    /// </summary>
    public class SAttack : PositiveSkill {

        /// <summary>
        /// 以后会使用xml来保存技能的属性
        /// </summary>
        public SAttack() {
            id = 0;
            name = "Attack";
            maxLevel = 5;
        }

        public override void UpdateDescription() {
            description = "消耗一直武器卡进行攻击，伤害修正为 " + (1 + curLevel * .1f);
        }

        public override void Effect(TurnBaseManager turnBaseManager) {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SReload : PassiveSkill {

        private Buff buff;

        public SReload() {
            id = 2;
            name = "Reload";
            maxLevel = 5;
        }

        public override void UpdateDescription() {
            description = "有 " + curLevel * 2 + "% 的几率再抽一张牌";
            if (curLevel >= maxLevel) {
                description += "；[max]: 成功发动效果后有10%的几率再抽一张";
            }
        }

        public override void Addon(TurnBaseManager turnBaseManager) {
//            turnBaseManager.addBuff(character);
        }
    }
}