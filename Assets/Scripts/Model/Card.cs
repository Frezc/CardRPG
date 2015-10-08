namespace Model {
    public class Card {
        public int Id {
            get { return id; }
        }

        public string Name {
            get { return name; }
        }

        protected int id = 0;
        protected string name = "undefined";

        public virtual void Effect() { }
    }

    public class WeaponCard : Card {

        public WeaponCard() {
            this.id = 0;
            this.name = "Attack";
        }
    }
}