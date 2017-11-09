using static EGNValidationDecoratorPattern.ExtensionMethod;
namespace EGNValidation.DecoratorInstances
{
    using Decorator;
    using System;

    /// <summary>
    /// Elderly decorator for egn validation
    /// </summary>
    public class ElderlyDecorator:EgnDecorator
    {

        private int manBarrier;
        private int womanBarrier;
        const string cErrorMessage = "Грешка ЕГН.Картодържателят трябва да е женa над {0} или мъж над {1} !";
        private bool baseFail = false;
        /// <summary>
        /// Wraps base validation with elderly decorator
        /// </summary>
        /// <param name="validation">basic validation instance</param>
        /// <param name="manBarrier">barrier for man pension in months</param>
        /// <param name="womanBarrier">barrier for woman pension months </param>
        public ElderlyDecorator(EgnAbstractValidation validation, int manBarrier, int womanBarrier) : base(validation)
        {
            this.manBarrier = manBarrier;
            this.womanBarrier = womanBarrier;
        }
        /// <summary>
        /// Get elderly error message
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
                    baseMessage = String.Format(cErrorMessage,womanBarrier.FromMonthsToTextDate(),manBarrier.FromMonthsToTextDate());
                }
            }
            return baseMessage;
        }
        /// <summary>
        /// Execute elderly validation if basic validation succeeded
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
                if (!ElderlyValidate())
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
        private bool ElderlyValidate()
        {
            bool result = false;
            try
            {
                DateTime now = DateTime.Now;
                DateTime womanYearBarrier = validation.EGNDate.AddMonths(womanBarrier);
                DateTime menYearBarrier = validation.EGNDate.AddMonths(manBarrier);
                if (validation.IsMale && now > menYearBarrier)
                {
                    result = true;
                }
                if (!(validation.IsMale) && now > womanYearBarrier)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Validate method in ElderlyDecorator", ex);
            }
            return result;
        }
    }
}