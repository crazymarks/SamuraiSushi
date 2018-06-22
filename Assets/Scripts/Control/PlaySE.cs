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
        N = Random.Range(1, 2);
        if (N == 1)
        {
            SEmaintain.PlayOneShot(Kill01);
        }
        if (N == 2)
        {
            SEmaintain.PlayOneShot(Kill02);
        }
       
    }
    public void KillPeoplePoison()
        {
        //毒
            SEmaintain.PlayOneShot(Poison01);
        }
}