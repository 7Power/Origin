namespace Shaiya.Origin.Game.Model.Entity
{
    public class UpdateFlags
    {
        // If a movement update is required
        private bool _movementUpdate = false;

        // If an equipment update is required
        private bool _equipmentUpdate = false;

        // If a buff update is required
        private bool _buffUpdate = false;

        // If an update on the hitpoints is required
        private bool _hitpointUpdate = false;

        /// <summary>
        ///  If an update on the entity's movement / position is required.
        /// </summary>
        /// <returns></returns>
        public bool IsMovementUpdateRequired()
        {
            return _movementUpdate;
        }

        /// <summary>
        /// If an update on the entity's equipped items is required.
        /// </summary>
        /// <returns></returns>
        public bool IsEquipmentUpdateRequired()
        {
            return _equipmentUpdate;
        }

        /// <summary>
        /// If an update is required on the entity's buffs/debuffs.
        /// </summary>
        /// <returns></returns>
        public bool IsBuffUpdateRequired()
        {
            return _buffUpdate;
        }

        /// <summary>
        /// If an update is required on the entity's HP/MP/SP.
        /// </summary>
        /// <returns></returns>
        public bool IsHitpointUpdateRequired()
        {
            return _hitpointUpdate;
        }

        /// <summary>
        /// Reset the movement update flag.
        /// </summary>
        public void ResetMovementUpdate()
        {
            _movementUpdate = false;
        }

        /// <summary>
        /// Reset the equipment update flag.
        /// </summary>
        public void ResetEquipmentUpdate()
        {
            _equipmentUpdate = false;
        }

        /// <summary>
        /// Reset the buff update flag.
        /// </summary>
        public void ResetBuffUpdate()
        {
            _buffUpdate = false;
        }

        /// <summary>
        /// Reset the hitpoint update flag.
        /// </summary>
        public void ResetHitpointUpdate()
        {
            _hitpointUpdate = false;
        }
    }
}