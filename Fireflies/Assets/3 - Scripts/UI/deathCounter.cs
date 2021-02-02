using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class deathCounter : MonoBehaviour
{   
    public static deathCounter instance { get; private set; }
    public TextMeshProUGUI deathText;
    int counter = 0;
    
    // Start is called before the first frame update
    void Awake()
    {
        // Singleton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        deathText.text = counter.ToString();

        DontDestroyOnLoad(gameObject);
    }

    public void addDeath()
    {
        counter += 1;
        deathText.text = counter.ToString();
    }
}
