using UnityEngine;

public class NoteScroller : MonoBehaviour
{
    public float BeatTempo;
    public bool hasStarted;
    private float noteSpeed;

    void Start()
    {
        noteSpeed = BeatTempo / 60f;
    }

    void Update()
    {
        if(hasStarted)
        {
            transform.position -= new Vector3(0f, noteSpeed * Time.deltaTime, 0f);
        }
    }
}
