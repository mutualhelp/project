using UnityEngine;
using System.Collections;

public class Demo : MonoBehaviour {
	private Rect drawArea;
	
	private string message;
	private string newGestureName = "";
	// Use this for initialization
	void Start () {
		drawArea = new Rect(0, 0, Screen.width - Screen.width / 3, Screen.height);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnGUI(){
		GUI.Box(drawArea, "Draw Area");
		
		GUI.Label(new Rect(10, Screen.height - 40, 500, 50), message);
		
		if (GUI.Button (new Rect (Screen.width - 100, 10, 100, 30), "Recognize")) {
		}
		GUI.Label(new Rect(Screen.width - 200, 150, 70, 30), "Add as: ");
		newGestureName = GUI.TextField(new Rect(Screen.width - 150, 150, 100, 30), newGestureName);
		
		if (GUI.Button (new Rect (Screen.width - 50, 150, 50, 30), "Add")  && newGestureName != "") {
		}
	}
}