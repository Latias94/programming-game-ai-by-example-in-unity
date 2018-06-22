using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float UpdateTimeInterval = 1;
    private Miner miner;

    private void Start()
    {
        miner = new Miner(1);
        StartCoroutine(MinerStartToWork());
    }

    IEnumerator MinerStartToWork()
    {
        for (int i = 0; i < 20; i++)
        {
            miner.EntityUpdate();
            yield return new WaitForSeconds(UpdateTimeInterval);
        }
    }
}