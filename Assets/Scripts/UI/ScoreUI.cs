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
    private bool isIncrementing = false;
    
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
        string newText = $"Score: {ScoreManager.Instance.Score:N0}";
        if(scoreText.text != newText && !isIncrementing)
        {
            TextCountAnimation(scoreText, ScoreManager.Instance.Score);
            isIncrementing = true;
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
    
    private void TextCountAnimation(TextMeshProUGUI textObject, int finalScore)
    {
        LeanTween.value(currScore, finalScore, countTime).setOnUpdate((float val) =>
        {
            currScore = (int)val;
            textObject.text = $"Score: {val:N0}";
        }).setOnComplete(() =>
        {
            isIncrementing = false;
        });
    }
}
