using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AttachToGround : MonoBehaviour
{
    public Grapple grappleScript;
    Rigidbody2D grappleRigidbody;
    public GameObject playerObject;
    [SerializeField] float grappleMoveSpeed;
    bool doMoveHook = false;
    bool attachedToMovingObject = false;
    GameObject movingObject;
    Vector2 grappleTowards;
    // Start is called before the first frame update
    void Start()
    {
        grappleRigidbody = GetComponent<Rigidbody2D>();
    }
    private void Awake()
    {
        grappleRigidbody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (attachedToMovingObject)
        {
            transform.position = movingObject.transform.position;
            grappleScript.AttachedHookToMovingObject(transform.position);
        }
    }
    public void ShootHook(Vector2 mousePos)
    {
     //   doMoveHook = true;
      // grappleTowards = mousePos;
      grappleRigidbody.AddForce((mousePos-(Vector2)transform.position).normalized * grappleMoveSpeed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            grappleRigidbody.isKinematic = true;
            grappleRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            doMoveHook = false;
            grappleScript.AttachedHook((Vector2)transform.position);
        }
        if(collision.gameObject.layer == 6)
        {
            attachedToMovingObject = true;
            movingObject = collision.gameObject;
            grappleScript.AttachedHook((Vector2)transform.position);
            grappleScript.distanceForMovingObj = ((Vector2)transform.position - (Vector2)playerObject.transform.position).magnitude;
        }
    }
}
