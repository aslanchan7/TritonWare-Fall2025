using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    
    public int Score;
    public int CurrentCombo;

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
            Debug.Log("Perfect!");
        }
        else
        {
            Debug.Log("Note Hit!");
        }
        
        IncrementCurrentCombo();
        UpdateScore(100);
    }
    
    public void NoteMissed()
    {
        Debug.Log("Note Missed!");
        
        ResetCombo();
    }
}
