using UnityEngine;
using Wasmtime;

namespace Mochineko.WastimeDotNetUnity.Demo
{
    internal sealed class WasmHelloWorld : MonoBehaviour
    {
        private void Start()
        {
            var watPath = System.IO.Path.Combine(Application.streamingAssetsPath, "hello.wat");

            using var engine = new Engine();
            using var module = Module.FromTextFile(engine, watPath);
            using var linker = new Linker(engine);
            using var store = new Store(engine);

            linker.Define(
                "",
                "hello",
                Function.FromCallback(store, () => Debug.Log("Hello from C#, WebAssembly!"))
            );

            var instance = linker.Instantiate(store, module);

            var run = instance.GetAction("run");
            if (run is null)
            {
                Debug.LogError("error: run export is missing");
                return;
            }

            run();
        }
    }
}