using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject personaje;
    [SerializeField] private Button botonReintentar;
    [SerializeField] private Text puntos;
    private Personaje p;
    private Obstaculo obs;
    private bool estaJugando = true;
    private bool deNuevo;
    private Vector3 originalPos;
    private float totPuntos;
    private float velPuntos;
    

    public bool EstaJugando { get => estaJugando; set => estaJugando = value; }

    // Start is called before the first frame update
    void Start()
    {
        totPuntos = 0;
        velPuntos = 0.005f;
        estaJugando = true;
        deNuevo = false;
        p = personaje.gameObject.GetComponent<Personaje>();
        originalPos = new Vector3(p.gameObject.transform.position.x, p.gameObject.transform.position.y, p.gameObject.transform.position.z);
        botonReintentar.gameObject.SetActive(false);
        obs = GetComponent<Obstaculo>();
        puntos.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (p.Perdio) {
            botonReintentar.gameObject.SetActive(true);
            if (deNuevo) {
                p.Reset();
                obs.Reset();
                deNuevo = false;
                Vector3 cambio = new Vector3(0,5,0);
                p.gameObject.transform.position = originalPos+cambio;
            }
            
        } else {
            SetUI();
        }
    }

    public void SetUI() {
        botonReintentar.gameObject.SetActive(false);
        totPuntos += velPuntos;
        puntos.text = Mathf.Round(totPuntos).ToString();
    }

    //Corre esta función onClick
    public void intentarDeNuevo() {
        deNuevo = true;
        totPuntos = 0;
    }
    
}
