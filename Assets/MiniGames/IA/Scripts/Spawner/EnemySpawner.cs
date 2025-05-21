using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnRate;
    [SerializeField] GameObject[] enemyPrefab;
    [SerializeField] bool canSpawn = true;



    private void Start()
    {
        
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {

        int i = 0;
        while (i < 10) 
        {
            spawnRate = 2f;
            WaitForSeconds wait = new WaitForSeconds(spawnRate);
            yield return wait; 
            int random  = Random.Range(0,enemyPrefab.Length);
            GameObject enemyToSpawn = enemyPrefab[random];
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            i++;
        }
        while (i < 20)
        {
            spawnRate = 1f;
            WaitForSeconds wait = new WaitForSeconds(spawnRate);
            yield return wait;
            int random = Random.Range(0, enemyPrefab.Length);
            GameObject enemyToSpawn = enemyPrefab[random];
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            i++;
        }

    }
}
