import { useEffect, useMemo, useState } from 'react';
import { productApi, brandApi, purchaseApi } from '../services/api.js';
import { useCart } from '../context/CartContext.jsx';
import ProductCard from '../components/ProductCard.jsx';
import SearchFilters from '../components/SearchFilters.jsx';
import '../styles/Products.css';

export default function Products() {
  const [products, setProducts] = useState([]);
  const [brands, setBrands] = useState([]);
  const [search, setSearch] = useState('');
  const [brandId, setBrandId] = useState('');
  const [minRating, setMinRating] = useState('');
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [latestPurchases, setLatestPurchases] = useState([]);
  const { addToCart } = useCart();

  useEffect(() => {
    const load = async () => {
      try {
        setLoading(true);
        const [productsData, brandsData, purchasesData] = await Promise.all([
          productApi.getAll(),
          brandApi.getAll(),
          purchaseApi.getLatest(5),
        ]);
        setProducts(productsData);
        setBrands(brandsData);
        setLatestPurchases(purchasesData);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    load();
  }, []);

  const filteredProducts = useMemo(() => {
    return products.filter((product) => {
      const matchesName = product.name?.toLowerCase().includes(search.toLowerCase());
      const matchesBrand = !brandId || product.brandId === Number(brandId);
      const matchesRating = !minRating || product.rating >= Number(minRating);
      return matchesName && matchesBrand && matchesRating;
    });
  }, [products, search, brandId, minRating]);

  return (
    <div className="page-shell">
      <h1>Каталог товаров</h1>
      {error && <div className="error">Ошибка: {error}</div>}
      <SearchFilters
        search={search}
        onSearch={setSearch}
        brandId={brandId}
        onBrandChange={setBrandId}
        minRating={minRating}
        onMinRatingChange={setMinRating}
        brands={brands}
      />
      <section className="recent-purchases-card">
        <h2>Последние покупки</h2>
        {latestPurchases.length === 0 ? (
          <p>Пока покупок нет.</p>
        ) : (
          <ul className="recent-purchases-list">
            {latestPurchases.map((purchase, index) => (
              <li key={`${purchase.userId}-${purchase.dateTime}-${index}`}>
                <strong>Покупка #{index + 1}</strong>
                <span>{new Date(purchase.dateTime).toLocaleString('ru-RU')}</span>
                <span>Сумма: {Number(purchase.totalPrice).toFixed(2)} ₽</span>
              </li>
            ))}
          </ul>
        )}
      </section>
      {loading ? (
        <div className="loading">Загрузка товаров...</div>
      ) : filteredProducts.length === 0 ? (
        <div className="loading">Товары не найдены</div>
      ) : (
        <div className="product-grid">
          {filteredProducts.map((product) => (
            <ProductCard
              key={product.id}
              product={product}
              brandName={brands.find((brand) => brand.id === product.brandId)?.name || 'Без бренда'}
              onAdd={() => addToCart(product)}
            />
          ))}
        </div>
      )}
    </div>
  );
}
