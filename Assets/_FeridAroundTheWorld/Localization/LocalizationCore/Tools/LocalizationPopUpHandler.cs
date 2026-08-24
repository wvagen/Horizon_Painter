using NoSuchStudio.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace com.horizon.LocalizationSystem
{
    [Serializable]
    class LanguageBtn
    {
        public Locale Language;
        public Button Btn;

        public bool IsValid()
        {
            if (Btn == null) { Debug.LogError(nameof(Btn) + " is null"); return false; }
            return true;
        }
    }
    public class LocalizationPopUpHandler : MonoBehaviour
    {
        //// used to differentiate the behaviour of this script based on whether the previous scene is "-1-Animation_Scene" or settings  
        //public static string PreviousSceneName = string.Empty;

        //public const string CURRENT_LANGUAGE_PLAYER_PREFS_KEY = "currentLanguage";
        //[SerializeField] private bool IsPanelAlwaysActive = false; // to not hide it in the change language scene
        //[SerializeField] private bool CanCallSetUpAppLanguage = false;// show and hide the panel dynamically based on player prefs

        //[SerializeField] private Color SelectedBtnColor = Color.green;
        //[SerializeField] private Color UnSelectedBtnColor = Color.white;
        //[SerializeField] private List<LanguageBtn> LanguageBtnsList = new List<LanguageBtn>();
        //[SerializeField] private Button AcceptBtn;
        //[SerializeField] private Button AppLanguageBtn;
        //[SerializeField] private GameObject PopUpPanel;
        //[Space]
        //[SerializeField] private BtnsController BtnsController;
        //[SerializeField] private AlertPanel AlertPanel;

        //private void Start()//some functions here require localization service
        //{
        //    Screen.orientation = ScreenOrientation.Portrait;
        //    SetupBtnsListners();

        //    if (!IsPanelAlwaysActive)
        //    {
        //        HideLanguagePopUpPanel();
        //        if (CanCallSetUpAppLanguage)
        //            SetUpAppLanguage();
        //    }
        //    else
        //    {
        //        SelectCurrentLanguageBtn();
        //    }

        //}

        ////get the current language
        //private void OnEnable()
        //{
        //    SelectCurrentLanguageBtn();
        //}

        //private void SetUpAppLanguage()
        //{

        //    if (PlayerPrefs.HasKey(CURRENT_LANGUAGE_PLAYER_PREFS_KEY))
        //    {
        //        string currentLang = PlayerPrefs.GetString(CURRENT_LANGUAGE_PLAYER_PREFS_KEY);
        //        if (!string.IsNullOrEmpty(currentLang))
        //        {
        //            ////test
        //            //string prefsLang = PlayerPrefs.GetString(CURRENT_LANGUAGE_PLAYER_PREFS_KEY);
        //            //Debug.Log($"Loaded from prefs: '{currentLang}'");
        //            ////

        //            List<string> languagesList = LocalizationService.Instance.GetLocales().Select(l => l.Name).ToList();
        //            Debug.Log($"Available: {string.Join(", ", languagesList)}");
        //            //List<string> languagesList = LocalizationService.Instance.localeNames.ToList<string>();

        //            if (languagesList != null && languagesList.Count > 0)
        //            {
        //                if (languagesList.Contains(currentLang))
        //                {
        //                    LocalizationHelper.SetCurrentLanguage(currentLang);
        //                    return;
        //                }
        //                else
        //                {
        //                    Debug.LogError($"langage stored in PlayerPrefs \"{currentLang}\" is not valid" +
        //                        $" (not found in locales list)");
        //                }
        //            }
        //            else
        //            {
        //                Debug.LogError(nameof(languagesList) + " is null or empty");
        //            }
        //        }
        //        else
        //        {
        //            Debug.LogError(nameof(currentLang) + " string is null or empty");
        //        }
        //    }
        //    else
        //    {
        //        Debug.LogWarning(nameof(CURRENT_LANGUAGE_PLAYER_PREFS_KEY) + " key entry is not stored in the playerPrefs");
        //    }

        //    //fail
        //    ShowLanguagePopUpPanel();
        //}



        //public static bool IsLanguageSet()
        //{
        //    if (!PlayerPrefs.HasKey(CURRENT_LANGUAGE_PLAYER_PREFS_KEY))
        //    {
        //        Debug.LogWarning(nameof(CURRENT_LANGUAGE_PLAYER_PREFS_KEY) + " key entry is not stored in the playerPrefs");
        //        return false;
        //    }

        //    string currentLang = PlayerPrefs.GetString(CURRENT_LANGUAGE_PLAYER_PREFS_KEY);

        //    if (string.IsNullOrEmpty(currentLang))
        //    {
        //        Debug.LogError(nameof(currentLang) + " string is null or empty");
        //        return false;
        //    }

        //    List<string> languagesList = LocalizationService.Instance.GetLocales().Select(l => l.Name).ToList();

        //    if (languagesList == null || languagesList.Count == 0)
        //    {
        //        Debug.LogError(nameof(languagesList) + " is null or empty");
        //        return false;
        //    }

        //    if (!languagesList.Contains(currentLang))
        //    {
        //        Debug.LogError(
        //            $"language stored in PlayerPrefs \"{currentLang}\" is not valid " +
        //            $"(not found in locales list)"
        //        );

        //        return false;
        //    }

        //    return true;
        //}

        //private void SetupBtnsListners()
        //{
        //    // -- accept btn
        //    if (AcceptBtn == null)
        //        Debug.LogError(nameof(AcceptBtn) + " is null");
        //    else
        //        AcceptBtn.onClick.AddListener(HandleAcceptBtn);

        //    // -- App language btn
        //    if (AppLanguageBtn == null)
        //        Debug.LogError(nameof(AppLanguageBtn) + " is null");
        //    else
        //        AppLanguageBtn.onClick.AddListener(ShowLanguagePopUpPanel);


        //    // -- language btns
        //    if (LanguageBtnsList == null || LanguageBtnsList.Count == 0) { Debug.LogError(nameof(LanguageBtnsList) + " is null or empty"); return; }
        //    for (int i = 0; i < LanguageBtnsList.Count; i++)
        //    {
        //        LanguageBtn Langbtn = LanguageBtnsList[i];
        //        if (Langbtn == null)
        //            continue;
        //        if (!Langbtn.IsValid())
        //            continue;
        //        Langbtn.Btn.onClick.AddListener(() =>
        //        {

        //            SetAppLanguage(Langbtn);
        //        });
        //    }
        //}

        //private void HandleAcceptBtn()
        //{
        //    //HideLanguagePopUpPanel();

        //    if (PreviousSceneName == Constants.SCENE_ANIMATION_SCENE)
        //    {
        //        // BtnsController.Open_Profiles_scene();
        //        if (PlayerPrefs.HasKey(Constants.ACCOUNT_ID) || PlayerPrefs.HasKey(AccountDataManager.ACCOUNTS_DATA_KEY))
        //        {
        //            AnalyticsTracker.Instance.TrackScreenVisit(Constants.SCENE_AUTH_PROFILE);
        //            StartCoroutine(Load_Scene_And_Wait_For_Logo(Constants.SCENE_AUTH_PROFILE));
        //        }
        //        else
        //        {
        //            AnalyticsTracker.Instance.TrackScreenVisit(Constants.SCENE_AUTH);
        //            StartCoroutine(Load_Scene_And_Wait_For_Logo(Constants.SCENE_AUTH));

        //        }
        //    } else
        //    {
        //        if (BtnsController == null) { Debug.LogError(nameof(BtnsController) + " is null"); return; }
        //        if (PreviousSceneName == Constants.SCENE_SETTINGS)
        //        {
        //            AnalyticsTracker.Instance.TrackScreenVisit(Constants.SCENE_SETTINGS);
        //            BtnsController.Open_Settings();
        //        }
        //        else
        //        {
        //            AnalyticsTracker.Instance.TrackScreenVisit(Constants.SCENE_MAIN_MENU);
        //            BtnsController.Open_Main_Menu();
        //        }
        //    }
          
        //}

        //private IEnumerator Load_Scene_And_Wait_For_Logo(string scene_name)
        //{

        //    if (AlertPanel == null)
        //    {
        //        Debug.LogError(nameof(AlertPanel) + " is null");
        //        yield return null;
        //    }
        //    AlertPanel.Loading(true);
        //    yield return new WaitForSeconds(1f); // wait for logo
        //    AlertPanel.Load_Scene(scene_name);

        //}

        //private void ShowLanguagePopUpPanel()
        //{
        //    if (PopUpPanel == null) { Debug.LogError(nameof(PopUpPanel) + " is null"); return; }
        //    PopUpPanel.SetActive(true);
        //}
        //private void HideLanguagePopUpPanel()
        //{
        //    if (PopUpPanel == null) { Debug.LogError(nameof(PopUpPanel) + " is null"); return; }
        //    PopUpPanel.SetActive(false);
        //}

        //private void SetAppLanguage(LanguageBtn Langbtn)
        //{
        //    LocalizationHelper.SetCurrentLanguage(Langbtn.Language);
        //    SelectLanguageBtn(Langbtn, true);
        //}

        //private void SelectCurrentLanguageBtn()
        //{
        //    LanguageBtn currentBtn = GetCurrentLanguageBtn();
        //    if (currentBtn == null || !currentBtn.IsValid())
        //        return;
        //    SelectLanguageBtn(currentBtn, false);
        //}

        ////saveInPlayerPrefs=true must be in case of the player physically pressed the button , false when it s selected internally via code
        //private void SelectLanguageBtn(LanguageBtn langBtn, bool saveInPlayerPrefs)
        //{
        //    // select button
        //    var btnImg = langBtn.Btn.GetComponent<Image>();
        //    if (btnImg == null)
        //    {
        //        Debug.LogError(nameof(btnImg) + " is null");
        //        return;
        //    }
        //    btnImg.color = SelectedBtnColor;
        //    //
        //    if (saveInPlayerPrefs)
        //        SaveSelectedLanguageInPlayerPrefs(langBtn.Language);

        //    UnSelectOtherBtns(langBtn);
        //}

        //private void UnSelectOtherBtns(LanguageBtn currentLangBtn)
        //{
        //    if (LanguageBtnsList == null || LanguageBtnsList.Count == 0) { Debug.LogError(nameof(LanguageBtnsList) + " is null or empty"); return; }
        //    for (int i = 0; i < LanguageBtnsList.Count; i++)
        //    {
        //        LanguageBtn Langbtn = LanguageBtnsList[i];
        //        if (Langbtn == null)
        //            continue;
        //        if (Langbtn == currentLangBtn)//skip current lang btn
        //            continue;

        //        // unselect btn
        //        var btnImg = Langbtn.Btn.GetComponent<Image>();
        //        if (btnImg == null)
        //        {
        //            Debug.LogError(nameof(btnImg) + " is null");
        //            return;
        //        }
        //        btnImg.color = UnSelectedBtnColor;

        //    }
        //}

        //private Locale? GetAndSetCurrentLanguageFromPlayerPrefs()
        //{
        //    if (!PlayerPrefs.HasKey(CURRENT_LANGUAGE_PLAYER_PREFS_KEY))
        //    {
        //        Debug.LogWarning(nameof(CURRENT_LANGUAGE_PLAYER_PREFS_KEY) + " key entry is not stored in the playerPrefs");
        //        return null;
        //    }
        //    string currentLang = PlayerPrefs.GetString(CURRENT_LANGUAGE_PLAYER_PREFS_KEY);
        //    if (string.IsNullOrEmpty(currentLang))
        //    {
        //        Debug.LogError(nameof(currentLang) + " string is null or empty");
        //        return null;
        //    }
        //    List<string> languagesList = LocalizationService.Instance.GetLocales().Select(l => l.Name).ToList();
        //    //  Debug.Log($"Available: {string.Join(", ", languagesList)}");
        //    if (languagesList == null || languagesList.Count == 0)
        //    {
        //        Debug.LogError(nameof(languagesList) + " is null or empty");
        //        return null;
        //    }

        //    if (!languagesList.Contains(currentLang))
        //    {
        //        Debug.LogError($"langage stored in PlayerPrefs \"{currentLang}\" is not valid" +
        //          $" (not found in locales list)");
        //        return null;
        //    }
        //    SetUpAppLanguage();
        //    return currentLang;
        //}




        //private LanguageBtn GetCurrentLanguageBtn()
        //{
        //    if (LanguageBtnsList == null || LanguageBtnsList.Count == 0) { Debug.LogError(nameof(LanguageBtnsList) + " is null or empty"); return null; }


        //    Locale? currentLang = GetAndSetCurrentLanguageFromPlayerPrefs();
        //    if (currentLang == null)
        //        currentLang = LocalizationHelper.GetCurrentLanugage(); // if not found in player prefs , default to the LocalizationService default language

        //    if (currentLang == null)
        //    {
        //        Debug.LogError(nameof(currentLang) + " ref is null");
        //        return null;
        //    }
        //    for (int i = 0; i < LanguageBtnsList.Count; i++)
        //    {
        //        LanguageBtn btn = LanguageBtnsList[i];
        //        if (btn == null)
        //        {
        //            Debug.LogError(nameof(btn) + " is null");
        //            continue;
        //        }
        //        if (!btn.IsValid())
        //            continue;
        //        if (btn.Language == currentLang)
        //            return btn;

        //    }
        //    Debug.Log("No Button corresponding to the Languge \"" + currentLang + "\"");
        //    return null;
        //}


        //private void SaveSelectedLanguageInPlayerPrefs(Locale currentLang)
        //{
        //    // Use the same property you use to look up in localeNames
        //    string langString = currentLang.LanguageInName;
        //    PlayerPrefs.SetString(CURRENT_LANGUAGE_PLAYER_PREFS_KEY, langString);
        //    PlayerPrefs.Save();
        //    Debug.Log($"Saved language: {langString}");
        //}

        //public static void DeleteSavedLanguageFromPlayerPrefs()
        //{
        //    if (PlayerPrefs.HasKey(CURRENT_LANGUAGE_PLAYER_PREFS_KEY))
        //    {
        //        PlayerPrefs.DeleteKey(CURRENT_LANGUAGE_PLAYER_PREFS_KEY);
        //        PlayerPrefs.Save();

        //        Debug.Log($"Deleted PlayerPrefs key: {CURRENT_LANGUAGE_PLAYER_PREFS_KEY}");
        //    }
        //    else
        //    {
        //        Debug.LogWarning($"PlayerPrefs key not found: {CURRENT_LANGUAGE_PLAYER_PREFS_KEY}");
        //    }
        //}


    }


    
}