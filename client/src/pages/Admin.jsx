import { useState, useEffect } from 'react';
import { productApi, brandApi, userApi, reviewApi } from '../services/api';
import ProductForm from '../components/ProductForm';
import BrandForm from '../components/BrandForm';
import '../styles/Admin.css';

export default function Admin() {
  const [activeTab, setActiveTab] = useState('products');
  const [products, setProducts] = useState([]);
  const [brands, setBrands] = useState([]);
  const [users, setUsers] = useState([]);
  const [reviews, setReviews] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [editingItem, setEditingItem] = useState(null);

  useEffect(() => {
    fetchAllData();
  }, []);

  const fetchAllData = async () => {
    try {
      setLoading(true);
      const [productsData, brandsData, usersData, reviewsData] = await Promise.all([
        productApi.getAll(),
        brandApi.getAll(),
        userApi.getAll(),
        reviewApi.getAll(),
      ]);
      setProducts(productsData);
      setBrands(brandsData);
      setUsers(usersData);
      setReviews(reviewsData);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  const handleDeleteProduct = async (id) => {
    if (confirm('Вы уверены?')) {
      try {
        await productApi.delete(id);
        setProducts(products.filter((p) => p.id !== id));
      } catch (err) {
        alert('Ошибка при удалении: ' + err.message);
      }
    }
  };

  const handleDeleteBrand = async (id) => {
    if (confirm('Вы уверены?')) {
      try {
        await brandApi.delete(id);
        setBrands(brands.filter((b) => b.id !== id));
      } catch (err) {
        alert('Ошибка при удалении: ' + err.message);
      }
    }
  };

  const handleDeleteUser = async (id) => {
    if (confirm('Вы уверены?')) {
      try {
        await userApi.delete(id);
        setUsers(users.filter((u) => u.id !== id));
      } catch (err) {
        alert('Ошибка при удалении: ' + err.message);
      }
    }
  };

  const handleDeleteReview = async (id) => {
    if (confirm('Вы уверены?')) {
      try {
        await reviewApi.delete(id);
        setReviews(reviews.filter((r) => r.id !== id));
      } catch (err) {
        alert('Ошибка при удалении: ' + err.message);
      }
    }
  };

  const handleSaveProduct = async (productData) => {
    try {
      if (editingItem) {
        await productApi.update(editingItem.id, productData);
        setProducts(
          products.map((p) => (p.id === editingItem.id ? { ...p, ...productData } : p))
        );
      } else {
        const newProduct = await productApi.create(productData);
        setProducts([...products, newProduct]);
      }
      setEditingItem(null);
    } catch (err) {
      alert('Ошибка: ' + err.message);
    }
  };

  const handleSaveBrand = async (brandData) => {
    try {
      if (editingItem) {
        await brandApi.update(editingItem.id, brandData);
        setBrands(
          brands.map((b) => (b.id === editingItem.id ? { ...b, ...brandData } : b))
        );
      } else {
        const newBrand = await brandApi.create(brandData);
        setBrands([...brands, newBrand]);
      }
      setEditingItem(null);
    } catch (err) {
      alert('Ошибка: ' + err.message);
    }
  };

  if (loading) return <div className="loading">Загрузка...</div>;

  return (
    <div className="admin-container">
      <h1>Админ-панель</h1>

      {error && <div className="error">Ошибка: {error}</div>}

      <div className="admin-tabs">
        <button
          className={`tab-btn ${activeTab === 'products' ? 'active' : ''}`}
          onClick={() => setActiveTab('products')}
        >
          Товары
        </button>
        <button
          className={`tab-btn ${activeTab === 'brands' ? 'active' : ''}`}
          onClick={() => setActiveTab('brands')}
        >
          Бренды
        </button>
        <button
          className={`tab-btn ${activeTab === 'users' ? 'active' : ''}`}
          onClick={() => setActiveTab('users')}
        >
          Пользователи
        </button>
        <button
          className={`tab-btn ${activeTab === 'reviews' ? 'active' : ''}`}
          onClick={() => setActiveTab('reviews')}
        >
          Отзывы
        </button>
      </div>

      <div className="admin-content">
        {/* Products Tab */}
        {activeTab === 'products' && (
          <div>
            <h2>Управление товарами</h2>
            <ProductForm
              brands={brands}
              onSave={handleSaveProduct}
              editingItem={editingItem}
              onCancel={() => setEditingItem(null)}
            />

            <div className="items-list">
              {products.length === 0 ? (
                <p>Товаров не найдено</p>
              ) : (
                <table>
                  <thead>
                    <tr>
                      <th>Название</th>
                      <th>Бренд</th>
                      <th>Рейтинг</th>
                      <th>Описание</th>
                      <th>Действия</th>
                    </tr>
                  </thead>
                  <tbody>
                    {products.map((product) => (
                      <tr key={product.id}>
                        <td>{product.name}</td>
                        <td>
                          {brands.find((b) => b.id === product.brandId)?.name || 'N/A'}
                        </td>
                        <td>{product.rating?.toFixed(1) || 'N/A'}</td>
                        <td>{product.description || '-'}</td>
                        <td className="actions">
                          <button
                            className="edit-btn"
                            onClick={() => setEditingItem(product)}
                          >
                            Изменить
                          </button>
                          <button
                            className="delete-btn"
                            onClick={() => handleDeleteProduct(product.id)}
                          >
                            Удалить
                          </button>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              )}
            </div>
          </div>
        )}

        {/* Brands Tab */}
        {activeTab === 'brands' && (
          <div>
            <h2>Управление брендами</h2>
            <BrandForm
              onSave={handleSaveBrand}
              editingItem={editingItem}
              onCancel={() => setEditingItem(null)}
            />

            <div className="items-list">
              {brands.length === 0 ? (
                <p>Брендов не найдено</p>
              ) : (
                <table>
                  <thead>
                    <tr>
                      <th>Название</th>
                      <th>Описание</th>
                      <th>Действия</th>
                    </tr>
                  </thead>
                  <tbody>
                    {brands.map((brand) => (
                      <tr key={brand.id}>
                        <td>{brand.name}</td>
                        <td>{brand.description || '-'}</td>
                        <td className="actions">
                          <button
                            className="edit-btn"
                            onClick={() => setEditingItem(brand)}
                          >
                            Изменить
                          </button>
                          <button
                            className="delete-btn"
                            onClick={() => handleDeleteBrand(brand.id)}
                          >
                            Удалить
                          </button>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              )}
            </div>
          </div>
        )}

        {/* Users Tab */}
        {activeTab === 'users' && (
          <div>
            <h2>Пользователи</h2>
            <div className="items-list">
              {users.length === 0 ? (
                <p>Пользователей не найдено</p>
              ) : (
                <table>
                  <thead>
                    <tr>
                      <th>ID</th>
                      <th>Имя</th>
                      <th>Действия</th>
                    </tr>
                  </thead>
                  <tbody>
                    {users.map((user) => (
                      <tr key={user.id}>
                        <td>{user.id}</td>
                        <td>{user.name}</td>
                        <td className="actions">
                          <button
                            className="delete-btn"
                            onClick={() => handleDeleteUser(user.id)}
                          >
                            Удалить
                          </button>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              )}
            </div>
          </div>
        )}

        {/* Reviews Tab */}
        {activeTab === 'reviews' && (
          <div>
            <h2>Отзывы</h2>
            <div className="items-list">
              {reviews.length === 0 ? (
                <p>Отзывов не найдено</p>
              ) : (
                <table>
                  <thead>
                    <tr>
                      <th>ID</th>
                      <th>Оценка</th>
                      <th>Комментарий</th>
                      <th>Действия</th>
                    </tr>
                  </thead>
                  <tbody>
                    {reviews.map((review) => (
                      <tr key={review.id}>
                        <td>{review.id}</td>
                        <td>{review.rate} ⭐</td>
                        <td>{review.comment || '-'}</td>
                        <td className="actions">
                          <button
                            className="delete-btn"
                            onClick={() => handleDeleteReview(review.id)}
                          >
                            Удалить
                          </button>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              )}
            </div>
          </div>
        )}
      </div>
    </div>
  );
}
