using ElectronicStore.Application.Responses;
using ElectronicStore.Domain.Contracts.Repositories;
using MediatR;

namespace ElectronicStore.Application.Queries.Purchases;

public class GetLatestPurchasesQueryHandler(IPurchaseRepository purchaseRepository)
    : IRequestHandler<GetLatestPurchasesQuery, IReadOnlyCollection<PurchaseResponse>>
{
    public async Task<IReadOnlyCollection<PurchaseResponse>> Handle(GetLatestPurchasesQuery request, CancellationToken cancellationToken)
    {
        var purchases = await purchaseRepository.GetAll();

        return purchases
            .OrderByDescending(p => p.DateTime)
            .Take(request.Count)
            .Select(p => p.ToResponse())
            .ToArray();
    }
}
