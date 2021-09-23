using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Painter_ColorBtn : MonoBehaviour
{

    public Painter_Manager painterMan;
    public Image myFrame,myColorImg;


    public Color Get_Color()
    {
        return myColorImg.color;
    }

    public void Color_Selection()
    {
         painterMan.Set_Current_Col(this);
    }


}
