using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter_MainMenu : MonoBehaviour
{

    public Game_Over_Manager gameOverMan;

    void Start()
    {
        if (!PlayerPrefs.HasKey(Game_Over_Manager.GAME_DATA_KEY + gameOverMan.levelMapGameName + "_" + 3))
        {
            for (int i = 0; i < 3; i++)
            {
                PlayerPrefs.SetInt(Game_Over_Manager.GAME_DATA_KEY + gameOverMan.levelMapGameName + "_" + i.ToString(), 3);
            }
        }
    }


}
