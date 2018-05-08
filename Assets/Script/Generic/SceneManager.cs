using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : SingletonMonoBehaviour<SceneManager> {

    public static new SceneManager Instance
    {
        get
        {
            if(instance != null) { return instance; }
            instance = FindObjectOfType<SceneManager>();
            if(instance == null)
            {
                var obj = new GameObject(typeof(SceneManager).Name);
                instance = obj.AddComponent<SceneManager>();
            }
            return instance;
        }
    }

    public static AsyncOperation LoadSceneAsync(string sceneName)
    {
        return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
    }

    protected new void Awake()
    {
        if (CheckInstance())
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    
    //シーンを新規作成したら、ここに定数を追加しよう
    public const string MainGame = "MainGame";

    /// <summary>
    /// シーンのロードをする。
    /// ex) SceneManager.Instance.LoadScene(SceneManager.MainGame);
    /// </summary>
    /// <param name ="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
