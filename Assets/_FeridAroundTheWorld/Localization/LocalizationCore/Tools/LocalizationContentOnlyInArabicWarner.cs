using NoSuchStudio.Localization;
using UnityEngine;

namespace com.horizon.LocalizationSystem
{
    public class LocalizationContentOnlyInArabicWarner : MonoBehaviour
    {


        //private AlertPanel alertPanel;

        //private void Start()
        //{
        //    //Don't display the warning in arabic mode
        //    if (LocalizationHelper.GetCurrentLanugage() == "ar")
        //        return;

        //    Invoke(nameof(Alert), 1f);
        //}

        //private void FindAlertPanel()
        //{
        //    if (alertPanel != null)
        //        return;

        //    AlertPanel[] panels = Resources.FindObjectsOfTypeAll<AlertPanel>();

        //    if (panels.Length > 0)
        //        alertPanel = panels[0];
        //}

        //private void Alert()
        //{
        //    FindAlertPanel();
        //    if (alertPanel == null)
        //    {
        //        Debug.LogError(nameof(alertPanel) + " is null");
        //        return;
        //    }

        //    alertPanel.Display_Panel(
        //        LocalizationHelper.GetLocalizedStr(
        //         LocalizationHelper.CONTENT_AVAILABLE_ONLY_IN_ARABIC_WARNING_LOCALIZATION_PHRASE));
        //}
    }
}