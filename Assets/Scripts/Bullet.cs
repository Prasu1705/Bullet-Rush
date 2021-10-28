using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody bulletRigidBody;
    //private AimManagerLeft aimManagerLeft;
    public float speed;
    
    public float lastfired, FireRate = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //aimManagerLeft = GetComponentInChildren<AimManagerLeft>();
        bulletRigidBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position += this.transform.forward * speed;

        //if (Time.time - lastfired > 1 / FireRate)
        //{
        //    lastfired = Time.time;
            
        //    gameObject.SetActive(false);
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {
          
            gameObject.SetActive(false);
            
        }
        else
        {
            //gameObject.SetActive(false);
        }
    }
}
