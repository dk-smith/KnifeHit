using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelController : MonoBehaviour
{

    [SerializeField] private TextFormat[] texts;

    public void SetTexts(params string[] texts)
    {
        for (int i = 0; i < texts.Length; i++)
        {
            this.texts[i].SetText(texts[i]);
        }
    }

}
