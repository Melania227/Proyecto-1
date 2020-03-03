using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;



public class Lector : MonoBehaviour
{
    public DatosDelJuego datos;


    // Start is called before the first frame update
    void Start()
    {
        using (StreamReader sr = File.OpenText("Cat.txt"))
        {
            string s = String.Empty;
            s = sr.ReadLine();
            this.Posiciones(s);
            sr.ReadLine();
            for (int n= 0; n<datos.getX; n++)
            {
                Pista(n, s, 'x');
                s = sr.ReadLine();
            }
            sr.ReadLine();
            for (int n = 0; n < datos.getY; n++)
            {
                Pista(n, s, 'y');
                s = sr.ReadLine();
            }

        }

    }

    public void Posiciones(string lineaActual)
    {
        int largo = lineaActual.Length;
        string res = "";
        for (int n=0; n<largo; n++) {
            if (lineaActual[n] != ',')
            {
                res += lineaActual[n];
            }
            else
            {
                this.datos.setX(Convert.ToInt32(res));
                res = "";
                n++;
            }
        }
        this.datos.setY(Convert.ToInt32(res));

    }

    public void Pista(int linea, string lineaActual, char tipo)
    {
        int largo = lineaActual.Length;
        string res = "";
        for (int n = 0; n < largo; n++)
        {
            if (lineaActual[n] != ',')
            {
                res += lineaActual[n];
            }
            else
            {
                res = "";
                n++;
            }
        }
    }


}
