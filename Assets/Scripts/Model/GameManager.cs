using UnityEngine;
using Object = System.Object;

namespace Model {
    /// <summary>
    /// 游戏数据都会保存在这
    /// </summary>
    public class GameManager {

        /// <summary>
        /// Singleton
        /// </summary>
        public static GameManager Instance {
            get {
                if (null == instance) {
                    lock (SynObject) {
                        if (null == instance) {
                            instance = new GameManager();
                        }
                    }
                }
                return instance;
            }
        }

        private static GameManager instance = null;
        private static readonly object SynObject = new Object();

        private GameManager() {}

        //Field

        public Player Player {
            get { return player; }
        }
        private Player player;


        //Method



    }
}