using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {
	
	//時間調整関数
	public float NinjaChangeTime1 = 45.0f;	//忍者データ変化時間１、以下同
    public int NinjaChangeFlag = 0;     //忍者のデータ変化回数

    public float _45sminCreateTime;
    public float _45smaxCreateTime;


    //時間を図る変数を用意
    public float CurrentTime{
		get;
		private set;
	}
    public void _45sNinjiaDataPass()
    {
        _45sminCreateTime = GameObject.Find("GameController").GetComponent<LevelReader>()._45sNinjiaMinCreateTime;
        _45smaxCreateTime = GameObject.Find("GameController").GetComponent<LevelReader>()._45sNinjiaMaxCreateTime;
    }

    public void start()
    {
        _45sNinjiaDataPass();
    }
	
	public void Update(){
		CurrentTime += Time.deltaTime;

        //特定時間になったら、データを変化させる、以下同
        if (CurrentTime >= NinjaChangeTime1){
			if(NinjaChangeFlag == 0){
				NinjaChangeFlag = 1;
				Debug.Log("忍者データ変更1");//デバッグ用
				//デフォルト初出現	45s
				//デフォルト出現間隔	min2 max4
				//デフォルト出現率	fly1.0f wall0.0f
				GameObject.Find("GameController").GetComponent<NinjaCreate>().minCreateTime= _45sminCreateTime;//忍者出現間隔変更、下同
				GameObject.Find("GameController").GetComponent<NinjaCreate>().maxCreateTime= _45smaxCreateTime;
			}
		}
    }
}
