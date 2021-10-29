using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandEnemyTarget : MonoBehaviour
{
    public GameObject enemy;
    public GameObject fovStartPoint;
    
    public float lookSpeed = 200;
    public float maxAngle = 45;
    public float maxAngleReset = 90;
   

    public bool canLean = false;
    public bool leftArm = false;
    public bool rightArm = false;

    private Vector3 offset;
    private float angle;

    public static bool canShoot = false;

    private bool canShootLeftHand = false;
    private bool canShootRightHand = false;

    public LeftHandFindAndShootNearestEnemy LeftHandFindAndShootNearestEnemy;

    public float lastfired, FireRate = 20f;


    private Quaternion lookAt;
    private Quaternion targetRotation;


    void Update()
    {
        if (leftArm)
        {
            if (canShootLeftHand)
            {
                canShoot = true;
            }
        }

        if (enemy != null && EnemyInFieldOfView(fovStartPoint))
        {
           Vector3 direction = enemy.transform.position - transform.position;
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (!canLean)
            {
                direction = new Vector3(direction.x, 0, direction.z);
            }
            targetRotation = Quaternion.LookRotation(direction);
            lookAt = Quaternion.RotateTowards(
            transform.rotation, targetRotation, Time.deltaTime * lookSpeed);
           
            transform.rotation = lookAt;
            if (leftArm)
            {
                canShootLeftHand = true;
                canShootRightHand = false;
            }
            if (rightArm)
            {
                canShootRightHand = true;
                canShootLeftHand = false;
            }
            if (Time.time - lastfired > 1 / FireRate)
            {
                lastfired = Time.time;
                if (distance < 8f)
                {
                    LeftHandFindAndShootNearestEnemy.ShootAtEnemy();
                }
            }
        }
        if (enemy != null && EnemyInFieldOfViewNoResetPoint(fovStartPoint))
        {
            return;
        }
        else
        {
            if (leftArm)
            {
                // make arms point at the ground
                Quaternion targetRotation = Quaternion.Euler(90, 0, 0);
                transform.localRotation = Quaternion.RotateTowards(
                transform.localRotation, targetRotation, Time.deltaTime * lookSpeed);
                if (leftArm)
                {
                    canShootLeftHand = false;
                }
            }
            else
            {
                Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
                transform.localRotation = Quaternion.RotateTowards(
                transform.localRotation, targetRotation, Time.deltaTime * lookSpeed);
            }
        }

        bool EnemyInFieldOfView(GameObject looker)
        {

            Vector3 targetDir = enemy.transform.position - looker.transform.position;
            float angle = Vector3.Angle(targetDir, looker.transform.forward);
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
