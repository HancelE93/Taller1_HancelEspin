
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Fuentes de audio")]
    public AudioSource fuenteMusica;
    public AudioSource fuenteSFX;

    [Header("Música de fondo")]
    public AudioClip musicaFondo;

    [Header("Sonidos del jugador")]
    public AudioClip sonidoSalto;
    public AudioClip sonidoAtaque;
    public AudioClip sonidoDanioJugador;

    [Header("Sonidos de enemigos")]
    public AudioClip sonidoGolpe;
    public AudioClip sonidoMuerteEnemigo1;
    public AudioClip sonidoMuerteEnemigo2;

    [Header("Otros sonidos")]
    public AudioClip sonidoMoneda;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (fuenteMusica != null && musicaFondo != null)
        {
            fuenteMusica.clip = musicaFondo;
            fuenteMusica.loop = true;
            fuenteMusica.Play();
        }
    }

    public void ReproducirSfx(AudioClip clip)
    {
        if (clip != null && fuenteSFX != null)
        {
            fuenteSFX.PlayOneShot(clip);
        }
    }
}
