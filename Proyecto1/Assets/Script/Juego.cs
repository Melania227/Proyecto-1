using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juego : MonoBehaviour
{
    public Lector lector;
    public DatosDelJuego datos;


    // Start is called before the first frame update
    void Start()
    {
        lector.Leer();
        datos = lector.datos;
        int[,] matriz = new int[datos.getX(), datos.getY()]; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
