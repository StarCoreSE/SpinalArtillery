using System.Collections.Generic;
using SpinalArtillery.Communication;
using VRage.Game.ModAPI;

namespace SpinalArtillery
{
    public class SpinalArtilleryAssembly
    {
        public static ModularDefinitionApi ModularApi => ModularDefinition.ModularApi;

        public readonly int AssemblyId;
        public readonly IMyCubeBlock BaseBlock;

        private readonly Dictionary<string, int> _blockCounts = new Dictionary<string, int>()
        {
            // Barrel Parts
            ["SpinalBarrelBlock"] = 0,
            ["SpinalCoilPart"] = 0,
            
            // Breech/Misc
            ["SpinalBreech"] = 1,
            
            // Loader Stack
            ["SpinalStackExtender"] = 0,
            ["SpinalExplosivePacker"] = 0,
            ["SpinalAutoloader"] = 0,
            ["SpinalMagazine"] = 0,
            ["SpinalGunpowderPacker"] = 0,
        };

        public SpinalArtilleryAssembly(int assemblyId, IMyCubeBlock baseBlock)
        {
            AssemblyId = assemblyId;
            BaseBlock = baseBlock;
        }

        public void UpdateAfterSimulation()
        {

        }

        public void Close()
        {

        }

        public void OnPartAdd(IMyCubeBlock block)
        {
            _blockCounts[block.BlockDefinition.SubtypeId]++;
        }
        
        public void OnPartRemove(IMyCubeBlock block)
        {
            _blockCounts[block.BlockDefinition.SubtypeId]--;
        }

        public void OnPartDestroy(IMyCubeBlock block)
        {
            /*
             * TODO: Make the following blocks explode upon damage:
             * - SpinalExplosivePacker
             * - SpinalMagazine
             * - SpinalGunpowderPacker
             */ 
        }
    }
}
