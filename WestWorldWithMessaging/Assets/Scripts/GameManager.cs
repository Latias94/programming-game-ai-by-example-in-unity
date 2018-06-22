using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float UpdateTimeInterval = 1;
    private Miner miner;
    private MinersWife minersWife;

    private void Start()
    {
        miner = new Miner(1, "Bob");
        EntityManager.Instance.RegisterEntity(miner);
        minersWife = new MinersWife(2, "Elsa");
        EntityManager.Instance.RegisterEntity(minersWife);

        StartCoroutine(MinerStartToWork());
    }

    IEnumerator MinerStartToWork()
    {
        for (int i = 0; i < 20; i++)
        {
            miner.EntityUpdate();
            minersWife.EntityUpdate();
            // 处理延时消息
            MessageDispatcher.Instance.DisplayDelayedMessages();
            yield return new WaitForSeconds(UpdateTimeInterval);
        }
    }
}