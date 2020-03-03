using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosDelJuego : MonoBehaviour
{
    private int x;
    private int y;
    private List<Estructura> Pistas = new List<Estructura>();

    public DatosDelJuego() {
        x = y = 0;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setX(int x) {
        this.x = x;
    }
    public void setY(int y)
    {
        this.y = y;
    }
    public int getX()
    {
        return this.x;
    }
    public int getY()
    {
        return this.y;
    }
    public void addList(Estructura estruc)
    {
        this.Pistas.Add(estruc);
    }
}
