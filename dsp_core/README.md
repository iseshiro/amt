# dsp_core (binary distribution)

The DSP engine used by amt (signal generation, FFT / convolution, real-time convolution,
WAV streaming, ...). Its source code is not public; this folder contains only what is
needed to build and run amt.

| Path | Contents |
|---|---|
| `include/` | Headers used by `asio_io` (`soundtools.h`, `dsp_common.h`, `channel_info.h`, `wav_stream.h`) |
| `lib/x64/dsp_core.lib` | Import library (x64) |
| `bin/x64/dsp_core.dll` | DLL (x64, Release, MSVC v145, oneMKL sequential statically linked) |
| `bin/x64/vcomp140.dll` | Microsoft OpenMP runtime required by `dsp_core.dll` (VC++ 14.52 redistributable) |

- `asio_io.vcxproj` links `lib/x64/dsp_core.lib` and includes `include/`.
- `amt.csproj` copies `bin/x64/dsp_core.dll` and `vcomp140.dll` next to `amt.exe`.
- The installer (`amt_setup`) packages both DLLs.

Do not change the headers: they must match the DLL. If you need a new function or a change
in dsp_core, please open an issue.

License: see [THIRD_PARTY_NOTICES.md](../THIRD_PARTY_NOTICES.md). Not covered by the MIT License.
