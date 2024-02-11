namespace Lexington.Model
{
    public class ResoureCount
    {
        public int Oil { get; set; } = 0;

        public int Munition { get; set; } = 0;

        public int Steel { get; set; } = 0;

        public int Aluminium { get; set; } = 0;

        public int QuickConstruction { get; set; } = 0;

        public int RapidRepair { get; set; } = 0;
        public int ShipBlueprint { get; set; } = 0;
        public int EquipmentBlueprint { get; set; } = 0;

        private ResoureCount(int oil, int munition, int steel, int aluminium, int quickConstruction, int rapidRepair, int shipBlueprint, int equipmentBlueprint)
        {
            Oil = oil;
            Munition = munition;
            Steel = steel;
            Aluminium = aluminium;
            QuickConstruction = quickConstruction;
            RapidRepair = rapidRepair;
            ShipBlueprint = shipBlueprint;
            EquipmentBlueprint = equipmentBlueprint;
        }

        public ResoureCount()
        {

        }

        public static ResoureCount operator +(ResoureCount v1, ResoureCount v2)
        {
            return new ResoureCount(
            v1.Oil + v2.Oil,
            v1.Munition + v2.Munition,
            v1.Steel + v2.Steel,
            v1.Aluminium + v2.Aluminium,
            v1.QuickConstruction + v2.QuickConstruction,
            v1.RapidRepair + v2.RapidRepair,
            v1.ShipBlueprint + v2.ShipBlueprint,
            v1.EquipmentBlueprint + v2.EquipmentBlueprint
        );
        }

    }
}
