using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "CombinationData", menuName = "Potions/Combination")]
public class CombinationData : ScriptableObject
{
    public List<string> Ingredients; // List of ingredients required for this combination
    public string ResultName;        // Name of the resulting potion
    public PotionData Result;
}
