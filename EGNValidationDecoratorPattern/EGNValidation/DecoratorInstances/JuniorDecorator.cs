namespace EGNValidation.DecoratorInstances
{
    using EGNValidation.Decorator;
    using System;
    /// <summary>
    /// Add junior decorator to egn validation
    /// </summary>
    public class JuniorDecorator :EgnDecorator
    {
        const string cErrorMessage = "Грешка ЕГН.Картодържателят трябва да е под 26 години!";
        private bool baseFail = false;
        /// <summary>
        /// Wrap egn base validation of junior decorator 
        /// </summary>
        /// <param name="validation">base validation</param>
        public JuniorDecorator(EgnAbstractValidation validation) : base(validation)
        {

        }
        /// <summary>
        /// Get junior error message 
        /// </summary>
        /// <returns></returns>
        public override string GetMessage()
        {
            string baseMessage = validation.GetMessage();
            if (validation.isValid)
            {
                return string.Empty;
            }
            else
            {
                if (!baseFail)
                {
                    baseMessage = cErrorMessage;
                }
            }
            return baseMessage;
        }
        /// <summary>
        /// Execute junior validate of base succeeded
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            if (!validation.Validate())
            {
                baseFail = true;
                validation.isValid = false;
            }
            else
            {
                if (!JuniorValidate())
                {

                    validation.isValid = false;
                }
                else
                {
                    validation.isValid = true;
                }
            }
            return validation.isValid;
        }
        private bool JuniorValidate()
        {
            bool result = false;
            try
            {
                DateTime now = DateTime.Now;
                DateTime YearBarrier = validation.EGNDate.AddYears(26);

                if (now <= YearBarrier)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Validate method in JuniorDecorator", ex);
            }
            return result;
        }
    }
}