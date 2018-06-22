using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 管理消息发送的类
/// 处理立刻被发送的消息，和打上时间戳的消息
/// </summary>
public class MessageDispatcher
{
    public static MessageDispatcher Instance { get; private set; }

    static MessageDispatcher()
    {
        Instance = new MessageDispatcher();
    }

    private MessageDispatcher()
    {
        priorityQueue = new HashSet<Telegram>();
    }

    /// <summary>
    /// 根据时间排序的优先级队列
    /// </summary>
    private HashSet<Telegram> priorityQueue;

    /// <summary>
    /// 该方法被 DispatchMessage 或者 DispatchDelayedMessages 利用。
    /// 该方法用最新创建的 telegram 调用接受实体的消息处理成员函数 receiver
    /// </summary>
    public void Discharge(BaseGameEntity receiver, Telegram telegram)
    {
        if (!receiver.HandleMessage(telegram))
        {
            Debug.LogWarning("消息未处理");
        }
    }

    /// <summary>
    /// 创建和管理消息
    /// </summary>
    /// <param name="delay">时间的延迟（要立刻发送就用零或负值）</param>
    /// <param name="senderId">发送者 ID</param>
    /// <param name="receiverId">接受者 ID</param>
    /// <param name="message">消息本身</param>
    /// <param name="extraInfo">附加消息</param>
    public void DispatchMessage(
        float delay,
        int senderId,
        int receiverId,
        MessageType message,
        Dictionary<string, string> extraInfo)
    {
        // 获得消息发送者
        BaseGameEntity sender = EntityManager.Instance.GetEntityFromId(senderId);

        // 获得消息接受者
        BaseGameEntity receiver = EntityManager.Instance.GetEntityFromId(receiverId);
        if (receiver == null)
        {
            Debug.LogWarning("[MessageDispatcher] 找不到消息接收者");
            return;
        }

        float currentTime = Time.time;
        if (delay <= 0)
        {
            Telegram telegram = new Telegram(0, sender, receiver, message, extraInfo);

            Debug.Log(string.Format(
                "消息发送时间: {0} ，发送者是：{1}，接收者是：{2}。消息是 {3}",
                currentTime,
                sender.Name,
                receiver.Name,
                message.ToString()));
            Discharge(receiver, telegram);
        }
        else
        {
            Telegram delayedTelegram = new Telegram(currentTime + delay, sender, receiver, message, extraInfo);
            priorityQueue.Add(delayedTelegram);

            Debug.Log(string.Format(
                "延时消息发送时间: {0} ，发送者是：{1}，接收者是：{2}。消息是 {3}",
                currentTime,
                sender.Name,
                receiver.Name,
                message.ToString()));
        }
    }


    /// <summary>
    /// 发送延时消息
    /// 这个方法会放在游戏的主循环中，以正确地和及时地发送任何定时的消息
    /// </summary>
    public void DisplayDelayedMessages()
    {
        float currentTime = Time.time;
        while (priorityQueue.Count > 0 &&
               priorityQueue.First().DispatchTime < currentTime &&
               priorityQueue.First().DispatchTime > 0)
        {
            Telegram telegram = priorityQueue.First();
            BaseGameEntity receiver = telegram.Receiver;

            Debug.Log(string.Format("延时消息开始准备分发，接收者是 {0}，消息是 {1}",
                receiver.Name,
                telegram.Message.ToString()));
            // 开始分发消息
            Discharge(receiver, telegram);
            priorityQueue.Remove(telegram);
        }
    }
}