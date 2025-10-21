using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance;

    public float health;
    public float maxHealth;
    [SerializeField] float healthLossPerMiss;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        if (maxHealth == 0f)
        {
            Debug.LogWarning("Max Health not set");
        }

        health = maxHealth;
    }

    public void LoseHealth()
    {
        health -= healthLossPerMiss;

        if (health < 0f)
        {
            health = 0f;
            ResultsManager.Instance.ShowResultsMenu();
        }
    }
}
