// Пример использования Unit of Work паттерна
//
// Unit of Work паттерн централизует управление сохранением изменений.
// Все репозитории больше не имеют метода SaveChanges() - это управляется через IUnitOfWork.

/*
 * ===== ПРИМЕР 1: Базовое использование UnitOfWork =====
 * 
 * public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ErrorOr<Created>>
 * {
 *     private readonly IProductRepository _productRepository;
 *     private readonly IUnitOfWork _unitOfWork;
 *
 *     public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
 *     {
 *         _productRepository = productRepository;
 *         _unitOfWork = unitOfWork;
 *     }
 *
 *     public async Task<ErrorOr<Created>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
 *     {
 *         // Создание продукта
 *         var product = Product.Create(request.BrandId, request.Name, request.Price, request.Amount, request.Description);
 *         
 *         if (product.IsError)
 *             return product.Errors;
 *
 *         // Добавление в репозиторий (не сохраняется в БД)
 *         await _productRepository.Create(product.Value);
 *
 *         // Сохранение всех изменений в БД
 *         await _unitOfWork.SaveChangesAsync(cancellationToken);
 *
 *         return Result.Created;
 *     }
 * }
 */

/*
 * ===== ПРИМЕР 2: Множественные операции =====
 * 
 * public class AddCartItemCommandHandler : IRequestHandler<AddCartItemCommand, ErrorOr<Updated>>
 * {
 *     private readonly ICartRepository _cartRepository;
 *     private readonly IProductRepository _productRepository;
 *     private readonly IUnitOfWork _unitOfWork;
 *
 *     public AddCartItemCommandHandler(
 *         ICartRepository cartRepository, 
 *         IProductRepository productRepository,
 *         IUnitOfWork unitOfWork)
 *     {
 *         _cartRepository = cartRepository;
 *         _productRepository = productRepository;
 *         _unitOfWork = unitOfWork;
 *     }
 *
 *     public async Task<ErrorOr<Updated>> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
 *     {
 *         // Получаем корзину и товар
 *         var cart = await _cartRepository.GetCartByUserId(request.UserId, cancellationToken);
 *         var product = await _productRepository.GetProductById(request.ProductId, cancellationToken);
 *
 *         if (cart == null || product == null)
 *             return Error.NotFound("Cart or product not found");
 *
 *         // Добавляем товар в корзину
 *         var result = cart.AddItem(product, request.Quantity);
 *
 *         if (!result.IsError)
 *         {
 *             // Сохраняем изменения одним вызовом
 *             await _unitOfWork.SaveChangesAsync(cancellationToken);
 *         }
 *
 *         return result;
 *     }
 * }
 */

/*
 * ===== ПРИМЕР 3: Создание новой сущности =====
 * 
 * public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ErrorOr<Created>>
 * {
 *     private readonly IReviewRepository _reviewRepository;
 *     private readonly IUnitOfWork _unitOfWork;
 *
 *     public async Task<ErrorOr<Created>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
 *     {
 *         // Создаём отзыв с валидацией
 *         var review = Review.Create(
 *             request.UserId, 
 *             request.ProductId, 
 *             request.Rating, 
 *             request.Comment);
 *
 *         if (review.IsError)
 *             return review.Errors;
 *
 *         // Добавляем в репозиторий
 *         await _reviewRepository.Create(review.Value, cancellationToken);
 *
 *         // Сохраняем в БД
 *         await _unitOfWork.SaveChangesAsync(cancellationToken);
 *
 *         return Result.Created;
 *     }
 * }
 */

// ===== ПРЕИМУЩЕСТВА UNIT OF WORK ПАТТЕРНА =====
// 1. Централизованное управление сохранением изменений
// 2. Репозитории отвечают только за доступ к данным, не за сохранение
// 3. Одна точка входа для сохранения (IUnitOfWork)
// 4. Легче тестировать - можно мокировать IUnitOfWork
// 5. Ясная разделение ответственности

// ===== РЕГИСТРАЦИЯ В DEPENDENCY INJECTION =====
// services.AddScoped<IUnitOfWork, UnitOfWork>();
// services.AddScoped<IProductRepository, ProductRepository>();
// services.AddScoped<IUserRepository, UserRepository>();
// services.AddScoped<ICartRepository, CartRepository>();
// services.AddScoped<IReviewRepository, ReviewRespository>();
// services.AddScoped<IBrandRepository, BrandRepository>();
// services.AddScoped<IPurchaseRepository, PurchaseRepository>();
