using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePanelController : MonoBehaviour
{
    [SerializeField] TextFormat scoreText;
    [SerializeField] TextFormat stageText;
    [SerializeField] Transform knifeLeftPanel;
    [SerializeField] GameObject knifeLeftIcon;
    [SerializeField] Color[] knifeLeftColors;
    private List<GameObject> knifeIcons = new List<GameObject>();
    public void SetKnifeIcons(int amount)
    {
        foreach (Transform child in knifeLeftPanel.transform)
        {
            Destroy(child.gameObject);
        }
        knifeIcons.Clear();
        for (int i = 0; i < amount; i++)
        {
            knifeIcons.Add(Instantiate(knifeLeftIcon, knifeLeftPanel));
            knifeIcons[i].GetComponent<Image>().color = knifeLeftColors[0];
        }
    }

    public void UpdateKnifeIcons(int left)
    {
        knifeIcons[knifeIcons.Count - left].GetComponent<Image>().color = knifeLeftColors[1];
    }

    public void SetTexts(params string[] texts)
    {
        SetScoreText(texts[0]);
        SetStageText(texts[1]);
    }

    public void SetScoreText(string score)
    {
        scoreText.SetText(score);
    }

    public void SetStageText(string stage)
    {
        stageText.SetText(stage);
    }


}
