using VRage.Game.Components;
using VRage.Utils;
using static SpinalArtillery.ModularAssemblies.Communication.DefinitionDefs;

namespace SpinalArtillery.ModularAssemblies.Communication
{
    [MySessionComponentDescriptor(MyUpdateOrder.Simulation, int.MinValue)]
    internal class ModularDefinitionSender : MySessionComponentBase
    {
        internal ModularDefinitionContainer StoredDef;

        public override void LoadData()
        {
            MyLog.Default.WriteLineAndConsole(
                $"{ModContext.ModName}.ModularDefinition: Init new ModularAssembliesDefinition");

            // Init
            StoredDef = global::SpinalArtillery.ModularAssemblies.ModularDefinition.GetBaseDefinitions();

            // Send definitions over as soon as the API loads, and create the API before anything else can init.
            global::SpinalArtillery.ModularAssemblies.ModularDefinition.ModularApi.Init(ModContext, SendDefinitions);
        }

        protected override void UnloadData()
        {
            global::SpinalArtillery.ModularAssemblies.ModularDefinition.ModularApi.UnloadData();
        }

        private void SendDefinitions()
        {
            global::SpinalArtillery.ModularAssemblies.ModularDefinition.ModularApi.RegisterDefinitions(StoredDef);
        }
    }
}