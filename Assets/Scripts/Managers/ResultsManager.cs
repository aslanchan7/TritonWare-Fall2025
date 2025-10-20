using UnityEngine;

public class ResultsManager : MonoBehaviour
{
    public static ResultsManager Instance;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        } else
        {
            Instance = this;
        }
    }
}
