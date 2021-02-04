using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gamePlayPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject applePanel;
    [SerializeField] private Toggle vibrationToggle;
    private List<GameObject> panels = new List<GameObject>();

    public GameObject StartPanel { get => startPanel; set => startPanel = value; }
    public GameObject GamePlayPanel { get => gamePlayPanel; set => gamePlayPanel = value; }
    public GameObject GameOverPanel { get => gameOverPanel; set => gameOverPanel = value; }
    public GameObject ApplePanel { get => applePanel; set => applePanel = value; }

    private void Awake()
    {
        panels.Add(startPanel);
        panels.Add(gamePlayPanel);
        panels.Add(gameOverPanel);
        panels.Add(applePanel);
    }

    public void ShowStartPanel(PlayerData data)
    {
        ShowPanel(startPanel);
        applePanel.GetComponent<PanelController>().SetTexts(data.Apples.ToString());
        startPanel.GetComponent<PanelController>().SetTexts(data.ScoreStage.ToString(), data.ScoreRecord.ToString());
    }

    public void ShowPanel(GameObject panel)
    {
        foreach (var item in panels)
            if (item != applePanel) item.SetActive(item == panel);
    }

    public void SetVibration(bool vibration)
    {
        vibrationToggle.isOn = vibration;
    }
}
