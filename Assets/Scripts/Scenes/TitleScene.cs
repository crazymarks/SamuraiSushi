using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour {

    public GameObject flashText;
    public GameObject loadingBG;
    public Text loadingProgress;
    public Image loadingBar;
    private float nextTime;
    private float interval = 0.5f;

    private AsyncOperation async;

    void Start()
    {
        loadingBG.SetActive(false);
        nextTime = Time.time;
    }

    void Update () {

        if(Time.time > nextTime)
        {
            float textAlpha = flashText.GetComponent<CanvasRenderer>().GetAlpha();
            if(textAlpha == 1.0f)
            {
                flashText.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
            }
            else
            {
                flashText.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
            }
            nextTime += interval;
        }

        if (Input.GetMouseButtonDown(0))
        {           
            StartCoroutine("Loading");
            loadingBG.SetActive(true);
        }
	}

    private IEnumerator Loading()
    {
        async = SceneManager.LoadSceneAsync("MainGame");
        while (!async.isDone)
        {
            loadingBar.fillAmount = async.progress;
            loadingProgress.text = (async.progress * 100).ToString() + "%";
            yield return new WaitForSeconds(0);
        }
        yield return async;
    }
}
