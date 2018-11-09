using Entity;
using System.Collections.Generic;

namespace Service
{
    public interface ITaxFactory
    {
        List<Tax> GetTaxable();
    }

    public class TaxFactory : ITaxFactory
    {

        /// <summary>
        /// Get Taxable Information
        /// </summary>
        /// <returns><see cref="{List<Tax>}"/></returns>
        public List<Tax> GetTaxable()
        {
            var taxable = new List<Tax>();

            //the tax paramters should come out of a database, however for the sake of simplicity, I used hard coded value.

            var tax1 = new Tax(0, 18200, 0, 0); //nill
            var tax2 = new Tax(18201, 37000, (decimal)0.19, 0); //19c for each $1 over $18,200
            var tax3 = new Tax(37001, 87000, (decimal)0.325, 3572); //$3,572 plus 32.5c for each $1 over $37,000
            var tax4 = new Tax(87001, 180000, (decimal)0.37, 19822); //$19,822 plus 37c for each $1 over $87, 000
            var tax5 = new Tax(180001, decimal.MaxValue, (decimal)0.45, 54232); //$54,232 plus 45c for each $1 over $180, 000

            taxable.AddRange(new[] { tax1, tax2, tax3, tax4, tax5 });

            return taxable;
        }
    }
}
