using UnityEngine;

public class Personaje : MonoBehaviour
{
    private bool estaEnPiso;
    private bool perdio;
    private Animator anim;
    private Rigidbody rb;
    private int fuerzaSalto;
    private Vector3 fuerza;

    public bool Perdio { get => perdio; set => perdio = value; }

    // Start is called before the first frame update
    void Start()
    {
        estaEnPiso = true;
        Perdio = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        fuerzaSalto = 1400;
        fuerza = new Vector3(0, fuerzaSalto, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (estaEnPiso) {
            if (Input.GetButtonDown("Jump")) {
                estaEnPiso = false;
                rb.AddForce(fuerza);
            }
        } else {
            anim.SetBool("estaSaltando", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Piso")) {
            estaEnPiso = true;
            anim.SetBool("estaSaltando", false);
        }

        if (other.gameObject.CompareTag("Obstaculo"))
        {
            Perdio = true;
            Time.timeScale = 0;
        }
    }

    public void Reset()
    {
        perdio = false;
        Time.timeScale = 1;

    }
}
