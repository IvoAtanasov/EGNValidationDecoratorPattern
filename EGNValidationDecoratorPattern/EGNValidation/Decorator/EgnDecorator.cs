namespace EGNValidation.Decorator
{
    /// <summary>
    /// Decorator class
    /// </summary>
    public class EgnDecorator:EgnAbstractValidation
    {
        /// <summary>
        /// validation instanst used to be wrap in decorator pattern
        /// </summary>
        protected EgnAbstractValidation validation;
        /// <summary>
        /// Wrap egnvalidation
        /// </summary>
        /// <param name="validation"></param>
        public EgnDecorator(EgnAbstractValidation validation)
        {
            this.validation = validation;
        }
        /// <summary>
        /// Validation error message
        /// </summary>
        /// <returns></returns>
        public override string GetMessage()
        {
            return validation.GetMessage();
        }
        /// <summary>
        /// Executes validation
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            return validation.Validate();
        }
    }
}