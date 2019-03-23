using NitroxModel.DataStructures.GameLogic;
using NitroxModel.Helper;
using UnityEngine;

namespace NitroxClient.GameLogic.Helper.VehicleChildObjectSyncers
{
    public class EnergyMixinVehicleChildIdentifierSyncer : IVehicleChildIdentifierSyncer
    {
        public InteractiveChildObjectIdentifier ExtractId(Component component, string componentPath)
        {
            EnergyMixin energyMixin = (EnergyMixin)component;
            StorageSlot slot = (StorageSlot)energyMixin.ReflectionGet("batterySlot");
            InventoryItem inventoryItem = slot.storedItem;
            
            string guid = GuidHelper.GetGuid(inventoryItem.item.gameObject);

            return new InteractiveChildObjectIdentifier(component.GetType(), guid, componentPath);
        }

        public void SetId(GameObject gameObject, InteractiveChildObjectIdentifier childIdentifier)
        {
            EnergyMixin energyMixin = gameObject.GetComponent<EnergyMixin>();
            StorageSlot slot = (StorageSlot)energyMixin.ReflectionGet("batterySlot");
            InventoryItem inventoryItem = slot.storedItem;

            GuidHelper.SetNewGuid(inventoryItem.item.gameObject, childIdentifier.Guid);
        }
    }
}
