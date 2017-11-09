namespace EGNValidation.DecoratorInstances
{
    using EGNValidation.Decorator;
    using System;
    /// <summary>
    /// Child validation decorator to egn validation
    /// </summary>
    public class ChildDecorator:EgnDecorator
    {
        
        const string cErrorMessage = "Грешка ЕГН.Картодържателят трябва да е между 7 години и 10 години!";
        private bool baseFail = false;
        /// <summary>
        /// Add validation for child
        /// </summary>
        /// <param name="validation">basic validation</param>
        public ChildDecorator(EgnAbstractValidation validation) : base(validation)
        {

        }
        /// <summary>
        /// Get message for child egn error
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
        /// If basic validation succeeded execute child validation
        /// </summary>
        /// <returns>bool</returns>
        public override bool Validate()
        {
            if (!validation.Validate())
            {
                baseFail = true;
                validation.isValid = false;
            }
            else
            {
                if (!ChildValidate())
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
        private bool ChildValidate()
        {
            bool result = false;
            try
            {
                DateTime now = DateTime.Now;
                DateTime sevenYearBarrier = validation.EGNDate.AddYears(7);
                DateTime tenYearBarrier = validation.EGNDate.AddYears(10);
                if (now >= sevenYearBarrier && now <= tenYearBarrier)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Validate method in ChildDecorator", ex);
            }
            return result;
        }
    }
}