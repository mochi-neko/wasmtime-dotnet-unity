using UnityEngine;
using Wasmtime;

namespace Mochineko.WastimeDotNetUnity.Demo
{
    internal sealed class WasmSerialization : MonoBehaviour
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

            var serializedModule = module.Serialize(); // Compiled binary(.wasmu)

            var deserializedModule = Module.Deserialize(engine, "hello", serializedModule);

            using var linker = new Linker(engine);
            using var store = new Store(engine);

            linker.Define(
                "",
                "hello",
                Function.FromCallback(store, () => Debug.Log("Hello from C#, precomplied WebAssembly!"))
            );

            var instance = linker.Instantiate(store, deserializedModule);

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