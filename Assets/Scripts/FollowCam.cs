using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {
    static public FollowCam S;
    public int delay = 25;
    public float ease = 0.05f;
    public bool wtfIsThis2;


    private Vector3 lookAhead;
    private Vector2 minXY = new Vector2(0, 0);
    public GameObject poi;
    private float camZ;

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
        lookAhead = poi.transform.position;
        lookAhead.x += poi.GetComponent<Rigidbody>().velocity.x * 0.75f;
        lookAhead.y += poi.GetComponent<Rigidbody>().velocity.y * 0.5f;
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
