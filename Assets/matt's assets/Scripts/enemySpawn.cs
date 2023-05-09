using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    public GameObject slimes;
    private GameObject newInstance;

    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    public GameObject spawnPoint3;
    public GameObject spawnPoint4;

    Vector3 spawnLocation1;
    bool canSpawn;

    void Update()
    {
        StartCoroutine(SpawnEnemy1());

        StartCoroutine(SpawnEnemy2());
        
        StartCoroutine(SpawnEnemy3());
    }

    
    private void SpawnEnemies1()
    {
        Vector3 spawnLocation1 = new Vector3(spawnPoint1.transform.position.x, spawnPoint1.transform.position.y, spawnPoint1.transform.position.z);
        Vector3 spawnLocation2 = new Vector3(spawnPoint2.transform.position.x, spawnPoint2.transform.position.y, spawnPoint2.transform.position.z);
        Vector3 spawnLocation3 = new Vector3(spawnPoint3.transform.position.x, spawnPoint3.transform.position.y, spawnPoint3.transform.position.z);
        Vector3 spawnLocation4 = new Vector3(spawnPoint4.transform.position.x, spawnPoint4.transform.position.y, spawnPoint4.transform.position.z);

        newInstance = Instantiate(slimes, spawnLocation1, Quaternion.identity);
    }

    private void SpawnEnemies2()
    {
        Vector3 spawnLocation1 = new Vector3(spawnPoint1.transform.position.x, spawnPoint1.transform.position.y, spawnPoint1.transform.position.z);
        Vector3 spawnLocation2 = new Vector3(spawnPoint2.transform.position.x, spawnPoint2.transform.position.y, spawnPoint2.transform.position.z);
        Vector3 spawnLocation3 = new Vector3(spawnPoint3.transform.position.x, spawnPoint3.transform.position.y, spawnPoint3.transform.position.z);
        Vector3 spawnLocation4 = new Vector3(spawnPoint4.transform.position.x, spawnPoint4.transform.position.y, spawnPoint4.transform.position.z);

        newInstance = Instantiate(slimes, spawnLocation1, Quaternion.identity);
        newInstance = Instantiate(slimes, spawnLocation2, Quaternion.identity);
    }

    private void SpawnEnemies3()
    {
        Vector3 spawnLocation1 = new Vector3(spawnPoint1.transform.position.x, spawnPoint1.transform.position.y, spawnPoint1.transform.position.z);
        Vector3 spawnLocation2 = new Vector3(spawnPoint2.transform.position.x, spawnPoint2.transform.position.y, spawnPoint2.transform.position.z);
        Vector3 spawnLocation3 = new Vector3(spawnPoint3.transform.position.x, spawnPoint3.transform.position.y, spawnPoint3.transform.position.z);
        Vector3 spawnLocation4 = new Vector3(spawnPoint4.transform.position.x, spawnPoint4.transform.position.y, spawnPoint4.transform.position.z);

        newInstance = Instantiate(slimes, spawnLocation1, Quaternion.identity);
        newInstance = Instantiate(slimes, spawnLocation2, Quaternion.identity);
        newInstance = Instantiate(slimes, spawnLocation3, Quaternion.identity);
        newInstance = Instantiate(slimes, spawnLocation4, Quaternion.identity);
    }

    private void DestroyEnemies()
    {
        Destroy(newInstance);
    }

    IEnumerator SpawnEnemy1()
    {
        canSpawn = true;
        if (canSpawn)
        {
            SpawnEnemies1();
            canSpawn = false;   
        }
        
        yield return new WaitForSeconds(4);
        DestroyEnemies();
    }
    IEnumerator SpawnEnemy2()
    {
        canSpawn = true;
        if (canSpawn)
        {
            slimes.transform.localScale = new Vector3(1, 1, 1);
            SpawnEnemies2();
            canSpawn = false;
        }

        yield return new WaitForSeconds(4);
        DestroyEnemies();
    }
    IEnumerator SpawnEnemy3()
    {
        canSpawn = true;
        if (canSpawn)
        {
            slimes.transform.localScale = new Vector3(.5f, .5f, .5f);
            SpawnEnemies3();
            canSpawn = false;
        }

        yield return new WaitForSeconds(4);
        DestroyEnemies();
    }
}
