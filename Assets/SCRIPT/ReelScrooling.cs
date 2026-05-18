using UnityEngine;
using System.Collections;
using TMPro;

public class ReelScrooling : MonoBehaviour
{
    [SerializeField] GameObject Reel_1;
    [SerializeField] GameObject Reel_2;
    [SerializeField] GameObject Reel_3;
    [SerializeField] CoinManager coinManager;
    [SerializeField] TMP_Text News;

    bool spinning = false;

    float[] stopPositions = { -2f, -3.5f, -5f, -6.5f };

    float reel1Result;
    float reel2Result;
    float reel3Result;

    Animator handleAnimator;

    void Start()
    {
        handleAnimator = GetComponent<Animator>();
        News.text = "Press Space To Spin";
    }

    void Update()
    {
        if (spinning)
        {
            WrapReel(Reel_1);
            WrapReel(Reel_2);
            WrapReel(Reel_3);

            News.text = "***************";
        }

        if (Input.GetKeyDown(KeyCode.Space) && !spinning)
        {
            handleAnimator.SetTrigger("Handle");
            StartCoroutine(Spin());
        }
    }

    void WrapReel(GameObject reel)
    {
        if (reel.transform.localPosition.y < -8f)
        {
            Vector3 pos = reel.transform.localPosition;
            pos.y = -2f;
            reel.transform.localPosition = pos;
        }
    }

    IEnumerator Spin()
    {
        spinning = true;

        reel1Result = stopPositions[Random.Range(0, stopPositions.Length)];
        reel2Result = stopPositions[Random.Range(0, stopPositions.Length)];
        reel3Result = stopPositions[Random.Range(0, stopPositions.Length)];

        StartCoroutine(SpinReel(Reel_1, 3f, 12f, reel1Result));
        StartCoroutine(SpinReel(Reel_2, 4f, 14f, reel2Result));
        StartCoroutine(SpinReel(Reel_3, 5f, 16f, reel3Result));

        yield return new WaitForSeconds(6f);

        spinning = false;

        CheckWin();
    }

    IEnumerator SpinReel(GameObject reel, float spinTime, float speed, float stopY)
    {
        float timer = 0f;

        while (timer < spinTime)
        {
            reel.transform.localPosition += Vector3.down * speed * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        while (true)
        {
            reel.transform.localPosition += Vector3.down * speed * Time.deltaTime;

            float y = reel.transform.localPosition.y;

            if (y <= stopY)
            {
                Vector3 pos = reel.transform.localPosition;
                pos.y = stopY;
                reel.transform.localPosition = pos;
                break;
            }

            yield return null;
        }
    }

    void CheckWin()
    {
        Debug.Log("Final Result -> " + reel1Result + " | " + reel2Result + " | " + reel3Result);

        if (reel1Result == reel2Result && reel2Result == reel3Result)
        {
            Debug.Log("WIN!");
            coinManager.Result(true);
            News.text = "WIN! 5x Rewards";
        }
        else
        {
            Debug.Log("Lose");
            coinManager.Result(false);
            News.text = "LOOSE! 1x reduce";
        }
    }
}