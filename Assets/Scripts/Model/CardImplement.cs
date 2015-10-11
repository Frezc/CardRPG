namespace Model {


    public class SwordCard : WeaponCard {

        public SwordCard() {
            id = 0;
            name = "Sword";
            description = "A normal sword";
        }

        public override void Effect() {

        }

        public override float Damage(Character character) {
            return character.Strength * 1.1f;
        }
    }
}