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
        while (async.progress < 0.9f)
        {
            loadingBar.fillAmount = async.progress;
            loadingProgress.text = (async.progress * 100).ToString("F0") + "%";
            yield return null;            
        }
        //進行度が90%で止まってしまう仕様なのでロードが終わったら手動で100%にする
        loadingBar.fillAmount = 1.0f;
        loadingProgress.text = "100" + "%";
        yield return async;
    }
}
