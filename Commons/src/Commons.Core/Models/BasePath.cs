namespace MudRunner.Suspension.Core.Models
{
    /// <summary>
    /// It contains the base paths used in the application.
    /// </summary>
    public static class BasePath
    {
        /// <summary>
        /// This method returns the input file name.
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        public static string GetInputFileName(string fileExtension) => $"input.{fileExtension}";

        /// <summary>
        /// This method returns the output file name.
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <returns></returns>
        public static string GetOutputFileName(string fileExtension) => $"output.{fileExtension}";
    }
}
