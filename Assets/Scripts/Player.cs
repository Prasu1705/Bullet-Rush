using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 mouseStartPos, mouseCurrentPos, dragDirection;
    public AimManagerLeft aimManagerLeft;
    public AimManagerRight aimManagerRight;
    public GameObject nearestEnemy;
    public float speed;
    private float angle;
    private Vector3 offset;
    public float rotationSpeed =30f;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.OnClickDrag += ClickDragControls;
    }

    private void Update()
    {
        Rotate();
    }
    // Update is called once per frame
    void ClickDragControls(InputControls inputControls)
    {
        switch(inputControls)
        {
            case InputControls.CLICK:
                mouseStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseStartPos.y = transform.position.y;
                break;
            case InputControls.HOLD:
                mouseCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseCurrentPos.y = transform.position.y;
                dragDirection = mouseCurrentPos - mouseStartPos;
                PlayerManager.Instance.player.transform.position += dragDirection * Time.deltaTime * speed;
                if (dragDirection.magnitude > 0.3f)
                {
                    angle = Mathf.Atan2(dragDirection.x, dragDirection.z) * Mathf.Rad2Deg;                  
                }
                break;
            case InputControls.RELEASE:
                break;
        }
    }

    void Rotate()
    {
        if(aimManagerLeft.closestEnemy == null && aimManagerRight.closestEnemy == null)
        {
            PlayerManager.Instance.player.transform.rotation = Quaternion.Lerp(PlayerManager.Instance.player.transform.rotation, Quaternion.AngleAxis(angle, Vector3.up), Time.deltaTime * rotationSpeed);
        }
        else if(aimManagerLeft.closestEnemy != null && aimManagerRight.closestEnemy == null)
        {
            offset = aimManagerLeft.closestEnemy.transform.position - transform.position;
            angle =  Mathf.Atan2(offset.x, offset.z) * Mathf.Rad2Deg;

            if (Vector3.Distance(transform.position, aimManagerLeft.closestEnemy.transform.position) < 10)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.up), Time.deltaTime * rotationSpeed);
            }

        }
        else if (aimManagerLeft.closestEnemy == null && aimManagerRight.closestEnemy != null)
        {
            offset = aimManagerRight.closestEnemy.transform.position - transform.position;
            angle = Mathf.Atan2(offset.x, offset.z) * Mathf.Rad2Deg;

            if (Vector3.Distance(transform.position, aimManagerRight.closestEnemy.transform.position) < 10)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.up), Time.deltaTime * rotationSpeed);
            }

        }
        else
        {
            if (Vector3.Distance(transform.position, aimManagerLeft.closestEnemy.transform.position) < 20 && Vector3.Distance(transform.position, aimManagerRight.closestEnemy.transform.position) < 20)
            {

                var commonPoint = aimManagerRight.closestEnemy.transform.position + aimManagerLeft.closestEnemy.transform.position;
                commonPoint /= 2;

                offset = commonPoint - transform.position;

                angle = Mathf.Atan2(offset.x, offset.z) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.up), Time.deltaTime * rotationSpeed);

            }
        }
    }
}
