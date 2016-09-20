using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

    public GameObject projectilePrefab;
    public bool wtfIsThis;

    private GameObject launchPoint;
    private Vector3 launchPos;
    private GameObject projectile;
    private bool aimingMode;




    void Awake()
    {
        launchPoint = transform.Find("LaunchPoint").gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPoint.transform.position;
    }
	
	void Start () {
	
	}
	

    void OnMouseEnter()
    {
        launchPoint.SetActive(true);
    }

    void OnMouseExit()
    {
        launchPoint.SetActive(false);
    }

    void OnMouseDown()
    {
        aimingMode = true;
        projectile = Instantiate(projectilePrefab) as GameObject;
        projectile.transform.position = launchPos;
        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }

}
