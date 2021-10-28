using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 mouseStartPos, mouseCurrentPos, dragDirection;

    public GameObject nearestEnemy;
    public float speed;
    private float angle;
    public float rotationSpeed =30f;
    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.OnClickDrag += ClickDragControls;
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
                    PlayerManager.Instance.player.transform.rotation = Quaternion.Lerp(PlayerManager.Instance.player.transform.rotation, Quaternion.AngleAxis(angle, Vector3.up), Time.deltaTime * rotationSpeed);
                }
                break;
            case InputControls.RELEASE:
                break;
        }
    }
}
