using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PotionData", menuName = "Potions/Potion")]
public class PotionData : ScriptableObject
{
    public string PotionName;
    public string Description;
    public GameObject PotionPrefab;
    public List<PotionEffect> Effects;
    public bool Consumable;
}