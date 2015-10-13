using System;
using System.Collections.Generic;
using System.Linq;
using LitJson;

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
        protected int currentHP;

        /// <summary>
        /// 卡组
        /// </summary>
        public int[] Deck {
            get { return deck.ToArray(); }
        }
        protected List<int> deck = new List<int>();

        /// <summary>
        /// 技能
        /// </summary>
        public SkillData[] Skills {
            get { return skills.ToArray(); }
        }
        protected List<SkillData> skills = new List<SkillData>();
        
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
        /// 玩家的姓名
        /// </summary>
        public string Name {
            get { return name; }
        }
        private string name;

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

        /// <summary>
        /// 得到当前玩家的数据包
        /// </summary>
        /// <returns></returns>
        public PlayerData GetPlayerData() {
            
            var playerData = new PlayerData {
                Name = name,
                Growth = growth,
                Experience = experience,
                Level = Level,
                Strength = Strength,
                Agility = Agility,
                Intelligence = Intelligence,
                CurrentHP = currentHP,
                Deck = deck,
                Skills = skills
            };

            return playerData;
        }


        /// <summary>
        /// 得到当前玩家数据的json字符串格式
        /// </summary>
        /// <returns></returns>
        public string GetPlayerDataJson() {
            return JsonMapper.ToJson(GetPlayerData());
        }

        /// <summary>
        /// 从数据生成玩家对象
        /// </summary>
        /// <param name="data">数据类</param>
        /// <returns></returns>
        public static Player LoadFromData(PlayerData data) {
            var player = new Player();
            player.name = data.Name;
            player.growth = data.Growth;
            player.experience = data.Experience;
            player.Level = data.Level;
            player.Strength = data.Strength;
            player.Agility = data.Agility;
            player.Intelligence = data.Intelligence;
            player.currentHP = data.CurrentHP;
            player.deck = data.Deck;
            player.skills = data.Skills;

            return player;
        }

        /// <summary>
        /// 从json数据生成玩家对象
        /// </summary>
        /// <param name="json">Json格式数据</param>
        /// <returns></returns>
        public static Player LoadFromJson(string json) {
            PlayerData data = JsonMapper.ToObject<PlayerData>(json);

            return LoadFromData(data);
        }


        //todo: 升级时算出属性的随机提升，并通知ui，让玩家进行确认，如果玩家不升级则将经验退回0
    }


    /// <summary>
    /// 保存玩家数据的类
    /// </summary>
    public class PlayerData {
        public string Name { get; set; }
        public Growth Growth { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int CurrentHP { get; set; }
        public List<int> Deck { get; set; }
        public List<SkillData> Skills { get; set; }
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
        public int[] Hands {
            get { return hands.ToArray(); }
        }
        private List<int> hands;

        /// <summary>
        /// 人物身上的buff
        /// </summary>
        public Buff[] Buffs {
            get { return buffs.ToArray(); }
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