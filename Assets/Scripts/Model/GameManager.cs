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

        /// <summary>
        /// 保存当前的玩家对象
        /// </summary>
        public void SavePlayer() {
            if (player == null) {
                return;
            }
            PlayerPrefs.SetString("Player", player.GetPlayerDataJson());
        }

        /// <summary>
        /// 加载保存的玩家对象
        /// </summary>
        /// <returns>加载成功的对象，null为失败了</returns>
        public Player LoadPlayer() {
            if (PlayerPrefs.HasKey("Player")) {
                var json = PlayerPrefs.GetString("Player");
                player = Player.LoadFromJson(json);
                return player;
            } else {
                return null;
            }
        }

        /// <summary>
        /// 删除玩家数据
        /// </summary>
        public void DeletePlayer() {
            PlayerPrefs.DeleteKey("Player");
        }

    }
}