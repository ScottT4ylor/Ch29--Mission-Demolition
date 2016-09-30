using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileLine : MonoBehaviour
{
	static public ProjectileLine S;

	float minDist = 0.1f;
	bool wut;


	public LineRenderer line;
	private GameObject _poi;
	public List<Vector3> points;



	void Awake ()
	{
		S = this;
		line = GetComponent<LineRenderer> ();
		line.enabled = false;
		points = new List<Vector3> ();
	}

	public GameObject poi
	{
		get
		{
			return(_poi);
		}
		set
		{
			_poi = value;
			if (_poi != null)
			{
				line.enabled = false;
				points = new List<Vector3> ();
				AddPoint ();
			}
		}
	}
	public void Clear()
	{
		poi = null;
		line.enabled = false;
		points = new List<Vector3> ();
	}
	public void AddPoint()
	{
		Vector3 pt = poi.transform.position;
		if (points.Count > 0 && (pt - lastPoint.magnitude < minDist)) return;
		if (points.Count == 0)
		{
			Vector3 launchPos = Slingshot.S.launchPoint.transform.position;
			Vector3 launchPossDiff = pt - launchPos;
			points.Add (pt + launchPossDiff);
			points.Add (pt);
			lineSetVertexCount (2);
			line.SetPosition (0, points [0]);
			line.SetPosition (1, points [1]);
			ProjectileLine.enabled = true;
		}
		else
		{
			points.Add (pt);
			ProjectileLine.SetVertexCount (points.Count);
			ProjectileLine.SetPosition (points.Count - 1, lastPoint);
			ProjectileLine.enabled = true;
		}
	}
	public Vector3 lastPoint
	{
		get
		{
			if (points == null)
			{
				return(Vector3.zero);
			}
			return (points [points.Count - 1]);
		}
	}
	void FixedUpdate()
	{
		if (poi == null)
		{
			if (FollowCam.S.poi != null)
			{
				if (FollowCam.S.poi.tag == "Projectile")
				{
					poi = FollowCam.S.poi;
				}
			}
			else
			{
				return;
			}
		}
		AddPoint ();
		if (poi.GetComponent<Rigidbody>().IsSleeping ())
		{
			poi = null;
		}
	}
}
