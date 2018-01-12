# Lz4
NetStandard 2.0 Port of lz4Net

Forked from https://github.com/MiloszKrajewski/lz4net

Primary goal is to port to NetStandard 2.0 with linux support (done)
Secondary goal is to add Native Framing support per https://github.com/lz4/lz4/wiki/lz4_Frame_format.md
  * Need to have compatibility with native lz4 command line tools as well as 7zip-zstandard release (https://github.com/mcmilk/7-Zip-zstd)

C# Port of LZ4 here https://github.com/IonKiwi/lz4.net does support LZ4 standard framing, but unfortunately it is C++ / CLI which does not work on linux / .Net Standard 2.0
It may serve as a good example though.

The original project talks about compatibility here as well https://github.com/MiloszKrajewski/lz4net#compatibility
