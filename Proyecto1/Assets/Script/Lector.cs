﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;



public class Lector : MonoBehaviour
{
    public DatosDelJuego datos = new DatosDelJuego();
    
    //Realizar la lectura del archivo .TXT

    public void Leer(string path)
    {
        datos = new DatosDelJuego();
        using (StreamReader sr = File.OpenText(path))
        {
            string s = String.Empty;
            s = sr.ReadLine();
            this.Posiciones(s);
            s = sr.ReadLine();
            int largoX = datos.getX();
            int largoY = datos.getY();
            this.datos.addListX(new List<int>());
            for (int n = 0; n < largoX; n++)
            {
                s = sr.ReadLine();
                Pista(s, 'x');
            }
            sr.ReadLine();
            this.datos.addListY(new List<int>());
            for (int n= 0; n< largoY; n++)
            {
                s = sr.ReadLine();
                Pista(s, 'y');

            }

        }

    }

    public void Posiciones(string lineaActual)
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
                this.datos.setX(System.Convert.ToInt32(res));
                res = "";
                n++;
            }
        }
        this.datos.setY(System.Convert.ToInt32(res));

    }

    public void Pista(string lineaActual, char tipo)
    {
        int largo = lineaActual.Length;
        List<int> lista = new List<int>();
        string res = "";
        for (int n = 0; n < largo; n++)
        {
            if (lineaActual[n] != ',')
            {
                
                res += lineaActual[n];

            }
            else
            {
                lista.Add(System.Convert.ToInt32(res));
                res = "";
                n++;
            }
        }
        lista.Add(System.Convert.ToInt32(res));
        if (tipo == 'x') this.datos.addListX(lista);
        else this.datos.addListY(lista);

    }

}
