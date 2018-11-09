using Entity;
using System;
using System.Threading.Tasks;

namespace Repository
{
    public interface IPaySlipRepository
    {
        Task<int> CreatePaySlipAsync(PaySlip paySlip);
    }

    public class PaySlipRepository : IPaySlipRepository
    {
        /// <summary>
        /// Fake repo to return a random int.
        /// </summary>
        /// <param name="paySlip"></param>
        /// <returns>99</returns>
        public async Task<int> CreatePaySlipAsync(PaySlip paySlip)
        {
            var result = await Task.Run(() =>
            {
                Random rnd = new Random();
                return rnd.Next(1, 100);
            });

            return result;
        }
    }
}
