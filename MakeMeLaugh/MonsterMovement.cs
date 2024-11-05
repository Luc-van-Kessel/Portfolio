using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    /// <summary>
    /// Used to move the monster to an assigned place
    /// </summary>
    public Transform[] spawnpoints;
    public GameObject[] monsterPrefabs; 
    private int highestSpawnPointIndex = -1; 
    private GameObject currentMonster; 
    
    public GameObject timelineActivatedObject; 
    private bool hasDestroyedTimelineObject = false; 

    private void Start()
    {
        ResultManager.OnPointLost += ReactToPointLoss;
    }

    void ReactToPointLoss()
    {
        StartCoroutine(Delay());
    }

    /// <summary>
    /// Delay the monster movement to allow the light flicker to finish
    /// </summary>
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.25f);
        MoveMonster();
    }

    /// <summary>
    /// Spawns a new monster at a designated spawn point based on the player's negative score
    /// </summary>
    void MoveMonster()
    {
        if (ResultManager.instance.score < 0 && Mathf.Abs(ResultManager.instance.score) <= spawnpoints.Length)
        {
            int targetSpawnPointIndex = Mathf.Abs(ResultManager.instance.score) - 1;

            if (targetSpawnPointIndex > highestSpawnPointIndex)
            {
                if (currentMonster != null)
                {
                    Destroy(currentMonster);
                }

                Vector3 spawnPosition = spawnpoints[targetSpawnPointIndex].position;
                Quaternion spawnRotation = spawnpoints[targetSpawnPointIndex].rotation;

                currentMonster = Instantiate(monsterPrefabs[targetSpawnPointIndex], spawnPosition, spawnRotation);

                highestSpawnPointIndex = targetSpawnPointIndex;

                // If the timeline-activated object exists and hasn't been destroyed yet, destroy it and mark it as destroyed
                if (timelineActivatedObject != null && !hasDestroyedTimelineObject)
                {
                    Destroy(timelineActivatedObject);
                    hasDestroyedTimelineObject = true;
                }
            }
        }
    }
}
