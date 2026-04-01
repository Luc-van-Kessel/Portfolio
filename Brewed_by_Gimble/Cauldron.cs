using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public List<string> Ingredients = new List<string>();
    [SerializeField] private ParticleSystem bubblesFX;
    private bool hasAddedIng;
    private void OnCollisionEnter(Collision collision)
    {
        Ingredient ingredient = collision.gameObject.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            Destroy(ingredient.gameObject);
            IngredientAddedInterval();
            if (!hasAddedIng)
            {
                Ingredients.Add(ingredient.ingredientName);
                Debug.Log($"Added ingredient: {ingredient.ingredientName}");
                bubblesFX.Play();
            }
        }
    }

    public void Combine()
    {
        if (Ingredients.Count < 2)
        {
            Debug.Log("Not enough ingredients! You need at least 2.");
            return;
        }

        Combine combiner = FindObjectOfType<Combine>();
        if (combiner != null)
        {
            //TriggerMixingEffect();
            string result = combiner.CombineIngredients(Ingredients.ToArray());
            Debug.Log($"Combination Result: {result}");
            bubblesFX.Stop();
        }
        else
        {
            Debug.LogError("Combine script not found in the scene!");
        }

        Ingredients.Clear();
    } 

    IEnumerator IngredientAddedInterval()
    {
        hasAddedIng = true;
        yield return new WaitForSeconds(0.1f);
        hasAddedIng = false;
    }
}
