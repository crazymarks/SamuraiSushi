using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {
	
	//時間調整関数
	public float FishChangeTime = 5.0f;		//
	public int FishChangeFlag = 0;		//魚のデータ変化回数
	public float PeopleChangeTime = 10.0f;	//
	public int PeopleChangeFlag = 0;		//お客のデータ変化回数
	public float NinjaChangeTime1 = 50.0f;	//忍者データ変化時間１、以下同
	public float NinjaChangeTime2 = 70.0f;	//２
	public float NinjaChangeTime3 = 90.0f;	//３
	public int NinjaChangeFlag = 0;		//忍者のデータ変化回数
	
	//時間を図る変数を用意
	public float CurrentTime{
		get;
		private set;
	}

	
	public void Update(){
		CurrentTime += Time.deltaTime;
		
		if (CurrentTime >= FishChangeTime){
			if(FishChangeFlag == 0){
				FishChangeFlag = 1;
				Debug.Log("魚データ変更");//デバッグ用
			}
		}
		
		if (CurrentTime >= PeopleChangeTime){
			if(PeopleChangeFlag == 0){
				PeopleChangeFlag = 1;
				Debug.Log("お客データ変更");//デバッグ用
			}
		}
		
		//特定時間になったら、データを変化させる、以下同
		if (CurrentTime >= NinjaChangeTime1){
			if(NinjaChangeFlag == 0){
				NinjaChangeFlag = 1;
				Debug.Log("忍者データ変更1");//デバッグ用
				//デフォルト初出現	30
				//デフォルト出現間隔	min2 max4
				//デフォルト出現率	fly1.0f wall0.0f
				GameObject.Find("GameController").GetComponent<NinjaCreate>().minCreateTime=1.5f;//忍者出現間隔変更、下同
				GameObject.Find("GameController").GetComponent<NinjaCreate>().maxCreateTime=3.0f;
				
				GameObject.Find("GameController").GetComponent<NinjaCreate>().probabilityNinjaFlyTest=0.7f;//忍者出現率変更、下同
				GameObject.Find("GameController").GetComponent<NinjaCreate>().probabilityNinjaWallTest=0.3f;
			}
		}
		if (CurrentTime >= NinjaChangeTime2){
			if(NinjaChangeFlag == 1){
				NinjaChangeFlag = 2;
				Debug.Log("忍者データ変更2");//デバッグ用
				GameObject.Find("GameController").GetComponent<NinjaCreate>().minCreateTime=1.0f;//忍者出現間隔変更、下同
				GameObject.Find("GameController").GetComponent<NinjaCreate>().maxCreateTime=2.0f;
				
				GameObject.Find("GameController").GetComponent<NinjaCreate>().probabilityNinjaFlyTest=0.5f;//忍者出現率変更、下同
				GameObject.Find("GameController").GetComponent<NinjaCreate>().probabilityNinjaWallTest=0.5f;
			}
		}
		if (CurrentTime >= NinjaChangeTime3){
			if(NinjaChangeFlag == 2){
				NinjaChangeFlag = 3;
				Debug.Log("忍者データ変更3");//デバッグ用
				GameObject.Find("GameController").GetComponent<NinjaCreate>().minCreateTime=0.5f;//忍者出現間隔変更、下同
				GameObject.Find("GameController").GetComponent<NinjaCreate>().maxCreateTime=1.0f;
				
				GameObject.Find("GameController").GetComponent<NinjaCreate>().probabilityNinjaFlyTest=0.4f;//忍者出現率変更、下同
				GameObject.Find("GameController").GetComponent<NinjaCreate>().probabilityNinjaWallTest=0.6f;
			}
		}
	}
}
