using MapAssist.Types;
using System.Runtime.InteropServices;

namespace MapAssist.Structs
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Level
    {
        [FieldOffset(0x1F8)] public Area LevelId;
    }
}
