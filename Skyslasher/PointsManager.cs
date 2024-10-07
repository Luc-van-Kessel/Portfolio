using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PointsManager : MonoBehaviour
{
    public List<EnemyStats> enemyStatsList;

    private float totalPoints;

    public delegate void PointsChanged(float newPoints);
    public event PointsChanged OnPointsChanged;

    public void AddPoints(EnemyStats enemyStats)
    {
        totalPoints += enemyStats.points;
        OnPointsChanged?.Invoke(totalPoints);
    } 



    public int GetTotalPoints()
    {
        return Mathf.RoundToInt(totalPoints);
    }
}
