using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndlessLevelHandler : MonoBehaviour
{
    [SerializeField]
    GameObject[] sectionPrefabs; // A array que contém todas as possíveis secções de estrada.

    GameObject[] sectionPool = new GameObject[20]; // A array que, no início da partida, escolhe 20 para usar durante essa sessão.

    GameObject[] renderedSections = new GameObject[10]; // A array que contém as secções visíveis ao player (ativas).

    Transform playerTransform;

    WaitForSeconds waitFor100ms = new WaitForSeconds(0.1f); // Temporizador para atualizar a array das secções visíveis.

    const float sectionLength = 26f; // Cumprimento atual de uma secção de estrada no eixo Z.

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        LoadPrefabsToPool();

        LoadSectionsToLevel();

        StartCoroutine(UpdateLessOfTenCo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator UpdateLessOfTenCo()
    //Chama a função UpdateSectionPositions() a cada 0.1 segundos.
    {
        while (true)
        {
            UpdateSectionPositions();
            yield return waitFor100ms;
        }
    }

    void UpdateSectionPositions()
    /*
        Verifica se o jogador já ultrapassou as secções de estrada. Caso tenha ultrapassado, tornar esse segmento
        invisível (inativo) e substitui-lo por um aleatório não atualmente visível. A posição dessa nova secção é
        no fim do segmento.
    */
    {
        for (int i = 0; i < renderedSections.Length; i++)
        {
            if (renderedSections[i].transform.position.z - playerTransform.position.z < - sectionLength)
            {
                Vector3 lastSectionPosition = renderedSections[i].transform.position;
                renderedSections[i].SetActive(false);

                renderedSections[i] = GetRandomSectionFromPool();

                renderedSections[i].transform.position = new Vector3(0, -10, lastSectionPosition.z + sectionLength * renderedSections.Length);
                renderedSections[i].SetActive(true);
            }
        }
    }

    void LoadPrefabsToPool()
    /*
        Ao início da partida, escolher de todas as secções existentes uma porção para serem utilizados nesta
        sessão. Isto com o intuíto de tornar o jogo mais leve durante execução.
    */
    {
        int sectionIndex = 0;

        for (var i = 0; i < sectionPool.Length; i++)
        {
            sectionPool[i] = Instantiate(sectionPrefabs[sectionIndex]);
            sectionPool[i].SetActive(false);

            sectionIndex++;
            sectionIndex = (sectionIndex < sectionPrefabs.Length) ? sectionIndex : 0;
        }
    }

    void LoadSectionsToLevel()
    /*
        Carregar do array das secções da sessão ao array de secções visíveis para serem colocadas
        em frente à posição atual do jogador.  
    */
    {
        for (var i = 0; i < renderedSections.Length; i++)
        {
            GameObject section = GetRandomSectionFromPool();
            
            section.transform.position = new Vector3(0, -10, i * sectionLength);
            section.SetActive(true);
            
            renderedSections[i] = section;
        }

    }

    GameObject GetRandomSectionFromPool()
    // Devolve uma secção de estrada aleatório que ainda não esteja ativa no atual segmento.  
    {
        int randomIndex = Random.Range(0, sectionPool.Length);
        bool foundUnactiveSection = false;
        
        while (!foundUnactiveSection)
        {
            if (!sectionPool[randomIndex].activeInHierarchy)
                foundUnactiveSection = true;
            else
                randomIndex++;
                randomIndex = (randomIndex < sectionPool.Length) ? randomIndex : 0;
        }
        
        return sectionPool[randomIndex];
    }

}
