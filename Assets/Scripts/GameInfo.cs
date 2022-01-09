using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour
{
    public int Health;
    public int MaxHealth;
    public float Energy;


    public float MaxEnergy;
    public float Speed;
    public int Point = 1;
    public int speedpoint = 0;
    public int energypoint = 0;
    public int healthpoint = 0;
    public Text textHealth;
    public Text textSpeed;
    public Text textEnergy;
    public Text textpoint;
    public GameObject canvas;
    void Start()
    {
        textEnergy.text = energypoint.ToString();
        textSpeed.text = speedpoint.ToString();
        textpoint.text = Point.ToString();
        textHealth.text = healthpoint.ToString();

        DontDestroyOnLoad(this.gameObject);

    }
    public void PlusHealth()
    {
        if (Point <= 0) return;
        healthpoint += 1;
        MaxHealth += 1;
        Point--;
        textpoint.text = Point.ToString();
        textHealth.text = healthpoint.ToString();

    }
    public void PlusEnergy()
    {
        if (Point <= 0) return;
        energypoint += 1;
        MaxEnergy += 5;
        Point--;
        textpoint.text = Point.ToString();
        textEnergy.text = energypoint.ToString();
    }
    public void PlusSpeed()
    {
        if (Point <= 0) return;
        speedpoint += 1;
        Speed += 0.2f;
        Point--;
        textpoint.text = Point.ToString();
        textSpeed.text = speedpoint.ToString();
    }
    public void PlusPoint()
    {
        
        Point++;
        textpoint.text = Point.ToString();
      
    }
    public void Done()
    {
        Debug.Log(Energy);
        FindObjectOfType<Explorer>().setStat(Speed, Health, MaxHealth, Energy, MaxEnergy);
        canvas.SetActive(false);
    }
    public void setCanvas(bool tmp)
    {
        canvas.SetActive(tmp);
    }
    public void setStat(float a, int b, int c, float d, float e)
    {
        Speed = a;
        Health = b;
        MaxHealth = c;
        Energy = d;
        MaxEnergy = e;
    }
}
