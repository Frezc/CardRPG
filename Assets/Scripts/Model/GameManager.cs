namespace Model {
    /// <summary>
    /// 游戏数据都会保存在这
    /// </summary>
    public class GameManager {

        public GameManager Instance {
            get { return instance; }
        }

        private GameManager instance = new GameManager();
    }
}