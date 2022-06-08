using System.ComponentModel;
using System.Threading.Tasks;

namespace MapAssist.Types
{
    public class MapSeed
    {
        private BackgroundWorker BackgroundCalculator;
        private ulong GameSeedXor { get; set; } = 0;

        public bool IsReady => BackgroundCalculator != null && GameSeedXor != 0;

        public uint Get(UnitPlayer player)
        {
            if (GameSeedXor != 0)
            {
                return (uint)(player.InitSeedHash ^ GameSeedXor);
            }
            else if (BackgroundCalculator == null)
            {
                var InitSeedHash = player.InitSeedHash;
                var EndSeedHash = player.EndSeedHash;

                BackgroundCalculator = new BackgroundWorker();

                BackgroundCalculator.DoWork += (sender, args) =>
                {
                    Parallel.For(0, uint.MaxValue, (trySeed, state) =>
                    {
                        if ((((uint)trySeed * 0x6AC690C5 + 666) & 0xFFFFFFFF) == EndSeedHash)
                        {
                            GameSeedXor = InitSeedHash ^ (uint)trySeed;

                            state.Stop();
                        }
                    });

                    BackgroundCalculator.Dispose();
                };

                BackgroundCalculator.RunWorkerAsync();
            }

            return 0;
        }
    }
}
