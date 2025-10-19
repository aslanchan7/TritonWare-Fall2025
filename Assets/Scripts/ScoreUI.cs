using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI comboText;

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
    }

    void Update()
    {
        scoreText.text = "Score: " + ScoreManager.Instance.Score;

        if(ScoreManager.Instance.CurrentCombo != 0)
        {
            comboText.text = "x" + ScoreManager.Instance.CurrentCombo;
        } else
        {
            comboText.text = "";
        }
    }
}
