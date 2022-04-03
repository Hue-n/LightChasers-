using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Winner(int id);
public delegate void TimeCaster(float val);

public class GameSetter : MonoBehaviour
{
    public static event Winner winnerCaster;
    public static event TimeCaster timeCaster;

    public Transform attackerSpawn;
    public Transform defenderSpawn;

    public GameObject attackerPrefab;
    public GameObject defenderPrefab;
    GameObject attackerReference;
    GameObject defenderReference;

    [SerializeField] float gameTime;
    bool game = false;

    // Start is called before the first frame update
    void Start()
    {
        //gameTime = NewGameManager.instance.maxGameTime;
        AttackCollisionCheck.aWinCaster += AnnounceWinner;
        StartGame();
    }

    private void OnDestroy()
    {
        AttackCollisionCheck.aWinCaster -= AnnounceWinner;
    }

    void AnnounceWinner(int winner)
    {
        winnerCaster?.Invoke(winner);
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
            gameTime = Mathf.Clamp(gameTime, 0, float.MaxValue);
            timeCaster?.Invoke(gameTime);

            if (gameTime == 0)
            {
                AnnounceWinner(0);
                game = false;
            }
        }
    }
}
