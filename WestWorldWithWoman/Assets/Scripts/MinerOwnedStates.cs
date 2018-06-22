using UnityEngine;

public class EnterMineAndDigForNugget : State<Miner>
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
            Debug.Log(miner.Name+"：走去金矿");
            miner.ChangeLocation(LocationType.Goldmine);
        }
    }

    public override void Execute(Miner miner)
    {
        miner.AddToGoldCarried(1);
        miner.IncreaseFatigue();
        Debug.Log(miner.Name+"：采到一个金块 | 身上有 " + miner.GoldCarried() + " 个金块");
        // 口袋里金块满了就去银行存
        if (miner.PocketsFull())
        {
            miner.GetFSM().ChangeState(VisitBankAndDepositGold.Instance);
        }

        // 口渴了就去酒吧喝威士忌
        if (miner.Thirsty())
        {
            miner.GetFSM().ChangeState(QuenchThirst.Instance);
        }
    }

    public override void Exit(Miner miner)
    {
        Debug.Log(miner.Name+"：离开金矿");
    }
}

/// <summary>
/// 回家睡觉
/// </summary>
public class GoHomeAndSleepTilRested : State<Miner>
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

        Debug.Log(miner.Name+"：走去小木屋");
        miner.ChangeLocation(LocationType.Shack);
    }

    public override void Execute(Miner miner)
    {
        if (!miner.Fatigued())
        {
            Debug.Log(miner.Name+"：睡得真好！是时候挖矿了！");
            miner.GetFSM().ChangeState(EnterMineAndDigForNugget.Instance);
        }
        else
        {
            miner.DecreaseFatigue();
            Debug.Log(miner.Name+"：ZZZZ...");
        }
    }

    public override void Exit(Miner miner)
    {
        Debug.Log(miner.Name+"：离开小木屋");
    }
}

/// <summary>
/// 去银行存金块喽
/// </summary>
public class VisitBankAndDepositGold : State<Miner>
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

        Debug.Log(miner.Name+"：走去银行");
        miner.ChangeLocation(LocationType.Bank);
    }

    public override void Execute(Miner miner)
    {
        miner.AddToWealth(miner.GoldCarried());
        miner.SetGoldCarried(0);
        Debug.Log(miner.Name+"：存金块，银行内现在共有 " + miner.Wealth() + " 个金块");
        if (miner.Wealth() >= miner.ComfortLevel)
        {
            Debug.Log(miner.Name+"：WooHoo！存够钱啦，现在回家泡妹啦！");
            miner.GetFSM().ChangeState(GoHomeAndSleepTilRested.Instance);
        }
        else
        {
            Debug.Log(miner.Name+"：没存够钱，继续挖矿吧~");
            miner.GetFSM().ChangeState(EnterMineAndDigForNugget.Instance);
        }
    }

    public override void Exit(Miner miner)
    {
        Debug.Log(miner.Name+"：离开银行");
    }
}

/// <summary>
/// 去银行存金块喽
/// </summary>
public class QuenchThirst : State<Miner>
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

        Debug.Log(miner.Name+"：走去酒吧");
        miner.ChangeLocation(LocationType.Saloon);
    }

    public override void Execute(Miner miner)
    {
        if (miner.Thirsty())
        {
            miner.BuyAndDrinkAWhiskey();
            Debug.Log(miner.Name+"：好酒好酒！！！");
            miner.GetFSM().ChangeState(EnterMineAndDigForNugget.Instance);
        }
        else
        {
            Debug.LogError("出错啦");
        }
    }

    public override void Exit(Miner miner)
    {
        Debug.Log(miner.Name+"：离开酒吧，感觉很舒服！");
    }
}