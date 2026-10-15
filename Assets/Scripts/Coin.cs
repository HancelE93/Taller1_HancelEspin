
using UnityEngine;

public class Coin : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = GetComponentInParent<AudioSource>();

        if (audioSource == null)
            audioSource = GetComponentInChildren<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") ||
            other.transform.root.CompareTag("Player"))
        {
            CoinUI coinUI = FindFirstObjectByType<CoinUI>();

            if (coinUI != null)
            {
                coinUI.AddCoin();

                if (audioSource != null && audioSource.clip != null)
                {
                    AudioSource.PlayClipAtPoint(
                        audioSource.clip,
                        transform.position,
                        1f
                    );
                }
                else
                {
                    Debug.LogWarning(
                        "No se encontró el sonido de la moneda."
                    );
                }

                Destroy(transform.root.gameObject);
            }
        }
    }
}
