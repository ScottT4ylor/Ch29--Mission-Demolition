using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {
    static public FollowCam S;
    public int delay = 25;
    public float ease = 0.05f;
    public float velocityEase = 0.1f;
    public bool wtfIsThis2;


    private Vector3 lookAhead;
    private Vector2 minXY = new Vector2(0, 0);
    public GameObject poi;
    private float camZ;
    private Vector3 velocityOffset = Vector3.zero;
    private float groundBias;

    void Awake()
    {
        S = this;
        camZ = transform.position.z;
    }

    void FixedUpdate()
    {
        if (poi == null) return;
        if (delay > 0 && poi.transform.position.x < 0 && poi.transform.position.y < 7)
        {
            delay--;
            return;
        }
        delay = 0;
        velocityOffset.x = Mathf.Lerp(velocityOffset.x, poi.GetComponent<Rigidbody>().velocity.x*0.75f, velocityEase);
        velocityOffset.y = Mathf.Lerp(velocityOffset.y, poi.GetComponent<Rigidbody>().velocity.y*0.75f, velocityEase);
        lookAhead = poi.transform.position;
        lookAhead += velocityOffset;
        groundBias = 1 / (1 + Mathf.Exp(-0.1f * (GetComponent<Camera>().orthographicSize - 10)));
        if (groundBias < 0) groundBias = 0;
        lookAhead.y = Mathf.Lerp(lookAhead.y, (poi.transform.position.y/3)*2, groundBias);
        Vector3 destination = Vector3.Lerp(transform.position, lookAhead, ease);
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        destination.z = camZ;
        GetComponent<Camera>().orthographicSize = destination.y + 10;
        transform.position = destination;
    }

    void returnToSlingshot()
    {
        //because it has to return to the slingshot eventually.
    }
}
