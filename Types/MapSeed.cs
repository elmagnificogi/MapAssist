using MapAssist.Helpers;
using MapAssist.Interfaces;
using System;

namespace MapAssist.Types
{
    public class MapSeed : IUpdatable<MapSeed>
    {
        private readonly IntPtr _pMapSeed;
        public uint Seed { get; private set; }

        public MapSeed(IntPtr pMapSeed)
        {
            _pMapSeed = pMapSeed;
            Update();
        }

        public MapSeed Update()
        {
            using (var processContext = GameManager.GetProcessContext())
            {
                try
                {
                    //var pMapSeedData = processContext.Read<IntPtr>(_pMapSeed);
                    var mapSeedData = processContext.Read<Structs.MapSeed>(_pMapSeed);
                    
                    if (Program.map_index_fix != -1)
                        mapSeedData.mapindex = (uint)Program.map_index_fix;

                    if (mapSeedData.mapindex == 0)
                        Seed = mapSeedData.mapSeed1;
                    if (mapSeedData.mapindex == 1)
                        Seed = mapSeedData.mapSeed2;
                    if (mapSeedData.mapindex == 2)
                        Seed = mapSeedData.mapSeed3;
                    if (mapSeedData.mapindex == 3)
                        Seed = mapSeedData.mapSeed4;

                    


                    //Seed = mapSeedData.mapSeed1 ; // Use this if check offset is 0x110
                    //Seed = mapSeedData.check > 0 ? mapSeedData.mapSeed2 : mapSeedData.mapSeed1; // Use this if check offset is 0x124
                    //Seed = mapSeedData.check > 0 ? mapSeedData.mapSeed1 : mapSeedData.mapSeed2; // Use this if check offset is 0x830
                }
                catch (Exception) { }
            }
            return this;
        }
    }
}
