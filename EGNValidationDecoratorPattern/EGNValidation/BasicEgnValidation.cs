namespace EGNValidation
{
    using System;
    using System.Text.RegularExpressions;
    /// <summary>
    /// Class execution basic egn validation
    /// check only if egn is valid
    /// </summary>
    public class BasicEgnValidation :EgnAbstractValidation
    {
        private const string cErrorMessage = "Невалидно егн!";
        private int[] egnWeights = new int[] { 2, 4, 8, 5, 10, 9, 7, 3, 6 };
        private string egn;
        protected int year;
        protected int month;
        protected int day;
        /// <summary>
        /// Basic validation of EGN
        /// </summary>
        /// <param name="egn"></param>
        public BasicEgnValidation(string egn)
        {
            this.errorMessage = cErrorMessage;
            this.egn = egn;
        }
        /// <summary>
        /// Get message if error occurs
        /// </summary>
        /// <returns>string for an error message</returns>
        public override string GetMessage()
        {
            if (isValid)
            {
                errorMessage = string.Empty;
            }
            else
            {
                errorMessage = cErrorMessage;
            }
            return errorMessage;
        }
        /// <summary>
        /// Execute validation
        /// Length==10, only digits are allowed,year month day for first 6 digits
        /// last digit check sum executes
        /// </summary>
        /// <returns>bool</returns>
        public override bool Validate()
        {
            isValid = false;
            try
            {
                if (egn.Length != 10)
                {
                    return isValid;
                }
                if (!RegexEgnCheck(egn))
                {
                    return isValid;
                }

                year = int.Parse(egn.Substring(0, 2));
                month = int.Parse(egn.Substring(2, 2));
                day = int.Parse(egn.Substring(4, 2));

                SetDateByMonth(ref year, ref month, ref day);
                if (!CheckDate(year, month, day))
                {
                    return isValid;
                }
                if (!CheckSum(int.Parse(egn.Substring(9, 1))))
                {
                    return isValid;
                }
                isValid = true;
                if (int.Parse(egn.Substring(8, 1)) % 2 == 0)
                {
                    IsMale = true;
                }
            }
            catch (Exception ex)
            {

            }
            return isValid;
        }
        private bool CheckSum(int check)
        {
            bool result = false;
            try
            {
                int egnSum = 0;
                int validCheckSum = 0;
                for (int index = 0; index < 9; index++)
                {
                    egnSum += (int.Parse(egn.Substring(index, 1))) * egnWeights[index];
                }
                validCheckSum = egnSum % 11;
                if (validCheckSum == 10)
                {
                    validCheckSum = 0;
                }
                if (check == validCheckSum)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        private bool CheckDate(int year, int month, int day)
        {
            bool result = false;
            try
            {
                if (year < 1800)
                {
                    return result;
                }
                DateTime dt = new DateTime(year, month, day);
                result = true;
                EGNDate = dt;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                result = false;
            }
            return result;
        }
        private void SetDateByMonth(ref int year, ref int month, ref int day)
        {
            if (month > 40)
            {
                year += 2000;
                month -= 40;
            }
            else
            {
                if (month > 20)
                {
                    year += 1800;
                    month -= 20;
                }
                else
                {
                    year += 1900;
                }

            }
        }

        private bool RegexEgnCheck(string egn)
        {
            Regex r = new Regex("[0-9]{2}[0,1,2,3,4,5][0-9][0-9]{2}[0-9]{4}");
            if (r.IsMatch(egn))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}