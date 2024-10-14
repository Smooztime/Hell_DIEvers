using Code.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ControlArm : MonoBehaviour
{
    [SerializeField]
    Transform armTransform;
    Vector3 armPosition;
    [SerializeField]
    Transform armHolder;
    Vector3 targetToPoint;
    bool pointAtMouse;
    [SerializeField]
    float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //targetToPoint = armTransform.position;
        //armPosition = armHolder.position;
    }
    public void SetTargetToPoint(Vector2 pos)
    {
       targetToPoint = pos;
    }

    // Update is called once per frame
    void Update()
    {
        //armPosition = armHolder.position;
        Vector3 aimDirection = targetToPoint - GetComponent<PlayerController>().RB.transform.position;
        aimDirection.Normalize();
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
       // Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
     //   Debug.Log("Angle " + angle + " " + targetRotation);

        armTransform.eulerAngles =new Vector3(0,0,angle + 90);
        //armTransform.position = armPosition;
    }
}
