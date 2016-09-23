using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

    public GameObject projectilePrefab;
    public float velocityMult = 4f;
    public bool wtfIsThis;

    private GameObject launchPoint;
    private Vector3 launchPos;
    private GameObject projectile;
    private bool aimingMode;
    private float maxMagnitude;



    void Awake()
    {
        launchPoint = transform.Find("LaunchPoint").gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPoint.transform.position;
        maxMagnitude = gameObject.GetComponent<SphereCollider>().radius;
    }
	
	void Start () {
	
	}


    void Update()
    {
        if (!aimingMode) return;
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 mouseDelta = mousePos3D - launchPos;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta = mouseDelta.normalized * maxMagnitude;
        }
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            projectile.GetComponent<Rigidbody>().isKinematic = false;
            projectile.GetComponent<Rigidbody>().velocity = -mouseDelta * velocityMult;
            FollowCam.S.poi = projectile;
            projectile = null;
        }

    }
	

    void OnMouseEnter()
    {
        if(aimingMode == false) launchPoint.SetActive(true);
    }

    void OnMouseOver()
    {
        if (aimingMode == false) launchPoint.SetActive(true);
    }

    void OnMouseExit()
    {
        if(aimingMode == false) launchPoint.SetActive(false);
    }

    void OnMouseDown()
    {
        aimingMode = true;
        projectile = Instantiate(projectilePrefab) as GameObject;
        projectile.transform.position = launchPos;
        projectile.GetComponent<Rigidbody>().isKinematic = true;
        launchPoint.SetActive(false);
    }

}
