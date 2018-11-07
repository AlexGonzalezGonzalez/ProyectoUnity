using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nave : MonoBehaviour
{
    public float velocidadGiro = 15;
    private Rigidbody rb;
    public UnityEngine.UI.Text derrota, tiempo, victoria;
    private GameObject puertaI, puertaD;
    public GameObject Barrera,EsferaEsc, EsferaEsc2, Terreno;
    
    private GameObject t1;
    private GameObject[] obs;
    Transform tr;
    Animation an;
    AnimationClip ac;
    Animation animI,animD;
    AnimationCurve curve;
    AnimationClip clipI,clipD;
    private float moveTerreno = 191.1f, moveBarrera = 191.1f, moveEsfera = 191.1f, moveEsfera2 = 191.1f;
    private float z=30;
    private int zero = 0,random,cuentaTerrenos=0,time=0;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        derrota.text = "";
        asignarTerreno();
        asignarAnimacion();
        rb.angularVelocity = new Vector3(0, 10, 0);

    }
    void asignarTerreno()
    {
        //Le damos valor al objeto puertaI
        tr = Terreno.GetComponent<Transform>();
        tr = tr.Find("PuertaIzquierda");
        puertaI = tr.gameObject;
        //Le damos valor al objeto puertaD
        tr = Terreno.GetComponent<Transform>();
        tr = tr.Find("PuertaDerecha");
        puertaD = tr.gameObject;
       
    }
    void asignarAnimacion()
    {
        //Animacion de la puerta Izquierda
        animI = puertaI.GetComponent<Animation>();
        AnimationCurve curve;
        clipI = new AnimationClip();
        clipI.legacy = true;
        Keyframe[] keys;
        keys = new Keyframe[2];

        //Curva x de la animacion
        keys[0] = new Keyframe(0.0f, -2.339915f);
        keys[1] = new Keyframe(0.3f, -7.5f);
        curve = new AnimationCurve(keys);
        clipI.SetCurve("", typeof(Transform), "localPosition.x", curve); keys = new Keyframe[2];

        //Curva y de la animacion
        keys[0] = new Keyframe(0.0f, 7.73f);
        keys[1] = new Keyframe(0.3f, 7.73f);
        curve = new AnimationCurve(keys);
        clipI.SetCurve("", typeof(Transform), "localPosition.y", curve);

        //Curva z de la animacion
        keys[0] = new Keyframe(0.0f, 4.888f);
        keys[1] = new Keyframe(0.3f, 4.888f);
        curve = new AnimationCurve(keys);
        clipI.SetCurve("", typeof(Transform), "localPosition.z", curve);

        //Animacion de la puerta Derecha
        animD = puertaD.GetComponent<Animation>();
        
        clipD = new AnimationClip();
        clipD.legacy = true;
        
        keys = new Keyframe[2];

        //Curva x de la animacion 
        keys[0] = new Keyframe(0.0f, 2.49f);
        keys[1] = new Keyframe(0.3f, 7.5f);
        curve = new AnimationCurve(keys);
        clipD.SetCurve("", typeof(Transform), "localPosition.x", curve); keys = new Keyframe[2];

        //Curva y de la animacion
        keys[0] = new Keyframe(0.0f, 7.73f);
        keys[1] = new Keyframe(0.3f, 7.73f);
        curve = new AnimationCurve(keys);
        clipD.SetCurve("", typeof(Transform), "localPosition.y", curve);

        //Curva z de la animacion
        keys[0] = new Keyframe(0.0f, 4.888f);
        keys[1] = new Keyframe(0.3f, 4.888f);
        curve = new AnimationCurve(keys);
        clipD.SetCurve("", typeof(Transform), "localPosition.z", curve);
    }
    void asignarObstaculos()
    {
        obs = new GameObject[] { Barrera,EsferaEsc,EsferaEsc2 };
        for(int i = 0; i < obs.Length; i++)
        {
            random = Random.Range(0, 2);
           
            if (zero == random)
            {
                if (i == 0)
                {
                    Barrera=Instantiate(Barrera);
                    tr = Barrera.GetComponent<Transform>();
                    tr.SetPositionAndRotation(new Vector3(-4.2f,1.15f,(float)(0.236+moveBarrera*cuentaTerrenos)), new Quaternion(0, 0, 0, 0));
                }
                if (i == 1)
                {
                    EsferaEsc = Instantiate(obs[i]);
                    tr = EsferaEsc.GetComponent<Transform>();
                    tr.SetPositionAndRotation(new Vector3(-4.26f, 6.1f, (float)(2.794+moveEsfera*cuentaTerrenos)), new Quaternion(0, 0, 0, 0));

                }
                if (i == 2)
                {
                    EsferaEsc2 = Instantiate(obs[i]);
                    tr = EsferaEsc2.GetComponent<Transform>();
                    tr.SetPositionAndRotation(new Vector3(3.82f,6.1f, (float)(2.794+moveEsfera2*cuentaTerrenos)), new Quaternion(0, 0, 0, 0));
                }
            }
        }
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        
        float moverH = Input.GetAxis("Horizontal");
        Vector3 v3 = new Vector3(moverH * velocidadGiro, 0f, z);
        if (rb.velocity.z < 30) {
            z = 1;
        }
        else
        {
            z = -1;
        }
        
        rb.AddForce(v3);
        
        

    }
   

    private void OnCollisionEnter(Collision collision)
    {
        derrota.text = "Game Over";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Activador"))
        {
            
            //Abrimos puertas
            animI.AddClip(clipI, clipI.name);
            animD.AddClip(clipD, clipD.name);
            animI.Play(clipI.name);
            animD.Play(clipD.name);

            //Guardamos el primer Terreno en t1
            t1 = Terreno.gameObject;
            //Creamos el siguiente terreno
            Terreno=Instantiate(Terreno);
            cuentaTerrenos = cuentaTerrenos + 1;
            Terreno.name = "TerrenoNuevo";
            //Asignamos las puertas del nuevo Terreno a las variables puertas y los obstaculos
            asignarTerreno();
            //instancia de forma aleatoria obstaculos en los empty objects
            asignarObstaculos();
            //Asignamos las animaciones de las puertas a las nuevas puertas
            asignarAnimacion();
            //Cogemos el transform del siguiente terreno
            tr = Terreno.transform;
            //Posicionamos el siguiente terreno despues del anterior
            tr.SetPositionAndRotation(new Vector3(0f, 0, moveTerreno), new Quaternion(0f, 0f, 0f, 0f));
            moveTerreno = moveTerreno + 191.1f;
            

        } 
        if (other.gameObject.CompareTag("Destruir")){
            //Destruimos el terreno que ya hemos pasado
            Destroy(t1);
            
        }
    }
}
