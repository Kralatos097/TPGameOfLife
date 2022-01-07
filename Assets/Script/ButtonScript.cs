using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public int state = 0;
    public Image img;

    public void ChangeColor()
    {
        if (state == 0)
        {
            ToBlack();
        }
        else
        {
            ToWhite();
        }
    }
    
    public void ToWhite()
    {
        state = 0;
        img.color = Color.white;
    }
    
    public void ToBlack()
    {
        state = 1;
        img.color = Color.black;
    }
}
