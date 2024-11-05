using UnityEngine;

namespace Skyslasher.Player
{
    public class ApplyRunTimeStats : MonoBehaviour
    {
        [Header("PLAYER")]
        [Space]
        [Header("References")]
        public PlayerStats playerStats;
        public SlowMotion slowMotion;
        public MeleeDamage meleeDamage;
        public Health playerhealth;
        public NavMeshTargeting speed;

        public void UpdateSpeed(float modifier, ModifierType type)
        {
            speed.movementSpeed = ApplyModifier(speed.movementSpeed, modifier, type);
        }

        public void UpdateDamage(float modifier, ModifierType type)
        {
            meleeDamage.DamageAmount = ApplyModifier(meleeDamage.DamageAmount, modifier, type);
        }

        public void UpdateSlow(float modifier, ModifierType type)
        {
            slowMotion._slowDownAmount = ApplyModifier(slowMotion._slowDownAmount, modifier, type);
        }

        public void UpdateHealth(float modifier, ModifierType type)
        {
            playerhealth._currentHealth = ApplyModifier(playerhealth._currentHealth, modifier, type);
        }

        private float ApplyModifier(float currentValue, float modifier, ModifierType type)
        {
            switch (type)
            {
                case ModifierType.Add:
                    return currentValue + modifier;
                case ModifierType.Subtract:
                    return currentValue - modifier;
                case ModifierType.Multiply:
                    return currentValue * modifier;
                case ModifierType.Divide:
                    return currentValue / modifier;
                default:
                    return currentValue;
            }
        }
    }
}
