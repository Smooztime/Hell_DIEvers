using Code.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.U2D.IK;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ActiveRagDoll : MonoBehaviour
{
    [SerializeField] private bool isRagDoll;
    [SerializeField] private HingeJoint2D[] jointHinge;
    [SerializeField] private Rigidbody2D[] jointRB;
    [SerializeField] private Collider2D[] jointCol;
    [SerializeField] private WheelJoint2D wheelJoint;
    
    private Rigidbody2D rb;
    private Animator animator;
    private IKManager2D ikManager;
    private PlayerInputController input;
    //private WheelJoint2D wheelJoint;
    private PlayerController controller;
    private bool canControl;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        ikManager = GetComponent<IKManager2D>();
        input = GetComponent<PlayerInputController>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRagDoll)
            wheelJoint.enabled = true;
        else
            wheelJoint.enabled = false;
    }

    private void RagDollActive()
    {
        if(isRagDoll)
        {
            animator.enabled = false;
            ikManager.enabled = false;
            input.enabled = false;
            rb.freezeRotation = false;
            canControl = false;
            foreach(HingeJoint2D hinge in jointHinge)
            {
                hinge.enabled = true;
            }

            foreach(Rigidbody2D rb in jointRB)
            {
                rb.simulated = true;
            }

            foreach(Collider2D col in jointCol)
            {

                col.enabled = true;
            }
        }
        else
        {
            //jointRB[0].gameObject.transform.position = Vector2.MoveTowards(jointRB[0].gameObject.transform.position, new Vector2(jointRB[0].gameObject.transform.position.x, -0.1f), 10 * Time.deltaTime);
            animator.enabled = true;
            ikManager.enabled = true;
            input.enabled = true;
            transform.localRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
            rb.freezeRotation = true;
            foreach (HingeJoint2D hinge in jointHinge)
            {
                hinge.enabled = false;
            }

            foreach (Rigidbody2D rb in jointRB)
            {
                rb.simulated = false;
            }

            foreach (Collider2D col in jointCol)
            {

                col.enabled = false;
            }
        }
    }

    public void SetActiveRagDoll(bool value)
    {
        isRagDoll = value;
        foreach(Rigidbody2D rb in jointRB)
        {
            rb.velocity = controller.RB.velocity;
        }
        RagDollActive();
    }

    public bool IsRagDoll { get { return isRagDoll; } }

    public void IbelieveICanFly(float value)
    {
        isRagDoll = true;
        RagDollActive();
        jointRB[0].AddForce((Vector2.up + ((Vector2.right * controller.Data.MovementDirection) / 5)) * value);
    }

    public void KnockBack(float value, Vector2 pos, bool applyToAll = false)
    {
        if(!IsRagDoll)
        {
            RagDollActive();
        }
        isRagDoll = true;
            jointRB[0].AddForce((pos-(Vector2)controller.RB.position).normalized * -value);
        GetComponent<Grapple>().LetGoOfHook();
    }

    public void SetZero()
    {
        if (controller.IsGrounded)
        {
            foreach (Rigidbody2D rb in jointRB)
            {
                rb.velocity = Vector2.zero;
                rb.simulated = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            SoundManager.PlaySound(SoundType.OnGround, 0.2f);
            if(isRagDoll == true)
                controller.DelayGroundCheck();
        }
    }
}
