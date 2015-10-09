using System;
using System.Collections.Generic;
using System.Linq;

namespace Model {
    /// <summary>
    /// 保存人物的成长率
    /// </summary>
    public class Growth {
        public float StrGrowthConst;
        public float AgiGrowthConst;
        public float IntGrowthConst;
        public float StrGrowthCur;
        public float AgiGrowthCur;
        public float IntGrowthCur;
    }
    
    /// <summary>
    /// 基本角色的基类，用以Player
    /// </summary>
    public class Character {
        public static int BaseMaxHP = 10;
        public static int BaseMaxDeck = 10;
        public static int BaseMaxHand = 3;

        public int Level { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }

        /// <summary>
        /// 最大hp
        /// </summary>
        public int MaxHP {
            get { return BaseMaxHP + Strength * 3; }
        }

        /// <summary>
        /// 当前hp
        /// </summary>
        public int CurrentHP {
            get { return currentHP; }
            set {
                if (currentHP > MaxHP) {
                    currentHP = MaxHP;
                } else if (currentHP < 0) {
                    currentHP = 0;
                }
            }
        }
        private int currentHP;

        /// <summary>
        /// 卡组
        /// </summary>
        public List<int> Deck {
            get { return deck; }
        }
        private List<int> deck = new List<int>();

        /// <summary>
        /// 技能
        /// </summary>
        public List<Skill> Skills {
            get { return skills; }
        } 
        private List<Skill> skills = new List<Skill>();

        /// <summary>
        /// 手牌上限
        /// </summary>
        public int MaxHand {
            get { return BaseMaxHand + (int) Math.Floor(Math.Min(Math.Min(Strength, Agility), Intelligence) * .1); }
        }

        /// <summary>
        /// 卡组上限
        /// </summary>
        public int MaxDeck {
            get { return BaseMaxDeck + (int) Math.Floor(Intelligence * .4); }
        }

        /// <summary>
        /// 从当前的角色得到战斗状态对象
        /// </summary>
        /// <returns></returns>
        public BattleState GetBattleState() {
            return new BattleState(this);
        }
    }

    /// <summary>
    /// 保存玩家的状态，主要是加了成长等属性
    /// </summary>
    public class Player : Character {

        /// <summary>
        /// 玩家的成长率
        /// </summary>
        public Growth Growth {
            get { return growth; }
        }
        private Growth growth;

        /// <summary>
        /// 当前的经验值
        /// </summary>
        public int Experience {
            get { return experience; }
        }
        private int experience;

        /// <summary>
        /// 升级所需经验值
        /// </summary>
        public int LvlUpExp {
            get { return Level * 10; }
        }

        //todo: 升级时算出属性的随机提升，并通知ui，让玩家进行确认，如果玩家不升级则将经验退回0
    }

    /// <summary>
    /// 战斗时的状态
    /// </summary>
    public class BattleState {

        public Character Character {
            get { return character; }
        }
        private Character character;

        /// <summary>
        /// 剩余的卡组里的牌
        /// </summary>
        public List<int> Deck {
            get { return deck; }
        } 
        private List<int> deck;

        /// <summary>
        /// 手牌
        /// </summary>
        public List<int> Hands {
            get { return hands; }
        }
        private List<int> hands;

        /// <summary>
        /// 人物身上的buff
        /// </summary>
        public List<Buff> Buffs {
            get { return buffs; }
        } 
        private List<Buff> buffs = new List<Buff>();

        /// <summary>
        /// 构造函数，初始化参数
        /// </summary>
        /// <param name="character"></param>
        public BattleState(Character character) {
            this.character = character;
            deck = new List<int>(character.Deck);
        }

        /// <summary>
        /// 从deck中随机抽n张卡到hands上, 注意deck中如果没有卡片了则会返回已经取到的卡
        /// </summary>
        /// <param name="n">抽卡的数量</param>
        /// <returns></returns>
        public List<int> Draw(int n) {
            var draw = new List<int>();
            var random = new Random();
            while (n-- != 0) {
                if (deck.Count <= 0) {
                    return draw;
                } else {
                    var i = random.Next(0, deck.Count);
                    draw.Add(deck[i]);
                    deck.RemoveAt(i);
                }
            }
            return draw;
        }
    }
}