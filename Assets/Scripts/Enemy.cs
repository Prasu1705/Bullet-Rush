using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    private GameObject DestroyEffect;
    private AimManagerLeft aimManagerLeft;
    private AimManagerRight aimManagerRight;
    float enemyRotationSpeed = 1.5f, enemyMoveSpeed = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.Instance.player;
        aimManagerLeft = FindObjectOfType<AimManagerLeft>();
        aimManagerRight = FindObjectOfType<AimManagerRight>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("Enemy"))
        {
           
                gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(PlayerManager.Instance.player.transform.position - gameObject.transform.position), enemyRotationSpeed * Time.deltaTime);
                gameObject.transform.position += gameObject.transform.forward * enemyMoveSpeed * Time.deltaTime;
         
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("bullet"))
        {
            //DestroyEffect = ObjectPoolManager.PoolInstance.GetPooledObject("ParticleEffect");
            // DestroyEffect.transform.position = gameObject.transform.position;
            aimManagerLeft.enemiesList.Remove(gameObject);
            aimManagerRight.enemiesList.Remove(gameObject);
            gameObject.SetActive(false);
            //DestroyEffect.SetActive(true);
      
            //aimManager.enemiesList.Remove(gameObject);
        }
    }
}
