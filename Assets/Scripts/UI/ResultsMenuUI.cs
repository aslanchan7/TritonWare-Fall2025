using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ResultsMenuUI : MonoBehaviour
{
    [Header("Background")]
    [SerializeField] RectTransform backgroundLeft, backgroundRight;
    [SerializeField] float backgroundAnimTime;

    [Header("Title")]
    [SerializeField] RectTransform title;
    [SerializeField] float titleAnimTime;
    [SerializeField] float titleInitScale;

    [Header("Ranking")]
    [SerializeField] RectTransform ranking;
    [SerializeField] float rankingAnimTime;
    [SerializeField] float rankingInitScale;

    [Header("Game Stats")]
    [SerializeField] RectTransform stats;
    [SerializeField] float statsAnimTime;

    [Header("Leaderboard")]
    [SerializeField] RectTransform leaderboard;
    [SerializeField] float leaderboardAnimTime;

    public void StartAnimation()
    {
        // Setup to initial values
        backgroundLeft.sizeDelta = new(0, backgroundLeft.sizeDelta.y);
        backgroundRight.sizeDelta = new(0, backgroundLeft.sizeDelta.y);

        title.gameObject.SetActive(false);
        title.GetComponent<TextMeshProUGUI>().alpha = 0f;

        ranking.gameObject.SetActive(false);


        stats.gameObject.SetActive(false);
        for (int i = 0; i < stats.childCount; i++)
        {
            stats.GetChild(i).gameObject.SetActive(false);
        }

        leaderboard.gameObject.SetActive(false);
        leaderboard.GetComponent<CanvasGroup>().alpha = 0f;

        // background closes like a door
        LeanTween.value(0f, 960f, backgroundAnimTime).setOnUpdate((float val) =>
        {
            backgroundLeft.sizeDelta = new(val, backgroundLeft.sizeDelta.y);
            backgroundRight.sizeDelta = new(val, backgroundRight.sizeDelta.y);
        }).setEaseInCubic().setOnComplete(() =>
        {
            // Title fades in
            title.gameObject.SetActive(true);
            StartCoroutine(StartTitleAnim());
        });
    }

    public IEnumerator StartTitleAnim()
    {
        yield return new WaitForSeconds(0.25f);
        LeanTween.value(0f, 1f, titleAnimTime).setOnUpdate((float val) =>
        {
            title.GetComponent<TextMeshProUGUI>().alpha = val;
        }).setEaseInExpo();

        LeanTween.value(titleInitScale, 1f, titleAnimTime).setOnUpdate((float val) =>
        {
            title.localScale = new(val, val, val);
        }).setEaseInExpo().setOnComplete(() =>
        {
            // Stats anim starts
            stats.gameObject.SetActive(true);
            StartCoroutine(StartStatsAnim());
        });
    }

    public IEnumerator StartStatsAnim()
    {
        yield return new WaitForSeconds(0.25f);
        // Final Score
        DisplayStat(stats.Find("FinalScoreText"), stats.Find("FinalScore"), ScoreManager.Instance.Score);
        yield return new WaitForSeconds(statsAnimTime);

        // Perfect Hits
        DisplayStat(stats.Find("PerfectNotesText"), stats.Find("PerfectNotes"), ScoreManager.Instance.PerfectHits);
        yield return new WaitForSeconds(statsAnimTime);

        // Good Hits
        DisplayStat(stats.Find("GoodNotesText"), stats.Find("GoodNotes"), ScoreManager.Instance.GoodHits);
        yield return new WaitForSeconds(statsAnimTime);

        // Missed Notes
        DisplayStat(stats.Find("MissedNotesText"), stats.Find("MissedNotes"), ScoreManager.Instance.MissedNotes);
        yield return new WaitForSeconds(statsAnimTime);

        // Max Combo
        DisplayStat(stats.Find("ComboText"), stats.Find("Combo"), ScoreManager.Instance.MaxCombo);
        yield return new WaitForSeconds(statsAnimTime);

        StartGradeAnim();
    }

    void DisplayStat(Transform text, Transform stat, int value)
    {
        text.gameObject.SetActive(true);
        stat.gameObject.SetActive(true);
        LeanTween.value(0f, value, statsAnimTime).setOnUpdate((float val) =>
        {
            stat.GetComponent<TextMeshProUGUI>().text = $"{val:N0}";
        });

        LeanTween.value(0f, 1f, titleAnimTime).setOnUpdate((float val) =>
        {
            text.GetComponent<TextMeshProUGUI>().alpha = val;
            stat.GetComponent<TextMeshProUGUI>().alpha = val;
        });
    }

    void StartGradeAnim()
    {
        ranking.GetComponent<CanvasGroup>().alpha = 0f;
        ranking.gameObject.SetActive(true);
        LeanTween.value(0f, 1f, rankingAnimTime).setOnUpdate((float val) =>
        {
            ranking.GetComponent<CanvasGroup>().alpha = val;
        }).setEaseInExpo();

        LeanTween.value(rankingInitScale, 1f, rankingAnimTime).setOnUpdate((float val) =>
        {
            ranking.localScale = new(val, val, val);
        }).setEaseInExpo().setOnComplete(() =>
        {
            // Leaderboard Animation
            StartCoroutine(StartLeaderboardAnim());
        });
    }
    
    IEnumerator StartLeaderboardAnim()
    {
        yield return new WaitForSeconds(0.5f);
        leaderboard.gameObject.SetActive(true);
        LeanTween.value(0f, 1f, leaderboardAnimTime).setOnUpdate((float val) =>
        {
            leaderboard.GetComponent<CanvasGroup>().alpha = val;
        });
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            StartAnimation();
        }
    }
}
