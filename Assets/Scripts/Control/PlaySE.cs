using UnityEngine;
using System.Collections;

public class PlaySE : MonoBehaviour
{

    private AudioSource SEmaintain;
    public AudioClip fishsuccess01;
    public AudioClip fishsuccess02;
    public AudioClip fishsuccess03;
    int N;
    void Start()
        {
            //AudioSourceコンポーネントを取得し、変数に格納
            SEmaintain = GetComponent<AudioSource>();
        }

    void FishSuccess()
        {
            N = Random.Range(1, 3);
            if (N == 1)
            {
                SEmaintain.PlayOneShot(fishsuccess01);
            }
            if (N == 2)
            {
                SEmaintain.PlayOneShot(fishsuccess02);
            }
            if (N == 3)
            {
                SEmaintain.PlayOneShot(fishsuccess03);
            }
        }
    void Update()
    {
        //指定のキーが押されたら音声ファイル再生

        if (Input.GetKeyDown(KeyCode.L)) {
            FishSuccess();
        }
        
    }
    
}