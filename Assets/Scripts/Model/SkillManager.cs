using System;
using System.Collections.Generic;

namespace Model {
    public class SkillManager {
        /// <summary>
        /// Singleton
        /// </summary>
        public static SkillManager Instance {
            get {
                if (null == instance) {
                    lock (SynObject) {
                        if (null == instance) {
                            instance = new SkillManager();
                        }
                    }
                }
                return instance;
            }
        }

        private static SkillManager instance = null;
        private static readonly object SynObject = new Object();

        private SkillManager() {
            LoadAllSkill();
        }




        //Field
        private List<Skill> skillList = new List<Skill>();


        //Method

        private void LoadAllSkill() {
            skillList.Add(new SAttack());
            //
            //
            skillList.Add(new SReload());

        }
        
        /// <summary>
        /// 通过id获取技能对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Skill GetSkillById(int id) {
            return Instance.skillList[id];
        }

        /// <summary>
        /// 获得技能的战斗状态
        /// </summary>
        /// <param name="character">技能所有者</param>
        /// <param name="data">技能数据</param>
        /// <returns></returns>
        public static SkillBattleState CreateSkillBattleStateByData(Character character, SkillData data) {
            var skill = GetSkillById(data.Id);
            return skill.CreateBattleState(character, data);
        }
    }
}