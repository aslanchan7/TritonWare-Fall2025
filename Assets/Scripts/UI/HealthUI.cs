using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Slider healthBar;

    void Update()
    {
        float sliderVal = HealthManager.Instance.health / HealthManager.Instance.maxHealth;
        healthBar.value = sliderVal;
    }
}
