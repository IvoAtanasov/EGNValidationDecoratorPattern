namespace EGNValidation
{
    using System;
    /// <summary>
    /// Abstract class egn validation properties and methods
    /// </summary>
    public  abstract class EgnAbstractValidation
    {
        /// <summary>
        /// EGN error message
        /// </summary>
        public string errorMessage { get; set; }
        /// <summary>
        /// Indicates whether egn is valid
        /// </summary>
        public bool isValid { get; set; }
        /// <summary>
        /// EGN birth date from first 6 digits
        /// </summary>
        public DateTime EGNDate { get; set; }
        /// <summary>
        /// indicates whether cardholder is a male ot not
        /// </summary>
        public bool IsMale { get; set; }
        /// <summary>
        /// Get error message
        /// </summary>
        /// <returns></returns>
        public abstract string GetMessage();
        /// <summary>
        /// Executes validation
        /// </summary>
        /// <returns></returns>
        public abstract bool Validate();
    }
}