using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Spawns enemies at random positions within a circular area around the spawner.
/// </summary>
public class EnemySpawner : MonoBehaviour
{

    [Serializable]
    public class EnemySpawnInfo
    {
        public GameObject enemyPrefab;
        public float spawnChance;
    }
    [SerializeField] private LayerMask obstacleLayers; // Specify which layers enemies should avoid
    [SerializeField] private Vector2 _spawnArea;
    [SerializeField] private GameObject _spawnEffect;
    public float SpawnTimer
    {
        get
        {
            return _spawnTimer;
        } 

        set
        {
            _spawnTimer = value;
        }
    }
       
    private float _spawnTimer;
    [SerializeField] private float maxSpawnModifier;
    [SerializeField] private Transform _player;


    public List<EnemySpawnInfo> EnemyChancesList
    {
        get
        {
            return _enemyChances;
        }

        set
        {
            _enemyChances = value;
        }
    }

    [SerializeField] private List<EnemySpawnInfo> _enemyChances;
    private float timer;


    public int MaxEnemyCount
    {
        get
        {
            return maxEnemyCount;
        }

        set
        {
            maxEnemyCount = value;
        }
    }
    private int maxEnemyCount;
    private int currentEnemyCount; 
    private void Start()
    {
        timer = _spawnTimer;
    }

    private void Update()
    { 

        // Spawn enemy if timer reaches zero or less
        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = _spawnTimer; // Reset the timer
        }
        else
        {
            timer -= Time.deltaTime; // Decrement the timer
        }
    }

    public void ResetEnemyCount()
    {
        currentEnemyCount = 0;
    }
    public void StartSpawning()
    {
        // You can remove the timer decrement and the if statement
         timer -= Time.deltaTime;
        if (timer < 0f)
        {
            SpawnEnemy();
            timer = _spawnTimer;
        }
    } 

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 center = transform.position;
        float spawnRadius = _spawnArea.x; // Assuming _spawnArea.x represents the radius of the circular spawn area
        float maxSpawnDistance = spawnRadius * maxSpawnModifier; // Use maxSpawnModifier here

        // Draw the spawn circle
        DrawCircle(center, spawnRadius);

        // Draw the outer circle for additional spawning area
        Gizmos.color = Color.red;
        DrawCircle(center, maxSpawnDistance);
    }

    private void DrawCircle(Vector3 center, float radius)
    {
        int segments = 36; // Number of segments to approximate a circle
        float angleIncrement = 360f / segments;

        // Draw a wire circle using Gizmos.DrawLine between consecutive points
        Vector3 previousPoint = center + new Vector3(radius, 0, 0);
        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleIncrement * Mathf.Deg2Rad;
            float x = center.x + Mathf.Cos(angle) * radius;
            float z = center.z + Mathf.Sin(angle) * radius;
            Vector3 currentPoint = new Vector3(x, center.y, z);
            Gizmos.DrawLine(previousPoint, currentPoint);
            previousPoint = currentPoint;
        }
    }


    private void SpawnEnemy()
    {
        if (currentEnemyCount >= maxEnemyCount)
        {
            //Debug.Log("Maximum enemy count reached. Cannot spawn more enemies."); 
            // reset current enemy count
            return;
        }

        GameObject selectedPrefab = SelectRandomPrefab();
        if (selectedPrefab != null)
        {
            Vector3 position = GenerateRandomPosition(); 
            GameObject newEnemy = Instantiate(selectedPrefab, position, Quaternion.identity);
            if (_spawnEffect != null)
            {
                ObjectPoolManager.SpawnObject(_spawnEffect, position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);
                //Instantiate(_spawnEffect, position, Quaternion.identity);
            }
            // Set any additional properties or targets for the enemy here

            // Increment current enemy count
            currentEnemyCount++;
            // Instantiate the spawn effect
           

            // Destroy the enemy after a certain time (adjust the time as needed)
        }
    }

    private GameObject SelectRandomPrefab()
    {
        List<EnemySpawnInfo> allEnemies = new List<EnemySpawnInfo>();
        allEnemies.AddRange(_enemyChances);

        float totalSpawnChance = 0f;
        foreach (var info in allEnemies)
        {
            totalSpawnChance += info.spawnChance;
        }

        float randomValue = UnityEngine.Random.Range(0f, totalSpawnChance);
        float cumulativeChance = 0f;

        foreach (var info in allEnemies)
        {
            cumulativeChance += info.spawnChance;
            if (randomValue <= cumulativeChance)
            {
                return info.enemyPrefab;
            }
        }

        // In case of failure, return null
        return null;
    }


    // In GenerateRandomPosition method
    // Avoid recursion, as it can lead to stack overflow and memory issues
    private Vector3 GenerateRandomPosition()
    {
        Vector3 position = Vector3.zero;

        // Generate random angle in radians
        float randomAngle = UnityEngine.Random.Range(0f, Mathf.PI * 2f);

        // Generate random distance from the center within maxSpawnDistance
        float maxSpawnDistance = _spawnArea.x * maxSpawnModifier; // Use maxSpawnModifier here
        float randomDistance = UnityEngine.Random.Range(_spawnArea.x, maxSpawnDistance);

        // Convert polar coordinates to Cartesian coordinates
        position.x = Mathf.Cos(randomAngle) * randomDistance;
        position.z = Mathf.Sin(randomAngle) * randomDistance;
        position.y = 0f; // You can adjust the Y position as needed

        // Optionally adjust the position based on player's position
        if (_player != null)
        {
            position += _player.position;
        }

        // Check if the position is valid
        if (!IsPositionValid(position))
        {
            // If not valid, try again 
            return GenerateRandomPosition();
        }

        return position;
    }

    // Check if the generated position is valid and not obstructed by obstacles
    private bool IsPositionValid(Vector3 position)
    {
        // Increase the radius of the sphere to cover a larger area
        float checkRadius = 2.0f; // Adjust the radius as needed

        // Check for colliders within the specified sphere
        Collider[] colliders = Physics.OverlapSphere(position, checkRadius, obstacleLayers);

        // Return true if there are no colliders (obstacles) within the sphere
        return colliders.Length == 0;
    }


}
