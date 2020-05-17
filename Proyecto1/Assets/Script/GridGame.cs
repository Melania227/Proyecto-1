using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class GridGame : MonoBehaviour
{

    public float X = 0;
    public float Y = 0;

    private Vector2 tileSize;

    private GameObject[,] ObjectList;

    private float inicial1 =0;
    private float inicial =0;

    private float calculo1=0;
    private float calculo2 =0;

    private int rows =0;
    private int columns =0;

    public GameObject juego;

    public GameObject FrameLines;
    public GameObject tiles;

    public GameObject text;
    public GameObject clue_X;
    public GameObject clue_Y;

    private List<List<int>> PistasX = new List<List<int>>();
    private List<List<int>> PistasY = new List<List<int>>();

    public Button atras;



    //Definir las variables de la matriz y llamar a las funciones que la dibujan
    public void setVariables(int r, int c, List<List<int>> x1, List<List<int>> y1) 
    {
        rows = r;
        columns = c;
        PistasX = x1;
        PistasY = y1;
        defineValues();
        paintTiles();
        defineLines();
    }

    //Definir los tamaños
    public void defineValues()
    {
        if (columns > rows)
        {
            X = 2500;
            Y = (X / columns) * rows;
        }
        else
        {
            Y = 2500;
            X = (Y / rows) * columns;
        }

        inicial1 = (Y / 200);
        calculo1 = (inicial1 + inicial1) / rows;

        inicial = (X / 200);
        calculo2 = (inicial + inicial) / columns;

        tileSize = new Vector2((Y / rows) - 5, (X / columns) - 5);
        ObjectList = new GameObject[rows, columns];
    }

    //Pintar las lineas de la matriz
    public void defineLines()
    {
        //Otras lineas
        float totalX = -inicial + (calculo2 / 2);
        float totalY = -inicial1 + (calculo1 / 2);

        for (int row = 0; row <= rows; row++)
        {

            GameObject obj = Instantiate(FrameLines);
            obj.transform.localScale = new Vector3(X, 15, 2);
            obj.transform.position = new Vector3(0, inicial1 - (calculo1 * row), 2);
            obj.transform.SetParent(juego.transform);

        }
        for (int col = 0; col <= columns; col++)
        {

            GameObject obj = Instantiate(FrameLines);
            obj.transform.localScale = new Vector3(15, Y, 2);
            obj.transform.position = new Vector3(inicial - (calculo2 * col), 0, 2);
            obj.transform.SetParent(juego.transform);

        }

    }

    //Pintar los cuadros
    public void paintTiles()
    {

        float totalX = -inicial + (calculo2 / 2);
        float totalY = -inicial1 + (calculo1 / 2);

        for (int j = 0; j < rows; j++)
        {
            for (int i = 0; i < columns; i++)
            {
               
                    GameObject tileObj = Instantiate(tiles);
                    tileObj.transform.localScale = new Vector3(tileSize.x, tileSize.y, 0);
                    tileObj.transform.position = new Vector3(totalX + (calculo2 * i), -totalY - (calculo1 * j), 0);

                    tileObj.GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.4f, 0.6f, 0.3f);
                    ObjectList[j, i] = tileObj;
                    tileObj.transform.SetParent(juego.transform);
                
            }
        }
        clues();
    }

    //Pintar la respuesta correcta
    public void correctAnswer(int[,] answer)
    {

        for (int j = 0; j < rows; j++)
        {
            for (int i = 0; i < columns; i++)
            {
                if (answer[j, i] == 1)
                {
                    GameObject tileObj = ObjectList[j, i];
                    tileObj.GetComponent<SpriteRenderer>().color =  new Color(38f / 255f, 30f / 255f, 54f / 255f);
                }
                else
                {
                    GameObject tileObj = ObjectList[j, i];
                    tileObj.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }
    }

    //Pintar un cuadro
    public void paintTile(int i, int j, int answer)
    {
        if (answer == 1)
        {
            GameObject tileObj = ObjectList[i, j];
            tileObj.GetComponent<SpriteRenderer>().color = new Color(38f / 255f, 30f / 255f, 54f / 255f);
            tileObj.transform.SetParent(juego.transform);
        }
        else if (answer == 2)
        {
            GameObject tileObj = ObjectList[i, j];
            tileObj.GetComponent<SpriteRenderer>().color = Color.white;
            tileObj.transform.SetParent(juego.transform);
        }
        else {
            GameObject tileObj = ObjectList[i, j];
            tileObj.GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.4f, 0.6f, 0.3f);
            tileObj.transform.SetParent(juego.transform);
        }
    
    }

    //Mostrar las pistas
    public void clues() {

        float totalX = -inicial + (calculo2 / 2);
        float totalY = -inicial1 + (calculo1 / 2);

        for (int j = 0; j < rows; j++)
        {
            
            
            //NUMEROS
            GameObject txtObj = Instantiate(clue_X);
            txtObj.transform.localScale = new Vector3((float)0.1, (float)0.1);
            txtObj.transform.position = new Vector3(totalX + (calculo2 * -(float)3)-1, -totalY - (calculo1 * j), 0);
            List<int> listaActual = PistasX[j+1];
            string actualClue = "";
            for (int x = 0; x < listaActual.Count; x++) {
                actualClue += listaActual[x].ToString();
                if (x != listaActual.Count - 1)
                {
                    actualClue += " ";
                }
            }
            txtObj.GetComponent<UnityEngine.UI.Text>().text = (actualClue);
            txtObj.transform.SetParent(juego.transform);

        }
        for (int j = 0; j < columns; j++)
        {
            
            //NUMEROS
            GameObject txtObj = Instantiate(clue_Y);
            txtObj.transform.localScale = new Vector3((float)0.1, (float)0.1);
            txtObj.transform.position = new Vector3(totalX + (calculo2 * j), -totalY - (calculo1 * -(float)1.5)+ (float)3.5, 0);
            List<int> listaActual = PistasY[j + 1];
            string actualClue = "";
            for (int x = 0; x < listaActual.Count; x++)
            {
                actualClue += listaActual[x].ToString();
                if (x != listaActual.Count - 1)
                {
                    actualClue += "\n";
                }
            }
            txtObj.GetComponent<UnityEngine.UI.Text>().text = (actualClue);
            txtObj.transform.SetParent(juego.transform);
        }

    }

    //Muestra el tiempo, activa el botón de reversa
    public void showTime(string time) 
    {
        text.GetComponent<UnityEngine.UI.Text>().text = (time);
        atras.interactable = true;
    }



}