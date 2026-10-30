# Third-party notices

## dsp_core (binary)

`dsp_core/bin/x64/dsp_core.dll`, `dsp_core/lib/x64/dsp_core.lib` and the headers in
`dsp_core/include/` are Copyright (c) Shiro Ise. They are **not** covered by the MIT License
of this repository. They may be used and redistributed, unmodified, only as part of amt
or of software built from this repository. Reverse engineering is not permitted.

`dsp_core.dll` statically links Intel® oneAPI Math Kernel Library (oneMKL),
which is redistributed under the
[Intel Simplified Software License](https://www.intel.com/content/www/us/en/developer/articles/license/onemkl-license-faq.html).

## Steinberg ASIO SDK

amt uses the Steinberg ASIO SDK. The SDK is **not included** in this repository;
download it from <https://www.steinberg.net/developers/>.
The SDK is dual-licensed under the Steinberg ASIO License or GPLv3.
ASIO is a trademark and software of Steinberg Media Technologies GmbH.

## NuGet packages

Restored at build time and redistributed in the installer under their own licenses:

| Package | License |
|---|---|
| Newtonsoft.Json | MIT |
| Microsoft.AspNet.WebApi.Client / WebApi.Core / WebHost | MIT (Microsoft) |
| Microsoft.Bcl.AsyncInterfaces, System.Buffers, System.Memory, System.Numerics.Vectors, System.Runtime.CompilerServices.Unsafe, System.Text.Encodings.Web, System.Threading.Tasks.Extensions, System.ValueTuple | MIT (.NET Foundation / Microsoft) |

## Microsoft Visual C++ runtime

`vcomp140.dll` is redistributed under the Microsoft Visual Studio redistribution terms.
