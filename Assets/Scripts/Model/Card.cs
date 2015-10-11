using System;
using UnityEngine;

namespace Model {

    public enum CardType {
        Weapon,
        Armor,
        Element
    }

    public abstract class Card : IRequirement{

        public int Id {
            get { return id; }
        }

        public string Name {
            get { return name; }
        }

        public string Description {
            get { return description; }
        }

        protected int id = 0;
        protected string name = "undefined";
        protected string description = "undefined";

        /// <summary>
        /// 卡片效果
        /// </summary>
        public abstract void Effect();


        /// <summary>
        /// 卡片装备需求
        /// </summary>
        /// <param name="character">装备者</param>
        /// <returns></returns>
        public virtual bool MeetRequirement(Character character) {
            return true;
        }

        /// <summary>
        /// 卡片类型
        /// </summary>
        /// <returns></returns>
        public abstract CardType GetCardType();
    }

    public abstract class WeaponCard : Card {

        public override CardType GetCardType() {
            return CardType.Weapon;
        }

        /// <summary>
        /// 武器牌所能造成的伤害
        /// </summary>
        /// <returns></returns>
        public abstract float Damage(Character character);

    }

    public abstract class ArmorCard : Card {
        public override CardType GetCardType() {
            return CardType.Armor;
        }
    }

    public abstract class ElementCard : Card {
        public override CardType GetCardType() {
            return CardType.Element;
        }
    }
}