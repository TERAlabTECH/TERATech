using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonMusica : MonoBehaviour
{
    // =====================
    // AUDIO SOURCES
    // =====================
    [Header("Audio - Bajo")]
    [SerializeField] AudioSource music11;
    [SerializeField] AudioSource music21;
    [SerializeField] AudioSource music31;
    [SerializeField] AudioSource music41;

    [Header("Audio - Complemento")]
    [SerializeField] AudioSource music12;
    [SerializeField] AudioSource music22;
    [SerializeField] AudioSource music32;
    [SerializeField] AudioSource music42;

    [Header("Audio - Percusion")]
    [SerializeField] AudioSource music13;
    [SerializeField] AudioSource music23;
    [SerializeField] AudioSource music33;
    [SerializeField] AudioSource music43;

    [Header("Audio - Melodia")]
    [SerializeField] AudioSource music14;
    [SerializeField] AudioSource music24;
    [SerializeField] AudioSource music34;
    [SerializeField] AudioSource music44;

    // =====================
    // TRANSFORMS
    // =====================
    [Header("Transforms - Bajo")]
    [SerializeField] Transform tbtn11, tbtn21, tbtn31, tbtn41;

    [Header("Transforms - Complemento")]
    [SerializeField] Transform tbtn12, tbtn22, tbtn32, tbtn42;

    [Header("Transforms - Percusion")]
    [SerializeField] Transform tbtn13, tbtn23, tbtn33, tbtn43;

    [Header("Transforms - Melodia")]
    [SerializeField] Transform tbtn14, tbtn24, tbtn34, tbtn44;

    // =====================
    // RENDERERS
    // =====================
    [Header("Renderers - Bajo")]
    [SerializeField] Renderer rbtn11, rbtn21, rbtn31, rbtn41;

    [Header("Renderers - Complemento")]
    [SerializeField] Renderer rbtn12, rbtn22, rbtn32, rbtn42;

    [Header("Renderers - Percusion")]
    [SerializeField] Renderer rbtn13, rbtn23, rbtn33, rbtn43;

    [Header("Renderers - Melodia")]
    [SerializeField] Renderer rbtn14, rbtn24, rbtn34, rbtn44;

    // =====================
    // CONFIGURACION
    // =====================
    [Header("Emision")]
    [SerializeField] Color colorEmision = new Color(1f, 0.9f, 0.2f, 1f);
    [SerializeField] float emisionIntensidad = 3f;

    [Header("Animacion boton")]
    [SerializeField] float presionY = 0.05f;
    [SerializeField] float animDuracion = 0.08f;

    // =====================
    // ESTADOS
    // =====================
    bool play11, play21, play31, play41;
    bool play12, play22, play32, play42;
    bool play13, play23, play33, play43;
    bool play14, play24, play34, play44;

    Dictionary<Transform, Vector3> posicionesOriginales = new Dictionary<Transform, Vector3>();

    // =====================
    // START
    // =====================
    void Start()
    {
        foreach (var t in new Transform[]{
            tbtn11, tbtn21, tbtn31, tbtn41,
            tbtn12, tbtn22, tbtn32, tbtn42,
            tbtn13, tbtn23, tbtn33, tbtn43,
            tbtn14, tbtn24, tbtn34, tbtn44
        })
            if (t != null) posicionesOriginales[t] = t.localPosition;
    }

    // =====================
    // HELPERS
    // =====================
    void SetVisual(Renderer r, Transform t, bool activo)
    {
        if (r != null)
        {
            // Solo toca la emision, no cambia el color base del material
            if (activo)
            {
                r.material.EnableKeyword("_EMISSION");
                r.material.SetColor("_EmissionColor", colorEmision * emisionIntensidad);
            }
            else
            {
                r.material.DisableKeyword("_EMISSION");
                r.material.SetColor("_EmissionColor", Color.black);
            }
        }

        if (t != null) StartCoroutine(AnimarBoton(t, activo));
    }

    IEnumerator AnimarBoton(Transform btn, bool activo)
    {
        if (!posicionesOriginales.ContainsKey(btn)) yield break;

        Vector3 origen = posicionesOriginales[btn];
        Vector3 destino = activo ? origen + new Vector3(0, -presionY, 0) : origen;
        Vector3 inicio = btn.localPosition;

        float elapsed = 0f;
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime / animDuracion;
            btn.localPosition = Vector3.Lerp(inicio, destino, elapsed);
            yield return null;
        }
        btn.localPosition = destino;
    }

    void SetVolume(AudioSource a, bool activo)
    {
        if (a != null) a.volume = activo ? 1f : 0f;
    }

    // =====================
    // BAJO
    // =====================
    public void onMusic11()
    {
        play21 = false; SetVolume(music21, false); SetVisual(rbtn21, tbtn21, false);
        play31 = false; SetVolume(music31, false); SetVisual(rbtn31, tbtn31, false);
        play41 = false; SetVolume(music41, false); SetVisual(rbtn41, tbtn41, false);
        play11 = !play11;
        SetVolume(music11, play11);
        SetVisual(rbtn11, tbtn11, play11);
    }
    public void onMusic21()
    {
        play11 = false; SetVolume(music11, false); SetVisual(rbtn11, tbtn11, false);
        play31 = false; SetVolume(music31, false); SetVisual(rbtn31, tbtn31, false);
        play41 = false; SetVolume(music41, false); SetVisual(rbtn41, tbtn41, false);
        play21 = !play21;
        SetVolume(music21, play21);
        SetVisual(rbtn21, tbtn21, play21);
    }
    public void onMusic31()
    {
        play11 = false; SetVolume(music11, false); SetVisual(rbtn11, tbtn11, false);
        play21 = false; SetVolume(music21, false); SetVisual(rbtn21, tbtn21, false);
        play41 = false; SetVolume(music41, false); SetVisual(rbtn41, tbtn41, false);
        play31 = !play31;
        SetVolume(music31, play31);
        SetVisual(rbtn31, tbtn31, play31);
    }
    public void onMusic41()
    {
        play11 = false; SetVolume(music11, false); SetVisual(rbtn11, tbtn11, false);
        play21 = false; SetVolume(music21, false); SetVisual(rbtn21, tbtn21, false);
        play31 = false; SetVolume(music31, false); SetVisual(rbtn31, tbtn31, false);
        play41 = !play41;
        SetVolume(music41, play41);
        SetVisual(rbtn41, tbtn41, play41);
    }

    // =====================
    // COMPLEMENTO
    // =====================
    public void onMusic12()
    {
        play22 = false; SetVolume(music22, false); SetVisual(rbtn22, tbtn22, false);
        play32 = false; SetVolume(music32, false); SetVisual(rbtn32, tbtn32, false);
        play42 = false; SetVolume(music42, false); SetVisual(rbtn42, tbtn42, false);
        play12 = !play12;
        SetVolume(music12, play12);
        SetVisual(rbtn12, tbtn12, play12);
    }
    public void onMusic22()
    {
        play12 = false; SetVolume(music12, false); SetVisual(rbtn12, tbtn12, false);
        play32 = false; SetVolume(music32, false); SetVisual(rbtn32, tbtn32, false);
        play42 = false; SetVolume(music42, false); SetVisual(rbtn42, tbtn42, false);
        play22 = !play22;
        SetVolume(music22, play22);
        SetVisual(rbtn22, tbtn22, play22);
    }
    public void onMusic32()
    {
        play12 = false; SetVolume(music12, false); SetVisual(rbtn12, tbtn12, false);
        play22 = false; SetVolume(music22, false); SetVisual(rbtn22, tbtn22, false);
        play42 = false; SetVolume(music42, false); SetVisual(rbtn42, tbtn42, false);
        play32 = !play32;
        SetVolume(music32, play32);
        SetVisual(rbtn32, tbtn32, play32);
    }
    public void onMusic42()
    {
        play12 = false; SetVolume(music12, false); SetVisual(rbtn12, tbtn12, false);
        play22 = false; SetVolume(music22, false); SetVisual(rbtn22, tbtn22, false);
        play32 = false; SetVolume(music32, false); SetVisual(rbtn32, tbtn32, false);
        play42 = !play42;
        SetVolume(music42, play42);
        SetVisual(rbtn42, tbtn42, play42);
    }

    // =====================
    // PERCUSION
    // =====================
    public void onMusic13()
    {
        play23 = false; SetVolume(music23, false); SetVisual(rbtn23, tbtn23, false);
        play33 = false; SetVolume(music33, false); SetVisual(rbtn33, tbtn33, false);
        play43 = false; SetVolume(music43, false); SetVisual(rbtn43, tbtn43, false);
        play13 = !play13;
        SetVolume(music13, play13);
        SetVisual(rbtn13, tbtn13, play13);
    }
    public void onMusic23()
    {
        play13 = false; SetVolume(music13, false); SetVisual(rbtn13, tbtn13, false);
        play33 = false; SetVolume(music33, false); SetVisual(rbtn33, tbtn33, false);
        play43 = false; SetVolume(music43, false); SetVisual(rbtn43, tbtn43, false);
        play23 = !play23;
        SetVolume(music23, play23);
        SetVisual(rbtn23, tbtn23, play23);
    }
    public void onMusic33()
    {
        play13 = false; SetVolume(music13, false); SetVisual(rbtn13, tbtn13, false);
        play23 = false; SetVolume(music23, false); SetVisual(rbtn23, tbtn23, false);
        play43 = false; SetVolume(music43, false); SetVisual(rbtn43, tbtn43, false);
        play33 = !play33;
        SetVolume(music33, play33);
        SetVisual(rbtn33, tbtn33, play33);
    }
    public void onMusic43()
    {
        play13 = false; SetVolume(music13, false); SetVisual(rbtn13, tbtn13, false);
        play23 = false; SetVolume(music23, false); SetVisual(rbtn23, tbtn23, false);
        play33 = false; SetVolume(music33, false); SetVisual(rbtn33, tbtn33, false);
        play43 = !play43;
        SetVolume(music43, play43);
        SetVisual(rbtn43, tbtn43, play43);
    }

    // =====================
    // MELODIA
    // =====================
    public void onMusic14()
    {
        play24 = false; SetVolume(music24, false); SetVisual(rbtn24, tbtn24, false);
        play34 = false; SetVolume(music34, false); SetVisual(rbtn34, tbtn34, false);
        play44 = false; SetVolume(music44, false); SetVisual(rbtn44, tbtn44, false);
        play14 = !play14;
        SetVolume(music14, play14);
        SetVisual(rbtn14, tbtn14, play14);
    }
    public void onMusic24()
    {
        play14 = false; SetVolume(music14, false); SetVisual(rbtn14, tbtn14, false);
        play34 = false; SetVolume(music34, false); SetVisual(rbtn34, tbtn34, false);
        play44 = false; SetVolume(music44, false); SetVisual(rbtn44, tbtn44, false);
        play24 = !play24;
        SetVolume(music24, play24);
        SetVisual(rbtn24, tbtn24, play24);
    }
    public void onMusic34()
    {
        play14 = false; SetVolume(music14, false); SetVisual(rbtn14, tbtn14, false);
        play24 = false; SetVolume(music24, false); SetVisual(rbtn24, tbtn24, false);
        play44 = false; SetVolume(music44, false); SetVisual(rbtn44, tbtn44, false);
        play34 = !play34;
        SetVolume(music34, play34);
        SetVisual(rbtn34, tbtn34, play34);
    }
    public void onMusic44()
    {
        play14 = false; SetVolume(music14, false); SetVisual(rbtn14, tbtn14, false);
        play24 = false; SetVolume(music24, false); SetVisual(rbtn24, tbtn24, false);
        play34 = false; SetVolume(music34, false); SetVisual(rbtn34, tbtn34, false);
        play44 = !play44;
        SetVolume(music44, play44);
        SetVisual(rbtn44, tbtn44, play44);
    }
}