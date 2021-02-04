using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextFormat : MonoBehaviour
{
    [SerializeField] private string format;
    [SerializeField] private TextMeshProUGUI tmp;

    protected void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string text)
    {
        tmp.text = format == "" ? text : string.Format(format, text);
    }
}
