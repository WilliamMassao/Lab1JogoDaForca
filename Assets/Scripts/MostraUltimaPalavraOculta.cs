using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostraUltimaPalavraOculta : MonoBehaviour // Essa classe é responsável por exibir a palavra oculta na página de vitória
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("palavraOculta").GetComponent<Text>().text = "A palavra era: " + PlayerPrefs.GetString("ultimaPalavra"); // Exibe a última palavra oculta na tela
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
