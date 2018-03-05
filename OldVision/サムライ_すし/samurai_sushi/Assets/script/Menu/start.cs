using UnityEngine;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour {

	public GameObject Ctrl;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Line")
		{
			Ctrl.SendMessage("Change_Scene");
        }

    }
}
