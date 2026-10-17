
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") ||
            other.transform.root.CompareTag("Player"))
        {
            CoinUI coinUI =
                FindFirstObjectByType<CoinUI>();

            if (coinUI != null)
            {
                coinUI.AddCoin();
            }

            // Sonido de moneda desde AudioManager
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.ReproducirSfx(
                    AudioManager.Instance.sonidoMoneda
                );
            }
            else
            {
                Debug.LogWarning(
                    "No se encontró el AudioManager."
                );
            }

            Destroy(transform.root.gameObject);
        }
    }
}
