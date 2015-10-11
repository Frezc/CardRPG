using System.Collections.Generic;
using System.Linq;

namespace Model {
    public enum SkillType {
        Positive,
        Passive
    }

    /// <summary>
    /// 技能发动所需的卡片
    /// </summary>
    public class CostCards {
        private CardType[] cardTypes;
        private int[] ids;

        public CostCards(CardType[] cardTypes = null, int[] ids = null) {
            this.cardTypes = cardTypes;
            this.ids = ids;
        }

        /// <summary>
        /// 传入的卡片是否符合卡片要求
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public bool MeetCost(int[] cards) {
            if (cards.Length <= 0) {
                return false;
            }

            var array = cards.ToList();

            if (this.ids != null) {
                foreach (var id in ids) {
                    if (!array.Remove(id)) {
                        return false;
                    }
                }
            }

            if (this.cardTypes != null) {
                if (array.Count <= 0) {
                    return false;
                }

                foreach (var cardType in cardTypes) {
                    var i = 0;
                    for (; i < array.Count; i++) {
                        if (CardManager.Instance.GetCardTypeById(array[i]) == cardType) {
                            array.RemoveAt(i);
                            i = -1; // mark as type getting flag
                            break;
                        }
                    }

                    if (i != -1) {
                        return false;
                    }
                }
            }

            return true;
        }
    }

    public abstract class Skill : IRequirement {

        /// <summary>
        /// 技能的所属者
        /// </summary>
        public Character Owner {
            get { return owner; }
        }
        protected Character owner;

        /// <summary>
        /// 技能的最大等级
        /// </summary>
        public int MaxLevel {
            get { return maxLevel; } 
        }
        protected int maxLevel = 0;

        /// <summary>
        /// 当前技能等级
        /// </summary>
        public int CurLevel {
            get { return curLevel; }
        }
        protected int curLevel = 0;

        /// <summary>
        /// 技能的唯一id
        /// </summary>
        public int Id {
            get { return id; }
        }
        protected int id = -1;

        /// <summary>
        /// 技能名字
        /// </summary>
        public string Name {
            get { return name; }
        }
        protected string name = "undefined";

        /// <summary>
        /// 描述
        /// </summary>
        public string Description {
            get { return description; }
        }
        protected string description = "undefined";


        public Skill(Character character) {
            this.owner = character;
        }

        /// <summary>
        /// 技能类型
        /// </summary>
        /// <returns></returns>
        public abstract SkillType GetSkillType();

        /// <summary>
        /// 技能装备的要求
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public virtual bool MeetRequirement(Character character) {
            return true;
        }

        /// <summary>
        /// 更新技能描述
        /// </summary>
        public virtual void UpdateDescription() {
        }
    }


    /// <summary>
    /// 主动技能是在战斗中能直接使用的技能
    /// </summary>
    public abstract class PositiveSkill : Skill {
        /// <summary>
        /// 讲技能效果保存到manager里，在结算阶段一起调用
        /// </summary>
        /// <param name="turnBaseManager"></param>
        public abstract void Effect(TurnBaseManager turnBaseManager);

        /// <summary>
        /// 技能冷却时间
        /// </summary>
        public int Cooldown {
            get { return cooldown; }
        }
        protected int cooldown = 0;

        public override SkillType GetSkillType() {
            return SkillType.Positive;
        }

        public PositiveSkill(Character character) : base(character) {
        }
    }

    /// <summary>
    /// 被动技能主要是在战斗开始时给玩家或对方附加buff或一次性状态
    /// </summary>
    public abstract class PassiveSkill : Skill {

        public override SkillType GetSkillType() {
            return SkillType.Passive;
        }

        /// <summary>
        /// 战斗开始时发动的附加状态
        /// </summary>
        /// <param name="turnBaseManager"></param>
        public abstract void Addon(TurnBaseManager turnBaseManager);

        public PassiveSkill(Character character) : base(character) {
        }
    }

    /// <summary>
    /// 技能在战斗时所需的一些属性
    /// </summary>
    public class SkillBattleState {

        private PositiveSkill skill;

        /// <summary>
        /// 冷却时间
        /// </summary>
        public int Cooldown { get { return cooldown; } }
        private int cooldown;

        /// <summary>
        /// 是否被激活
        /// </summary>
        public bool IsActive { get; set; }

        public SkillBattleState(PositiveSkill skill) {
            this.skill = skill;
            IsActive = true;
        }

        /// <summary>
        /// 减少技能cd
        /// </summary>
        /// <param name="turns">减少的回合数</param>
        public void ReduceCd(int turns) {
            cooldown -= turns;
            if (cooldown <= 0) {
                cooldown = 0;
                IsActive = true;
            }
        }

        /// <summary>
        /// 施放技能
        /// </summary>
        public void Spell(TurnBaseManager turnBaseManager) {
            if (!IsActive)
                return;

            skill.Effect(turnBaseManager);

            this.cooldown += skill.Cooldown;
            if (this.cooldown > 0) {
                IsActive = false;
            }
        }
    }

}