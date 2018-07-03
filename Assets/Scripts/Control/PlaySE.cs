using UnityEngine;
using System.Collections;

public class PlaySE : MonoBehaviour
{

    private AudioSource SEmaintain;
    public AudioClip FishSuccess01;
    public AudioClip FishSuccess02;
    public AudioClip FishSuccess03;
    public AudioClip FishFail01;
    public AudioClip FishFail02;
    public AudioClip Kill01;
    public AudioClip Kill02;
    public AudioClip Poison01;
    public AudioClip Poison02;
    public AudioClip EndByWin;
    public AudioClip EndByNinjya;
    public AudioClip EndByKiller;

    int N;
    void Start()
    {
        //AudioSourceコンポーネントを取得し、変数に格納
        SEmaintain = GetComponent<AudioSource>();
    }

    public void FishSuccess()
    {
        //切った
        N = Random.Range(1, 3);
        if (N == 1)
        {
            SEmaintain.PlayOneShot(FishSuccess01);
        }
        if (N == 2)
        {
            SEmaintain.PlayOneShot(FishSuccess02);
        }
        if (N == 3)
        {
            SEmaintain.PlayOneShot(FishSuccess03);
        }
    }
    public void FishFail()
    //切らなかった
    {
        N = Random.Range(1, 2);
        if (N == 1)
        {
            SEmaintain.PlayOneShot(FishFail01);
        }
        if (N == 2)
        {
            SEmaintain.PlayOneShot(FishFail02);
        }
    }
    public void KillPeople()
    //村人を切った
    {
       SEmaintain.PlayOneShot(Kill01);            
    }

    public void KillNinja()
    //村人を切った
    {
        SEmaintain.PlayOneShot(Kill02);
    }

    public void KillManPoison()
    {
        //毒
        SEmaintain.PlayOneShot(Poison01);
    }
    public void KillWomanPoison()
    {
        //毒
        SEmaintain.PlayOneShot(Poison02);
    }

    public void WinEnd()
    {
        SEmaintain.PlayOneShot(EndByWin);
    }
    public void NinjyaWinEnd()
    {
        SEmaintain.PlayOneShot(EndByNinjya);
    }
        public void KillerWinEnd()
    {
        SEmaintain.PlayOneShot(EndByKiller);
    }
}