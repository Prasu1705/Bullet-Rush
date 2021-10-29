using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    private GameObject DestroyEffect;
    private LeftHandFindAndShootNearestEnemy LeftHandFindAndShootNearestEnemy;
    private RightHandFindAndShootNearestEnemy RightHandFindAndShootNearestEnemy;
    float enemyRotationSpeed = 1.7f, enemyMoveSpeed = 1.7f;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.Instance.player;
        LeftHandFindAndShootNearestEnemy = FindObjectOfType<LeftHandFindAndShootNearestEnemy>();
        RightHandFindAndShootNearestEnemy = FindObjectOfType<RightHandFindAndShootNearestEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Enemy"))
        {

            EnemyRotation();
            EnemyMove();
         
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("bullet"))
        {
            EnemyKill();
        }
    }

    void EnemyRotation()
    {
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(PlayerManager.Instance.player.transform.position - gameObject.transform.position), enemyRotationSpeed * Time.deltaTime);
    }

    void EnemyMove()
    {
        gameObject.transform.position += gameObject.transform.forward * enemyMoveSpeed * Time.deltaTime;
    }

    void EnemyKill()
    {
        LeftHandFindAndShootNearestEnemy.enemiesList.Remove(gameObject);
        RightHandFindAndShootNearestEnemy.enemiesList.Remove(gameObject);
        gameObject.SetActive(false);
    }
}
