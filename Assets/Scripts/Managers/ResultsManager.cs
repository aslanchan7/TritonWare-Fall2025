using UnityEngine;

public class ResultsManager : MonoBehaviour
{
    public static ResultsManager Instance;

    public ResultsMenuUI ResultsMenuUI;

    public int[] GradeThresholds = new int[5]; // Scoring more than GradeThreshold[0] gets a "C" grade

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void ShowResultsMenu()
    {
        if (ResultsMenuUI.gameObject.activeSelf) return;
        ResultsMenuUI.gameObject.SetActive(true);
        ResultsMenuUI.StartAnimation();
    }

    public Grade GetGrade()
    {
        int score = ScoreManager.Instance.Score;

        if(HealthManager.Instance.health <= 0f)
        {
            return Grade.F;
        }

        for (int i = 0; i < GradeThresholds.Length; i++)
        {
            if(score <= GradeThresholds[i])
            {
                // typecast int as Grade
                return (Grade)i;
            }
        }

        return Grade.SS;
    }
}

public enum Grade
{
    F, C, B, A, S, SS
}
