using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI comboText;
    [SerializeField] TextMeshProUGUI multText;

    void Start()
    {
        if (scoreText == null)
        {
            Debug.LogWarning("scoreText not set");
        }

        if (comboText == null)
        {
            Debug.LogWarning("comboText not set");
        }

        if (multText == null)
        {
            Debug.LogWarning("multText not set");
        }
    }

    void Update()
    {
        scoreText.text = "Score: " + ScoreManager.Instance.Score;

        if (ScoreManager.Instance.CurrentCombo != 0)
        {
            comboText.text = $"Combo: x{ScoreManager.Instance.CurrentCombo}";
        }
        else
        {
            comboText.text = "";
        }

        multText.text = $"Multiplier: x{ScoreManager.Instance.ScoreMult:F1}";
    }
}
