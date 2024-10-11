using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatForm : MonoBehaviour
{
    [SerializeField] float maxRange;
    [field: SerializeField, Range(0,1)] float PointOnRange;
    [SerializeField] float movementSpeed;
    [SerializeField] bool MoveRL;
    [SerializeField] bool movingPositive;
    Transform startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float leftPos = startPos.position.x - (maxRange * PointOnRange);
        float rightPos = startPos.position.x + (maxRange - (PointOnRange * maxRange));
        float downPos = startPos.position.y - (maxRange * PointOnRange);
        float upPos = startPos.position.y + (maxRange - (PointOnRange * maxRange));
        if (movingPositive)
        {
            PointOnRange += movementSpeed * .01f;
        }
        else if (!movingPositive)
        {
            PointOnRange -= movementSpeed * .01f;
        }
        if (PointOnRange < 0 || PointOnRange > 1)
        {
            movingPositive = !movingPositive;
        }
        if (MoveRL)
        {
            transform.position =new Vector2(Mathf.Lerp(leftPos,rightPos,PointOnRange),startPos.position.y);
        }
        else
        {
            transform.position = new Vector2(startPos.position.x,Mathf.Lerp(downPos, upPos, PointOnRange));
        }
        
        
    }
}
