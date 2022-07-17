namespace MudRunner.Commons.DataContracts.Models.Enums
{
    /// <summary>
    /// It represents the fastening types.
    /// </summary>
    public enum FasteningType
    {
        /// <summary>
        /// Body is pinned at both ends.
        /// </summary>
        BothEndPinned = 1,

        /// <summary>
        /// Body is fixed at both ends.
        /// </summary>
        BothEndFixed = 2,

        /// <summary>
        /// Body fixed at one end and at the another end is pinned.
        /// </summary>
        OneEndFixedOneEndPinned = 3,

        /// <summary>
        /// Body fixed at one end and the another end is free.
        /// </summary>
        OneEndFixed = 4,
    }
}
