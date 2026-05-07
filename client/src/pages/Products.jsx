import { useState, useEffect } from 'react';
import { productApi, brandApi } from '../services/api';
import { useCart } from '../context/CartContext';
import ProductCard from '../components/ProductCard';
import SearchFilters from '../components/SearchFilters';
import '../styles/Products.css';

export default function Products() {
  const [products, setProducts] = useState([]);
  const [brands, setBrands] = useState([]);
  const [filteredProducts, setFilteredProducts] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedBrand, setSelectedBrand] = useState(null);
  const [minRating, setMinRating] = useState(0);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const { addToCart } = useCart();

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        const [productsData, brandsData] = await Promise.all([
          productApi.getAll(),
          brandApi.getAll(),
        ]);
        setProducts(productsData);
        setBrands(brandsData);
        setFilteredProducts(productsData);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  useEffect(() => {
    let result = products;

    // Filter by search term
    if (searchTerm) {
      result = result.filter((product) =>
        product.name.toLowerCase().includes(searchTerm.toLowerCase())
      );
    }

    // Filter by brand
    if (selectedBrand) {
      result = result.filter((product) => product.brandId === selectedBrand);
    }

    // Filter by rating
    result = result.filter((product) => product.rating >= minRating);

    setFilteredProducts(result);
  }, [searchTerm, selectedBrand, minRating, products]);

  if (loading) return <div className="loading">Загрузка товаров...</div>;
  if (error) return <div className="error">Ошибка: {error}</div>;

  return (
    <div className="products-container">
      <h1>Товары</h1>

      <SearchFilters
        searchTerm={searchTerm}
        onSearchChange={setSearchTerm}
        brands={brands}
        selectedBrand={selectedBrand}
        onBrandChange={setSelectedBrand}
        minRating={minRating}
        onRatingChange={setMinRating}
      />

      <div className="products-grid">
        {filteredProducts.length > 0 ? (
          filteredProducts.map((product) => (
            <ProductCard
              key={product.id}
              product={product}
              onAddToCart={addToCart}
            />
          ))
        ) : (
          <p className="no-products">Товары не найдены</p>
        )}
      </div>
    </div>
  );
}
