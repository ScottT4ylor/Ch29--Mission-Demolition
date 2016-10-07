using UnityEngine;
using System.Collections;

public class MissionDemolition : MonoBehaviour
{
	public enum GameMode
	{
		idle,
		playing,
		levelEnd
	}

	public static MissionDemolition S;

	public GameObject[] castles;
	public GameObject UILevel;
	public GameObject UIScore;
	public Vector3 castlePos;
	public bool wtfIsThis3;

	public int level;
	public int levelMax;
	public int shotsTaken;
	public GameObject castle;
	public GameMode mode = GameMode.idle;
	public string showing = "Slingshot";

	void Start()
	{
		S = this;

		level = 0;
		levelMax = castles.Length;
		StartLevel();
	}


	void StartLevel()
	{
		if(castle !=null)
		{
			Destroy(castle);
		}

		GameObject[]gos = GameObject.FindGameObjectsWithTag("Projectile");
		foreach(GameObject pTemp in gos)
		{
			Destroy(pTemp);
		}

		castle = Instantiate(castles[level]) as GameObject;
		castle.transform.position = castlePos;
		shotsTaken = 0;

		SwitchView("Both");
		ProjectileLine.S.Clear();
		Goal.goalMet = false;

		ShowGT();
		mode = GameMode.playing;
	}


	void ShowGT()
	{
		UILevel.GetComponent<Text>().text = "Level: "+level+1+" of "+levelMax;
		UIScore.GetComponent<Text>().text = "Shots Taken: "+shotsTaken;
	}


	void Update()
	{
		ShowGT();

		if (mode ==GameMode.playing && Goal.goalMet)
		{
			mode = GameMode.levelEnd;
			SwitchView("Both");
			Invoke("NextLevel", 2f);
		}
	}


	void NextLevel()
	{
		level++;
		if (level == levelMax)
		{
			level = 0;
		}
		StartLevel();
	}


	void OnGUI()
	{
		//Need to write up all of this from scratch at some point because they changed the way the UI works.
	}

	public static void SwitchView(string eView)
	{
		S.showing = eView;
		switch(S.showing)
		{
		case "Slingshot":
				FollowCam.S.poi = null;
				break;
			case "Castle":
				FollowCam.S.poi = S.castle;
				break;
			case "Both":
				FollowCam.S.poi = GameObject.Find("ViewBoth");
				break;
		}
	}


	public static void ShotFired()
	{
		S.shotsTaken++;
	}
}