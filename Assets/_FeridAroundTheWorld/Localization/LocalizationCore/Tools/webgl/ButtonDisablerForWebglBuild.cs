using System.Collections.Generic;
using UnityEngine;

public class ButtonDisablerForWebglBuild : MonoBehaviour
{
    [SerializeField] private List<GameObject> Button = new List<GameObject>();

    private void Awake()
    {
#if UNITY_WEBGL
        DisableButton();
#endif
    }

    private void DisableButton()
    {
        if (Button == null)
        {
            Debug.LogError(nameof(Button) + " is null");
            return;
        }

        if (Button.Count == 0)
        {
            return;
        }

        for (int i = 0; i < Button.Count; i++)
        {
            if (Button[i] == null)
            {
                Debug.LogError($"Button[{i}] is null");
                continue;
            }

            Button[i].SetActive(false);
        }
    }
}