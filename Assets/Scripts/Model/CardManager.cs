using System;
using System.Collections.Generic;

namespace Model {
    /// <summary>
    /// 用来加载所有卡片，因为卡片在游戏中是不变的，所以会在游戏开始时创建好所有对象
    /// </summary>
    public class CardManager {
        /// <summary>
        /// Singleton
        /// </summary>
        public static CardManager Instance {
            get {
                if (null == instance) {
                    lock (SynObject) {
                        if (null == instance) {
                            instance = new CardManager();
                        }
                    }
                }
                return instance;
            }
        }
        private static CardManager instance = null;
        private static readonly object SynObject = new Object();

        private CardManager() {
            LoadAllCard();
        }

        //Field


        private List<Card> cards = new List<Card>();
        
        
        //Method 

        /// <summary>
        /// 由id得到卡片对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Card GetCardById(int id) {
            return Instance.cards[id];
        }

        /// <summary>
        /// 由卡片名得到对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Card GetCardByName(string name) {
            var cards = Instance.cards;
            foreach (var card in cards) {
                if (card.Name == name) {
                    return card;
                }
            }

            return null;
        }


        /// <summary>
        /// 由id得到卡片类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CardType GetCardTypeById(int id) {
            return GetCardById(id).GetCardType();
        }

        /// <summary>
        /// 加载所有卡片
        /// </summary>
        private void LoadAllCard() {
            //todo: 打算使用json来存储会用到的卡片列表 然后通过反射创建对象
            cards.Add(new SwordCard());
        }

    }
}