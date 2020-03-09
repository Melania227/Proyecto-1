using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juego : MonoBehaviour
{
    public Lector lector;
    public DatosDelJuego datos;

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

    bool backtrackingSolved()
    {
        int[] porValidar = sigPuntoVacio(datos.getMatrizLogica());
        if (porValidar[0] == 0 && porValidar[1] == 0)
        {
            return true; //validarColumnas() && validarFilas();
        }
        
        porValidar[0] = porValidar[0] - 1;
        porValidar[1] = porValidar[1] - 1;

        if (validarPosicion(porValidar, 1)) { //si el 1 tiene sentido
            datos.setMatrizLogica(porValidar[0], porValidar[1], 1);
            
            if (backtrackingSolved()){
                return true;
            }

            datos.setMatrizLogica(porValidar[0], porValidar[1], 0);
        }
        
        else if (validarPosicion(porValidar, 2)){
            datos.setMatrizLogica(porValidar[0], porValidar[1], 2);
            if (backtrackingSolved())
            {
                return true;
            }

            datos.setMatrizLogica(porValidar[0], porValidar[1], 0);
        }
        return false;
    }

    bool validarColumnas() {
        return true;
    }

    bool validarFilas() {
        return true;
    }

    int[] sigPuntoVacio(int[,] matrizLogica){
        int[] puntoAValidar = { 0, 0 };
        for (int i = 0; i < datos.getX(); i++) {
            for (int j = 0; j < datos.getY(); j++) {
                if (matrizLogica[i, j] == 0) { 
                    puntoAValidar[0] = i+1;//x
                    puntoAValidar[1] = j+1;//y
                    return puntoAValidar;
                }
            }
        }
        return puntoAValidar;
    }

    bool validarPosicion(int[] porValidar, int pruebeCon) {
        int columnaAct = porValidar[1];
        int filaAct = porValidar[0];
        int[] fila = genereArrayFila(filaAct);
        int[] columna = genereArrayColumna(columnaAct);
        if (pruebeCon == 2) {
            return validarLineasPa2(fila, datos.getPistasX()[filaAct]) &&validarLineasPa2(columna, datos.getPistasY()[columnaAct]);
        }

        if (datos.getPistasX().Count == 0 || datos.getPistasY().Count == 0) {
            return false;
        }

        if (datos.getPistasX()[filaAct+1][0] == datos.getX() || datos.getPistasY()[columnaAct+1][0] == datos.getY()) {
            return true;
        }
        print("VA POR ACA DE COLUMNA: " + columnaAct);
        print("VA POR ACA DE FILA: " + filaAct);
        if (validarLineas(columna, datos.getPistasY()[columnaAct]) && validarLineas(fila, datos.getPistasX()[filaAct])) {
            return true;
        }
        return false;
    }

    bool validarLineasPa2(int[] linea, List<int> pistas) {
        if (espaciosDisponibles(linea) < porRellenar(linea, pistas)) {
            return false;
        }
        return true;
    }

    int espaciosDisponibles(int[] linea) {
        int contador = 0;
        for (int i = 0; i < linea.Length; i++) {
            if (linea[i]!=1 && linea[i]!=2) {
                contador++;
            }
        }
        return contador;
    }

    int porRellenar(int[] linea, List<int> pistas) {
        int contador = 0;
        for (int i = 0; i < linea.Length; i++) {
            if (linea[i]==1) {
                contador++;
            }
        }
        contador = totalAMarcar(pistas) - contador;
        return contador;
    }

    bool validarLineas(int[] linea, List <int>pistas) {
        int[] gruposMarcadosV = gruposMarcados(linea, (pistas.Count));
        
        //faltan mas if, es decir, mas condiciones

        for (int i = 0; i < pistas.Count; i++) {
            if (gruposMarcadosV[i] > pistas[i])
            {
                return false;
            }
            if (lineaCompleta(linea, pistas) && gruposMarcadosV[i] != pistas[i]) {
                return false;
            }
            
        }
        
        return true;
    }

    int[] gruposMarcados(int[] linea, int cantidad) {
        int contador = 0;
        int pos = 0;
        int[] gruposFinales = new int[cantidad];
        print("LA CANTIDAD ESSSS: "+ cantidad);
        impresionMatriz(datos.getMatrizLogica());
        for (int j = 0; j < cantidad; j++) {
            gruposFinales[j] = 0;
            print("LISTA 1: " + gruposFinales[pos]);
        }
        for (int i = 0; i < linea.Length; i++) {
            //print("LISTA: " + gruposFinales[pos]); 
            print("LA POS ES: "+pos);
            print("EL CONTADOR VA: "+contador);
            if (linea[i] == 1) {
                contador += 1;
            }
            
            else if (contador != 0 && linea[i]!=1) {
                print("ENTRA");
                print("hola");
                gruposFinales[0] = contador;
                gruposFinales[pos] = contador;
                contador = 0;
                pos++;
            } 
        }
        //gruposFinales[pos] = contador;
        return gruposFinales;
    }

    int marcadosEnLinea(int[] linea) {
        int marcados = 0;
        for (int j = 0; j < linea.Length; j++)
        {
            marcados += linea[j];
        }
        return marcados;
    }

    int totalAMarcar(List<int> pistas) {
        int marcados = 0;
        for (int i = 0; i < pistas.Count; i++)
        {
            marcados += pistas[i];
        }
        return marcados;
    }

    bool lineaCompleta(int [] linea, List<int> pistas) {
        int marcados = totalAMarcar(pistas);
        int estan = marcadosEnLinea(linea);
        return marcados == estan;
    }

    int[] genereArrayFila(int filaAct_Aux)
    {
        int[] fila = new int[datos.getY()];
        for (int i = 0; i < datos.getY(); i++)
        {
            fila[i] = datos.getMatrizLogica()[filaAct_Aux, i];
        }
        return fila;
    }

    int[] genereArrayColumna(int columnaAct_Aux)
    {
        int[] columna = new int[datos.getX()];
        for (int i = 0; i < datos.getX(); i++)
        {
            columna[i] = datos.getMatrizLogica()[i, columnaAct_Aux];
        }
        return columna;
    }

    void Start()
    {
        lector.Leer();
        datos = lector.datos;
        datos.rellenaMatrizLogica();
        impresionMatriz(datos.getMatrizLogica());
        backtrackingSolved();
        impresionMatriz(datos.getMatrizLogica());
    }

    void Update()
    {
        
    }

    
}
