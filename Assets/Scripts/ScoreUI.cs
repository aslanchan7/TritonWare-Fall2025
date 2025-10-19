using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI comboText;
    [SerializeField] TextMeshProUGUI multText;

    [Header("Tweening")]
    [SerializeField] float scaleTime;
    [SerializeField] float countTime;
    private int currScore;
    
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
        UpdateScoreText();

        UpdateComboText();

        multText.text = $"Multiplier: x{ScoreManager.Instance.ScoreMult:F1}";
    }

    void UpdateScoreText()
    {
        string newText = $"Score: {ScoreManager.Instance.Score}";
        if(scoreText.text != newText)
        {
            // scoreText.text = "Score: " + ScoreManager.Instance.Score;
            // StopCoroutine(TextCountAnimation(scoreText, ScoreManager.Instance.Score));

            StartCoroutine(TextCountAnimation(scoreText, ScoreManager.Instance.Score));
        }
    }

    void UpdateComboText()
    {
        if (ScoreManager.Instance.CurrentCombo != 0)
        {
            string newText = $"x{ScoreManager.Instance.CurrentCombo}";
            if (comboText.text != newText)
            {
                comboText.text = newText;
                StopCoroutine(TextScale(comboText));
                StartCoroutine(TextScale(comboText));
            }
        }
        else
        {
            comboText.text = "";
        }
    }

    IEnumerator TextScale(TextMeshProUGUI textObject)
    {
        textObject.rectTransform.localScale = new(1.1f, 1.1f, 1.1f);

        float time = 0f;
        while (time < 1.0f)
        {
            float temp = Mathf.Lerp(1.1f, 1.0f, time);
            textObject.rectTransform.localScale = new(temp, temp, temp);

            time += Time.deltaTime / scaleTime;
            yield return null;
        }
        yield return null;
    }
    
    private IEnumerator TextCountAnimation(TextMeshProUGUI textObject, int finalScore)
    {
        // int currScore = this.currScore;
        // int increment = (finalScore - currScore) / AnimIncrements;
        while (currScore <= finalScore)
        {
            currScore += 1;
            textObject.text = $"Score: {currScore}";
            yield return new WaitForSeconds(countTime / (float)(finalScore - currScore));
        }
        textObject.text = $"Score: {finalScore}"; ;
    }
}
