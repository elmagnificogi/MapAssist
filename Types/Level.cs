using MapAssist.Helpers;
using MapAssist.Interfaces;
using System;

namespace MapAssist.Types
{
    public class Level : IUpdatable<Level>
    {
        private readonly IntPtr _pLevel = IntPtr.Zero;
        private Structs.Level _level;

        public Level(IntPtr pLevel)
        {
            _pLevel = pLevel;
            Update();
        }

        public Level Update()
        {
            using (var processContext = GameManager.GetProcessContext())
            {
                _level = processContext.Read<Structs.Level>(_pLevel);
            }

            return this;
        }

        public Area LevelId => _level.LevelId;
    }
}
