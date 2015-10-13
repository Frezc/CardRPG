namespace Model {

    /// <summary>
    /// 战斗中的附加状态
    /// </summary>
    public abstract class Buff : ITurnBaseEvents {

        /// <summary>
        /// Buff附加者
        /// </summary>
        public Character Owner {
            get { return owner; }
        }

        protected Character owner;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name {
            get { return name; }
        }

        protected string name;

        /// <summary>
        /// 状态描述
        /// </summary>
        public string Description {
            get { return description; }
        }

        protected string description;

        /// <summary>
        /// buff是否可见
        /// </summary>
        public bool Visible {
            get { return visible; }
        }

        protected bool visible = false;


        /// <summary>
        /// 是否为固有buff
        /// </summary>
        public bool Inherent {
            get { return inherent; }
        }

        protected bool inherent = false;

        /// <summary>
        /// buff的持续时间，-1代表无限
        /// </summary>
        public int Duration {
            get { return duration; }
        }

        protected int duration = 1;

        /// <summary>
        /// 发动几率
        /// </summary>
        public float Chance {
            get { return chance; }
        }

        protected float chance = 1f;

        /// <summary>
        /// 是否为负面buff
        /// </summary>
        public bool Debuff {
            get { return debuff; }
        }

        protected bool debuff = false;


        /// <summary>
        /// 附加属性
        /// </summary>
        public bool Addition { get; set; }
        protected bool addtition = false;

        public Buff(Character character) {
            owner = character;
        }

        /// <summary>
        /// 刷新持续时间，用于重叠buff的一般情况
        /// </summary>
        /// <param name="duration">新加buff的持续时间</param>
        public virtual void RefreshDuration(int duration) {
            if (this.duration == -1) {
                return;
            }

            if (this.duration < duration || duration == -1) {
                this.duration = duration;
            }
        }

        /// <summary>
        /// 改变buff的持续时间
        /// </summary>
        /// <param name="turns">增减的持续回合数</param>
        public virtual void ChangeDuration(int turns) {
            if (duration == -1) {
                return;
            }

            duration += turns;
            if (duration < 0) {
                duration = 0;
            }
        }

        /// <summary>
        /// 更新Buff描述
        /// </summary>
        public virtual void UpdateDescription() { }
    }
}