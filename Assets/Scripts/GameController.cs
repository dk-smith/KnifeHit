using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform woodSpawnPoint;
    [SerializeField] private Transform knifeSpawnPoint;
    [SerializeField] private GameObject knife;
    [SerializeField] private GameObject wood;
    [SerializeField] private UIController ui;
    private int score = 0;
    private int stage = 0;
    private PlayerData playerData;
    private DataSaver saver = new DataSaver();
    private bool gameOver = false;
    private GameObject currentWood;
    private bool vibration;

    public bool isVibration { get => vibration; set => vibration = value; }
    public bool isGameOver { get => gameOver; set => gameOver = value; }

    private void Awake()
    {
        Vibration.Init();
        playerData = saver.Load();
        ShowStartPanel();
        saver.Open();
        if (PlayerPrefs.HasKey("Vibration")) vibration = PlayerPrefs.GetInt("Vibration") > 0;
        else vibration = true;
        ui.SetVibration(vibration);
    }

    public void ToggleVibration(UnityEngine.UI.Toggle toggle)
    {
        vibration = toggle.isOn;
        PlayerPrefs.SetInt("Vibration", vibration ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ShowStartPanel()
    {
        if (currentWood) Destroy(currentWood);
        ui.ShowStartPanel(playerData);
    }

    public void StartGame()
    {
        gameOver = false;
        stage = 0;
        score = 0;
        ui.ShowPanel(ui.GamePlayPanel);
        ui.GamePlayPanel.GetComponent<GamePanelController>().SetTexts(score.ToString(), stage.ToString());
        StartCoroutine(ReloadLevel());
    }

    public void AddScore(int addScore)
    {
        score += addScore;
        if (score > playerData.ScoreRecord)
        {
            playerData.ScoreRecord = score;
        }
        saver.Save(playerData).Close();
        ui.GamePlayPanel.GetComponent<GamePanelController>().SetScoreText(score.ToString());
    }

    public void HitWood()
    {
        knifeSpawnPoint.GetComponent<KnifeSpawn>().Spawn();
        ui.GamePlayPanel.GetComponent<GamePanelController>()
            .UpdateKnifeIcons(currentWood.GetComponent<WoodController>().Health);
        AddScore(1);
    }

    public void HitApple()
    {
        playerData.Apples += 2;
        AddScore(2);
        ui.ApplePanel.GetComponent<PanelController>().SetTexts(playerData.Apples.ToString());
    }

    public void PassStage()
    {
        if (stage > playerData.ScoreStage)
        {
            playerData.ScoreStage = stage;
        }
        saver.Save(playerData).Close();
        StartCoroutine(ReloadLevel(1f));
        knifeSpawnPoint.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        gameOver = true;
        knifeSpawnPoint.gameObject.SetActive(false);
        ui.ShowPanel(ui.GameOverPanel);
        ui.GameOverPanel.GetComponent<PanelController>().SetTexts(score.ToString(), stage.ToString());
    }

    public IEnumerator ReloadLevel(float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        stage++;
        if (currentWood) Destroy(currentWood);
        currentWood = Instantiate(wood, woodSpawnPoint.position, Quaternion.identity, woodSpawnPoint);
        ui.GamePlayPanel.GetComponent<GamePanelController>().SetKnifeIcons(currentWood.GetComponent<WoodController>().StartHealth);
        ui.GamePlayPanel.GetComponent<GamePanelController>().SetStageText(stage.ToString());
        yield return new WaitWhile(() => currentWood.GetComponent<Animation>().isPlaying);
        knifeSpawnPoint.gameObject.SetActive(true);
    }
}
