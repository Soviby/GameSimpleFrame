using System.Collections.Generic;
/// <summary>
/// 事件处理委托
/// </summary>
/// <param name="args">事件参数</param>
/// <typeparam name="T">事件类型</typeparam>
public delegate void ExEventHandler<in T>(T args) where T : ExEvent<T>;

/// <summary>
/// 事件接口
/// </summary>
public interface IEvent//为了解决EventCrossThreadHelper中事件数据的存储
{
    /// <summary>
    /// 事件发送者
    /// </summary>
    object Sender { get; set; }

    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="sender">传参自动修改Sender</param>
    void Trigger(object sender = null);
}

/// <summary>
/// 事件基类
/// </summary>
/// <typeparam name="T">事件类型</typeparam>
public abstract partial class ExEvent<T> : IEvent where T : ExEvent<T>, IEvent
{
    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="sender">传参自动修改Sender</param>
    public void Trigger(object sender = null)
    {
        Sender = sender ?? Sender;
        Trigger((T)this);
    }

    /// <summary>
    /// 事件发送方
    /// </summary>
    public object Sender { get; set; }
}

/// <summary>
/// 事件管理类
/// </summary>
/// <typeparam name="T">事件类型</typeparam>
public partial class ExEvent<T> : IEvent where T : ExEvent<T>, IEvent
{
    /// <summary>
    /// 添加事件
    /// </summary>
    /// <param name="eventHandler"></param>
    public static void Add(ExEventHandler<T> eventHandler)
    {
        _event += eventHandler;
    }

    /// <summary>
    /// 移除事件
    /// </summary>
    /// <param name="eventHandler"></param>
    public static void Remove(ExEventHandler<T> eventHandler)
    {
        _event -= eventHandler;
    }

    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="args"></param>
    public static void Trigger(T args = default)
    {
        _event?.Invoke(args);
    }

    /// <summary>
    /// 支线程想主线程触发事件：主要是为了解决网络部分数据处理的问题
    /// </summary>
    /// <param name="args"></param>
    public static void JumpTrigger(T args = default)
    {
        ExEventCrossThreadHelper.AddEvent(args);
    }
    public static event ExEventHandler<T> _event;
}

/// <summary>
/// 跨线程触发事件辅助类（主要用于将网络/串口数据同步到主线程）
/// </summary>
public class ExEventCrossThreadHelper
{
    /// <summary>
    /// 主线程需要循环调用此方法
    /// </summary>
    public static void MainThreadUpdate()
    {
        if (_eventQueue.Count > 0)
        {
            lock (_lockHelper)
            {
                int count = 0;
                while (_eventQueue.Count > 0 && count < MaxHandleCount)
                {
                    _eventQueue.Dequeue()?.Trigger();
                    count++;
                }
            }
        }
    }

    /// <summary>
    /// 添加事件
    /// </summary>
    /// <param name="eventArg"></param>
    public static void AddEvent(IEvent eventArg)
    {
        lock (_lockHelper)
        {
            _eventQueue.Enqueue(eventArg);
        }
    }

    /// <summary>
    /// 每一帧处理事件的最大数量
    /// </summary>
    public static int MaxHandleCount = 30;

    static Queue<IEvent> _eventQueue = new Queue<IEvent>();
    static object _lockHelper = new object();
}