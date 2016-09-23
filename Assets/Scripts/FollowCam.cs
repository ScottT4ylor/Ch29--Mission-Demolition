using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {
    static public FollowCam S;

    public bool wtfIsThis2;


    public GameObject poi;
    public float camZ;

    void Awake()
    {
        S = this;
        camZ = transform.position.z;
    }

    void Update()
    {
        if (poi == null) return;

        Vector3 destination = poi.transform.position;
        destination.z = camZ;
        transform.position = destination;
    }
}
