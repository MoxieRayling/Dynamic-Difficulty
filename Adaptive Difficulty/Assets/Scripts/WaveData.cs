using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveData 
{
    private List<EnemyData> enemies;
    public List<EnemyData> Enemies { get => enemies; set => enemies = value; }

    public WaveData(List<EnemyData> enemies)
    {
        Enemies = enemies;
    }

}
