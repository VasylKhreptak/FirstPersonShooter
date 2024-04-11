using Main.Health.Damages.Core;

namespace Main.Health.Damages
{
    public class PlayerDamage : Damage
    {
        public string Username;

        public PlayerDamage() : base(0f)
        {
            Username = string.Empty;
        }

        public PlayerDamage(string username, float value) : base(value)
        {
            Username = username;
        }
    }
}