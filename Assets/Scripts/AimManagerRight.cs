using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimManagerRight : MonoBehaviour
{
    public LookAtEnemyRight lookAtEnemyRight;
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
        StartCoroutine("EnemyList");
        InvokeRepeating("ClosestEnemy", 2f, 0.95f);

    }
    IEnumerator EnemyList()
    {
        yield return new WaitForSeconds(2f);
        lookAtEnemyRight = GetComponentInChildren<LookAtEnemyRight>();
        Enemy[] enemiesInScene = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemiesInScene)
        {
            enemiesList.Add(enemy.gameObject);
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    ClosestEnemy();

    //}

    void ClosestEnemy()
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
                PlayerManager.Instance.player.transform.rotation = Quaternion.Slerp(PlayerManager.Instance.player.transform.rotation, Quaternion.LookRotation(closestEnemy.transform.position - PlayerManager.Instance.player.transform.position), 200 * Time.deltaTime);
            }
          
        }
        lookAtEnemyRight.enemy = closestEnemy;
        //Shoot();

    }

    public void Shoot()
    {
        if (closestEnemy != null && LookAtEnemyRight.canShoot == true)
        {
            if (Time.time - lastfired > 1 / FireRate)
            {
                lastfired = Time.time;
                bulletObject = BulletManager.instance.SpawnBullet();
                bulletObject.transform.position = PlayerManager.Instance.player.transform.GetChild(2).transform.GetChild(1).position;
                bulletObject.SetActive(true);
                float distance = closestEnemy.transform.position.x - bulletObject.transform.position.x;
                Vector3 directionalVector = closestEnemy.transform.position - bulletObject.transform.position;

                float v2 = projectileSpeed * projectileSpeed;
                float v4 = v2 * v2;

                float x = closestEnemy.transform.position.x;
                float x2 = x * x;
                float y = closestEnemy.transform.position.y;

                float theta = 0.5f * Mathf.Asin((gravity * distance) / (projectileSpeed * projectileSpeed));
                Vector3 releaseVector = (Quaternion.AngleAxis(theta * Mathf.Rad2Deg, -Vector3.forward) * directionalVector).normalized;
                

               
                bulletObject.GetComponent<Rigidbody>().velocity = releaseVector * projectileSpeed;
                
            }


        }
    }
}