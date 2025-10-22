using UnityEngine;

public class NoteScroller : MonoBehaviour
{
    public float BeatTempo;
    private float noteSpeed;

    void Start()
    {
        noteSpeed = BeatTempo / 60f;
    }

    void Update()
    {
        if(GameManager.Instance.IsPlaying)
        {
            transform.position -= new Vector3(0f, noteSpeed * Time.deltaTime, 0f);
        }
    }
}
