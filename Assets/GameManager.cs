using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Vector3> spawnPoints;
    [SerializeField] private List<Vector3> excludeList;
    [SerializeField] private GameObject enemy;
    // Start is called before the first frame update
    public static Action<Transform> EnemySpawned;
    void Start()
    {
          for (int i = 1; i < 9; i++)
        {
            for (int k = 1; k < 8; k++)
            {
                spawnPoints.Add(new Vector3(i,0.5f,k));
            }
        }
        spawnPoints=spawnPoints.Except(excludeList).ToList();
        
        SpawnEnemy();

        print(excludeList.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnEnemy()
    {
     GameObject enemyInstance= Instantiate(enemy);
     enemyInstance.transform.position=spawnPoints[UnityEngine.Random.Range(0,spawnPoints.Count)];
     EnemySpawned?.Invoke(enemyInstance.transform);

    }
}
