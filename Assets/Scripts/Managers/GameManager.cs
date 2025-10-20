using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    void Awake()
    {
        if (Instance != null & Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void EndGame()
    {
        if(HealthManager.Instance.health <= 0f)
        {
            // Lost Game
            
        } else
        {
            
        }
    }
}
