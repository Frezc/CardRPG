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

        /// <summary>
        /// 技能需求的卡片类型和具体卡片
        /// </summary>
        /// <param name="cardTypes"></param>
        /// <param name="ids"></param>
        public CostCards(CardType[] cardTypes = null, int[] ids = null) {
            this.cardTypes = cardTypes;
            this.ids = ids;
        }

        /// <summary>
        /// 传入的卡片是否符合卡片要求
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="canCost">是否可以被消耗</param>
        /// <returns></returns>
        public bool MeetCost(int[] cards, out bool canCost) {
            canCost = false;
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
                        if (CardManager.GetCardTypeById(array[i]) == cardType) {
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

            if (array.Count == 0) {
                canCost = true;
            }
            return true;
        }
        
    }

    public abstract class Skill : IRequirement {

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

        /// <summary>
        /// 创建该技能的战斗状态，如果是被动技能则返回null
        /// </summary>
        /// <returns></returns>
        public virtual SkillBattleState CreateBattleState(Character owner, SkillData data) {
            return null;
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

        /// <summary>
        /// 卡片的消耗
        /// </summary>
        protected CostCards cost;

        public override SkillType GetSkillType() {
            return SkillType.Positive;
        }

        public override SkillBattleState CreateBattleState(Character owner, SkillData data) {
            var state = new SkillBattleState(this, owner, data.Level);
            return state;
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
        
    }

    /// <summary>
    /// 保存技能状态的数据
    /// </summary>
    public class SkillData {
        public int Id { get; set; }
        public int Level { get; set; }
    }

    /// <summary>
    /// 技能在战斗时所需的一些属性
    /// </summary>
    public class SkillBattleState {

        private PositiveSkill skill;

        /// <summary>
        /// 技能所属
        /// </summary>
        public Character Owner {
            get { return owner; }
        }
        private Character owner;
        
        /// <summary>
        /// 冷却时间
        /// </summary>
        public int Cooldown { get { return cooldown; } }
        private int cooldown;

        /// <summary>
        /// 是否被激活
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 技能当前的等级
        /// </summary>
        public int Level { get { return level; } }
        private int level;

        public SkillBattleState(PositiveSkill skill, Character owner, int level = 1) {
            this.skill = skill;
            this.owner = owner;
            IsActive = true;
            cooldown = skill.Cooldown;
            this.level = level;
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