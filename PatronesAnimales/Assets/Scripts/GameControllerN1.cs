using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;

// Como un futuro paso podemos:
//      - hacer que se randomice también la posición inicial de cada personaje
//      - en vez de que se tengan que inicializar cada personaje individualmente, podríamos generarlos dinámicamente en este script

public class GameController : MonoBehaviour
{

    public static GameController Instance;

    [Header("Scripts")]
    public CargarNivel referenciaCargarNivel; //El script que se encarga de la transición entre niveles

    [Header("Nivel")]
    public int nivel; //Parámetro que dan los botones para indicar qué nivel se va a cargar a continuación

    //Los diferentes objetos que están en la escena que quiero asociar para darles funcionamiento/obtener info
    //Los primeros 3 personajes son los utilizados en el primer nivel

    //Si queremos que el personaje de cada tipo sea el mismo modelo, hay que ver si es posible
    //arrastrar al game controller un unico modelo por tipo para no repetir cada tipo 3 veces
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

    //Los otros personajes no se están utilizando realmente pq se planificó usarlos en otros niveles, pero el funcionamiento sería similar
    //[Header("Personaje Tipo 4")]
    //[SerializeField] private GameObject personaje1Tipo4;
    //[SerializeField] private GameObject personaje2Tipo4;
    //[SerializeField] private GameObject personaje3Tipo4;

    //[Header("Personaje Tipo 5")]
    //[SerializeField] private GameObject personaje1Tipo5;
    //[SerializeField] private GameObject personaje2Tipo5;
    //[SerializeField] private GameObject personaje3Tipo5;

    //[Header("Personaje Tipo 6")]
    //[SerializeField] private GameObject personaje1Tipo6;
    //[SerializeField] private GameObject personaje2Tipo6;
    //[SerializeField] private GameObject personaje3Tipo6;

    //[Header("Personaje Tipo 7")]
    //[SerializeField] private GameObject personaje1Tipo7;
    //[SerializeField] private GameObject personaje2Tipo7;
    //[SerializeField] private GameObject personaje3Tipo7;

    [Header("Suelo")]//Agregamos un suelo a la experiencia para que no se pierdan los modelos con el suelo real
    [SerializeField] private GameObject suelo; //Debería de ser cuadrado, no rectangular

    [Header("Texto")]
    [SerializeField] private TextMeshProUGUI txtNivel; //Es el indicador de nivel que está dentro de la GUI

    [Header("Botones")]
    [SerializeField] private Button btnModelo1; //El boton con la figura del modelo 1
    [SerializeField] private Button btnModelo2; //El boton con la figura del modelo 2
    [SerializeField] private Button btnModelo3; //El boton con la figura del modelo 3
    [SerializeField] private Button btnTrayectoria1; //El boton con la figura de la trayectoria 1 (ej. cuadrado
    [SerializeField] private Button btnTrayectoria2; //El boton con la figura de la trayectoria 2 
    [SerializeField] private Button btnTrayectoria3; //El boton con la figura de la trayectoria 3 
    [SerializeField] private Button btnReset; //Botón para deshacer la selección de patrones
    [SerializeField] private Button btnCheck; //Botón para revisar las selecciones

    private Button[] btnModelo;
    private Button[] btnTrayectoria;
    //private Button[] btnModeloExtra;
    private Button[] btnTrayectoriaExtra;
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


    ////Personajes de tipo 4 (son del mismo modelo) y se mueven en trayectoria de corazon
    //private Personaje p1T4;
    //private Personaje p2T4;
    //private Personaje p3T4;

    //private Personaje p1T5;
    //private Personaje p2T5;
    //private Personaje p3T5;

    //Variables internas a la clase
    private Personaje[] pT; //Arreglo de los personajes de tipo 1 
    private Vector3[] pT1initialOffset; //Arreglo para guardar las posiciones iniciales de los modelos tipo 1
    private Vector3[] pT2initialOffset; //Arreglo para guardar las posiciones iniciales de los modelos tipo 2
    private Vector3[] pT3initialOffset; //Arreglo para guardar las posiciones iniciales de los modelos tipo 3
    //private Vector3[] pT4initialOffset;
    //private Vector3[] pT5initialOffset;

    //Es un arreglo y no un único valor pq no queremos que los tamaños de movimiento sean iguales
    private float[] pT1Radio; //Arreglo de los radios para los movimientos para los personajes tipo 1
    private float[] pT1Speed; //Arreglo de las velocidades para los movimientos de los personajes tipo 1
    private float[] pT2Arista; //Arreglo de las aristas para los movimientos para los personajes tipo 2
    private float[] pT2Speed; //Arreglo de las velocidades para los movimientos de los personajes tipo 2
    private float[] pT3Arista; //Arreglo de las aristas para los movimientos para los personajes tipo 2
    private float[] pT3Speed; //Arreglo de las velocidades para los movimientos de los personajes tipo 2
    //private float[] pT4Radio;
    //private float[] pT5Arista;


    private Boolean gano = false;
    float timeCounter = 0;

    //Se llama al inicio (1 vez) cuando se carga el juego
    private void Start()
    {
        //Se agregan los elementos de la UI a un arreglo para manejarlo más facilmente
        //la respuesta correcta es modelo1-trayectoria3, modelo2-trayectoria1, etc. 
        btnModelo = new Button[] { btnModelo1, btnModelo2, btnModelo3 };
        btnTrayectoria = new Button[] { btnTrayectoria3, btnTrayectoria1, btnTrayectoria2, };

        ////CHECAR EL PQ DE ESTOS
        //btnModeloExtra = new Button[] { btnModelo1, btnModelo2 };
        //btnTrayectoriaExtra = new Button[] {btnTrayectoria2, btnTrayectoria1 };

        //Si el botón fue seleccionado, cambia su color
        foreach (Button btn in btnModelo)
        {
            Button currentBtn = btn;
            btn.onClick.AddListener(() => ChangePressedColor(currentBtn)); 
        }

        foreach (Button btn in btnTrayectoria)
        {
            Button currentBtn = btn;
            btn.onClick.AddListener(() => ChangePressedColor(currentBtn)); 
        }

        //Reinicia la selección de patrones
        btnReset.onClick.AddListener(() => ResetBtnClicked());

        //Revisa la selección de patrones
        btnCheck.onClick.AddListener(() => CheckAnswers());

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

        if (nivel == 1 || nivel ==  2) {
            if (nivel == 1)
            {
                //Toma los game objects dados (ej. los modelos) y los guarda como personajes
                p1T1 = personaje1Tipo1.gameObject.GetComponent<Personaje>();
                p2T1 = personaje2Tipo1.gameObject.GetComponent<Personaje>();
                p3T1 = personaje3Tipo1.gameObject.GetComponent<Personaje>();

                p1T2 = personaje1Tipo2.gameObject.GetComponent<Personaje>();
                p2T2 = personaje2Tipo2.gameObject.GetComponent<Personaje>();
                p3T2 = personaje3Tipo2.gameObject.GetComponent<Personaje>();

                p1T3 = personaje1Tipo3.gameObject.GetComponent<Personaje>();
                p2T3 = personaje2Tipo3.gameObject.GetComponent<Personaje>();
                p3T3 = personaje3Tipo3.gameObject.GetComponent<Personaje>();

                //Hay que recalcular la posición de los modelos para que estén en relación del "piso" virtual y no del origen
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


            }
            //else if (nivel == 2)
            //{
            //    p1T4 = personaje1Tipo4.gameObject.GetComponent<Personaje>();
            //    p2T4 = personaje2Tipo4.gameObject.GetComponent<Personaje>();
            //    p3T4 = personaje3Tipo4.gameObject.GetComponent<Personaje>();

            //    p1T5 = personaje1Tipo5.gameObject.GetComponent<Personaje>();
            //    p2T5 = personaje2Tipo5.gameObject.GetComponent<Personaje>();
            //    p3T5 = personaje3Tipo5.gameObject.GetComponent<Personaje>();

            //    pT4initialOffset = new Vector3[] {
            //        p1T4.gameObject.transform.position - suelo.transform.position,
            //        p2T4.gameObject.transform.position - suelo.transform.position,
            //        p3T4.gameObject.transform.position - suelo.transform.position
            //    };

            //    pT5initialOffset = new Vector3[] {
            //        p1T5.gameObject.transform.position - suelo.transform.position,
            //        p2T5.gameObject.transform.position - suelo.transform.position,
            //        p3T5.gameObject.transform.position - suelo.transform.position
            //    };

            //    pT4Radio = new float[3];
            //    pT5Arista = new float[3];


            //    //Para que la trayectoria circular de cada personaje sea diferente entre partidas
            //    for (int i = 0; i < 3; i++)
            //    {
            //        pT4Radio[i] = restrictCircularMovement(pT4initialOffset[i]);
            //        pT5Arista[i] = restrictCircularMovement(pT5initialOffset[i]);

            //    }
            //}


        }
        
    } //FIN DEL METODO START

    //Se llama en cada frame
    void Update()
    {
        timeCounter += Time.deltaTime;
        if(!gano) {
            if (nivel == 1)
            {
                moverPersonajeTipo1();
                moverPersonajeTipo2();
                moverPersonajeTipo3();
            }
            //else if (nivel == 2) {
            //    moverPersonajeTipo4();
            //    moverPersonajeTipo5();
            //}

        }
        
    }

    //Necesita estar este metodo para que Unity compile
    public void ButtonClicked(ButtonClickHandler btnHandler)
    {
        // Your logic here
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

        //Esta en un for para que el movimiento de cada tipo sea igual pero en diferentes magnitudes
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

    //public void moverPersonajeTipo4() {
    //    float x, z;
    //    for (int i = 0; i < 3; i++) // Solo para un personaje, ajusta según lo necesites
    //    {
    //        float radioCirculo = pT4Radio[i];
    //        float r = radioCirculo * Mathf.Cos(2 * timeCounter);
    //        x = r * Mathf.Cos(timeCounter);
    //        z = r * Mathf.Sin(timeCounter);

    //        // Ajusta la posición basada en la posición del suelo o algún punto de referencia.
    //        x = suelo.gameObject.transform.position.x + pT4initialOffset[i].x + x;
    //        z = suelo.gameObject.transform.position.z + pT4initialOffset[i].z + z;

    //        switch (i)
    //        {
    //            case 0:
    //                p1T4.transform.position = new Vector3(x, suelo.gameObject.transform.position.y + 0.04f, z);
    //                break;
    //            case 1:
    //                p2T4.transform.position = new Vector3(x, suelo.gameObject.transform.position.y + 0.04f, z);
    //                break;
    //            case 2:
    //                p3T4.transform.position = new Vector3(x, suelo.gameObject.transform.position.y + 0.04f, z);
    //                break;
    //        }
    //    }
    //}

    //public void moverPersonajeTipo5()
    //{
    //    for (int i = 0; i < 3; i++)
    //    {
    //        float x = pT5initialOffset[i].x;
    //        float z = pT5initialOffset[i].z;
    //        float aristaSize = pT5Arista[i];
    //        float speed = pT2Speed[i];

    //        // Calcular el progreso del personaje.
    //        float progress = (timeCounter + i) * speed % 5;

    //        // Calcular el ángulo de giro del personaje.
    //        float angle = progress * 2 * Mathf.PI / 5;

    //        // Limitar el ángulo de giro.
    //        angle = Mathf.Repeat(angle, 2 * Mathf.PI);

    //        // Calcular la posición del personaje en la trayectoria del pentágono.
    //        x = pT5initialOffset[i].x + aristaSize * Mathf.Cos(angle);
    //        z = pT5initialOffset[i].z + aristaSize * Mathf.Sin(angle);

    //        // Actualizar la posición del personaje.
    //        x += suelo.gameObject.transform.position.x;
    //        z += suelo.gameObject.transform.position.z;

    //        switch (i)
    //        {
    //            case 0:
    //                p1T5.transform.position = new Vector3(x, suelo.gameObject.transform.position.y + 0.04f, z);
    //                break;
    //            case 1:
    //                p2T5.transform.position = new Vector3(x, suelo.gameObject.transform.position.y + 0.04f, z);
    //                break;
    //            case 2:
    //                p3T5.transform.position = new Vector3(x, suelo.gameObject.transform.position.y + 0.04f, z);
    //                break;
    //        }
    //    }
    //}

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

    //Cada par de botones seleccionado se colorea del mismo tono
    private void ChangePressedColor(Button btn)
    {
        if (selectedButtons >= 0 && selectedButtons < 6) {
            Color newColor = Color.white;

            //El primer y segundo boton seleccionados están del mismo color
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

        //Regresa el color de los botones a blanco
        if (nivel == 1) {
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
        //if (nivel==2) {
        //    foreach (Button btn in btnModeloExtra)
        //    {
        //        Button currentBtn = btn;
        //        ColorBlock cb = currentBtn.colors;
        //        cb.normalColor = Color.white;
        //        btn.colors = cb;
        //    }

        //    foreach (Button btn in btnTrayectoriaExtra)
        //    {
        //        Button currentBtn = btn;
        //        ColorBlock cb = currentBtn.colors;
        //        cb.normalColor = Color.white;
        //        btn.colors = cb;
        //    }
        //}
        
    }

    private Color makeRGBcolor(float r, float g, float b) {
        return new Color(r / 255f, g / 255f, b / 255f);
    }

    private void CheckAnswers() {
        //Checa que en efecto todos los botones estén clickeados
        String mensaje = "Nivel 1";
        int size = btnModelo.Length;
        //if (nivel == 2) {
        //    size = 2;
        //    int i = 0;
        //    Boolean todosClick = true;
        //    while (i < size && todosClick)
        //    {
        //        ColorBlock cb1 = btnModeloExtra[i].colors;
        //        ColorBlock cb2 = btnTrayectoriaExtra[i].colors;

        //        if (cb1.normalColor == Color.white || cb2.normalColor == Color.white)
        //        {
        //            todosClick = false;
        //        }

        //        i++;
        //    }

        //    if (!todosClick)
        //    {
        //        mensaje = "Empareja a todos los animales";
        //    }
        //    else
        //    {
        //        i = 0;
        //        Boolean seleccionCorrecta = true;
        //        while (i < size && seleccionCorrecta)
        //        {
        //                ColorBlock cb1 = btnModeloExtra[i].colors;
        //                ColorBlock cb2 = btnTrayectoriaExtra[i].colors;
                    


        //            if (cb1.normalColor != cb2.normalColor)
        //            {
        //                seleccionCorrecta = false;
        //            }

        //            i++;
        //        }

        //        if (!seleccionCorrecta)
        //        {
        //            mensaje = "La selección de patrones no es correcta";
        //        }
        //        else
        //        {
        //            mensaje = "La selección de patrones es correcta";
        //            gano = true;
        //            if (referenciaCargarNivel != null)
        //            {
        //                referenciaCargarNivel.nivelGanado();
        //            }

        //        }

        //    }

        //    txtNivel.text = mensaje;
        //}
        //else
        if (nivel == 1) {
            int i = 0;
            Boolean todosClick = true;
            while (i < size && todosClick)
            {
                ColorBlock cb1 = btnModelo[i].colors;
                ColorBlock cb2 = btnTrayectoria[i].colors;

                //Si algun botón está en blanco quiere decir que no está seleccionado
                if (cb1.normalColor == Color.white || cb2.normalColor == Color.white)
                {
                    todosClick = false;
                }

                i++;
            }

            if (!todosClick)
            {
                mensaje = "Empareja a todos los animales";
            } else {
                i = 0;
                Boolean seleccionCorrecta = true;
                while (i < size && seleccionCorrecta)
                {
                    ColorBlock cb1 =  btnModelo[i].colors;
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
                }
                else
                {
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
}
