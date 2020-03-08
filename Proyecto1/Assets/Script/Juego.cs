using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juego : MonoBehaviour
{
    public Lector lector;
    public DatosDelJuego datos;

    /* bool verificarCasilla(int filaAct_Aux, int columnaAct_Aux, int columnaMax_Aux, int filaMax_Aux, int[,] matrizLogica, List<List<int>> pistaFilas, List<List<int>> pistaColumnas) {
         List<int> Columna = new List<int>();
         List<int> Fila = new List<int>();
         Columna = genereArrayColumna(Columna, filaMax_Aux, columnaAct_Aux, matrizLogica);
         Fila = genereArrayFila(Fila, columnaMax_Aux, filaAct_Aux, matrizLogica);
         if (verificarFila(filaAct_Aux, columnaAct_Aux, columnaMax_Aux, Fila, pistaFilas)) {
             if (verificarColumna(filaAct_Aux, columnaAct_Aux, filaMax_Aux, Columna, pistaFilas)) {
                 return true;
             }
         }
         return false;
     }

     List<int> genereArrayFila(List<int> Fila, int columnaMax_Aux, int filaAct_Aux, int[,] matrizLogica) {
         for (int i = 0; i < columnaMax_Aux; i++) {
             Fila[i] = matrizLogica[filaAct_Aux - 1, i];
         }
         return Fila;
     }

     List<int> genereArrayColumna(List<int> Columna, int filaMax_Aux, int columnaAct_Aux, int[,] matrizLogica)
     {
         for (int i = 0; i < filaMax_Aux; i++)
         {
             Columna[i] = matrizLogica[i, columnaAct_Aux - 1];
         }
         return Columna;
     }


     bool verificarFila(int filaAct_Aux, int columnaAct_Aux, int columnaMax_Aux, List<int> Fila, List<List<int>> pistaFilas) {
         int contadorDe1s = 0;
         if (pistaFilas[filaAct_Aux][0] == 0)
         {
             return false;
         }
         else if (pistaFilas[filaAct_Aux].Count == 1)
         {
             for (int j = 0; j < columnaMax_Aux; j++)
             {
                 if (Fila[j] == 1)
                 {
                     contadorDe1s++;
                 }
             }
             if (pistaFilas[filaAct_Aux - 1][0] == contadorDe1s)
             {
                 return false;
             }
         }
         else
         {
             for (int i = 0; i < pistaFilas[filaAct_Aux][0]; i++) //aca quede :(
             {
                 if (true)
                 {
                     return true;
                 }
             }
         }
     }


     bool verificarColumna(int filaAct_Aux, int columnaAct_Aux, int filaMax_Aux, List<int> Columna, List<List<int>> pistaColumnas) {


     }

     int[,] marcar(int[,] matrizLogica) {
         int columnaMax = datos.getY();
         int filaMax = datos.getX();
         int columnaAct = 1;
         int filaAct = 1;
         List<List<int>> pistasFilas = datos.getPistasY();
         List<List<int>> pistasColumnas = datos.getPistasX();
         matrizLogica = marcar_Aux(matrizLogica, filaMax, columnaMax, filaAct, columnaAct, pistasFilas, pistasColumnas);
         return matrizLogica;
     }

     int[,] marcar_Aux(int[,] matrizLogica, int filaMax_Aux, int columnaMax_Aux, int filaAct_Aux, int columnaAct_Aux, List<List<int>> pistaFilas, List<List<int>> pistaColumnas) {
         if (filaAct_Aux > filaMax_Aux) //ya terminamos el nonogram yey
         {
             return matrizLogica;
         }

         else if (columnaAct_Aux > columnaMax_Aux) //ya terminamos la fila mijo, siga con la que sigue
         {
             return marcar_Aux(matrizLogica, filaMax_Aux, columnaMax_Aux, filaAct_Aux + 1, 1, pistaFilas, pistaColumnas);
         }

         if (matrizLogica[filaAct_Aux - 1, columnaAct_Aux - 1] == 1) //si el campo esta lleno, no vaya a revisar, siga con la siguiente
         {
             return marcar_Aux(matrizLogica, filaMax_Aux, columnaMax_Aux, filaAct_Aux, columnaAct_Aux + 1, pistaFilas, pistaColumnas);
         }
         else {
             if (verificarCasilla(filaAct_Aux, columnaAct_Aux, columnaMax_Aux, filaMax_Aux, matrizLogica, pistaFilas, pistaColumnas))
             {
                 matrizLogica[filaAct_Aux - 1, columnaAct_Aux - 1] = 1; //coloca porque si era valido
             }
             else {
                 //tocaria hacer backtracking
             }
         }

         /*if (pistaFilas[filaAct_Aux].Count == 1) { //La fila puede rellenarse completa
             if (pistaFilas[filaAct_Aux][0] == filaMax_Aux) {
                 for (int i = 0; i < columnaMax_Aux; i++) {
                     matrizLogica[filaAct_Aux-1,i]=1;
                 }
             } 
         }
         if (pistaColumnas[columnaAct_Aux].Count == 1) { //La columna puede rellenarse completa
             if (pistaColumnas[columnaAct_Aux][0] == columnaMax_Aux) {
                 for (int i = 0; i < filaMax_Aux; i++) {
                     matrizLogica[i, columnaAct_Aux - 1] = 1;
                 }
             }
         }
         return marcar_Aux(matrizLogica,filaMax_Aux,columnaMax_Aux,filaAct_Aux,columnaAct_Aux + 1,pistaFilas, pistaColumnas);

} */

    void impresionMatriz(int[,] matrizLogica) {
        string res = "";
        for (int i = 0; i < datos.getX(); i++)
        {
            res += "\n";
            for (int j = 0; j < datos.getY(); j++)
            {
                res += matrizLogica[i, j] + " ";
            }
        }
        print(res);
    }

    /* bool backtrackingSolved(int[,] matrizLogica)
    {
        int[] porValidar = sigPuntoVacio(matrizLogica);
        if (porValidar[0] == 0 && porValidar[1] == 0)
        {
            return true; //validarCol() && validarFilas;
        }
        
        porValidar[0] = porValidar[0] - 1;
        porValidar[1] = porValidar[1] - 1;

        if (validarPosicion(porValidar)) { 
        
        }
        for (int i = 0; i<10; i++)
        {
            if (valid(matrizLogica, i, (row, col))){
                bo[row][col] = i;
            }
            if (solve(matrizLogica)) {
                return true;
            }
            matrizLogica[row,col] = 0;
        }
        return false;
    }

    int[] sigPuntoVacio(int[,] matrizLogica){
        int[] puntoAValidar = { 0, 0 };
        for (int i = 0; i < datos.getX(); i++) {
            for (int j = 0; j < datos.getY(); j++) {
                if (matrizLogica[i, j] == 0) { 
                    puntoAValidar[0] = i+1;
                    puntoAValidar[1] = j+1;
                    return puntoAValidar;
                }
            }
        }
        return puntoAValidar;
    }

    bool validarPosicion() { 
    
    }*/

    // Start is called before the first frame update
    void Start()
    {
        lector.Leer();
        datos = lector.datos;
        datos.setMatrizLogica();
        impresionMatriz(datos.getMatrizLogica());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
