using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class spawnController : MonoBehaviour
{
    [SerializeField] public List<List<int>> enemyListtest = new List<List<int>>();
    [SerializeField] List<GameObject> enemyList = new List<GameObject>();
    [SerializeField] GameObject[][] test;
    [SerializeField] int[,] array;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Spawn()
    {
        Vector3 spawnDirection = Random.insideUnitCircle.normalized * 30f;
        Vector3 newSpawnArea = new Vector3(spawnDirection.x, 0, spawnDirection.y);
        Vector3 spawnPoint = transform.position + newSpawnArea;
        Quaternion newRotation = Quaternion.Euler(0, Random.Range(0, 360),0);
        int randomIndex = Random.Range(0, 100);
        int level = PlayerPrefs.GetInt("Level");
        switch (level)
        {
            case < 16:
                if (randomIndex < 90)
                {

                    GameObject enemySpawn = Instantiate(enemyList[0], spawnPoint, newRotation);
                }
                else
                {
                    GameObject enemySpawn = Instantiate(enemyList[1], spawnPoint, newRotation);
                }
                break;
            case < 18:
                if (randomIndex < 50)
                {
                    GameObject enemySpawn = Instantiate(enemyList[1], spawnPoint, newRotation);
                }
                else if (randomIndex < 90)
                {
                    GameObject enemySpawn = Instantiate(enemyList[2], spawnPoint, newRotation);
                }
                else
                {
                    GameObject enemySpawn = Instantiate(enemyList[3], spawnPoint, newRotation);
                }
                break;
            case < 20:
                if (randomIndex < 20)
                {
                    GameObject enemySpawn = Instantiate(enemyList[0], spawnPoint, newRotation);
                }
                else if (randomIndex < 60)
                {
                    GameObject enemySpawn = Instantiate(enemyList[1], spawnPoint, newRotation);
                }
                else if (randomIndex < 90)
                {
                    GameObject enemySpawn = Instantiate(enemyList[2], spawnPoint, newRotation);
                }
                else
                {
                    GameObject enemySpawn = Instantiate(enemyList[3], spawnPoint, newRotation);
                }
                break;
            case < 22:
                if (randomIndex < 5)
                {
                    GameObject enemySpawn = Instantiate(enemyList[0], spawnPoint, newRotation);
                }
                else if (randomIndex < 15)
                {
                    GameObject enemySpawn = Instantiate(enemyList[1], spawnPoint, newRotation);
                }
                else if (randomIndex < 35)
                {
                    GameObject enemySpawn = Instantiate(enemyList[2], spawnPoint, newRotation);
                }
                else
                {
                    GameObject enemySpawn = Instantiate(enemyList[3], spawnPoint, newRotation);
                }
                break;
            case < 24:
                if (randomIndex < 5)
                {
                    GameObject enemySpawn = Instantiate(enemyList[1], spawnPoint, newRotation);
                }
                else if (randomIndex < 15)
                {
                    GameObject enemySpawn = Instantiate(enemyList[2], spawnPoint, newRotation);
                }
                else if (randomIndex < 35)
                {
                    GameObject enemySpawn = Instantiate(enemyList[3], spawnPoint, newRotation);
                }
                else
                {
                    GameObject enemySpawn = Instantiate(enemyList[4], spawnPoint, newRotation);
                }
                break;
            case < 28:
                if (randomIndex < 5)
                {
                    GameObject enemySpawn = Instantiate(enemyList[2], spawnPoint, newRotation);
                }
                else if (randomIndex < 15)
                {
                    GameObject enemySpawn = Instantiate(enemyList[3], spawnPoint, newRotation);
                }
                else if (randomIndex < 35)
                {
                    GameObject enemySpawn = Instantiate(enemyList[4], spawnPoint, newRotation);
                }
                else
                {
                    GameObject enemySpawn = Instantiate(enemyList[5], spawnPoint, newRotation);
                }
                break;
            case < 30:
                if (randomIndex < 5)
                {
                    GameObject enemySpawn = Instantiate(enemyList[3], spawnPoint, newRotation);
                }
                else if (randomIndex < 15)
                {
                    GameObject enemySpawn = Instantiate(enemyList[4], spawnPoint, newRotation);
                }
                else if (randomIndex < 35)
                {
                    GameObject enemySpawn = Instantiate(enemyList[5], spawnPoint, newRotation);
                }
                else
                {
                    GameObject enemySpawn = Instantiate(enemyList[6], spawnPoint, newRotation);
                }
                break;
            case > 32:
                if (randomIndex < 5)
                {
                    GameObject enemySpawn = Instantiate(enemyList[9], spawnPoint, newRotation);
                }
                else if (randomIndex < 15)
                {
                    GameObject enemySpawn = Instantiate(enemyList[9], spawnPoint, newRotation);
                }
                else if (randomIndex < 35)
                {
                    GameObject enemySpawn = Instantiate(enemyList[9], spawnPoint, newRotation);
                }
                else
                {
                    GameObject enemySpawn = Instantiate(enemyList[9], spawnPoint, newRotation);
                }
                break;
            default:
                break;
        }
    }
}
