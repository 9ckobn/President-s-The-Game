namespace EffectSystem
{
    public class ApplyAttackEffect : ApplyEffect
    {
        private AttackEffect effect;

        public override void Apply(Effect currentEffect)
        {
            effect = currentEffect as AttackEffect;

            if (effect.TypeSelectTarget == TypeSelectTarget.Game)
            {
                GameSelectTarget();
            }
            else if (effect.TypeSelectTarget == TypeSelectTarget.Player)
            {
                PlayerSelectTarget();
            }
        }

        private void GameSelectTarget()
        {

        }

        private void PlayerSelectTarget()
        {

        }
    }
}