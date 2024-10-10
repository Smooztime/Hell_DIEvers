using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField]
    GameObject[] ropeParts;
    [SerializeField]
    HingeJoint2D[] ropeHinges;
    [SerializeField]
    Transform connectedRope;
    [SerializeField]
    Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {

    }
    public void RopeDistanceFromItself()
    {
        playerTransform = GameObject.Find("Player(Clone)").transform;
        float distanceFromPlayer;
        distanceFromPlayer = (playerTransform.position - connectedRope.position).magnitude;
        float distanceBetweenSegments;
        distanceBetweenSegments = distanceFromPlayer / ropeParts.Length;
        for(int i = 0; i < ropeParts.Length; i++)
        {
            ropeParts[i].transform.position = gameObject.transform.position;
            ropeParts[i].transform.position = Vector2.MoveTowards(ropeParts[i].transform.position,playerTransform.position,distanceBetweenSegments * i + 1);
        }
        ropeHinges[5].connectedBody = GameObject.Find("Player(Clone)").GetComponent<Rigidbody2D>();
        Debug.Log( distanceFromPlayer + " " + distanceBetweenSegments);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RopeDistanceFromItself();
        }
    }
}
