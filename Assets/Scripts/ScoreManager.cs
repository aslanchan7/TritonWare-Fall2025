using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    
    public int Score;
    public int CurrentCombo;
    public float ScoreMult = 1f;
    [SerializeField] private int scorePerPerfect;
    [SerializeField] private int scorePerHit;

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

    private void UpdateScore(int score)
    {
        Score += score;
    }

    private void IncrementCurrentCombo()
    {
        CurrentCombo++;
    }

    private void ResetCombo()
    {
        CurrentCombo = 0;
    }

    public void NoteHit(bool perfectHit)
    {
        if (perfectHit)
        {
            ScoreMult = ScoreMult >= 3.8f ? 4f : ScoreMult + 0.2f;
            int score = (int)(scorePerPerfect * ScoreMult);
            UpdateScore(score);
        }
        else
        {
            ScoreMult = ScoreMult >= 3.9f ? 4f : ScoreMult + 0.1f;
            int score = (int)(scorePerHit * ScoreMult);
            UpdateScore(score);
        }
        
        IncrementCurrentCombo();
    }
    
    public void NoteMissed()
    {
        ScoreMult = ScoreMult > 1.3f ? ScoreMult - 0.3f : 1f;
    
        ResetCombo();
    }
}
