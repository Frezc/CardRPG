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

        //todo: 通过id获取技能对象
        //todo: 获得技能的战斗状态
    }
}