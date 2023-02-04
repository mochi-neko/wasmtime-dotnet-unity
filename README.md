# wasmtime-dotnet-unity

Provides the [Wasmtime](https://github.com/bytecodealliance/wasmtime)([wasmtime-dotnet](https://github.com/bytecodealliance/wasmtime-dotnet)) for Unity via Unity Package Manager(UPM).


## How to import by UMP

Add these dependencies:

```json
{
  "dependencies": {
    "com.bytecodealliance.wasmtime": "https://github.com/mochi-neko/wasmtime-dotnet-unity.git?path=/Assets/BytecodeAlliance/Wasmtime",
    "com.bytecodealliance.wasmtime-dotnet": "https://github.com/mochi-neko/wasmtime-dotnet-unity.git?path=/Assets/BytecodeAlliance/WasmtimeDotNet",
  }
}
```

to your `manifest.json`.


## Supprted Platforms

- Windows (`x86_64`)
- macOS
  - Intel (`x86_64`)
  - Apple Silicon (`ARM64`)
- Linux (`x86_64`)

Planned

- Android
  - `ARM64`
  - `ARMv7`
- iOS (`ARM64`)


## NOTICE

[NOTICE](https://github.com/mochi-neko/wasmtime-dotnet-unity/blob/main/NOTICE)


## LICENSE

[MIT license](https://github.com/mochi-neko/wasmtime-dotnet-unity/blob/main/LICENSE)
