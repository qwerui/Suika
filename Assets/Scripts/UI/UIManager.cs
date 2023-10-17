using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("GameStart")]
    public TMP_Text countdownText;
    public RectTransform startPanel;
    public GameObject startObject;
    
    int countdown;

    [Header("HideScreen")]
    public Image hidePanel;
    public GameObject gameoverPanel;

    Color hidePanelColor;

    [Header("Pause")]
    public GameObject pausePanel;

    [Header("Ranking")]
    public ScoreManager scoreManager;
    public GameObject rankingObject;
    public List<Text> topThree;
    public Text latest;

    [Header("Sound")]
    public AudioClip clickClip;
    public AudioClip countdownClip;
    public AudioClip goClip;

    Sequence startSequence;
    Sequence returnSequence;

    WaitForSeconds waitForSeconds;
    Vector3 shakeVector;

    private void Awake() 
    {
        hidePanelColor = new Color(0,0,0,0.8f);
        waitForSeconds = new WaitForSeconds(1.0f);
        countdown = 3;
        shakeVector = new Vector3(0, 0, 45);

        startSequence = DOTween.Sequence();
        returnSequence = DOTween.Sequence();

        startSequence
        .SetAutoKill(false)
        .Append(startPanel.DOAnchorPosY(Screen.height, 1.0f))
        .OnComplete(()=>{
            ShowCountdown();
            });

        returnSequence
        .SetAutoKill(false)
        .Append(startPanel.DOAnchorPosY(0, 1.0f));
    }

    public void ShowGameOver()
    {
        hidePanel.color = hidePanel.color = hidePanelColor;
        gameoverPanel.SetActive(true);
    }

    void ShowCountdown()
    {
        hidePanel.color = hidePanelColor;
        countdownText.enabled = true;
        countdownText.SetText(countdown.ToString());

        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        for(int i=countdown; i > 0; i--)
        {
            countdownText.SetText(i.ToString());
            countdownText.transform.DOShakeRotation(1.0f, shakeVector, 10, 90, false);
            SoundQueue.GetSoundQueue().PlaySFX(countdownClip);
            yield return waitForSeconds;
        }

        countdownText.SetText("GO!");
        SoundQueue.GetSoundQueue().PlaySFX(goClip);

        yield return waitForSeconds;

        hidePanel.color = Color.clear;
        countdownText.enabled = false;
        GameManager.GetManager().OnGameStart?.Invoke();
    }

    public void OnPress_GameStart()
    {
        SoundQueue.GetSoundQueue().PlaySFX(clickClip);
        GameManager.GetManager().OnRequsetNewGame?.Invoke();
        countdownText.SetText(countdown.ToString());
        startSequence.Restart();
    }

    public void OnPress_Ranking()
    {
        SoundQueue.GetSoundQueue().PlaySFX(clickClip);
        int[] topThreeData = scoreManager.TopThree;
        for(int i=0;i<3;i++)
        {
            topThree[i].text = topThreeData[i].ToString();
        }
        latest.text = scoreManager.NowScore.ToString();
        startObject.SetActive(false);
        rankingObject.SetActive(true);
    }

    public void OnPress_Back()
    {
        SoundQueue.GetSoundQueue().PlaySFX(clickClip);
        startObject.SetActive(true);
        rankingObject.SetActive(false);
    }

    public void OnClick_Pause()
    {
        SoundQueue.GetSoundQueue().PlaySFX(clickClip);
        pausePanel.SetActive(true);
        hidePanel.color = hidePanelColor;
        GameManager.GetManager().OnPause.Invoke();
    }

    public void OnClick_Resume()
    {
        SoundQueue.GetSoundQueue().PlaySFX(clickClip);
        hidePanel.color = Color.clear;
        pausePanel.SetActive(false);
        GameManager.GetManager().OnResume.Invoke();
    }

    public void OnClick_Retry()
    {
        SoundQueue.GetSoundQueue().PlaySFX(clickClip);
        GameManager.GetManager().OnRequsetNewGame?.Invoke();
        ShowCountdown();
        gameoverPanel.SetActive(false);
    }

    public void OnClick_Retry_Pause()
    {
        SoundQueue.GetSoundQueue().PlaySFX(clickClip);
        GameManager.GetManager().OnRequsetNewGame?.Invoke();
        ShowCountdown();
        pausePanel.SetActive(false);
    }

    public void OnClick_Title()
    {
        SoundQueue.GetSoundQueue().PlaySFX(clickClip);
        gameoverPanel.SetActive(false);
        returnSequence.Restart();
    }

    public void OnClick_Title_Pause()
    {
        SoundQueue.GetSoundQueue().PlaySFX(clickClip);
        pausePanel.SetActive(false);
        returnSequence.Restart();
    }

}
