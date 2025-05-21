using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameLogic_Oleadas : MonoBehaviour
{

    [SerializeField] List<GameObject> personajes;
    [SerializeField] GameObject player1;
    //[SerializeField] GameObject player2;

    [SerializeField] Slider player1Health;
    //[SerializeField] Slider player2Health;

    private BaseFighter fighter1;
    //private BaseFighter fighter2;


    [Header("Input Actions")]
    public PlayerMovements.ActionSet actionSetPl1;
    //public PlayerMovements.ActionSet actionSetPl2;



    private void Start()
    {

        {

            string personaje1 = CharacterSelectorManager.confirmedCharacter1;
            GameObject personaje = personajes.Find((x) => x.name == personaje1);

            if (personaje1 == "Random")
            {
                int choice = Random.Range(0, personajes.Count);

                personaje = personajes[choice];
                InitCharacter(personaje, player1.transform, actionSetPl1, "Player1");

            }
            else
            {
                Debug.Log(personaje);
                InitCharacter(personaje, player1.transform, actionSetPl1, "Player1");
            }

        }

        //{
        //    string personaje2 = CharacterSelectorManager.confirmedCharacter2;
        //    GameObject personaje = personajes.Find((x) => x.name == personaje2);

        //    if (personaje2 == "Random")
        //    {
        //        int choice = Random.Range(0, personajes.Count);

        //        personaje = personajes[choice];
        //        InitCharacter(personaje, player2.transform, actionSetPl2, "Player2");

        //    }
        //    else
        //    {
        //        InitCharacter(personaje, player2.transform, actionSetPl2, "Player2");
        //    }
        //}
    }

    private void Update()
    {
        if (fighter1 != null)
            player1Health.value = fighter1.currentHealth;

        //if (fighter2 != null)
        //    player2Health.value = fighter2.currentHealth;
    }

    private void InitCharacter(GameObject prefabPersonaje, Transform playerTransform, PlayerMovements.ActionSet actionSet, string tag)
    {
        GameObject player = Instantiate(prefabPersonaje, playerTransform.position, Quaternion.identity, playerTransform);
        player.tag = tag;

        BaseFighter baseFighter = player.GetComponent<BaseFighter>();
        baseFighter.InitInputs(actionSet);

        if (tag == "Player1")
        {
            player1Health.maxValue = baseFighter.health;
            player1Health.value = baseFighter.health;

            fighter1 = baseFighter;
        }
        //else if (tag == "Player2")
        //{
        //    player2Health.maxValue = baseFighter.health;
        //    player2Health.value = baseFighter.health;

        //    fighter2 = baseFighter;
        //}
    }
}