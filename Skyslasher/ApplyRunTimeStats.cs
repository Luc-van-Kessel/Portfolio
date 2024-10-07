using System.Collections;
using System.Collections.Generic;
using Skyslasher.Enemies.Speed;
using Unity.VisualScripting;
using UnityEngine;

namespace Skyslasher.Enemy
{
    public class ApplyRunTimeStats : MonoBehaviour
    {
        [Header("ENEMIES")]
        [Space]
        [Header("References")]

        // enemy enemy stats array
        public EnemyStats[] enemyStats;



        //private float speedMultiplier = 1f;
        //private float fireRateMultiplier = 1f;
        private float damageMultiplier = 1f;
        //private float healthMultiplier = 1f;
        private float pointsMultiplier = 1f; 
        
        public void UpdateSpeed(float modifier, ModifierType type)
        {
            // change speed of all enemies 
            foreach (var enemy in enemyStats)
            { 
                enemy.speed = ApplyModifier(enemy.speed, modifier, type);
            } 
            // Additional logic if needed
        }

        public void UpdateDamage(float modifier, ModifierType type)
        {
            //damageMultiplier = ApplyModifier(damageMultiplier, modifier, type);
            foreach (var enemy in enemyStats)
            {
                enemy.damage = ApplyModifier(enemy.damage, modifier, type);
            }
            Debug.Log("Player Damage Multiplier updated to: " + damageMultiplier);
            // Additional logic if needed
        } 
        
        public void UpdateFireRate(float modifier, ModifierType type)
        {
            foreach (var enemy in enemyStats)
            {
                enemy.fireRate = ApplyModifier(enemy.fireRate, modifier, type);
            }
        }

        public void UpdateHealth(float modifier, ModifierType type)
        {
            foreach (var enemy in enemyStats)
            {
                enemy.health = ApplyModifier(enemy.health, modifier, type);
            } 
        }

        public void UpdatePoints(float modifier, ModifierType type)
        {
            foreach (var enemy in enemyStats)
            {
                enemy.points = ApplyModifier(enemy.points, modifier, type);
            }

            pointsMultiplier = ApplyModifier(pointsMultiplier, modifier, type);
            Debug.Log("Player Points Multiplier updated to: " + pointsMultiplier);
        } 
        

        private float ApplyModifier(float currentValue , float modifier, ModifierType type)
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
                case ModifierType.AdditiveMultiplier:
                    // Apply the additive multiplier logic
                    return currentValue * (1 + (modifier - 1));
                default:
                    return currentValue;
            }
        }
    }
}
