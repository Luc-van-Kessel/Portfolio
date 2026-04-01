using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Combine : MonoBehaviour
{
    public List<CombinationData> combinationDataList;
    public ParticleSystem CombineEffect;
    public ParticleSystem FailEffect;
    public float yOffset = 5;
    public bool CorrectCombinationResult;

    public AudioClip[] FailSounds;
    public AudioSource AudioSource;

    public AudioClip[] MixingSounds;

    public GameObject ULTIMATEpotion;

    public string CombineIngredients(params string[] ingredients)
    {
        if (ingredients.Length < 2)
        {
            return "Invalid Combination";
        }

        // Log input ingredients
        Debug.Log($"Combining ingredients: {string.Join(", ", ingredients)}");

        List<string> sortedIngredients = new List<string>(ingredients);
        sortedIngredients.Sort();
        Debug.Log($"Sorted input ingredients: {string.Join(", ", sortedIngredients)}");

        foreach (var combination in combinationDataList)
        {
            var sortedComboIngredients = combination.Ingredients.OrderBy(i => i).ToList();
            Debug.Log($"Checking combination: {string.Join(", ", sortedComboIngredients)}");

            if (sortedIngredients.SequenceEqual(sortedComboIngredients))
            {
                TriggerMixingEffect();
                CorrectCombinationResult = true;
                SpawnResult(combination);
                return combination.ResultName;
            }
        }
        TriggerFailEffect();
        CorrectCombinationResult = false;
        return "Invalid Combination";
    }

    private void SpawnResult(CombinationData combination)
    {
        // Trigger the effect of the combination
        CombineEffect.Play();

        if (combination.ResultName == "ULTIMATE POTION")
        {
            ULTIMATEpotion.SetActive(true);
        }
        // Check if there is a Result (PotionData) and instantiate its prefab
        if (combination.Result != null && combination.Result.PotionPrefab != null)
        {
            // Instantiate the resulting potion prefab
            Instantiate(combination.Result.PotionPrefab,
                        new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z),
                        Quaternion.identity);
        }

        // Log the potion creation with the potion's name
        Debug.Log($"Potion Created: {combination.ResultName}");
    }

    private void TriggerFailEffect()
    {
        // Play fail particle effect
        FailEffect.Play();

        // 50% chance to play a sound
        if (Random.value < 0.5f && FailSounds.Length > 0)
        {
            // Choose a random sound from the array
            int randomIndex = Random.Range(0, FailSounds.Length);
            AudioClip failSound = FailSounds[randomIndex];

            // Play the sound using the AudioSource
            AudioSource.PlayOneShot(failSound);

            // Log the fail sound being played
            Debug.Log($"Playing fail sound: {failSound.name}");
        }
    }

    private void TriggerMixingEffect()
    {
        // Play fail particle effect

        // 50% chance to play a sound
        if (Random.value < 0.5f && MixingSounds.Length > 0)
        {
            // Choose a random sound from the array
            int randomIndex = Random.Range(0, MixingSounds.Length);
            AudioClip failSound = MixingSounds[randomIndex];

            // Play the sound using the AudioSource
            AudioSource.PlayOneShot(failSound);

            // Log the fail sound being played
            Debug.Log($"Playing fail sound: {failSound.name}");
        }
    }
}
