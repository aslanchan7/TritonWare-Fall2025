using System.Collections;
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

    [Header("Game Stats")]
    [SerializeField] RectTransform stats;
    [SerializeField] float statsAnimTime;
    [SerializeField] float statsInitScale;

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
        stats.Find("FinalScoreText").gameObject.SetActive(false);
        stats.Find("FinalScore").gameObject.SetActive(false);
        
        leaderboard.gameObject.SetActive(false);

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
        stats.Find("FinalScoreText").gameObject.SetActive(true);
        stats.Find("FinalScore").gameObject.SetActive(true);
        LeanTween.value(0f, ScoreManager.Instance.Score, statsAnimTime).setOnUpdate((float val) =>
        {
            stats.Find("FinalScore").GetComponent<TextMeshProUGUI>().text = $"{val:N0}";
        });

        LeanTween.value(0f, 1f, titleAnimTime).setOnUpdate((float val) =>
        {
            stats.Find("FinalScoreText").GetComponent<TextMeshProUGUI>().alpha = val;
            stats.Find("FinalScore").GetComponent<TextMeshProUGUI>().alpha = val;
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
