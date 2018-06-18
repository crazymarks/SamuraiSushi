using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    public GameObject menuObject;

    public Toggle maguroToggle;
    public Toggle takoToggle;
    public Toggle fuguToggle;
    public Toggle peopleToggle;
    public Toggle ninjaWallToggle;
    public Toggle ninjaFlyToggle;
    
    //データが変えるように、変数で変えたデータを記録します
    float probabilityMaguro1 = 0f;
    float probabilityTako1 = 0f;
    float probabilityFugu1 = 0f;
    float probabilityNinjaWall1 = 0f;
    float probabilityNinjaFly1 = 0f;

    void Start () {
        menuObject.SetActive(false);
        maguroToggle.GetComponent<Toggle>();
        takoToggle.GetComponent<Toggle>();
        fuguToggle.GetComponent<Toggle>();
        peopleToggle.GetComponent<Toggle>();
        ninjaWallToggle.GetComponent<Toggle>();
        ninjaFlyToggle.GetComponent<Toggle>();
        probabilityMaguro1 = GameController.probabilityMaguro;
        probabilityTako1 = GameController.probabilityTako;
        probabilityFugu1 = GameController.probabilityFugu;
        probabilityNinjaWall1 = NinjaCreate.probabilityNinjaWall;
        probabilityNinjaFly1 = NinjaCreate.probabilityNinjaFly;
    }
	

    public void OpenMenu()
    {
        //メニューボタンが押された時
        menuObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseMenu()
    {
        //メニュー内のCloseボタンが押された時
        menuObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void CheckMaguroToggle()
    {
        if(!maguroToggle.isOn)
        {
            GameController.probabilityMaguro = 0f;                
        }
        else
        {
            GameController.probabilityMaguro = 0.5f;
        }
    }

    public void CheckTakoToggle()
    {
        if (!takoToggle.isOn)
        {
            GameController.probabilityTako = 0f;
        }
        else
        {
            GameController.probabilityTako = 0.4f;
        }
    }

    public void CheckFuguToggle()
    {
        if (!fuguToggle.isOn)
        {
            GameController.probabilityFugu = 0f;
        }
        else
        {
            GameController.probabilityFugu = 0.1f;
        }
    }

    public void CheckPeopleToggle()
    {
        if (!peopleToggle.isOn)
        {
            PeopleCreate.isPeopleCreate = false;
        }
        else
        {
            PeopleCreate.isPeopleCreate = true;
        }
    }

    public void CheckNinjaWallToggle()
    {
        if (!ninjaWallToggle.isOn)
        {
            NinjaCreate.probabilityNinjaWall = 0f;
        }
        else
        {
            NinjaCreate.probabilityNinjaWall = 0.5f;
        }
    }

    public void CheckNinjaFlyToggle()
    {
        if (!ninjaFlyToggle.isOn)
        {
            NinjaCreate.probabilityNinjaFly = 0f;
        }
        else
        {
            NinjaCreate.probabilityNinjaFly = 0.5f;
        }
    }
}
