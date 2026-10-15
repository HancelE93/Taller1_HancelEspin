
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [Header("Corazones en pantalla")]
    public GameObject[] hearts;

    [Header("Vida del jugador")]
    public PlayerHealth playerHealth;

    private int previousHealth;

    void Start()
    {
        if (playerHealth == null)
        {
            playerHealth = FindFirstObjectByType<PlayerHealth>();
        }

        if (playerHealth == null)
        {
            Debug.LogError("No se encontró PlayerHealth en la escena.");
            return;
        }

        previousHealth = playerHealth.CurrentHealth;
        UpdateHearts();
    }

    void Update()
    {
        if (playerHealth == null)
            return;

        if (playerHealth.CurrentHealth != previousHealth)
        {
            previousHealth = playerHealth.CurrentHealth;
            UpdateHearts();
        }
    }

    void UpdateHearts()
    {
        if (hearts == null)
            return;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null)
            {
                hearts[i].SetActive(
                    i < playerHealth.CurrentHealth
                );
            }
        }
    }
}
