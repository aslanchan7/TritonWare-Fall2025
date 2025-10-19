using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public KeyCode KeyToPress;

    private Vector3 initScale;
    private NoteObject collidingNote;
    private Collider2D currCollision;
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
                if(!collidingNote.IsLongNote)
                {
                    bool perfectHit = Mathf.Abs(collidingNote.transform.position.y - transform.position.y) <= perfectHitWindow;
                    ScoreManager.Instance.NoteHit(perfectHit);

                    collidingNote.gameObject.SetActive(false);
                    collidingNote = null;                    
                } else
                {
                    bool perfectHit = Mathf.Abs(collidingNote.StartCollider.transform.position.y - transform.position.y) <= perfectHitWindow;
                    ScoreManager.Instance.NoteHit(perfectHit);                    
                }
            }
        }

        if (Input.GetKeyUp(KeyToPress))
        {
            transform.localScale = initScale;

            // Handle releasing on long notes
            if (collidingNote != null)
            {
                if (currCollision == collidingNote.EndCollider)
                {
                    bool perfectHit = Mathf.Abs(collidingNote.EndCollider.transform.position.y - transform.position.y) <= perfectHitWindow;
                    ScoreManager.Instance.NoteHit(perfectHit);

                    collidingNote.gameObject.SetActive(false);
                    collidingNote = null;
                }
                else
                {
                    ScoreManager.Instance.NoteMissed();

                    collidingNote.gameObject.SetActive(false);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out NoteObject noteObject))
        {
            collidingNote = noteObject;
        }
        else if (collision.transform.parent.TryGetComponent(out NoteObject longNote))
        {
            if (longNote.StartCollider == collision)
            {
                collidingNote = longNote;
            }
        }

        currCollision = collision;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out NoteObject noteObject))
        {
            collidingNote = null;

            if (noteObject.gameObject.activeSelf)
            {
                ScoreManager.Instance.NoteMissed();
            }
        }
        else if (collision.transform.parent.TryGetComponent(out NoteObject longNote))
        {
            if (longNote.StartCollider == collision && !Input.GetKey(KeyToPress))
            {
                ScoreManager.Instance.NoteMissed();
            } else if(longNote.EndCollider == collision && Input.GetKey(KeyToPress))
            {
                ScoreManager.Instance.NoteMissed();
                collidingNote = null;
            }
        }

        currCollision = null;
    }
}
