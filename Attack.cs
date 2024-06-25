namespace CombatGame
{
    public class Attack
    {
        public int Kick { get; set; }
        public int Spin { get; set; }
        public int WandAttack { get; set; }

        public Attack(int kick, int spin, int wandAttack)
        {
            Kick = kick;
            Spin = spin;
            WandAttack = wandAttack;
        }
    }
}