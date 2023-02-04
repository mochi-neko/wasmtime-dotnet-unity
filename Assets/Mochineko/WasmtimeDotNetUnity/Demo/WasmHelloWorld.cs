using UnityEngine;
using Wasmtime;

namespace Mochineko.WastimeDotNetUnity.Demo
{
    internal sealed class WasmHelloWorld : MonoBehaviour
    {
        private void Start()
        {
            const string wat = @"
(module
  (type $t0 (func))
  (import """" ""hello"" (func $.hello (type $t0)))
  (func $run
    call $.hello
  )
  (export ""run"" (func $run))
)";

            using var engine = new Engine();
            using var module = Module.FromText(engine, "hello", wat);
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