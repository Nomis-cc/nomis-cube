namespace Nomis.Cubescan.Interfaces.Models
{
    /// <summary>
    /// Cube wallet score.
    /// </summary>
    public class CubeWalletScore
    {
        /// <summary>
        /// Nomis Score in range of [0; 1].
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Additional stat data used in score calculations.
        /// </summary>
        public CubeWalletStats? Stats { get; set; }
    }
}