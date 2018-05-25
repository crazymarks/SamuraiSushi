using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音と効果音を管理するクラス
/// </summary>

public class SoundManager : SingletonMonoBehaviour<SoundManager> {

    public AudioSource bgm_Source;
    //public AudioSource se_Source;
    private Dictionary<string, AudioClip> bgmDic; //seDic;

    private new void Awake()
    {
        if(this != Instance)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);

        bgmDic = new Dictionary<string, AudioClip>();
        //seDic = new Dictionary<string, AudioClip>();

        object[] bgmList = Resources.LoadAll("BGM");
        //object[] seList = Resources.LoadAll("SE");

        foreach(AudioClip bgm in bgmList)
        {
            bgmDic[bgm.name] = bgm;
        }

        /*foreach(AudioClip se in seList)
        {
            seDic[se.name] = se;
        }*/

    }

    private void Start()
    {
        
    }

    public void PlayBGM(string bgmName)
    {
        bgm_Source.clip = bgmDic[bgmName] as AudioClip;
        bgm_Source.Play();
    }

    /*public void PlaySE(string seName)
    {
        se_Source.clip = seDic[seName] as AudioClip;
        se_Source.Play();
    }*/
}
