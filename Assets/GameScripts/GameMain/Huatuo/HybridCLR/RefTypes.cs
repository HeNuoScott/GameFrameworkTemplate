using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Scripting;
using GameFramework;
using GameFrame.Main;
using System.IO;

[assembly: Preserve]
enum IntEnum : int
{
    A,
    B,
}
enum ByteEnum : byte
{
    A,
    B,
}
public class MyComparer<T> : Comparer<T>
{
    public override int Compare(T x, T y)
    {
        return 0;
    }
}
struct CampByteEnum
{
    private readonly ByteEnum m_First;
    private readonly ByteEnum m_Second;

    public CampByteEnum(ByteEnum first, ByteEnum second)
    {
        m_First = first;
        m_Second = second;
    }

    public ByteEnum First
    {
        get
        {
            return m_First;
        }
    }

    public ByteEnum Second
    {
        get
        {
            return m_Second;
        }
    }
}

public struct MyValue
{
    public int x;
    public float y;
    public string s;
}

public class TestData
{
    public string name;
    public int age;
}

class TestTable
{
    public int Id { get; set; }

    public string Name { get; set; }
}

class MyStateMachine : IAsyncStateMachine
    {
        public void MoveNext()
        {
            throw new NotImplementedException();
        }

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            throw new NotImplementedException();
        }
    }

public class RefTypes : MonoBehaviour
{
    List<Type> GetTypes()
    {
        return new List<Type>
        {
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GetTypes());
        GameObject.Instantiate<GameObject>(null);
        Instantiate<GameObject>(null, null);
        Instantiate<GameObject>(null, null, false);
        Instantiate<GameObject>(null, new Vector3(), new Quaternion());
        Instantiate<GameObject>(null, new Vector3(), new Quaternion(), null);
    }

    public void RefNumerics()
    {
        var a = new System.Numerics.BigInteger();
        a.ToString();
    }

    // TODO 游戏中使用的泛型
    void RefGame()
    {
        new Dictionary<string, bool>();
        new Dictionary<byte, object>();
        new Dictionary<IntEnum, object>();
        new Dictionary<ByteEnum, object>();
        new KeyValuePair<ByteEnum, ByteEnum>();
        new CampByteEnum(ByteEnum.A, ByteEnum.B);
        new Dictionary<KeyValuePair<ByteEnum, ByteEnum>, ByteEnum>();
        new Dictionary<KeyValuePair<ByteEnum, ByteEnum>, ByteEnum[]>();
        //Utility.Text.Format<ByteEnum, string, string>(null, ByteEnum.A, null, null);
        //GameEntry.Localization.GetString<string, string, string, string, string, string>(null, null, null, null, null, null, null);
        new List<MyValue>()
        {
            new MyValue() { x = 1, y = 10, s = "abc" }
        };
        TestData testData = new TestData() { name = "test", age = 1 };
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(testData);
        testData = Newtonsoft.Json.JsonConvert.DeserializeObject<TestData>(json);
    }

    void RefUnityEngine()
    {
        GameObject.Instantiate<GameObject>(null);
        Instantiate<GameObject>(null, null);
        Instantiate<GameObject>(null, null, false);
        Instantiate<GameObject>(null, new Vector3(), new Quaternion());
        Instantiate<GameObject>(null, new Vector3(), new Quaternion(), null);
        this.gameObject.AddComponent<RefTypes>();
        gameObject.AddComponent(typeof(RefTypes));
        gameObject.AddComponent(typeof(Animation));
    }

    void RefContainer()
    {
        new List<object>()
        {
            new Dictionary<int, int>(),
            new Dictionary<int, long>(),
            new Dictionary<int, object>(),
            new Dictionary<long, int>(),
            new Dictionary<long, long>(),
            new Dictionary<long, object>(),
            new Dictionary<object, long>(),
            new Dictionary<object, object>(),
            new SortedDictionary<int, long>(),
            new SortedDictionary<int, object>(),
            new SortedDictionary<long, int>(),
            new SortedDictionary<long, object>(),
            new HashSet<int>(),
            new HashSet<long>(),
            new HashSet<object>(),
            new List<int>(),
            new List<long>(),
            new List<float>(),
            new List<double>(),
            new List<object>(),
            new ValueTuple<int, int>(1, 1),
            new ValueTuple<long, long>(1, 1),
            new ValueTuple<object, object>(1, 1),
        };
    }

    void RefMisc()
    {

    }

    void RefComparers()
    {
        var a = new object[]
        {
            new MyComparer<int>(),
            new MyComparer<long>(),
            new MyComparer<float>(),
            new MyComparer<double>(),
            new MyComparer<object>(),
        };

        new MyComparer<int>().Compare(default, default);
        new MyComparer<long>().Compare(default, default);
        new MyComparer<float>().Compare(default, default);
        new MyComparer<double>().Compare(default, default);
        new MyComparer<object>().Compare(default, default);

        object b = EqualityComparer<int>.Default;
        b = EqualityComparer<long>.Default;
        b = EqualityComparer<float>.Default;
        b = EqualityComparer<double>.Default;
        b = EqualityComparer<object>.Default;
    }

    void RefNullable()
    {
        // nullable
        object b = null;
        int? a = 5;
        b = a;
        int d = (int?)b ?? 7;
        int e = (int)b;
        a = d;
        b = a;
        b = Enumerable.Range(0, 1).Reverse().Take(1).TakeWhile(x => true).Skip(1).All(x => true);
        b = new WaitForSeconds(1f);
        b = new WaitForSecondsRealtime(1f);
        b = new WaitForFixedUpdate();
        b = new WaitForEndOfFrame();
        b = new WaitWhile(() => true);
        b = new WaitUntil(() => true);
    }

    void RefAsyncMethod()
    {
        var stateMachine = new MyStateMachine();

        TaskAwaiter aw = default;
        var c0 = new AsyncTaskMethodBuilder();
        c0.Start(ref stateMachine);
        c0.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
        c0.SetException(null);
        c0.SetResult();

        var c1 = new AsyncTaskMethodBuilder();
        c1.Start(ref stateMachine);
        c1.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
        c1.SetException(null);
        c1.SetResult();

        var c2 = new AsyncTaskMethodBuilder<bool>();
        c2.Start(ref stateMachine);
        c2.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
        c2.SetException(null);
        c2.SetResult(default);

        var c3 = new AsyncTaskMethodBuilder<int>();
        c3.Start(ref stateMachine);
        c3.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
        c3.SetException(null);
        c3.SetResult(default);

        var c4 = new AsyncTaskMethodBuilder<long>();
        c4.Start(ref stateMachine);
        c4.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
        c4.SetException(null);

        var c5 = new AsyncTaskMethodBuilder<float>();
        c5.Start(ref stateMachine);
        c5.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
        c5.SetException(null);
        c5.SetResult(default);

        var c6 = new AsyncTaskMethodBuilder<double>();
        c6.Start(ref stateMachine);
        c6.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
        c6.SetException(null);
        c6.SetResult(default);

        var c7 = new AsyncTaskMethodBuilder<object>();
        c7.Start(ref stateMachine);
        c7.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
        c7.SetException(null);
        c7.SetResult(default);

        var c8 = new AsyncTaskMethodBuilder<IntEnum>();
        c8.Start(ref stateMachine);
        c8.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
        c8.SetException(null);
        c8.SetResult(default);

        var c9 = new AsyncVoidMethodBuilder();
        var b = AsyncVoidMethodBuilder.Create();
        c9.Start(ref stateMachine);
        c9.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
        c9.SetException(null);
        c9.SetResult();
        Debug.Log(b);
    }

    void RefNewtonsoftJson()
    {
        //AotHelper.EnsureList<int>();
        //AotHelper.EnsureList<long>();
        //AotHelper.EnsureList<float>();
        //AotHelper.EnsureList<double>();
        //AotHelper.EnsureList<string>();
        //AotHelper.EnsureDictionary<int, int>();
        //AotHelper.EnsureDictionary<int, string>();
    }

    public void RefProtobufNet()
    {
        
    }

    public void RefGoogleProtobuf()
    {
    }

    public void RefSQLite()
    {
    }

    public static async void TestAsync3()
    {
        Debug.Log("async task 1");
        await Task.Delay(10);
        Debug.Log("async task 2");
    }

    public static int Main_1()
    {
        Debug.Log("hello,hybridclr");

        var task = Task.Run(async () =>
        {
            await TestAsync2();
        });

        task.Wait();

        Debug.Log("async task end");
        Debug.Log("async task end2");

        return 0;
    }

    public static async Task TestAsync2()
    {
        Debug.Log("async task 1");
        await Task.Delay(3000);
        Debug.Log("async task 2");
    }

    // Update is called once per frame
    void Update()
    {
        TestAsync();
    }

    public static int TestAsync()
    {
        var t0 = Task.Run(async () =>
        {
            await Task.Delay(10);
        });
        t0.Wait();
        var task = Task.Run(async () =>
        {
            await Task.Delay(10);
            return 100;
        });
        Debug.Log(task.Result);
        return 0;
    }
}
