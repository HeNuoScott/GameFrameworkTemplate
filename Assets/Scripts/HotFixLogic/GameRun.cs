using UnityEngine;
using Sirius;
using System.IO;
using System;

public class GameRun
{

    public static void Init()
    {
        Debug.Log("HotFixLogic Init");

        HotFixEntry.LoadPreFabs();
    }

    public static void Run()
    {
        Debug.Log("HotFixLogic Run");

        HotFixEntry.InitCustomComponents();
    }

    /// <summary>
    /// 为aot assembly加载原始metadata， 这个代码放aot或者热更新都行。
    /// 一旦加载后，如果AOT泛型函数对应native实现不存在，则自动替换为解释模式执行
    /// </summary>
    //public static unsafe void LoadMetadataForAOTAssembly()
    //{
    //    // 可以加载任意aot assembly的对应的dll。这里以最常用的mscorlib.dll举例
    //    //
    //    // 加载打包时 unity在build目录下生成的 裁剪过的 mscorlib，注意，不能为原始mscorlib
    //    //
    //    string mscorelib = @$"{Application.dataPath}/../build-win64-2020.3.33/huatuo/Managed/mscorlib.dll";
    //    byte[] dllBytes = File.ReadAllBytes(mscorelib);

    //    fixed (byte* ptr = dllBytes)
    //    {
    //        // 加载assembly对应的dll，会自动为它hook。一旦aot泛型函数的native函数不存在，用解释器版本代码
    //        int err = Huatuo.HuatuoApi.LoadMetadataForAOTAssembly((IntPtr)ptr, dllBytes.Length);
    //        Debug.Log("LoadMetadataForAOTAssembly. ret:" + err);
    //    }
    //}
}
