using TMPro;
using UnityEngine;

public class TextPopUp : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    [SerializeField] private Vector2 minDisplacement;
    [SerializeField] private Vector2 maxDisplacement;
    [SerializeField] private float time;

    void Start()
    {
        Vector2 randomDisplacement = new(Random.Range(minDisplacement.x, maxDisplacement.x), Random.Range(minDisplacement.y, maxDisplacement.y));
        Vector2 finalPos = (Vector2)transform.position + randomDisplacement;
        gameObject.LeanMoveLocalX(finalPos.x, time).setEaseInCirc();
        gameObject.LeanMoveLocalY(finalPos.y, time);
        LeanTween.value(0f, 1f, time/2f).setOnUpdate((float val) =>
        {
            Color newColor = new(text.color.r, text.color.g, text.color.b, val);
            text.color = newColor;
        }).setOnComplete(() =>
        {
            LeanTween.value(1f, 0f, time / 2f).setOnUpdate((float val) =>
            {
                Color newColor = new(text.color.r, text.color.g, text.color.b, val);
                text.color = newColor;
            }).setOnComplete(DestroySelf);
        });
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
