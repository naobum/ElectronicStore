import '../styles/ProductCard.css';

export default function ProductCard({ product, brandName, onAdd }) {
  return (
    <article className="product-card">
      <div className="product-card-top">
        <h3>{product.name}</h3>
        <span className="product-price">{product.price} ₽</span>
      </div>
      <div className="product-meta">
        <span>{brandName}</span>
        <span>Рейтинг: {product.rating?.toFixed(1) ?? '—'}</span>
      </div>
      <p className="product-description">{product.description || 'Описание отсутствует'}</p>
      <div className="product-footer">
        <span>В наличии: {product.amount}</span>
        <button className="primary-btn" onClick={onAdd} disabled={product.amount <= 0}>
          В корзину
        </button>
      </div>
    </article>
  );
}
