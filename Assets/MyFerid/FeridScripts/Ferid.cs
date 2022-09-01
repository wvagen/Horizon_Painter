using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class Ferid : MonoBehaviour
{

    public SpriteResolver upperAccessory;
    public SpriteResolver downAccessory;
    public Animator myAnim;

    public static bool isSkinChanged = false;

    bool isTalking = false;

    void Awake()
    {
        ChangeMySkin();
    }

    void ChangeMySkin()
    {
        upperAccessory.SetCategoryAndLabel("Upper_Accessory", "0");
        downAccessory.SetCategoryAndLabel("Down_Accessory", "0");

        myAnim.Play("NewClothes");
    }

    public void Talk()
    {
        isTalking = true;
    }

    public void Shut_Up()
    {
        isTalking = false;
    }

    void Update()
    {
        if (isSkinChanged)
        {
            ChangeMySkin();
            isSkinChanged = false;
        }
        myAnim.SetBool("isTalking", isTalking);
    }

}
