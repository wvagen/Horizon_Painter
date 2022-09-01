﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UPersian.Components;

public class Game_Over_Manager : MonoBehaviour
{

    public Animator myAnim;

    public GameObject container;
    public GameObject HowToPlayPanel;
    public GameObject loadingPanel;

    public Image loadingFillableImg;

    public GameObject PausePanel;
    public Image counterImg;
    public Sprite[] numberSprites;

    public GameObject[] successGameObjects;
    public GameObject[] failureGameObjects;

    public Sprite musicBtnOnSprite, musicBtnOffSprite;
    public Sprite sfxBtnOnSprite, sfxBtnOffSprite;

    public Sprite infiniteModeLockedSprite;
    public Text inifiniteModeTxt;

    public static int levelIndex; //This will represent the levelIndex of a dlc game

    public GameObject taggleMusicOn, tagglemusicOff, taggleSfxOn, taggleSfxOff;
    public Image bestScoreMainMenuBtnImg, infiniteModeBtnImg;

    public Text bestScoreMainMenuValueTxt, bestScoreInGameValueTxt, ScoreInGameValueTxt;

    public Transform levelsContainer;

    public GameObject[] starsOwned;
    public RtlText instructionsTxt;

    public RtlText infoMsgTxt;

    public AudioSource myAudioSource, myMusicPlayer;
    public AudioClip successSFX, failureSFX, musicSfxBtnSwitchSFX, clickingSFX, openingMenuSFX, hidingBtnSFX, counterSFX, Score_Inc_SFX;
    public AudioClip[] starsSFX;

    public static bool musicOn = true;
    public static bool sfxOn = true;

    public string levelMapGameName;

    public bool isReachedLastLevel = false;

    float currentTime = 0;
    bool isPaused = false;
    bool isYouShouldNotPressThatButtonTwicePressed = false;
    int timerCounter = 1;

    bool isStarScaled = true;
    bool isContainerScaled = false;

    byte starsScaledCount = 0;

    public static string GAME_DATA_KEY = "game_data_level";
    public static string LEVEL_REACHED_KEY = "Level_Reached";

    public static bool isLevel = false;
    public static bool isOver = false;

    void Start()
    {
        Time.timeScale = 1;
        isOver = false;
        SoundsStats();
        Locked_Btns_Behaviour();
        myAnim.Play(Game_Over_Animations_Names.Intro_Anim);
    }

    void Check_Reached_Levels()
    {
        int levelPassed = 0;
        for (int i = 0; i < levelsContainer.childCount; i++)
        {
            if (levelsContainer.GetChild(i).GetComponent<Game_Over_Level_Btn>().isFinishedLevel)
            {
                levelPassed++;
            }
        }
        if (levelPassed == levelsContainer.childCount) isReachedLastLevel = true;
    }

    void Locked_Btns_Behaviour()
    {
        if (!PlayerPrefs.GetString(LEVEL_REACHED_KEY, string.Empty).Contains(levelMapGameName))
        {
            infiniteModeBtnImg.sprite = infiniteModeLockedSprite;
            inifiniteModeTxt.gameObject.SetActive(false);
        }
    }

    public void WinLoseLevelManager(bool isWin)
    {
        isOver = true;


        foreach (GameObject o in successGameObjects)
        {
            o.SetActive(isWin);
        }
        foreach (GameObject o in failureGameObjects)
        {
            o.SetActive(!isWin);
        }
        foreach (GameObject star in starsOwned)
        {
            star.SetActive(false);
        }


        if (isWin)
        {
            myAnim.Play(Game_Over_Animations_Names.Win_Anim);
            if (sfxOn)
                myAudioSource.PlayOneShot(successSFX);
        }
        else
        {
            myAnim.Play(Game_Over_Animations_Names.Lose_Anim);
            if (sfxOn)
                myAudioSource.PlayOneShot(failureSFX);
        }

    }

    public void Main_Menu_Best_Score_Btn(bool isOpen)
    {
        if (PlayerPrefs.GetString(LEVEL_REACHED_KEY, string.Empty).Contains(levelMapGameName))
        {
            if (isOpen)
            {
                myAudioSource.PlayOneShot(openingMenuSFX);
                bestScoreMainMenuValueTxt.text = GetBestScore().ToString();
                myAnim.Play(Game_Over_Animations_Names.Game_Over_Manager_Best_Score_In_Main_Menu_Expand_Anim);
            }
            else
            {
                myAnim.Play(Game_Over_Animations_Names.Game_Over_Manager_Best_Score_In_Main_Menu_Collapse_Anim);
                myAudioSource.PlayOneShot(hidingBtnSFX);
            }
        }
        else
        {
            Open_Info_Panel("عَليْكَ تَجْمِيعُ النِّقَاط في المُسْتَوى الّانِهَائِي أَوّلًا");
        }

    }

    public void In_Game_Score_Panel_Handler(int currentScore)
    {
        isOver = true;
        StartCoroutine(Score_Inc_Animation(currentScore));
        bestScoreInGameValueTxt.text = PlayerPrefs.GetInt("BestScore_" + levelMapGameName, currentScore).ToString();

        if (currentScore > PlayerPrefs.GetInt("BestScore_" + levelMapGameName, 0))
        {
            bestScoreInGameValueTxt.text = currentScore.ToString();
            PlayerPrefs.SetInt("BestScore_" + levelMapGameName, currentScore);
        }
        myAudioSource.PlayOneShot(successSFX);
        myAnim.Play(Game_Over_Animations_Names.Game_Over_Manager_Best_Score_In_Game_Expand_Anim);
    }

    IEnumerator Score_Inc_Animation(int currentScore)
    {
        yield return new WaitForSeconds(1.5f);
        float tempScore = 0;
        while (tempScore < currentScore)
        {
            tempScore += Time.deltaTime * currentScore;
            ScoreInGameValueTxt.text = ((int)tempScore).ToString();
            myAudioSource.PlayOneShot(Score_Inc_SFX);
            yield return new WaitForEndOfFrame();
        }
        ScoreInGameValueTxt.text = currentScore.ToString();
    }

    int GetBestScore()
    {
        return PlayerPrefs.GetInt("BestScore_" + levelMapGameName, 0);
    }



    public void WinLoseLevelManager(bool isWin, int starsCount)
    {
        WinLoseLevelManager(isWin);
        SaveDataManager(starsCount);
        IsContainerScaledTrigger();
        StartCoroutine(starsAnimation(starsCount));
    }

    void SoundsStats()
    {
        musicOn = (PlayerPrefs.GetInt("Sound_Enabled", 1) == 1);

        myMusicPlayer.mute = !musicOn;
        myAudioSource.mute = !sfxOn;

        if (musicOn)
        {
            taggleMusicOn.SetActive(true);
            tagglemusicOff.SetActive(false);
        }
        else
        {
            taggleMusicOn.SetActive(false);
            tagglemusicOff.SetActive(true);
        }

        if (sfxOn)
        {
            taggleMusicOn.SetActive(true);
            tagglemusicOff.SetActive(false);
        }
        else
        {
            taggleMusicOn.SetActive(true);
            tagglemusicOff.SetActive(false);
        }

    }
    public void IsContainerScaledTrigger()
    {
        isContainerScaled = true;
    }

    IEnumerator starsAnimation(int starsCount)
    {

        while (!isContainerScaled)
        {
            yield return null;
        }

        do
        {
            yield return new WaitForEndOfFrame();
            while (isStarScaled)
            {
                StartCoroutine(starAnimation(starsOwned[starsScaledCount]));
                yield return new WaitForEndOfFrame();
            }
        } while (starsScaledCount < starsCount - 1);

    }

    IEnumerator starAnimation(GameObject star)
    {
        isStarScaled = false;
        Vector2 initScale = star.transform.localScale;
        star.SetActive(true);

        star.transform.localScale = Vector2.zero;
        Vector2 wantedScale = initScale * 2;
        float realTimeScaleY = initScale.y;

        myAudioSource.PlayOneShot(starsSFX[starsScaledCount]);

        while (realTimeScaleY < wantedScale.y)
        {
            realTimeScaleY += Time.deltaTime * 3;
            star.transform.localScale = new Vector3(realTimeScaleY, realTimeScaleY);
            yield return new WaitForEndOfFrame();
        }

        while (realTimeScaleY > (initScale.y))
        {
            realTimeScaleY -= Time.deltaTime * 3;
            star.transform.localScale = new Vector3(realTimeScaleY, realTimeScaleY);
            yield return new WaitForEndOfFrame();
        }
        isStarScaled = true;
        starsScaledCount++;
    }


    IEnumerator LoadScene(string sceneName_Path)
    {
        loadingPanel.SetActive(true);
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName_Path);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        //When the load is still in progress, output the Text and progress bar

        while (!asyncOperation.isDone)
        {
            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    public void Start_Game()
    {
        if (!isYouShouldNotPressThatButtonTwicePressed)
        {
            isYouShouldNotPressThatButtonTwicePressed = true;
            StartCoroutine(LoadScene(levelMapGameName + "_2_MainGame"));
        }
    }

    public void HomeBtn()
    {
        if (!isYouShouldNotPressThatButtonTwicePressed)
        {
            isYouShouldNotPressThatButtonTwicePressed = true;
            StartCoroutine(LoadScene(levelMapGameName + "_1_MainMenu"));
        }
    }

    public void RetryLevel()
    {
        if (!isYouShouldNotPressThatButtonTwicePressed)
        {
            isYouShouldNotPressThatButtonTwicePressed = true;
            StartCoroutine(LoadScene(SceneManager.GetActiveScene().path));
        }
    }

    public void NextLevel()
    {
        if (!isYouShouldNotPressThatButtonTwicePressed)
        {
            isYouShouldNotPressThatButtonTwicePressed = true;
            if (levelIndex == (levelsContainer.childCount - 1) || isReachedLastLevel)
            {
                Set_Level_As_Finished();
                isYouShouldNotPressThatButtonTwicePressed = false;
                Quit();
            }
            else
            {
                levelIndex++;
                isYouShouldNotPressThatButtonTwicePressed = false;
                RetryLevel();
            }
        }
    }

    public void MusicBtn()
    {
        myAudioSource.PlayOneShot(musicSfxBtnSwitchSFX);
        if (musicOn)
        {
            musicOn = false;
            taggleMusicOn.SetActive(false);
            tagglemusicOff.SetActive(true);
            PlayerPrefs.SetInt("Sound_Enabled", 0);
        }
        else
        {
            musicOn = true;
            taggleMusicOn.SetActive(true);
            tagglemusicOff.SetActive(false);
            PlayerPrefs.SetInt("Sound_Enabled", 1);
        }
        myMusicPlayer.mute = !musicOn;
    }

    public void SfxBtn()
    {
        myAudioSource.PlayOneShot(musicSfxBtnSwitchSFX);

        if (sfxOn)
        {
            sfxOn = false;
            taggleSfxOff.SetActive(true);
            taggleSfxOn.SetActive(false);
            myAudioSource.mute = true;
        }
        else
        {
            sfxOn = true;
            taggleSfxOff.SetActive(false);
            taggleSfxOn.SetActive(true);
            myAudioSource.mute = false;
        }

    }

    public void PauseBtn()
    {
        myAudioSource.PlayOneShot(openingMenuSFX);
        Time.timeScale = 0;
        myAnim.Play(Game_Over_Animations_Names.Pause_Anim);
    }

    public void HowToPlayBtn(bool isOpen)
    {
        myAudioSource.PlayOneShot(clickingSFX);
        HowToPlayPanel.SetActive(isOpen);
    }

    public void Open_Info_Panel(string msgToDisplay)
    {
        infoMsgTxt.text = msgToDisplay;
        myAudioSource.PlayOneShot(openingMenuSFX);
        myAnim.Play(Game_Over_Animations_Names.Game_Over_Manager_Info_Panel_Expand_Anim);
    }

    public void Close_Info_Panel()
    {
        myAnim.Play(Game_Over_Animations_Names.Game_Over_Manager_Info_Panel_Collapse_Anim);
        myAudioSource.PlayOneShot(hidingBtnSFX);
    }

    public void ResumeBtn()
    {
        Debug.Log("test");
        PausePanel.SetActive(false);
        myAnim.Play(Game_Over_Animations_Names.Game_Over_Manager_Resume_Anim);
        counterImg.sprite = numberSprites[timerCounter];

        currentTime = Time.unscaledTime;
        currentTime++;

        myAudioSource.PlayOneShot(counterSFX);

        isPaused = true;

        Time.timeScale = 0;
    }

    void Update()
    {
        if (isPaused)
        {
            Resume_Behaviour();
        }
    }

    void Resume_Behaviour()
    {
        if (Time.unscaledTime > currentTime)
        {
            if (timerCounter <= 0)
            {
                timerCounter = 2;
                Time.timeScale = 1;
                Debug.Log(Time.timeScale);
                isPaused = false;
            }
            else
            {
                timerCounter--;
                currentTime++;
                counterImg.sprite = numberSprites[timerCounter];
                myAudioSource.PlayOneShot(counterSFX);
            }
        }
    }


    public void OptionsBtn(bool isOpen)
    {
        if (isOpen)
        {
            myAnim.Play(Game_Over_Animations_Names.Game_Over_Manager_Options_Expand_Anim);
            myAudioSource.PlayOneShot(openingMenuSFX);
        }
        else
        {
            myAnim.Play(Game_Over_Animations_Names.Game_Over_Manager_Options_Collapse_Anim);
            myAudioSource.PlayOneShot(hidingBtnSFX);
        }
    }

    public void Choice_Mode_Btn(bool isOpen)
    {
        if (isOpen)
        {
            myAnim.Play(Game_Over_Animations_Names.Game_Over_Manager_Transition_To_Choice_Mode_Panel_Anim);
            myAudioSource.PlayOneShot(clickingSFX);
        }
        else
        {
            myAnim.Play(Game_Over_Animations_Names.Game_Over_ManagerChoice_Mode_Panel_Exit_Anim);
            myAudioSource.PlayOneShot(hidingBtnSFX);
        }
    }

    public void Level_Select_Btn(bool isOpen)
    {
        if (isOpen)
        {
            myAnim.Play(Game_Over_Animations_Names.Game_Over_Manager_Transition_To_Level_Select_Panel_Anim);
            myAudioSource.PlayOneShot(clickingSFX);
        }
        else
        {
            myAnim.Play(Game_Over_Animations_Names.Game_Over_Manager_Level_Select_Panel_Exit_Anim);
            myAudioSource.PlayOneShot(hidingBtnSFX);
        }
    }

    public void Inifnite_Mode_Btn()
    {
        if (PlayerPrefs.GetString(LEVEL_REACHED_KEY, string.Empty).Contains(levelMapGameName))
        {
            myAudioSource.PlayOneShot(clickingSFX);
            isLevel = false;
            Start_Game();
        }
        else
        {
            Open_Info_Panel("عَليْكَ إِنْهَاءُ المُسْتَوَيَاتِ لِفَتْح المُسْتَوى الّانِهَائِي");
        }
    }

    public void Back_From_Level_Select_To_Choice()
    {
        myAnim.Play(Game_Over_Animations_Names.Game_Over_Manager_Transition_From_Level_Select_Panel_To_Choice_Mode_Panel_Anim);
        myAudioSource.PlayOneShot(clickingSFX);
    }

    public void Quit()
    {
        //Here we must adapt each code with the game
        if (!isYouShouldNotPressThatButtonTwicePressed)
        {
            isYouShouldNotPressThatButtonTwicePressed = true;
            Time.timeScale = 1;
            if (PlayerPrefs.GetInt("Dlc_Is_Open_From_Map", 0) == 0)
            {
                StartCoroutine(LoadScene("1-Main_Menu"));
            }
            else
            {
                StartCoroutine(LoadScene("4_Map_Scene"));
            }
        }
    }

    void SaveDataManager(int starsOwned)
    {
        if (PlayerPrefs.HasKey(GAME_DATA_KEY + levelMapGameName + "_" + levelIndex))
        {
            if (PlayerPrefs.GetInt(GAME_DATA_KEY + levelMapGameName + "_" + levelIndex, 0) < starsOwned)
            {
                PlayerPrefs.SetInt(GAME_DATA_KEY + levelMapGameName + "_" + levelIndex, starsOwned);
            }
        }
        else
        {
            PlayerPrefs.SetInt(GAME_DATA_KEY + levelMapGameName + "_" + levelIndex, starsOwned);
        }
    }

    public void Set_Level_As_Finished()
    {
        PlayerPrefs.SetString(LEVEL_REACHED_KEY, PlayerPrefs.GetString(LEVEL_REACHED_KEY, string.Empty) + levelMapGameName + "-");

        short score = 0;

        for (int i = 0; i < levelIndex; i++)
        {
            score += (short)PlayerPrefs.GetInt(GAME_DATA_KEY + levelMapGameName + "_" + levelIndex, 0);
        }

        if (score >= (levelIndex * 3) - levelIndex)
        {
            PlayerPrefs.SetInt(levelMapGameName + "stars", 3);
        }
        else if (score >= (levelIndex * 3) - (levelIndex * 2))
        {
            PlayerPrefs.SetInt(levelMapGameName + "stars", 2);
        }
        else
        {
            PlayerPrefs.SetInt(levelMapGameName + "stars", 1);
        }
    }

}
