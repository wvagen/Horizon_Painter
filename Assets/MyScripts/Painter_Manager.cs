using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Painter_Manager : MonoBehaviour
{
    public GameObject point;
    public Transform pointsContainer,colorsPaletteContainer,pensContainer;

    private List<Painter_ColorBtn> colorBtns = new List<Painter_ColorBtn>();
    private List<Painter_Pen> pens = new List<Painter_Pen>();

    private Painter_ColorBtn currentColorBtn;
    private Painter_Pen currentPen;
    private GameObject currentPoint;

    private bool canDraw = false;

    private void Start()
    {
        Set_Colors_Btn_List();
        Set_Pens_List();

        colorBtns[Random.Range(0, colorBtns.Count)].Color_Selection();
        pens[Random.Range(0, pens.Count)].Pen_Selection();
    }

    void Set_Colors_Btn_List()
    {
        for (int i = 0; i < colorsPaletteContainer.childCount; i++)
        {
            colorBtns.Add(colorsPaletteContainer.GetChild(i).GetComponent<Painter_ColorBtn>());
        }
    }

    void Set_Pens_List()
    {
        for (int i = 0; i < pensContainer.childCount; i++)
        {
            pens.Add(pensContainer.GetChild(i).GetComponent<Painter_Pen>());
        }
    }

    private void Update()
    {
       if (canDraw)
        {
            currentPoint.transform.position = Get_Mouse_Pos();
        }
    }


    public void Set_Current_Col(Painter_ColorBtn currentColorBtn)
    {
        if (this.currentColorBtn != null)
        {
            this.currentColorBtn.myFrame.enabled = false;
        }

        this.currentColorBtn = currentColorBtn;
        this.currentColorBtn.myFrame.enabled = true;
    }

    public void Set_Current_Pen(Painter_Pen currentPen)
    {
        if (this.currentPen != null)
        {
            this.currentPen.Pen_Selection_Sprite_Change(false);
        }

        this.currentPen = currentPen;
        this.currentPen.Pen_Selection_Sprite_Change(true);
    }


    public void Can_Draw()
    {
        canDraw = true;

        currentPoint = Instantiate(point, Vector2.zero, Quaternion.identity, pointsContainer);
        currentPoint.transform.position = Get_Mouse_Pos();
        currentPoint.GetComponent<TrailRenderer>().startColor = this.currentColorBtn.Get_Color();
        currentPoint.GetComponent<TrailRenderer>().endColor = this.currentColorBtn.Get_Color();

        currentPoint.GetComponent<TrailRenderer>().startWidth = this.currentPen.penSize;
        currentPoint.GetComponent<TrailRenderer>().endWidth = this.currentPen.penSize;
    }

    Vector2 Get_Mouse_Pos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void Cannot_Draw()
    {
        canDraw = false;
    }

}
