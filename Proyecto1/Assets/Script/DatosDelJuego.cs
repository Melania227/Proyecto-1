using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosDelJuego : MonoBehaviour
{
    private int x;
    private int y;
    private List<List<int>> PistasX = new List<List<int>>();
    private List<List<int>> PistasY= new List<List<int>>();

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
    
    public void addListX(List<int> l) {
        PistasX.Add(l);
    }
    
    public void addListY(List<int> l)
    {
        PistasY.Add(l);
    }

    public List<List<int>> getPistasX() {
        return PistasX;
    }

    public List<List<int>> getPistasY()
    {
        return PistasY;
    }

}
