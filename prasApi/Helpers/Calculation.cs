using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prasApi.Helpers
{
    public class Calculation
    {
        // Helper method to calculate age accurately
        public static int CalculateAge(DateOnly birthday)
        {
            var today = DateOnly.FromDateTime(DateTime.Today); // Convert today's date to DateOnly
            var age = today.Year - birthday.Year;

            // Check if the birthday has occurred this year
            if (birthday > today.AddYears(-age))
                age--;

            return age;
        }
    }
}