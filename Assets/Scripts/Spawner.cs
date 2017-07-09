using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    
    public GameObject Object;
    public float SpawnPerSecond = 1f; // 0 for never again
    public int maxSpawnedOnScene = 5; //Na całej scenie naraz
    public int maxSpawnedByMe = 5; //Limit na życie tego spawnera, ile może zespawnować zanim zniknie
    public float MinDistanceFromPlayer = 5f; // Jaka odległość dzieli playera od tego spawnera by zaczął działać
    public float MaxDistanceFromPlayer = 10f; // Jaka odległość dzieli playera od tego spawnera by zaczął działać

    private int spawnedByMe = 0;
    private float timeOfNextSpawn = 0;
    private Transform player;
	
    void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

	void Update () {
        if(MaxDistanceFromPlayer < Vector2.Distance(player.position, transform.position))
        {
            //Player jest za daleko, no to nic nie rób
            return;
        }
        if (MinDistanceFromPlayer > Vector2.Distance(player.position, transform.position))
        {
            //Player jest za daleko, no to nic nie rób
            return;
        }
        if (timeOfNextSpawn<= Time.time)
        {
            if(Enemy.EnemyCount < maxSpawnedOnScene)
            {
                Instantiate(Object, transform.position, transform.rotation);
                spawnedByMe++;
            }
            if(spawnedByMe >= maxSpawnedByMe)
            {
                Destroy(gameObject);
            }
            timeOfNextSpawn = Time.time + 1 / SpawnPerSecond;
        }
	}
}
