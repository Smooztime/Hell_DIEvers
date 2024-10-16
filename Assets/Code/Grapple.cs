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
    [SerializeField] Transform hookOrigin;
    [SerializeField] float distanceForBreak;
    PlayerController playerController;
    public float distanceForMovingObj;
    bool canJump = false;
    bool doRetract = false;
    ControlArm armScript;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        distanceJoint.enabled = false;
        grappleLine.enabled = false;
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        armScript = GetComponent<ControlArm>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //    ShootHook();
        //}
        //else if(Input.GetMouseButtonUp(0))
        //{
        //    LetGoOfHook();
        //}
        //if(Input.GetMouseButton(0) && Input.GetMouseButton(1))
        //{
        // PullInToHook();
        //}
        if(distanceJoint.distance >= distanceForBreak)
        {
            LetGoOfHook();
            distanceJoint.distance = 0;
        }
        grappleLine.SetPosition(1, new Vector2(hookOrigin.position.x,hookOrigin.position.y + heightOfGrappleFromPlayer));
        if (doRetract)
        {
            distanceJoint.distance -= PullInSpeed;
            distanceForMovingObj -= PullInSpeed;
        }
        TellControlArm(playerController.Data.PointingDirection);
        
    }
   public void PullInToHook()
    {
        playerController.ChangeState(PlayerStates.InAir);
        doRetract = true;
        transform.parent = null;
    }
    public void StopPullInToHook()
    {
        doRetract = false;
    }
   public void ShootHook(Vector2 mousePos)
    {
        Destroy(currentHook);
        GameObject grappleObject = Instantiate(grapplePrefab, new Vector2(hookOrigin.position.x,hookOrigin.position.y + heightOfGrappleFromPlayer),Quaternion.identity);
        armScript.pointToMouse = false;
        Physics2D.IgnoreCollision(grappleObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        AttachToGround attachToGroundScript = grappleObject.GetComponent<AttachToGround>();
        attachToGroundScript.ShootHook(mousePos);
        attachToGroundScript.GetComponent<AttachToGround>().playerObject = this.gameObject;
        attachToGroundScript.grappleScript = GetComponent<Grapple>();
        currentHook = grappleObject;
    }
    public void AttachedHook(Vector2 grapplePos)
    {
        //playerController.ChangeState(PlayerStates.InAir);
        grappleLine.SetPosition(0, grapplePos);
       armScript.SetTargetToGrapple(grapplePos);
        distanceJoint.connectedAnchor = grapplePos;
        distanceJoint.enabled = true;
        grappleLine.enabled = true;
    }
    public void AttachedHookToMovingObject(Vector2 grapplePos)
    {
        grappleLine.SetPosition(0, grapplePos);
        armScript.SetTargetToGrapple(grapplePos);
        distanceJoint.connectedAnchor = grapplePos;
        distanceJoint.distance = distanceForMovingObj;
    }
    public void LetGoOfHook()
    {
        armScript.pointToMouse = true;
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
    public void TellControlArm(Vector2 pos)
    {
        armScript.SetTargetToPoint(pos);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 6)
        {
            canJump = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 6)
        {
            canJump = true;
        }
    }
}
