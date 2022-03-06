using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject letra;                // Objeto de Texto que india a letra do jogo
    public GameObject centro;               // Objeto de Texto que indica o centro da tela

    private string palavraOculta = "";      // Palavra a ser descoberta 
    // private string[] palavrasOcultas = new string[] { "carro", "jogo", "Futebol" }; // Array de Palavras a serem descobertas - Lab2
    private int tamanhoPalavraOculta;       // Tamanho da palavra a ser descoberta
    char[] letrasOcultas;                  // Letras da palavra a ser descoberta
    bool[] letrasDescobertas;              // Indicador de quais letras já foram descoberas

    private int numTentativas;              // Arnazena as tentativas válidas da rodada
    private int MaxNumTentativas;           // Número máximo de tetativas para a Forca ou Salvação
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        centro = GameObject.Find("centroDaTela");
        InitGame();                                   // Inicia a exibição da palavra
        InitLetras();                                 // Inicia a captura das teclas fornecidas pelo jogador
        numTentativas = 0;                            // Define o início das tentativas como 0
        MaxNumTentativas = palavraOculta.Length + 4;  // Define o máximo de tentativas baseando-se no tamanho da palavra
        AtualizarNumeroTentativas();                  // Atualiza o número de tentativas na tela
        AtualizarScore();                             // Atualliza o Score na tela
    }

    // Update is called once per frame
    void Update()
    {
        verificarTeclado();
    }

    void InitLetras()
    {
        int numLetras = tamanhoPalavraOculta; // Define o Tamanho da palavra para ser utilizado no laço de repetição
        for (int i = 0; i < numLetras; i++)
        {
            Vector3 novaPosicao;
            novaPosicao = new Vector3(centro.transform.position.x + ((i - numLetras / 2.0f) * 80), centro.transform.position.y, centro.transform.position.z);
            GameObject l = (GameObject)Instantiate(letra, novaPosicao, Quaternion.identity);
            l.name = "letra" + (i + 1);   // Nomeia na hierarquia a GameObject com letra-(iésima+1), i = 1...numLetras
            l.transform.SetParent(GameObject.Find("Canvas").transform);  // Posiciona-se como filho do GameObject Canvas 

        }
    }

    void InitGame()
    {
        // palavraOculta = "Elefante";                         // Definição da palavra a ser descoberta (Lab 1 parte A)
        // int numeroAleatorio = Random.Range(0, palavrasOcultas.Length); // Sorteia-se aleatoriamente um número dentro do array de acordo com o seu tamanho - Lab 2
        // palavraOculta = palavrasOcultas[numeroAleatorio];   // Seleciona-se uma palavra dentro do array aleatoriamente - Lab 2
        palavraOculta = PegaUmaPalavraDoArquivo();    // Define a palavra a ser descoberta pegando-a do arquivo
        tamanhoPalavraOculta = palavraOculta.Length;        // Definição do número de letras da palavra baseado em seu tamnaho
        palavraOculta = palavraOculta.ToUpper();            // Converte todas as letras da palavra para maiúsculas
        letrasOcultas = new char[tamanhoPalavraOculta];     // Instancia-se o array char das letras da palavra
        letrasDescobertas = new bool[tamanhoPalavraOculta]; // Instancia-se o array bool do indicador de letras certas
        letrasOcultas = palavraOculta.ToCharArray();        // Copia-se a palavra do array de letras

    }


    void verificarTeclado()
    {
        if (Input.anyKeyDown)
        {
            GameObject.Find("mensagemErro").GetComponent<Text>().text = "";     // Limpa a mensagem de erro
            int lastScore = PlayerPrefs.GetInt("score");
            char letraTeclada = Input.inputString.ToCharArray()[0];             // Recebe o Input do Teclado
            int letraTecladaComoInt = System.Convert.ToInt32(letraTeclada);     // Convert o Input para Inteiro

            if (letraTecladaComoInt >= 97 && letraTecladaComoInt <= 122)
            {
                numTentativas++; // Incremento do número de tentativas
                AtualizarNumeroTentativas(); // Atualiza o número de tentativas na tela
                if (numTentativas > MaxNumTentativas)
                {
                    SceneManager.LoadScene("Lab1_forca"); // Muda de cena para a cena "Lab1_forca"
                }
                for (int i = 0; i < tamanhoPalavraOculta; i++)
                {
                    if (!letrasDescobertas[i])
                    {
                        letraTeclada = System.Char.ToUpper(letraTeclada); // Transforma em maiúscula o char
                        if (letrasOcultas[i] == letraTeclada)
                        {
                            letrasDescobertas[i] = true;
                            GameObject.Find("letra" + (i + 1)).GetComponent<Text>().text = letraTeclada.ToString(); // Sobrescreve o valor do Objeto para a letra acertada
                            score = PlayerPrefs.GetInt("score"); // Pega o valor da variável score
                            score++;                             // Itera 1 ao score ao acertar a letra
                            PlayerPrefs.SetInt("score", score);  // Atribui o novo valor à variável score
                            AtualizarScore();                    // Atualiza o score na tela
                        }
                        verificaSePalavraDescoberta();
                    }
                }
                Debug.Log("LastScore: " + lastScore);
                Debug.Log("CurrentScore: " + PlayerPrefs.GetInt("score"));

                if (lastScore == PlayerPrefs.GetInt("score"))
                {
                    GameObject.Find("mensagemErro").GetComponent<Text>().text = "Errou! Tente novamente..."; // Exibe uma mensagem de erro ao digitar uma letra incorreta

                }
            }
        }
    }

        void AtualizarNumeroTentativas() // Método que atualiza o número de tentativas na tela
        {
            GameObject.Find("numTentativas").GetComponent<Text>().text = numTentativas + "/" + MaxNumTentativas; // Atualiza o valor do objeto para o número atual de tentativas
        }

        void AtualizarScore() // Método que atualiza o score na tela
        {
            GameObject.Find("scoreUI").GetComponent<Text>().text = "Score: " + score; // Atualizar o valor do Score para o número atual de pontos
        }

        void verificaSePalavraDescoberta() // Metodo valida se o Jogador ganhou
        {
            if (score == tamanhoPalavraOculta) // Verificar se o Jogador ganhou o Jogo
            {
                PlayerPrefs.SetString("ultimaPalavra", palavraOculta);  // Passa a última palavra para outra tela
                SceneManager.LoadScene("Lab1_salvo");                   // Muda de cena para a cena "Lab1_salvo"
            }
        }

        string PegaUmaPalavraDoArquivo() // Metodo pega uma palavra de forma aleatória dentro do arquivo palavras.txt na pasta Resources
        {
            TextAsset t1 = (TextAsset)Resources.Load("palavras", typeof(TextAsset)); // Abre o arquivo palavras.txt
            string s = t1.text;                                             // Pega o conteúdo de dentro do arquivo
            string[] palavras = s.Split(' ');                               // Divide o conteúdo do arquivo através dos espaços em um array
            int posicaoAleatoria = Random.Range(0, palavras.Length);    // Sorteia um número baseado na quantidade de palavras 
            return palavras[posicaoAleatoria];                              // Retorna uma palavra baseada na posição sorteada
        }

}

