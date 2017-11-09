namespace EGNValidation.DecoratorInstances
{
    using EGNValidation.Decorator;
    using System;
    /// <summary>
    /// Add validation for classic type cards
    /// </summary>
    public class ClassicDecorator : EgnDecorator
    {
        const string cErrorMessage = "Грешка ЕГН.Картодържателят трябва да е над 26 години!";
        private bool baseFail = false;
        /// <summary>
        /// wrap basic validation to basic class type cards
        /// </summary>
        /// <param name="validation"></param>
        public ClassicDecorator(EgnAbstractValidation validation) : base(validation)
        {

        }
        /// <summary>
        /// Add egn error validation for basic class type cards
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
        /// Execute classic type cards validation if basic succeeded
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
                if (!ClassicValidate())
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
        private bool ClassicValidate()
        {
            bool result = false;
            try
            {
                DateTime now = DateTime.Now;
                DateTime YearBarrier = validation.EGNDate.AddYears(26);

                if (now > YearBarrier)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Validate method in ClassicDecorator", ex);
            }
            return result;
        }
    }
}