using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    List<GameObject> spawnedEnemyList;
    public Vector3 center, size;
    public GameObject player;
    float enemyRotationSpeed = 3.0f, enemyMoveSpeed = 3.0f;
    public int enemyCount;

    private GameObject cloneEnemy;
    // Start is called before the first frame update
    void Start()
    {
            spawnedEnemyList = new List<GameObject>();
           // cloneEnemy = enemyPrefab;
            StartCoroutine(SpawnEnemy(enemyCount));
    }

    // Update is called once per frame
    IEnumerator SpawnEnemy(int count)
    {
        for (int i = 1; i <= count; i++)
        {
            Vector3 SpawnPoints = new Vector3(Random.Range(-size.x / 2, size.x / 2), enemyPrefab.transform.position.y, Random.Range(-size.z / 2, size.z / 2));
            Vector3 pos = center + SpawnPoints;
            cloneEnemy = ObjectPoolManager.PoolInstance.GetPooledObject("Enemy");
            if(cloneEnemy!=null)
            {
                cloneEnemy.transform.position = pos;
                cloneEnemy.transform.rotation = Quaternion.identity;
                cloneEnemy.SetActive(true);
            }
    
            //Debug.Log(spawnedEnemyList.Count);
            yield return null;
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
