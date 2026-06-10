using ElectronicStore.Application.Responses;
using MediatR;

namespace ElectronicStore.Application.Queries.Purchases;

public record GetPurchasesByUserIdQuery(long UserId) : IRequest<IReadOnlyCollection<PurchaseResponse>>;
