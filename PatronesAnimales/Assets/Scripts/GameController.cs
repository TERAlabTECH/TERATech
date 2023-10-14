using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

// Como un futuro paso podemos:
//      - hacer que se randomice también la posición inicial de cada personaje
//      - en vez de que se tengan que inicializar cada personaje individualmente, podríamos generarlos dinámicamente en este script

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [Header("Scripts")]
    public CargarNivel referenciaCargarNivel; 

    //Los diferentes objetos que están en la escena que quiero asociar para darles funcionamiento/obtener info
    [Header("Personaje Tipo 1")]
    [SerializeField] private GameObject personaje1Tipo1;
    [SerializeField] private GameObject personaje2Tipo1;
    [SerializeField] private GameObject personaje3Tipo1;

    [Header("Personaje Tipo 2")]
    [SerializeField] private GameObject personaje1Tipo2;
    [SerializeField] private GameObject personaje2Tipo2;
    [SerializeField] private GameObject personaje3Tipo2;

    [Header("Personaje Tipo 3")]
    [SerializeField] private GameObject personaje1Tipo3;
    [SerializeField] private GameObject personaje2Tipo3;
    [SerializeField] private GameObject personaje3Tipo3;

    [Header("Suelo")]//Agregamos un suelo a la experiencia para que no se pierdan los modelos con el suelo real
    [SerializeField] private GameObject suelo; //Debería de ser cuadrado, no rectangular

    [Header("Texto")]
    [SerializeField] private TextMeshProUGUI txtNivel; //Debería de ser cuadrado, no rectangular

    [Header("Botones")]
    [SerializeField] private Button btnModelo1;
    [SerializeField] private Button btnModelo2;
    [SerializeField] private Button btnModelo3;
    [SerializeField] private Button btnTrayectoria1;
    [SerializeField] private Button btnTrayectoria2;
    [SerializeField] private Button btnTrayectoria3;
    [SerializeField] private Button btnReset;
    [SerializeField] private Button btnCheck;

    private Button[] btnModelo;
    private Button[] btnTrayectoria;
    private int selectedButtons = 0;

    //Personajes de tipo 1 (son del mismo modelo) y se mueven en trayectoria circular
    private Personaje p1T1;
    private Personaje p2T1;
    private Personaje p3T1;

    //Personajes de tipo 2 (son del mismo modelo) y se mueven en trayectoria cuadrangular
    private Personaje p1T2;
    private Personaje p2T2;
    private Personaje p3T2;

    //Personajes de tipo 3 (son del mismo modelo) y se mueven en trayectoria triangular
    private Personaje p1T3;
    private Personaje p2T3;
    private Personaje p3T3;

    //Variables internas a la clase
    private Personaje[] pT; //Arreglo de los personajes de tipo 1 
    private Vector3[] pT1initialOffset; //Arreglo para guardar las posiciones iniciales de los modelos tipo 1
    private Vector3[] pT2initialOffset; //Arreglo para guardar las posiciones iniciales de los modelos tipo 2
    private Vector3[] pT3initialOffset; //Arreglo para guardar las posiciones iniciales de los modelos tipo 3

    private float[] pT1Radio; //Arreglo de los radios para los movimientos para los personajes tipo 1
    private float[] pT1Speed; //Arreglo de las velocidades para los movimientos de los personajes tipo 1
    private float[] pT2Arista; //Arreglo de las aristas para los movimientos para los personajes tipo 2
    private float[] pT2Speed; //Arreglo de las velocidades para los movimientos de los personajes tipo 2
    private float[] pT3Arista; //Arreglo de las aristas para los movimientos para los personajes tipo 2
    private float[] pT3Speed; //Arreglo de las velocidades para los movimientos de los personajes tipo 2

    private Boolean gano = false;
    float timeCounter = 0;

    //Se llama al inicio (1 vez) cuando se carga el juego
    private void Start()
    {
        //Toma los game objects dados y los guarda como personajes
        //Ahoritan son esferas pero van a ser modelos .fbx
        p1T1 = personaje1Tipo1.gameObject.GetComponent<Personaje>();
        p2T1 = personaje2Tipo1.gameObject.GetComponent<Personaje>();
        p3T1 = personaje3Tipo1.gameObject.GetComponent<Personaje>();

        p1T2 = personaje1Tipo2.gameObject.GetComponent<Personaje>();
        p2T2 = personaje2Tipo2.gameObject.GetComponent<Personaje>();
        p3T2 = personaje3Tipo2.gameObject.GetComponent<Personaje>();

        p1T3 = personaje1Tipo3.gameObject.GetComponent<Personaje>();
        p2T3 = personaje2Tipo3.gameObject.GetComponent<Personaje>();
        p3T3 = personaje3Tipo3.gameObject.GetComponent<Personaje>();

        pT1initialOffset = new Vector3[] {
            p1T1.gameObject.transform.position - suelo.transform.position,
            p2T1.gameObject.transform.position - suelo.transform.position,
            p3T1.gameObject.transform.position - suelo.transform.position
        };

        pT2initialOffset = new Vector3[] {
            p1T2.gameObject.transform.position - suelo.transform.position,
            p2T2.gameObject.transform.position - suelo.transform.position,
            p3T2.gameObject.transform.position - suelo.transform.position
        };

        pT3initialOffset = new Vector3[] {
            p1T3.gameObject.transform.position - suelo.transform.position,
            p2T3.gameObject.transform.position - suelo.transform.position,
            p3T3.gameObject.transform.position - suelo.transform.position
        };

        pT1Radio = new float[3];
        pT2Arista = new float[3];
        pT3Arista = new float[3];

        //Para que la trayectoria circular de cada personaje sea diferente entre partidas
        for (int i = 0; i < 3; i++)
        {
            pT1Radio[i] = restrictCircularMovement(pT1initialOffset[i]);
            pT2Arista[i] = restrictCircularMovement(pT2initialOffset[i]);
            pT3Arista[i] = restrictCircularMovement(pT3initialOffset[i]);
        }

        //Para que la velocidad de cada personaje sea diferente entre partidas
        pT1Speed = new float[] {
            UnityEngine.Random.Range(0.8f, 6),
            UnityEngine.Random.Range(0.8f, 6),
            UnityEngine.Random.Range(0.8f, 6),
        };

        pT2Speed = new float[] {
            UnityEngine.Random.Range(0.8f, 6),
            UnityEngine.Random.Range(0.8f, 6),
            UnityEngine.Random.Range(0.8f, 6),
        };

        pT3Speed = new float[] {
            UnityEngine.Random.Range(0.8f, 6),
            UnityEngine.Random.Range(0.8f, 6),
            UnityEngine.Random.Range(0.8f, 6),
        };

        btnModelo = new Button[] { btnModelo1, btnModelo2 , btnModelo3 };
        btnTrayectoria = new Button[] { btnTrayectoria3, btnTrayectoria1, btnTrayectoria2,  };

        foreach (Button btn in btnModelo)
        {
            Button currentBtn = btn;
            btn.onClick.AddListener(() => ChangePressedColor(currentBtn)); // Change to your desired color for the Modelo buttons
        }

        foreach (Button btn in btnTrayectoria)
        {
            Button currentBtn = btn;
            btn.onClick.AddListener(() => ChangePressedColor(currentBtn)); // Change to your desired color for the Trayectoria buttons
        }

        btnReset.onClick.AddListener(() => ResetBtnClicked());

        btnCheck.onClick.AddListener(() => CheckAnswers());
    }

    public void ButtonClicked(ButtonClickHandler btnHandler)
    {
        // Your logic here
    }

    //Se llama en cada frame
    void Update()
    {
        timeCounter += Time.deltaTime;
        if(!gano) {
            moverPersonajeTipo1();
            moverPersonajeTipo2();
            moverPersonajeTipo3();
        }
        
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    //Mueve los objetos en una trayectoria circular
    public void moverPersonajeTipo1()
    {
        float x, z;
        float radioCirculo;
        for (int i = 0; i < 3; i++)
        {
            radioCirculo = pT1Radio[i];
            x = suelo.gameObject.transform.position.x + pT1initialOffset[i].x - (float)Math.Pow(-1, i) * (radioCirculo * Mathf.Cos(timeCounter * pT1Speed[i]));
            z = suelo.gameObject.transform.position.z + pT1initialOffset[i].z - (float)Math.Pow(-1, i) * (radioCirculo * Mathf.Sin(timeCounter * pT1Speed[i]));

            switch (i)
            {
                case 0:
                    p1T1.transform.position = new Vector3(x, suelo.gameObject.transform.position.y + 0.04f, z);
                    break;
                case 1:
                    p2T1.transform.position = new Vector3(x, suelo.gameObject.transform.position.y + 0.04f, z);
                    break;
                case 2:
                    p3T1.transform.position = new Vector3(x, suelo.gameObject.transform.position.y + 0.04f, z);
                    break;
            }
        }
    }

    //Mueve los objetos en una trayectoria cuadrangular
    public void moverPersonajeTipo2()
    {
        for (int i = 0; i < 3; i++)
        {
            float x = pT2initialOffset[i].x;
            float z = pT2initialOffset[i].z;
            float aristaSize = pT2Arista[i];
            float speed = pT2Speed[i]; 

            // Ciclo para que el movimiento cuadrado sea continuo
            float progress = (timeCounter * speed) % 4;

            if (progress < 1)
            {
                x += aristaSize * progress;
            }
            else if (progress < 2)
            {
                x += aristaSize;
                z += aristaSize * (progress - 1);
            }
            else if (progress < 3)
            {
                x += aristaSize * (3 - progress);
                z += aristaSize;
            }
            else
            {
                z += aristaSize * (4 - progress);
            }

            x += suelo.gameObject.transform.position.x;
            z += suelo.gameObject.transform.position.z;

            // Actualizar la posición del personaje tipo 2
            switch (i)
            {
                case 0:
                    p1T2.transform.position = new Vector3(x, suelo.gameObject.transform.position.y + 0.04f, z);
                    break;
                case 1:
                    p2T2.transform.position = new Vector3(x, suelo.gameObject.transform.position.y + 0.04f, z);
                    break;
                case 2:
                    p3T2.transform.position = new Vector3(x, suelo.gameObject.transform.position.y + 0.04f, z);
                    break;
            }
        }
    }

    //Mueve los objetos en una trayectoria triangular
    public void moverPersonajeTipo3()
    {
        for (int i = 0; i < 3; i++)
        {
            float x = pT3initialOffset[i].x;
            float z = pT3initialOffset[i].z;
            float aristaSize = pT3Arista[i];
            float speed = pT3Speed[i];

            float progress = (timeCounter * speed) % 3;

            if (progress < 1)
            {
                x += aristaSize * progress;
            }
            else if (progress < 2)
            {
                x += aristaSize;
                z += aristaSize * (progress - 1);
            }
            else
            {
                x += aristaSize * (3 - progress);
                z += aristaSize * (3 - progress);
            }

            x += suelo.gameObject.transform.position.x;
            z += suelo.gameObject.transform.position.z;

            switch (i)
            {
                case 0:
                    p1T3.transform.position = new Vector3(x, suelo.gameObject.transform.position.y + 0.04f, z);
                    break;
                case 1:
                    p2T3.transform.position = new Vector3(x, suelo.gameObject.transform.position.y + 0.04f, z);
                    break;
                case 2:
                    p3T3.transform.position = new Vector3(x, suelo.gameObject.transform.position.y + 0.04f, z);
                    break;
            }
        }
    }


    //Toma la posición del personaje y se asegura que el radio sea menor al espacio que queda
    //entre el personaje y el borde del suelo.
    //Esto con la intencion de que la trayectoria circular no ocurra fuera del suelo.
    public float restrictCircularMovement(Vector3 initialOffset)
    {
        float res;
        float quadrantSize = suelo.gameObject.transform.localScale.z / 2; //Supone que el suelo es un cuadrado no rectangulo
        float x = Math.Abs(initialOffset.x);
        float z = Math.Abs(initialOffset.z);

        float distanceToEdgeX = quadrantSize - x;
        float distanceToEdgeZ = quadrantSize - z;

        res = Mathf.Min(distanceToEdgeX, distanceToEdgeZ);

        return UnityEngine.Random.Range(0.05f, res);
    }

    private void ChangePressedColor(Button btn)
    {
        if (selectedButtons >= 0 && selectedButtons < 6) {
            Color newColor = Color.white;
            if (selectedButtons == 0 || selectedButtons == 1)
            {
                newColor = makeRGBcolor(247,249,166);
            }
            else if (selectedButtons == 2 || selectedButtons == 3)
            {
                newColor = makeRGBcolor(3, 152, 158);

            }
            else if (selectedButtons == 4 || selectedButtons == 5)
            {
                newColor = makeRGBcolor(1, 134, 203);

            }

            ColorBlock cb = btn.colors;
            cb.normalColor = newColor;
            cb.selectedColor = newColor;
            btn.colors = cb;
            selectedButtons++;

        }
    }

    private void ResetBtnClicked()
    {
        selectedButtons = 0;
        foreach (Button btn in btnModelo)
        {
            Button currentBtn = btn;
            ColorBlock cb = currentBtn.colors;
            cb.normalColor = Color.white;
            btn.colors = cb;
        }

        foreach (Button btn in btnTrayectoria)
        {
            Button currentBtn = btn;
            ColorBlock cb = currentBtn.colors;
            cb.normalColor = Color.white;
            btn.colors = cb;
        }
    }

    private Color makeRGBcolor(float r, float g, float b) {
        return new Color(r / 255f, g / 255f, b / 255f);
    }

    private void CheckAnswers() {
        //Checa que en efecto todos los botones estén clickeados
        String mensaje = "Nivel 1";
        int i = 0;
        Boolean todosClick = true;
        while ( i < btnModelo.Length && todosClick) {
            ColorBlock cb1 = btnModelo[i].colors;
            ColorBlock cb2 = btnTrayectoria[i].colors;

            if (cb1.normalColor == Color.white || cb2.normalColor == Color.white) {
                todosClick = false;
            }

            i++;
        }

        if (!todosClick) {
            mensaje = "Empareja a todos los animales";
        } else {
            i = 0;
            Boolean seleccionCorrecta = true;
            while (i < btnModelo.Length && seleccionCorrecta)
            {
                ColorBlock cb1 = btnModelo[i].colors;
                ColorBlock cb2 = btnTrayectoria[i].colors;

                if (cb1.normalColor != cb2.normalColor)
                {
                    seleccionCorrecta = false;
                }

                i++;
            }

            if (!seleccionCorrecta)
            {
                mensaje = "La selección de patrones no es correcta";
            } else {
                mensaje = "La selección de patrones es correcta";
                gano = true;
                if (referenciaCargarNivel != null)
                {
                    referenciaCargarNivel.nivelGanado(); 
                }

            }

        }

        txtNivel.text = mensaje;
    }
}
