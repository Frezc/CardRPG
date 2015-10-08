﻿using UnityEngine;
using System.Collections;

namespace TurnBase {
    public class State {
        public string Name {
            get { return name; }
            set { name = value; }
        }

        public bool IsOver {
            get { return isOver; }
        }

        private string name;
        private bool isOver;

        /// <summary>
        /// 进入该阶段时调用的函数
        /// </summary>
        public virtual void Enter() {
            isOver = false;
        }

        /// <summary>
        /// 离开该阶段
        /// </summary>
        public virtual void Exit() {
            isOver = true;
        }

        /// <summary>
        /// 每帧调用的函数
        /// </summary>
        public virtual void Update() { }
    }
}
