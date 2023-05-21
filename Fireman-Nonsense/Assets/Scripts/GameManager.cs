using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject obstacleMovablePrefab;
    [SerializeField] private GameObject obstacleStaticPrefab;

    private GameObject player;
    private GameObject[] enemy;
    private GameObject[] obstacleMovable;
    private GameObject[] obstacleStatic;
    private int sceneNumber;
    private int numberOfObstacleAndEnemies;
    private int lengthOfLevel;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        numberOfObstacleAndEnemies = (sceneNumber+2)*2;
        lengthOfLevel = 50 + 100 * (sceneNumber + 1);
        SpawnEverything();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnEverything()
    {
        float levelDivision = lengthOfLevel/(numberOfObstacleAndEnemies*3);
        for(int i = 0; i < numberOfObstacleAndEnemies; i++)
        {
            int random = Random.Range(0, 3);
            Instantiate(enemyPrefab, new Vector3(Random.Range(-5,5), 1, levelDivision * (i * 3 + (1 + random)%3 + 1)), Quaternion.identity);
            Instantiate(obstacleMovablePrefab, new Vector3(Random.Range(-5,5), 1, levelDivision * (i * 3 + (2 + random)%3 + 1)), Quaternion.identity);
            Instantiate(obstacleStaticPrefab, new Vector3(Random.Range(-5,5), 1, levelDivision * (i * 3 + (3+ random) % 3 + 1)), Quaternion.identity);
        }
    }
}
