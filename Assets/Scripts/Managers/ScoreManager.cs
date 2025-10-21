using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    
    public int Score;
    public int CurrentCombo;
    public float ScoreMult = 1f;
    [SerializeField] private int scorePerPerfect;
    [SerializeField] private int scorePerHit;
    [SerializeField] private GameObject textPopUpPrefab;

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

    private void InstantiateTextPopUp(string text, Color color, Vector3 pos)
    {
        GameObject instantiated = Instantiate(textPopUpPrefab);
        instantiated.GetComponent<TextMeshPro>().text = text;
        instantiated.GetComponent<TextMeshPro>().color = color;
        instantiated.transform.position = pos;
    }

    public void NoteHit(bool perfectHit, Transform button)
    {
        if (perfectHit)
        {
            ScoreMult = ScoreMult >= 3.8f ? 4f : ScoreMult + 0.2f;
            int score = (int)(scorePerPerfect * ScoreMult);
            UpdateScore(score);

            InstantiateTextPopUp("Perfect!", new Color(1f, 0.84f, 0f), button.position);
        }
        else
        {
            ScoreMult = ScoreMult >= 3.9f ? 4f : ScoreMult + 0.1f;
            int score = (int)(scorePerHit * ScoreMult);
            UpdateScore(score);

            InstantiateTextPopUp("Good!", new Color(0.56f, 0.93f, 0.56f), button.position);
        }
        
        IncrementCurrentCombo();
    }

    public void NoteMissed(Transform button)
    {
        HealthManager.Instance.LoseHealth();

        ScoreMult = ScoreMult > 1.3f ? ScoreMult - 0.3f : 1f;

        InstantiateTextPopUp("Miss!", new Color(1f, 0.28f, 0.3f), button.position);

        ResetCombo();
    }
}
