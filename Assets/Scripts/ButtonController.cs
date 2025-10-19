using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public KeyCode KeyToPress;

    private Vector3 initScale;
    private NoteObject collidingNote;
    [SerializeField] private float perfectHitWindow = 0.1f;

    void Start()
    {
        initScale = transform.localScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyToPress))
        {
            transform.localScale = Vector3.one;

            // Handle note hitting logic
            if (collidingNote == null)
            {
                ScoreManager.Instance.NoteMissed();
            }
            else
            {
                bool perfectHit = Mathf.Abs(collidingNote.transform.position.y - transform.position.y) <= perfectHitWindow;
                ScoreManager.Instance.NoteHit(perfectHit);

                collidingNote.gameObject.SetActive(false);
                collidingNote = null;
            }
        }

        if (Input.GetKeyUp(KeyToPress))
        {
            transform.localScale = initScale;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out NoteObject noteObject))
        {
            collidingNote = noteObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out NoteObject noteObject))
        {
            collidingNote = null;

            if(noteObject.gameObject.activeSelf)
            {
                ScoreManager.Instance.NoteMissed();
            }
        }
    }
}
