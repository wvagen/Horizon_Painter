using NoSuchStudio.Common.Service;
using NoSuchStudio.Localization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace com.horizon.LocalizationSystem
{
    //this is to ensure a single instance of the LocalizationService cuz this class
    //is the parent of the LocalizationService 
    public class LocalizationRuntimeSingleton : MonoBehaviour
    {
        private static LocalizationRuntimeSingleton instance;


        private void Awake()
        {
            InitSingleton();
            SubscribeToSceneLoaded();
        }
        private void Start()
        {
            SetUpAppLanguage();
        }
        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        private void SetUpAppLanguage()
        {

            //if (PlayerPrefs.HasKey(LocalizationPopUpHandler.CURRENT_LANGUAGE_PLAYER_PREFS_KEY))
            //{
            //    string currentLang = PlayerPrefs.GetString(LocalizationPopUpHandler.CURRENT_LANGUAGE_PLAYER_PREFS_KEY);
            //    if (!string.IsNullOrEmpty(currentLang))
            //    {
            //        //test
            //        string prefsLang = PlayerPrefs.GetString(LocalizationPopUpHandler.CURRENT_LANGUAGE_PLAYER_PREFS_KEY);
            //        Debug.Log($"Loaded from prefs: '{currentLang}'");
            //        //

            //        List<string> languagesList = LocalizationService.Instance.GetLocales().Select(l => l.Name).ToList();
            //        Debug.Log($"Available: {string.Join(", ", languagesList)}");
            //        //List<string> languagesList = LocalizationService.Instance.localeNames.ToList<string>();

            //        if (languagesList != null && languagesList.Count > 0)
            //        {
            //            if (languagesList.Contains(currentLang))
            //            {
            //                LocalizationHelper.SetCurrentLanguage(currentLang);
            //                return;
            //            }
            //            else
            //            {
            //                Debug.LogError($"langage stored in PlayerPrefs \"{currentLang}\" is not valid" +
            //                    $" (not found in locales list)");
            //            }
            //        }
            //        else
            //        {
            //            Debug.LogError(nameof(languagesList) + " is null or empty");
            //        }
            //    }
            //    else
            //    {
            //        Debug.LogError(nameof(currentLang) + " string is null or empty");
            //    }
            //}
            //else
            //{
            //    Debug.LogWarning(nameof(LocalizationPopUpHandler.CURRENT_LANGUAGE_PLAYER_PREFS_KEY) + " key entry is not stored in the playerPrefs");
            //}
        }

        #region --- Singleton
        public static LocalizationRuntimeSingleton GetInstance()
        {
            if (instance == null)
            {
                Debug.LogError(nameof(LocalizationRuntimeSingleton) + " instance is null");
            }
            return instance;
        }
        private void InitSingleton()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion



        // [Memory leak] there is an issue in the noSuchStudio package , it keeps references to missing gamebojects and keep
        // those references even if the scene switches , so they acumulate (600 --> 1000 --> 1300 etc) and if they get too big
        // eventually the loop over them will take a longer and longer time which will cause performance isues or just fills the memory 
        // with non used references ,after tinkering with the package i found out that pressing the "Restart button" in the 
        // "Localization service" script inpsector removes this problem : solution to call it from code on each new scene load 

        private void SubscribeToSceneLoaded()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            ReRegisterLocalizationService();
        }

        private void ReRegisterLocalizationService()
        {
            // reset the localizationService to remove the missing references
            try
            {
                Service<LocalizationService>.Instance?.ReRegisterService();
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

    }
}