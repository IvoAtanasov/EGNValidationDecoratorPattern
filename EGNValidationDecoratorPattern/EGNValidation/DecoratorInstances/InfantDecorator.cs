namespace EGNValidation.DecoratorInstances
{
    using EGNValidation.Decorator;
    using System;
    /// <summary>
    /// Add validation for infant
    /// </summary>
    public class InfantDecorator: EgnDecorator
    {
        const string cErrorMessage = "Грешка ЕГН.Картодържателят трябва да е по-млад от 7 години!";
        private bool baseFail = false;
        /// <summary>
        /// Add validation for infant
        /// </summary>
        /// <param name="validation">basic validation</param>
        public InfantDecorator(EgnAbstractValidation validation) : base(validation)
        {

        }
        /// <summary>
        /// Get message for infant egn error
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
        /// If basic validation succeeded execute infant validation
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
                if (!InfantValidate())
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
        private bool InfantValidate()
        {
            bool result = false;
            try
            {
                DateTime now = DateTime.Now;
                DateTime sevenYearBarrier = validation.EGNDate.AddYears(7);
         
                if (now < sevenYearBarrier)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}