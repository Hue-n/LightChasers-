using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void FinishGame();
public class GameSetter : MonoBehaviour
{
    public static event FinishGame finishGameCaster;

    public Transform attackerSpawn;
    public Transform defenderSpawn;

    public GameObject attackerPrefab;
    public GameObject defenderPrefab;
    GameObject attackerReference;
    GameObject defenderReference;

    float gameTime;
    bool game = false;

    // Start is called before the first frame update
    void Start()
    {
        gameTime = NewGameManager.instance.maxGameTime;
    }

    void StartGame()
    {
        game = true;
        attackerReference = Instantiate(attackerPrefab, attackerSpawn.transform.position, Quaternion.identity);
        defenderReference = Instantiate(defenderPrefab, defenderSpawn.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (game)
        {
            gameTime -= Time.deltaTime;
        }
    }
}
