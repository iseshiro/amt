# amt --- Acoustic Measurement Tool

ASIO 対応のオーディオインターフェースを使って、多チャンネルの音響計測
（インパルス応答計測、リアルタイム畳み込み、録音・再生など）を行う Windows アプリケーションです。

An acoustic measurement tool for Windows using ASIO audio interfaces
(multichannel impulse response measurement, real-time convolution, recording / playback).

## Install (users)

[Releases](../../releases) から `setup.exe` と `amt_setup.msi` を同じフォルダにダウンロードし、
`setup.exe` を実行してください。.NET Framework 4.7.2 と Visual C++ ランタイム (x64) が
なければ自動でインストールされます。

## Repository layout

| Folder | Contents |
|---|---|
| `amt/` | GUI (C#, WinForms, .NET Framework 4.7.2) |
| `asio_io/` | ASIO I/O wrapper (C++/CLI) |
| `dsp_core/` | DSP engine — **binary only** (headers + import library + DLL). See [dsp_core/README.md](dsp_core/README.md) |
| `amt_setup/` | Installer project (Visual Studio Installer Projects) |
| `check/` | MATLAB helper scripts |
| `external/ASIOSDK/` | Steinberg ASIO SDK — **not included**, put it here yourself |

## Build

### Requirements

- Windows 10/11 x64
- Visual Studio 2026 (v145 toolset) with
  - .NET desktop development
  - Desktop development with C++ and **C++/CLI support**
  - (installer only) Microsoft Visual Studio Installer Projects extension
- Steinberg ASIO SDK

### Steps

1. Download the ASIO SDK from Steinberg: <https://www.steinberg.net/developers/>
   (the SDK is not redistributed in this repository because of its license).
2. Extract it so that `external/ASIOSDK/common/asio.h` exists:
   ```
   amt/
   ├─ amt.sln
   └─ external/
      └─ ASIOSDK/
         ├─ common/   (asio.cpp, asio.h, iasiodrv.h, asiosys.h ...)
         └─ host/     (asiodrivers.cpp, ginclude.h, pc/asiolist.cpp ...)
   ```
   To use another location, set the environment variable or MSBuild property `AsioSdkDir`.
3. Open `amt.sln`, select **Release | x64** and build.
   NuGet packages are restored automatically by Visual Studio.
   From the command line:
   ```
   msbuild amt.sln -t:restore -p:RestorePackagesConfig=true
   msbuild amt.sln -p:Configuration=Release -p:Platform=x64
   ```
4. Output: `amt/bin/x64/Release/amt.exe` (with `asio_io.dll` and `dsp_core.dll`).

Only the **x64** platform is supported (`dsp_core` is provided for x64 only).

### Installer

Build the `amt_setup` project in Visual Studio (Release).
Command line: `devenv amt.sln /Build "Release|x64" /Project amt_setup`
(if it fails with `HRESULT = '8000000A'`, run `DisableOutOfProcBuild.exe` from
`Common7\IDE\CommonExtensions\Microsoft\VSI\DisableOutOfProcBuild` once).

## Contributing

Issues and pull requests are welcome. Changes to `amt/` and `asio_io/` are open to everyone.
`dsp_core` is maintained separately; if you need a change or a new function in it,
please open an issue describing the API you need.

## License

- `amt/`, `asio_io/`, `amt_setup/`, `check/`: [MIT License](LICENSE)
- `dsp_core/` binary and third-party components: see [THIRD_PARTY_NOTICES.md](THIRD_PARTY_NOTICES.md)

ASIO is a trademark and software of Steinberg Media Technologies GmbH.
