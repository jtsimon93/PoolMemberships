using System.Collections.Generic;
using System.Threading.Tasks;
using PoolMemberships.Models;
using PoolMemberships.Repositories;

namespace PoolMemberships.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<Payment> AddAsync(Payment payment)
    {
        return await _paymentRepository.AddAsync(payment);
    }

    public async Task<IEnumerable<Payment>> GetAllAsync()
    {
        return await _paymentRepository.GetAllAsync();
    }
}