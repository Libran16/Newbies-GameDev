using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTips : MonoBehaviour
{
    public string message;

    private void OnMouseEnter()
    {
        ToolTipManager._instance.SetAndShowToolTip(message);
    }

    private void OnMouseExit()
    {
        ToolTipManager._instance.HideToolTip();
    }
}
