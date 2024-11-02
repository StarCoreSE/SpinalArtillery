using System;
using System.Collections.Generic;
using System.Reflection;
using Sandbox.Game.GUI.DebugInputComponents;
using SpinalArtillery.Communication;
using VRage.Game.Components;
using VRage.Game.ModAPI;
using VRage.Utils;

namespace SpinalArtillery
{
    [MySessionComponentDescriptor(MyUpdateOrder.AfterSimulation)]
    public class MasterSession : MySessionComponentBase
    {
        public static MasterSession I;

        public readonly Dictionary<int, SpinalArtilleryAssembly> AllWeapons =
            new Dictionary<int, SpinalArtilleryAssembly>();
        
        #region Base Methods

        public override void LoadData()
        {
            I = this;
        }

        public override void UpdateAfterSimulation()
        {
            try
            {
                foreach (var weapon in AllWeapons)
                {
                    weapon.Value.UpdateAfterSimulation();
                }
            }
            catch (Exception ex)
            {
                MyLog.Default.WriteLineAndConsole(ex.ToString());
            }
        }

        protected override void UnloadData()
        {
            foreach (var weapon in AllWeapons)
                weapon.Value.Close();
            I = null;
        }

        #endregion

        public void RegisterWeapon(int assemblyId, IMyCubeBlock basePart)
        {
            AllWeapons.Add(assemblyId, new SpinalArtilleryAssembly(assemblyId, basePart));
        }

        public void UnregisterWeapon(int assemblyId)
        {
            AllWeapons[assemblyId].Close();
            AllWeapons.Remove(assemblyId);
        }

        public void OnPartAdd(int assemblyId, IMyCubeBlock block)
        {
            AllWeapons[assemblyId].OnPartAdd(block);
        }

        public void OnPartRemove(int assemblyId, IMyCubeBlock block)
        {
            AllWeapons[assemblyId].OnPartRemove(block);
        }

        public void OnPartDestroy(int assemblyId, IMyCubeBlock block)
        {
            AllWeapons[assemblyId].OnPartDestroy(block);
        }
    }
}
