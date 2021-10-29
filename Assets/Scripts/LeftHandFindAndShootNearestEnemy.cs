using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandFindAndShootNearestEnemy : MonoBehaviour
{
    public LeftHandEnemyTarget LeftHandEnemyTarget;
    public List<GameObject> enemiesList = new List<GameObject>();
    public GameObject closestEnemy;
    private GameObject bulletObject;
    public float projectileSpeed;
    private float gravity = Physics.gravity.y;
    public float maxRange = 1000;

    public float lastfired, FireRate = 20f;

    // Start is called before the first frame update
   
    void Start()
    {
        StartCoroutine("EnemyListCreation");
        InvokeRepeating("FindClosestEnemy", 0f, 0.1f);
    }

    IEnumerator EnemyListCreation()
    {
        yield return new WaitForSeconds(2f);
        LeftHandEnemyTarget = GetComponentInChildren<LeftHandEnemyTarget>();
        Enemy[] enemiesInScene = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemiesInScene)
        {
            enemiesList.Add(enemy.gameObject);
        }
    }
    

    void FindClosestEnemy()
    {
        float range = maxRange;
        closestEnemy = null;
        foreach (GameObject enemyGameObject in enemiesList)
        {
            float dist = Vector3.Distance(enemyGameObject.transform.position, transform.position);
            if (dist < range)
            {
                range = dist;
                closestEnemy = enemyGameObject;
            }
        }
        LeftHandEnemyTarget.enemy = closestEnemy;
    }

    public void ShootAtEnemy()
    {
        if (closestEnemy != null && LeftHandEnemyTarget.canShoot == true)
        {
            if (Time.time - lastfired > 1 / FireRate)
            {
                lastfired = Time.time;
                bulletObject = BulletManager.instance.SpawnBullet();
                bulletObject.transform.position = PlayerManager.Instance.player.transform.GetChild(1).transform.GetChild(1).position;
                bulletObject.SetActive(true);
                float distance = closestEnemy.transform.position.x - bulletObject.transform.position.x;
                Vector3 directionalVector = closestEnemy.transform.position - bulletObject.transform.position;
                float theta = 0.5f * Mathf.Asin((gravity * distance) / (projectileSpeed * projectileSpeed));
                Vector3 releaseVector = (Quaternion.AngleAxis(theta * Mathf.Rad2Deg, -Vector3.forward) * directionalVector).normalized;
                bulletObject.GetComponent<Rigidbody>().velocity = releaseVector * projectileSpeed; 
            }
        }
    }
}