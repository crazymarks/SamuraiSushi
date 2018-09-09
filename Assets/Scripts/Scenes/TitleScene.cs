using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class TitleScene : MonoBehaviour {

    public GameObject flashText;
    public GameObject loadingBG;
    private AudioSource mainSE;
    public AudioClip startSE;
    public Text loadingProgress;
    public Image loadingBar;
    public float nextTime;
    public float interval = 0.5f;
    public float textAlpha;
    public AsyncOperation async;
    public string dayresetnum = "1";

    public string path;


    void Start()
    {
        loadingBar.GetComponent<Image>().material.color = new Color(1, 1, 1, 1);
        loadingBG.SetActive(false);
        nextTime = Time.time;
        mainSE = GetComponent<AudioSource>();
        dayreset();
        flashText.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
    }

    void Update () {
        if (Time.time > nextTime)
        {
            textAlpha = flashText.GetComponent<CanvasRenderer>().GetAlpha();
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
            mainSE.Play();
            StartCoroutine("Loading");
            //loadingBG.SetActive(true);
        }
	}
    public void dayreset()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Application.persistentDataPath;
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            path = Application.dataPath;
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            path = Application.dataPath;
        }

        StreamWriter sw;
        FileInfo fi;
        fi = new FileInfo(path + @"\Resources\Datas\daycontroller.txt");
        sw = fi.CreateText();
        sw.WriteLine(dayresetnum);
        sw.Flush();
        sw.Close();
        sw.Dispose();

    }
    private IEnumerator Loading()
    {
        yield return new WaitForSeconds(2);
        loadingBG.SetActive(true);
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
