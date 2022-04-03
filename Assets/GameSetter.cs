using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Winner(int id);
public delegate void TimeCaster(float val);
public delegate void CataTimeCaster(float val);
public delegate void CatastropheDone();

public delegate void ShakeScreenCaster(float duration, float magnitude, float falloff);
// Catastrophes
public delegate void RotateMinimap();
public class GameSetter : MonoBehaviour
{
    public static event Winner winnerCaster;
    public static event TimeCaster timeCaster;
    public static event CataTimeCaster cataTime;
    public static event RotateMinimap rotateMiniMap;

    public Transform attackerSpawn;
    public Transform defenderSpawn;

    public GameObject attackerPrefab;
    public GameObject defenderPrefab;
    public GameObject explosionVFX;

    public GameObject minimapReference;
    GameObject attackerReference;
    GameObject defenderReference;

    [SerializeField] float gameTime;
    bool game = false;

    public List<float> Gates = new List<float>(3);
    float catastropheTimer;


    bool catastropheCallback = false;

    // Catastrophes
    public static event ShakeScreenCaster shakeScreenCaster;

    // Start is called before the first frame update
    void Start()
    {
        //gameTime = NewGameManager.instance.maxGameTime;
        AttackCollisionCheck.aWinCaster += AnnounceWinner;
        ShakeWindow.cataDoneCaster += ReceiveCatastropheCallback;

        catastropheTimer = Gates[0];
        StartCoroutine(CatastropheTime());
        StartGame();
    }

    private void OnDestroy()
    {
        AttackCollisionCheck.aWinCaster -= AnnounceWinner;
        ShakeWindow.cataDoneCaster -= ReceiveCatastropheCallback;
    }

    void AnnounceWinner(int winner)
    {
        Instantiate(explosionVFX, attackerReference.transform.position, Quaternion.identity);
        winnerCaster?.Invoke(winner);
    }

    void StartGame()
    {
        game = true;
        attackerReference = Instantiate(attackerPrefab, attackerSpawn.transform.position, Quaternion.identity);
        defenderReference = Instantiate(defenderPrefab, defenderSpawn.transform.position, Quaternion.identity);
    }

    IEnumerator CatastropheTime()
    {
        // first one starts at ten seconds, lasts 20 seconds
        // second one starts ten seconds after at 40 seconds, lasts 30 seconds
        // third one starts ten seconds after that and lasts 10 seconds

        for (int i = 0; i < 3; i++)
        {
            float time = 0;
            // countdown hits zero.
            yield return CountDown();
            // catastrophe and wait for callback
            switch (i)
            {
                case 0:
                    time = 20;
                    break;
                case 1:
                    time = 30;
                    break;
                case 2:
                    time = 10;
                    break;
            }

            CallForCatastrophe(time, i + 1);
            yield return WaitForCatastropheCallback();
            catastropheTimer = Gates[i];
        }
    }

    void CallForCatastrophe(float seconds, int round)
    {
        int answer = (int)Random.Range(0, 2);

        switch (answer)
        {
            case 0:
                attackerReference.GetComponent<AttackerMovementAlex>().duration = seconds;
                attackerReference.GetComponent<AttackerMovementAlex>().SetForward(false);

                attackerReference.GetComponent<PlayerControllerHuen>().duration = seconds;
                defenderReference.GetComponent<PlayerControllerHuen>().SetInverted(true);
                break;
            case 1:
                shakeScreenCaster(seconds, 5 * round, 1);
                break;
        }
    }

    IEnumerator WaitForCatastropheCallback()
    {
        while (!catastropheCallback)
        {
            yield return null;
        }
        catastropheCallback = false;
    }

    void ReceiveCatastropheCallback()
    {
        catastropheCallback = true;
    }

    IEnumerator CountDown()
    {
        while (catastropheTimer > 0)
        {
            catastropheTimer -= Time.deltaTime;
            catastropheTimer = Mathf.Clamp(catastropheTimer, 0, float.MaxValue);

            cataTime?.Invoke(catastropheTimer);
            yield return null;
        }
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
