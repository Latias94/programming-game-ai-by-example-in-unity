using UnityEngine;

public class EnterMineAndDigForNugget : State
{
    public static EnterMineAndDigForNugget Instance { get; private set; }

    static EnterMineAndDigForNugget()
    {
        Instance = new EnterMineAndDigForNugget();
    }

    public override void Enter(Miner miner)
    {
        if (miner.Location() != LocationType.Goldmine)
        {
            Debug.Log("矿工：走去金矿");
            miner.ChangeLocation(LocationType.Goldmine);
        }
    }

    public override void Execute(Miner miner)
    {
        miner.AddToGoldCarried(1);
        miner.IncreaseFatigue();
        Debug.Log("矿工：采到一个金块");
        // 口袋里金块满了就去银行存
        if (miner.PocketsFull())
        {
            miner.ChangeState(VisitBankAndDepositGold.Instance);
        }

        // 口渴了就去酒吧喝威士忌
        if (miner.Thirsty())
        {
            miner.ChangeState(QuenchThirst.Instance);
        }
    }

    public override void Exit(Miner miner)
    {
        Debug.Log("旷工：带着满口袋的金块离开了金矿");
    }
}

/// <summary>
/// 回家睡觉
/// </summary>
public class GoHomeAndSleepTilRested : State
{
    public static GoHomeAndSleepTilRested Instance { get; private set; }

    static GoHomeAndSleepTilRested()
    {
        Instance = new GoHomeAndSleepTilRested();
    }

    public override void Enter(Miner miner)
    {
        if (miner.Location() == LocationType.Shack)
        {
            return;
        }

        Debug.Log("矿工：走去小木屋");
        miner.ChangeLocation(LocationType.Shack);
    }

    public override void Execute(Miner miner)
    {
        if (!miner.Fatigued())
        {
            Debug.Log("睡得真好！是时候挖矿了！");
            miner.ChangeState(EnterMineAndDigForNugget.Instance);
        }
        else
        {
            miner.DecreaseFatigue();
            Debug.Log("矿工：ZZZZ...");
        }
    }

    public override void Exit(Miner miner)
    {
        Debug.Log("矿工：离开小木屋");
    }
}

/// <summary>
/// 去银行存金块喽
/// </summary>
public class VisitBankAndDepositGold : State
{
    public static VisitBankAndDepositGold Instance { get; private set; }

    static VisitBankAndDepositGold()
    {
        Instance = new VisitBankAndDepositGold();
    }

    public override void Enter(Miner miner)
    {
        if (miner.Location() == LocationType.Bank)
        {
            return;
        }

        Debug.Log("矿工：走去银行");
        miner.ChangeLocation(LocationType.Bank);
    }

    public override void Execute(Miner miner)
    {
        miner.AddToWealth(miner.GoldCarried());
        miner.SetGoldCarried(0);
        Debug.Log("矿工：存金块，银行内现在共有 " + miner.Wealth() + " 个金块");
        if (miner.Wealth() >= miner.ComfortLevel)
        {
            Debug.Log("矿工：WooHoo！存够钱啦，现在回家泡妹啦！");
            miner.ChangeState(GoHomeAndSleepTilRested.Instance);
        }
        else
        {
            Debug.Log("矿工：没存够钱，继续挖矿吧~");
            miner.ChangeState(EnterMineAndDigForNugget.Instance);
        }
    }

    public override void Exit(Miner miner)
    {
        Debug.Log("矿工：离开银行");
    }
}

/// <summary>
/// 去银行存金块喽
/// </summary>
public class QuenchThirst : State
{
    public static QuenchThirst Instance { get; private set; }

    static QuenchThirst()
    {
        Instance = new QuenchThirst();
    }

    public override void Enter(Miner miner)
    {
        if (miner.Location() == LocationType.Saloon)
        {
            return;
        }

        Debug.Log("矿工：走去酒吧");
        miner.ChangeLocation(LocationType.Saloon);
    }

    public override void Execute(Miner miner)
    {
        if (miner.Thirsty())
        {
            miner.BuyAndDrinkAWhiskey();
            Debug.Log("矿工：好酒好酒！！！");
            miner.ChangeState(EnterMineAndDigForNugget.Instance);
        }
        else
        {
            Debug.LogError("出错啦");
        }
    }

    public override void Exit(Miner miner)
    {
        Debug.Log("矿工：离开酒吧，感觉很舒服！");
    }
}