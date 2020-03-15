using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Juego : MonoBehaviour
{
    public Lector lector;
    public DatosDelJuego datos;
    public GridGame grid;
    public Thread hiloBackt;
    public Thread hiloPintar;

    public InputField field;
    public Toggle animacion;
    public GameObject text;
    public GameObject juego;

    public string path;
    public bool animation;

    void impresionMatriz(int[,] matrizLogica) {
        string res = "";

        for (int i = 0; i < datos.getX(); i++)
        {

            res += "\n";
            for (int j = 0; j < datos.getY(); j++)
            {
                if (datos.getMatrizLogica()[i, j] == 1)
                {
                    res += "▓";
                }
                else

                {
                    res += "▒";
                }

            }
        }

        print(res);
    }

    public void hiloBacktracking()
    {
        hiloBackt = new Thread(Run);
        hiloBackt.Start();
       

    }

    public void Run()
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        backtrackingSolvedAnimated();
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        //ESTO PASA EL TIEMPO MAL, HAY QUE ARREGLARLO PERO ESTOY CANSADA :(
        MainThread.thread.AddJob(() => {
            text.GetComponent<UnityEngine.UI.Text>().text = elapsedMs.ToString();
        });

    }

    bool backtrackingSolvedAnimated()
    {
        int[] porValidar = sigPuntoVacio(datos.getMatrizLogica());
        if (porValidar[0] == 0 && porValidar[1] == 0)
        {
            return true; //validarColumnas() && validarFilas();
        }

        porValidar[0] = porValidar[0] - 1;
        porValidar[1] = porValidar[1] - 1;


        if (validarPosicion(porValidar, 1))
        {
            datos.setMatrizLogica(porValidar[0], porValidar[1], 1);
            MainThread.thread.AddJob(() => { grid.paintTile(porValidar[0], porValidar[1], 1); });
            if (backtrackingSolvedAnimated())
            {
                return true;
            }
            MainThread.thread.AddJob(() => { grid.paintTile(porValidar[0], porValidar[1], 0); });
            datos.setMatrizLogica(porValidar[0], porValidar[1], 0);
        }

        if (validarPosicion(porValidar, 2))
        {
            datos.setMatrizLogica(porValidar[0], porValidar[1], 2);
            MainThread.thread.AddJob(() => { grid.paintTile(porValidar[0], porValidar[1], 2); });
            if (backtrackingSolvedAnimated())
            {
                return true;
            }
            MainThread.thread.AddJob(() => { grid.paintTile(porValidar[0], porValidar[1], 0); });
            datos.setMatrizLogica(porValidar[0], porValidar[1], 0);
        }

        return false;
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


        if (validarPosicion(porValidar, 1)) {
            datos.setMatrizLogica(porValidar[0], porValidar[1], 1);
            if (backtrackingSolved()) {
                return true;
            }
            datos.setMatrizLogica(porValidar[0], porValidar[1], 0);
        }

        if (validarPosicion(porValidar, 2)) {
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

    int[] sigPuntoVacio(int[,] matrizLogica) {
        int[] puntoAValidar = { 0, 0 };
        for (int i = 0; i < datos.getX(); i++) {
            for (int j = 0; j < datos.getY(); j++) {
                if (matrizLogica[i, j] == 0) {
                    puntoAValidar[0] = i + 1;//x
                    puntoAValidar[1] = j + 1;//y
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
        fila[columnaAct] = pruebeCon;
        int[] columna = genereArrayColumna(columnaAct);
        columna[filaAct] = pruebeCon;
        if (pruebeCon == 2) {
            return validarLineasPa2(fila, datos.getPistasX()[filaAct + 1]) && validarLineasPa2(columna, datos.getPistasY()[columnaAct + 1]);
        }

        if (datos.getPistasX()[filaAct + 1].Count == 0 || datos.getPistasY()[columnaAct + 1].Count == 0) {
            //print("AQUI WE");
            return false;
        }

        if (datos.getPistasX()[filaAct + 1][0] == datos.getY() || datos.getPistasY()[columnaAct + 1][0] == datos.getX()) {
            //print("AQUI PUTOS");
            return true;
        }

        //print("HOLA PPUTOS CBRIJNCAENOEQ: " + gruposTotales(fila, datos.getPistasX()[filaAct + 1].Count) + datos.getPistasX()[filaAct + 1].Count);
        if (gruposTotales(fila, datos.getPistasX()[filaAct + 1].Count) > datos.getPistasX()[filaAct + 1].Count || gruposTotales(columna, datos.getPistasY()[columnaAct + 1].Count) > datos.getPistasY()[columnaAct + 1].Count)
        {
            //print("AQUI MAMAVERGAS");
            return false;
        }

        //imprimirPistas(datos.getPistasX()[filaAct+1], datos.getPistasY()[columnaAct+1]);
        if (validarLineas(columna, datos.getPistasY()[columnaAct + 1]) && validarLineas(fila, datos.getPistasX()[filaAct + 1])) {
            return true;
        }

        return false;
    }

    void imprimirPistas(List<int> pistasC, List<int> pistasF) {
        string hola = "";
        string hola2 = "";

        for (int i = 0; i < pistasC.Count; i++) {
            hola += " " + pistasC[i];
        }
        //print("LAS PISTAS DE FILA SON:" + hola);
        for (int j = 0; j < pistasF.Count; j++)
        {
            hola2 += " " + pistasF[j];
        }
        //print("LAS PISTAS DE COLUMNA SON:" + hola2);
    }

    bool validarLineasPa2(int[] linea, List<int> pistas) {
        if (espaciosDisponibles(linea) < porRellenar(linea, pistas)) {
            return false;
        }

        int[] gruposComoPistas = gruposMarcados(linea, pistas.Count);
        for (int i = 0; i < gruposComoPistas.Length; i++)
        {
            if (gruposComoPistas[i] != 0 && gruposComoPistas[i] < pistas[i])
            {
                return false;
            }
        }

        return true;
    }

    int espaciosDisponibles(int[] linea) {
        int contador = 0;
        for (int i = 0; i < linea.Length; i++) {
            if (linea[i] != 1 && linea[i] != 2) {
                contador++;
            }
        }
        return contador;
    }

    int porRellenar(int[] linea, List<int> pistas) {
        int contador = 0;
        for (int i = 0; i < linea.Length; i++) {
            if (linea[i] == 1) {
                contador++;
            }
        }
        contador = totalAMarcar(pistas) - contador;
        return contador;
    }

    bool validarLineas(int[] linea, List<int> pistas) {
        int[] gruposMarcadosV = gruposMarcados(linea, (pistas.Count));

        for (int i = 0; i < pistas.Count; i++) {
            if (gruposMarcadosV[i] > pistas[i])
            {
                //print("AY");
                return false;
            }
            if (lineaCompleta(linea, pistas) && gruposMarcadosV[i] != pistas[i]) {
                //print("WE");
                return false;
            }

        }

        return true;
    }

    int gruposTotales(int[] linea, int cantidad)
    {
        int contador = 0;
        int cantGrupos = 0;
        for (int i = 0; i < linea.Length; i++)
        {
            if (linea[i] == 1)
            {
                contador += 1;
            }

            else if (contador != 0 && linea[i] != 1)
            {
                contador = 0;
                cantGrupos++;
            }
        }
        if (contador != 0)
        {
            cantGrupos++;
        }
        return cantGrupos;
    }

    int[] gruposMarcados(int[] linea, int cantidad) {
        int contador = 0;
        int pos = 0;
        int[] gruposFinales = new int[cantidad];
        for (int j = 0; j < cantidad; j++) {
            gruposFinales[j] = 0;
        }
        for (int i = 0; i < linea.Length; i++) {
            //print("LISTA: " + gruposFinales[pos]); 
            if (linea[i] == 1) {
                contador += 1;
            }

            else if (contador != 0 && pos < cantidad) {
                gruposFinales[pos] = contador;
                contador = 0;
                pos++;
            }
        }
        if (contador != 0 && pos < cantidad) {
            gruposFinales[pos] = contador;
        }
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

    bool lineaCompleta(int[] linea, List<int> pistas) {
        for (int i = 0; i < linea.Length; i++) {
            if (linea[i] == 0) {
                return false;
            }
        }
        return true;
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

    public void getPath() {
        path = field.text;
        //print(path);
        //print("gatito");

    }
   
    public void animationOn()
    {
        if(animacion.isOn)
            animation = true;
        else
            animation = false;
    }

    public void iniciar() {

        Start();
    }

    void Start()
    {
        if (path != null && path != "") {
            lector.Leer(path);
            datos = lector.datos;
            datos.rellenaMatrizLogica();
            grid.setVariables(datos.getX(), datos.getY());
            if (animation) {
                hiloBacktracking();
            }
            else
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                backtrackingSolved();
                grid.correctAnswer(datos.getMatrizLogica());
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                text.GetComponent<UnityEngine.UI.Text>().text = elapsedMs.ToString();
            }
        }
        else{
           
            EditorUtility.DisplayDialog("Error.", "Path not found, please add one in options section." , "Ok");
            juego.gameObject.SetActive(false);
        }
        
    }


    void Update()
    {
        

    }

    
}
