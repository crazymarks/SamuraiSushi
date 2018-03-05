using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Contraller : MonoBehaviour {

	private bool Ctrl_Flag = true;  //Use to Contral katana


	private GameObject start;
	private GameObject end;
	public GameObject start_break_1;
	public GameObject start_break_2;
	public GameObject end_break_1;
	public GameObject end_break_2;
	private Vector2 Start_Postion = new Vector2(3f, -1.5f);
	private Vector2 End_Postion = new Vector2(-3f, -1.5f);

	void Change_Scene()
	{
		if (Ctrl_Flag == true) 
		{
			start = GameObject.Find ("start");
			Ctrl_Flag = false;
			Instantiate(start_break_1, Start_Postion, Quaternion.identity);
			Instantiate(start_break_2, Start_Postion, Quaternion.identity);
			Invoke ("Change_Scene_Helper", 1.7f);
			Destroy (start);
		}
	}

	void Change_Scene_Helper()
	{
		SceneManager.LoadScene (1);
	}

	void End_Game ()
	{
		if (Ctrl_Flag == true) 
		{
			end = GameObject.Find ("end");
			Ctrl_Flag = false;
			Instantiate(end_break_1, End_Postion, Quaternion.identity);
			Instantiate(end_break_2, End_Postion, Quaternion.identity);
			Invoke ("End_Game_Helper", 1.7f);
			Destroy (end);
		}

	}

	void End_Game_Helper()
	{
		#if UNITY_STANDALONE
		//Quit the application
		Application.Quit();
		#endif

		//If we are running in the editor
		#if UNITY_EDITOR
		//Stop playing the scene
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}
}
