using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtEnemyRight : MonoBehaviour
{
    public GameObject enemy;
    // This is what the player is looking at. In this example it is the dinosaur's head.

    public GameObject fovStartPoint;
    // We will use the forward direction of whatever GameObject you give it.

    public float lookSpeed = 200;
    // How fast the rotation happens.

    public float maxAngle = 45;
    // The maximum fov to trigger looking at the enemy.

    public float maxAngleReset = 90;
    // The maximum fov to trigger returning to the base state.

    public bool canLean = false;
    // This turns on looking up/down depending on the enemy's height.

    public bool leftArm = false;
    public bool rightArm = false;

    private Vector3 offset;
    private float angle;

    public static bool canShoot = false;

    private bool canShootLeft = false;
    private bool canShootRight = false;

    public float lastfired, FireRate = 20f;
    public AimManagerRight aimManagerRight;


    private Quaternion lookAt;
    private Quaternion targetRotation;


    void Update()
    {
        if (rightArm)
        {
            if (canShootRight == true)
            {
                canShoot = true;


            }
            //if (canShootLeft == false && canShootRight == false)
            //{
            //    canShoot = false;
            //}
        }

        if (enemy != null && EnemyInFieldOfView(fovStartPoint))
        {
            Vector3 direction = enemy.transform.position - transform.position;
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (!canLean)
            {
                direction = new Vector3(direction.x, 0, direction.z);
            }

            // Rotate the current transform to look at the enemy
            targetRotation = Quaternion.LookRotation(direction);
            lookAt = Quaternion.RotateTowards(
            transform.rotation, targetRotation, Time.deltaTime * lookSpeed);
            transform.rotation = lookAt;

            if (leftArm)
            {
                canShootLeft = true;
                canShootRight = false;
            }
            if (rightArm)
            {
                canShootRight = true;
                canShootLeft = false;
            }
            if (Time.time - lastfired > 1 / FireRate)
            {
                lastfired = Time.time;
                if(distance < 8f)
                {
                    aimManagerRight.Shoot();
                }
                
            }


        }
        if (enemy != null && EnemyInFieldOfViewNoResetPoint(fovStartPoint))
        {
            return;
        }
        else
        {
            if (rightArm)
            {
                // make arms point at the ground
                Quaternion targetRotation = Quaternion.Euler(90, 0, 0);
                transform.localRotation = Quaternion.RotateTowards(
                transform.localRotation, targetRotation, Time.deltaTime * lookSpeed);
                if (leftArm)
                {
                    canShootLeft = false;
                }
                if (rightArm)
                {
                    canShootRight = false;
                }

            }

            else
            {
                // return to initial local angle
                Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
                transform.localRotation = Quaternion.RotateTowards(
                transform.localRotation, targetRotation, Time.deltaTime * lookSpeed);
            }
        }

        bool EnemyInFieldOfView(GameObject looker)
        {

            Vector3 targetDir = enemy.transform.position - looker.transform.position;
            // gets the direction to the enemy.

            float angle = Vector3.Angle(targetDir, looker.transform.forward);
            // gets the angle based on the direction.

            if (angle < maxAngle)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool EnemyInFieldOfViewNoResetPoint(GameObject looker)
        {
            Vector3 targetDir = enemy.transform.position - looker.transform.position;
            float angle = Vector3.Angle(targetDir, looker.transform.forward);

            if (angle < maxAngleReset)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
