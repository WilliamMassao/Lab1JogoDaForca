using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManageBotoes : MonoBehaviour // Essa classe é responsável por gerenciar os botões, realizando os redirecionamentos entre as cenas necessários.
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("score", 0); // Inicia a variável de Score (Pontuação do Jogo)
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMundoGame() // Método atribuído ao OnClick do botão de Start
    {
        SceneManager.LoadScene("Lab1"); // Muda de cena para a cena "Lab1"
        PlayerPrefs.SetInt("score", 0); // Inicia a variável de Score (Pontuação do Jogo)

    }

    public void RedirectStartPage() // Método atribuído ao OnClick do botão de Start Page
    {
        SceneManager.LoadScene("Lab1_start"); // Muda de cena para a cena "Lab1_start"
    }

    public void RedirectCreditsPage() // Método atribuído ao OnClick do botão de Creditos
    {
        SceneManager.LoadScene("Lab1_creditos"); // Muda de cena para a cena "Lab1_credits"
    }
}
