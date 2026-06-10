using ElectronicStore.Application.Responses;
using ElectronicStore.Domain.Contracts.Repositories;
using MediatR;

namespace ElectronicStore.Application.Queries.Purchases;

public class GetPurchasesByUserIdQueryHandler(IPurchaseRepository purchaseRepository)
    : IRequestHandler<GetPurchasesByUserIdQuery, IReadOnlyCollection<PurchaseResponse>>
{
    public async Task<IReadOnlyCollection<PurchaseResponse>> Handle(GetPurchasesByUserIdQuery request, CancellationToken cancellationToken)
    {
        var purchases = await purchaseRepository.GetByUserId(request.UserId);

        return purchases
            .Select(p => p.ToResponse())
            .ToArray();
    }
}
