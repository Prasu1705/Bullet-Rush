using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody bulletRigidBody;
    //private AimManagerLeft aimManagerLeft;
    //public float speed;
    
    //public float lastfired, FireRate = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //aimManagerLeft = GetComponentInChildren<AimManagerLeft>();
        bulletRigidBody = this.GetComponent<Rigidbody>();
        
    }

    private void OnEnable()
    {
        StartCoroutine("BulletPoolReturn");
    }

    private void OnDisable()
    {
        StopCoroutine("BulletPoolReturn");
    }
    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
          gameObject.SetActive(false);  
        }
    }
    IEnumerator BulletPoolReturn()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
