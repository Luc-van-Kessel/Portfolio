using UnityEngine;

/// <summary>
/// Create a new curse object
/// </summary>
[CreateAssetMenu(fileName = "New Effect", menuName = "Effects/Effect")]
public class Effect : ScriptableObject
{
    public string Name;
    public string description;  
    public GameObject curseIcon;

    public CurseEffect[] effects;
    public bool IsCurse; 
    public int CostInCoins;
}


[System.Serializable]
public class CurseEffect
{
    public GameEntity entity;
    public GameAttribute attribute; 
    public ModifierType modifierType;
    public float modifier;
}

public enum GameEntity
{
    Player,
    Enemy
}

public enum GameAttribute
{
    Health, 
    Speed,
    Attack,
    AttackSpeed,
    SlowLess,
    Points,
    PlayerHealth,
    FireRate


}

public enum ModifierType
{
    Add,
    Subtract,
    Multiply,
    Divide,
    AdditiveMultiplier
}

