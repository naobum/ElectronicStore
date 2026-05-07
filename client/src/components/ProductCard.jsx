import '../styles/ProductCard.css';

export default function ProductCard({ product, onAddToCart }) {
  return (
    <div className="product-card">
      <div className="product-header">
        <h3>{product.name}</h3>
        <span className="rating">{product.rating?.toFixed(1) || 'N/A'} ⭐</span>
      </div>

      <p className="brand">{product.brand?.name || 'Неизвестный бренд'}</p>

      {product.description && (
        <p className="description">{product.description}</p>
      )}

      {product.reviews && product.reviews.length > 0 && (
        <div className="reviews-count">
          {product.reviews.length} отзыв(ов)
        </div>
      )}

      <button
        className="add-to-cart-btn"
        onClick={() => onAddToCart(product)}
      >
        В корзину
      </button>
    </div>
  );
}
