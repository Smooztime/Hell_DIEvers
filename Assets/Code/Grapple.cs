using Code.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] LineRenderer grappleLine;
    [SerializeField] DistanceJoint2D distanceJoint;
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject grapplePrefab;
    public GameObject currentHook;
    [SerializeField] float heightOfGrappleFromPlayer;
    [SerializeField] float LetGoForce;
    [SerializeField] float horizontalLetGoForce;
    [SerializeField] float PullInSpeed;
    bool canJump = false;
    // Start is called before the first frame update
    void Start()
    {
        distanceJoint.enabled = false;
        grappleLine.enabled = false;
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ShootHook();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            LetGoOfHook();
        }
        if(Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
         PullInToHook();
        }
        grappleLine.SetPosition(1, new Vector2(playerTransform.position.x,playerTransform.position.y + heightOfGrappleFromPlayer));
    }
    void PullInToHook()
    {
        distanceJoint.distance -= PullInSpeed;
    }
    void ShootHook()
    {
        Destroy(currentHook);
        GameObject grappleObject = Instantiate(grapplePrefab, new Vector2(this.transform.position.x,this.transform.position.y + heightOfGrappleFromPlayer),Quaternion.identity);
        Physics2D.IgnoreCollision(grappleObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        AttachToGround attachToGroundScript = grappleObject.GetComponent<AttachToGround>();
        attachToGroundScript.ShootHook((Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition));
        attachToGroundScript.GetComponent<AttachToGround>().playerObject = this.gameObject;
        attachToGroundScript.grappleScript = GetComponent<Grapple>();
        currentHook = grappleObject;
    }
    public void AttachedHook(Vector2 grapplePos)
    {
        grappleLine.SetPosition(0, grapplePos);
       
        distanceJoint.connectedAnchor = grapplePos;
        distanceJoint.enabled = true;
        grappleLine.enabled = true;
    }
    public void LetGoOfHook()
    {
        Destroy(currentHook);
        distanceJoint.enabled = false;
        grappleLine.enabled = false;
        float hlgf;
        if (GetComponent<PlayerController>().movingRight)
        {
            hlgf = horizontalLetGoForce;
        }
        else
        {
            hlgf = -horizontalLetGoForce;
        }
        if(canJump)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(hlgf, LetGoForce));
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            canJump = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            canJump = true;
        }
    }
}
