using ElectronicStore.Application.Responses;
using MediatR;

namespace ElectronicStore.Application.Queries.Purchases;

public record GetLatestPurchasesQuery(int Count = 6) : IRequest<IReadOnlyCollection<PurchaseResponse>>;
