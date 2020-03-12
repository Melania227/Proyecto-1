using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGame : MonoBehaviour
{

    private float X;
    private float Y;

    private Vector2 tileSize;

    private GameObject[,] ObjectList;

    private float inicial1;
    private float inicial;

    private float calculo1;
    private float calculo2;

    private int rows;
    private int columns;

    public GameObject FrameLines;
    public GameObject tiles;


    // Start is called before the first frame update


    public void setVariables(int r, int c) {
        rows = r;
        columns = c;
        defineValues();
        paintTiles();
        drawFrame();
        otherLines();
    }

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

    public void drawFrame()
    {
        //Instanciar Marco principal
        GameObject Bottom = Instantiate(FrameLines);
        GameObject TopLine = Instantiate(FrameLines);
        GameObject Rigth = Instantiate(FrameLines);
        GameObject Left = Instantiate(FrameLines);

        //Posicion
        TopLine.transform.position = new Vector3(0, Y / 200, 2);
        Bottom.transform.position = new Vector3(0, -(Y / 200), 2);
        Rigth.transform.position = new Vector3((X / 200), 0, 2);
        Left.transform.position = new Vector3(-(X / 200), 0, 2);

        //Escala
        TopLine.transform.localScale = new Vector3(X, 15, 2);
        Bottom.transform.localScale = new Vector3(X, 15, 2);
        Rigth.transform.localScale = new Vector3(15, Y, 2);
        Left.transform.localScale = new Vector3(15, Y, 2);

    }

    public void otherLines()
    {
        //Otras lineas
        for (int row = 1; row < rows; row++)
        {

            GameObject obj = Instantiate(FrameLines);
            obj.transform.localScale = new Vector3(X, 15, 2);
            obj.transform.position = new Vector3(0, inicial1 - (calculo1 * row), 2);

        }
        for (int col = 1; col < columns; col++)
        {

            GameObject obj = Instantiate(FrameLines);
            obj.transform.localScale = new Vector3(15, Y, 2);
            obj.transform.position = new Vector3(inicial - (calculo2 * col), 0, 2);

        }

        Destroy(FrameLines);
    }

    public void paintTiles()
    {

        float totalX = -inicial + (calculo2 / 2);
        print(totalX);
        float totalY = -inicial1 + (calculo1 / 2);

        for (int j = 0; j < rows; j++)
        {
            for (int i = 0; i < columns; i++)
            {
                GameObject tileObj = Instantiate(tiles);
                tileObj.transform.localScale = new Vector3(tileSize.x, tileSize.y, 0);
                tileObj.transform.position = new Vector3(totalX + (calculo2 * i), -totalY - (calculo1 * j), 0);

                tileObj.GetComponent<SpriteRenderer>().color = Color.magenta;
                //yield return new WaitForSeconds((float)0.05 - (rows * (float)0.001));
                //tileObj.GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.4f, 0.6f, 0.3f);
                ObjectList[j, i] = tileObj;

            }
        }

    }

   /* IEnumerator correctAnswerAnimation()
    {

    }*/

    public void correctAnswer(int [,] answer)
    {
    
        for (int j = 0; j < rows; j++)
        {
            for (int i = 0; i < columns; i++)
            {
                if (answer[j,i] == 1)
                {
                    GameObject tileObj = Instantiate(tiles);
                    tileObj = ObjectList[j, i];
                    tileObj.GetComponent<SpriteRenderer>().color = Color.magenta;
                }
                else
                {
                    GameObject tileObj = Instantiate(tiles);
                    tileObj = ObjectList[j, i];
                    tileObj.GetComponent<SpriteRenderer>().color = Color.grey;
                }
            }
        }
    }


}
