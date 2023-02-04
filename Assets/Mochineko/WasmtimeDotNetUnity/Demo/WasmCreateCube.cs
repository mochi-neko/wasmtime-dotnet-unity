using UnityEngine;
using Wasmtime;

namespace Mochineko.WastimeDotNetUnity.Demo
{
    internal sealed class WasmCreateCube : MonoBehaviour
    {
        private void Start()
        {
            const string wat = @"
(module
  (type $t0 (func))
  (import """" ""create_cube"" (func $.create_cube (type $t0)))
  (func $run
    call $.create_cube
  )
  (export ""run"" (func $run))
)";

            using var engine = new Engine();
            using var module = Module.FromText(engine, "create_cube", wat);
            using var linker = new Linker(engine);
            using var store = new Store(engine);

            linker.Define(
                "",
                "create_cube",
                Function.FromCallback(store, () => {
                    var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.name = "Cube created from WebAssembly"; 
                })
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