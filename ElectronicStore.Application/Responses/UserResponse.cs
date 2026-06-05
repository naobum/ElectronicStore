namespace ElectronicStore.Application.Responses;

public record UserResponse(long Id, string Name, decimal Balance, CartDto Cart);
