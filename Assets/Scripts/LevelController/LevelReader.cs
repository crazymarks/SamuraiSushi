using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class LevelReader : MonoBehaviour {
    public string DayControllerMytxt;

    public int Day;

    public string dayroute;
    public string path;
    public TextAsset DayDataTxtFile;
    public string DayDataMytxt;

    public string[] TxtData = new string[10];
    /*[0]最初鱼生成延迟    [1] 鱼生成间隔  [2]鱼生成几率比 [3]通关鱼数   [4]最初忍者生成延迟 [5]忍者生成间隔   [6]45秒后 [7]忍者生成比    [8]最初行人生成延时   [9]行人生成间隔*/

    public float    firstFishCreateTime;//[0]最初鱼生成延迟

    public float FishCreateSpeed;//[1] 鱼生成间隔

    public string[] FishPer = new string[3];
    public float[] FishPerFloat = new float[3];
    public float probabilityMaguro;
    public float probabilityTako;
    public float probabilityFugu;//[2]鱼生成几率比

    public int maxFishAmount;//[3]通关鱼数 

    public float NinjiaFirstCreateTime;//[4]最初忍者生成延迟

    public string[] NinjiaCreateTime = new string[2];
    public float NinjiaMinCreateTime;
    public float NinjiaMaxCreateTime;//[5]忍者生成间隔

    public string[] _45sNinjiaCreateTime = new string[2];
    public float _45sNinjiaMinCreateTime;
    public float _45sNinjiaMaxCreateTime;//[6]45s后忍者生成间隔

    public string[] probabilityNinja = new string[2];
    public float probabilityNinjaFly;
    public float probabilityNinjaWall;//[7]忍者生成比

    public float peoplefirstCreateTime;//[8]最初行人生成延时

    public string[] peopleinvoke = new string[2];
    public float peopleminCreateTime;
    public float peoplemaxCreateTime;
   //[9]行人生成间隔
    
    public void ReadDayText()
    {
        //DayControllerMytxt = DayControllerTxtFile.text;
        //Day = int.Parse(DayControllerMytxt);
        /*読み込んだら、書き込むとまた読み込むと、変わらない*/
        StreamReader sr = null;
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

        sr = File.OpenText(path + @"\Resources\Datas\daycontroller.txt");

        if ((DayControllerMytxt = sr.ReadLine()) != null)
        {
            //do some thing with t_sLine
            //print(DayControllerMytxt);
        }
        else
        {
            print("Null!");
        }
        sr.Close();
        sr.Dispose();


        Day = int.Parse(DayControllerMytxt);
        //print(Day);

    }
    public void ReadText()
    {
        DayDataMytxt = DayDataTxtFile.text;
        TxtData = DayDataMytxt.Split('\n');

        firstFishCreateTime = float.Parse(TxtData[0]);//[0]最初鱼生成延迟
        FishCreateSpeed = float.Parse(TxtData[1]);//[1] 鱼生成间隔

        FishPer = TxtData[2].Split(':');
        for (int i = 0; i < 3; i++)
        {
            FishPerFloat[i] = float.Parse(FishPer[i]);
        }
        probabilityMaguro = FishPerFloat[0];
        probabilityTako = FishPerFloat[1];
        probabilityFugu = FishPerFloat[2];////[2]鱼生成几率比

        maxFishAmount = int.Parse(TxtData[3]);//[3]通关鱼数 
        NinjiaFirstCreateTime = float.Parse(TxtData[4]);//[4]最初忍者生成延迟

        NinjiaCreateTime = TxtData[5].Split('-');
        NinjiaMinCreateTime = float.Parse(NinjiaCreateTime[0]);
        NinjiaMaxCreateTime = float.Parse(NinjiaCreateTime[1]);//[5]忍者生成间隔

        _45sNinjiaCreateTime = TxtData[6].Split('-');
        _45sNinjiaMinCreateTime = float.Parse(_45sNinjiaCreateTime[0]);
        _45sNinjiaMaxCreateTime = float.Parse(_45sNinjiaCreateTime[1]);//[6]45s后忍者生成间隔

        probabilityNinja = TxtData[7].Split(':');
        probabilityNinjaFly = float.Parse(probabilityNinja[0]);
        probabilityNinjaWall = float.Parse(probabilityNinja[1]);//[7]忍者生成比

        peoplefirstCreateTime = float.Parse(TxtData[8]);//[8]最初行人生成延时

        peopleinvoke = TxtData[9].Split('-');
        peopleminCreateTime = float.Parse(peopleinvoke[0]);
        peoplemaxCreateTime = float.Parse(peopleinvoke[1]);

        //[9]行人生成间隔
    }
    public void Writein()
    {
        DayControllerMytxt = Day.ToString();
        StreamWriter sw;
        FileInfo fi;
        fi = new FileInfo(path + @"\Resources\Datas\daycontroller.txt");
        sw = fi.CreateText();
        sw.WriteLine(DayControllerMytxt);
        sw.Flush();
        sw.Close();
        sw.Dispose();

        //string filePath = Application.persistentDataPath + @"\Resources\Datas\test.txt";
        //File.WriteAllText(filePath, day);

        //書き込む





    }
    // Use this for initialization
    void Start () {
        ReadDayText();
        dayroute = "day" + DayControllerMytxt;
        DayDataTxtFile = Resources.Load<TextAsset>("Datas/" + dayroute);
        ReadText();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
