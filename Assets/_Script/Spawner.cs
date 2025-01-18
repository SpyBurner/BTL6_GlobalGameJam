using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject player;

    public GameObject[] enemies;

    public float spawnDistance = 250f;
    public float despawnDistance = 500f;

    public int maxSpawnCount = 1;
    public int currentSpawnCount = 0;

    public int countOnStart = 1;

    public float spawnRate = 2f;
    public bool isOnCooldown;

    public bool stopSpawning = true;

    // Update is called once per frame
    void Update()
    {
        if (!player) player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
            return;

        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance > spawnDistance && distance < despawnDistance)
        {
            stopSpawning = false;
        }
        else
        {
            stopSpawning = true;
            if (distance > despawnDistance)
                Despawn();
        }

        currentSpawnCount = transform.childCount;
        if (isOnCooldown || stopSpawning) return;

        if (currentSpawnCount < maxSpawnCount)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    void Despawn()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

   IEnumerator SpawnEnemy(bool ignoreCooldown = false)
    {
        isOnCooldown = true;
        int randomIndex = Random.Range(0, enemies.Length);
        GameObject enemy = enemies[randomIndex];

        Bounds bounds = GetComponent<BoxCollider2D>().bounds;

        Vector2 pos = new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));

        GameObject newEnemy = Instantiate(enemy, pos, transform.rotation);

        newEnemy.transform.parent = transform;

        yield return new WaitForSeconds(1/spawnRate);
        isOnCooldown = false;
    }
}
