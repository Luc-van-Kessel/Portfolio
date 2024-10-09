using System.Linq;
using UnityEngine;
using Skyslasher.Player;
using Skyslasher.Enemy;

public class EffectSystem : MonoBehaviour
{
    public Skyslasher.Player.ApplyRunTimeStats ApplyRunTimeStatsPlayer;
    public Skyslasher.Enemy.ApplyRunTimeStats ApplyRunTimeStatsEnemy;
    public PlayerStats playerStats;
    public EnemyStats[] enemies;
    public Effect[] Effects;

    [Header("Effects Buttons")]
    public EffectButton[] effectsButtons;
    public Health[] allEntities;

    void Start()
    {
        allEntities = FindObjectsOfType<Health>();
    }

    public void ResetEffects()
    {
        // reset effects for buttons
        foreach (EffectButton button in effectsButtons)
        {
            button.ResetButton();
        }
    }

    public void SetEffects()
    {
        ShuffleArray(Effects);
        for (int i = 0; i < effectsButtons.Length; i++)
        {
            if (i < Effects.Length)
            {
                effectsButtons[i].SetEffect(Effects[i]);
            }
            else
            {
                effectsButtons[i].SetEffect(null);
            }
        }
    }

    static T[] ShuffleArray<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int rnd = UnityEngine.Random.Range(i, array.Length);
            T temp = array[i];
            array[i] = array[rnd];
            array[rnd] = temp;
        }
        return array;
    }

    public void ApplyCurse(Effect curse)
    { 
        Debug.Log("Applying curse: " + curse.Name);
        foreach (CurseEffect effect in curse.effects)
        {
            switch (effect.entity)
            {
                case GameEntity.Player:
                    Debug.Log("Applying curse to player");
                    ApplyCurseEffect(effect);
                    break;
                case GameEntity.Enemy:
                    ApplyEnemyCurseEffect(effect);
                    break;
            }
        }
    }

    void ApplyCurseEffect(CurseEffect effect)
    {
        switch (effect.attribute)
        {
            case GameAttribute.PlayerHealth:
                ApplyRunTimeStatsPlayer.UpdateHealth(effect.modifier, effect.modifierType);
                break;
            case GameAttribute.Speed:
                ApplyRunTimeStatsPlayer.UpdateSpeed(effect.modifier, effect.modifierType);
                break;
            case GameAttribute.Attack:
                ApplyRunTimeStatsPlayer.UpdateDamage(effect.modifier, effect.modifierType);
                break;
            case GameAttribute.SlowLess:
                ApplyRunTimeStatsPlayer.UpdateSlow(effect.modifier, effect.modifierType);
                break;
        }
    }

    void ApplyEnemyCurseEffect(CurseEffect effect)
    {
        switch (effect.attribute)
        {
            case GameAttribute.Health:
                ApplyRunTimeStatsEnemy.UpdateHealth(effect.modifier, effect.modifierType);
                break;
            case GameAttribute.Speed:
                ApplyRunTimeStatsEnemy.UpdateSpeed(effect.modifier, effect.modifierType);
                break;
            case GameAttribute.Attack:
                ApplyRunTimeStatsEnemy.UpdateDamage(effect.modifier, effect.modifierType);
                break;
            case GameAttribute.AttackSpeed:
                ApplyRunTimeStatsEnemy.UpdateFireRate(effect.modifier, effect.modifierType);
                break;
            case GameAttribute.Points:
                ApplyRunTimeStatsEnemy.UpdatePoints(effect.modifier, effect.modifierType);
                break; 
            case GameAttribute.PlayerHealth:
                ApplyRunTimeStatsEnemy.UpdateHealth(effect.modifier, effect.modifierType);
                break;
        }
    }
}
