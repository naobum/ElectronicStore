import { useEffect, useState } from 'react';
import { productApi, brandApi, userApi, reviewApi } from '../services/api.js';
import ProductForm from '../components/ProductForm.jsx';
import BrandForm from '../components/BrandForm.jsx';
import '../styles/Admin.css';

export default function Admin() {
  const [activeTab, setActiveTab] = useState('products');
  const [products, setProducts] = useState([]);
  const [brands, setBrands] = useState([]);
  const [users, setUsers] = useState([]);
  const [reviews, setReviews] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [editingProduct, setEditingProduct] = useState(null);
  const [editingBrand, setEditingBrand] = useState(null);

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
    if (!confirm('Удалить товар?')) return;
    try {
      await productApi.delete(id);
      setProducts((items) => items.filter((item) => item.id !== id));
    } catch (err) {
      alert('Ошибка: ' + err.message);
    }
  };

  const handleDeleteBrand = async (id) => {
    if (!confirm('Удалить бренд?')) return;
    try {
      await brandApi.delete(id);
      setBrands((items) => items.filter((item) => item.id !== id));
    } catch (err) {
      alert('Ошибка: ' + err.message);
    }
  };

  const handleDeleteUser = async (id) => {
    if (!confirm('Удалить пользователя?')) return;
    try {
      await userApi.delete(id);
      setUsers((items) => items.filter((item) => item.id !== id));
    } catch (err) {
      alert('Ошибка: ' + err.message);
    }
  };

  const handleDeleteReview = async (id) => {
    if (!confirm('Удалить отзыв?')) return;
    try {
      await reviewApi.delete(id);
      setReviews((items) => items.filter((item) => item.id !== id));
    } catch (err) {
      alert('Ошибка: ' + err.message);
    }
  };

  const handleSaveProduct = async (payload) => {
    try {
      if (editingProduct) {
        await productApi.update(editingProduct.id, payload);
        setProducts((items) => items.map((item) => (item.id === editingProduct.id ? { ...item, ...payload } : item)));
      } else {
        const created = await productApi.create(payload);
        setProducts((items) => [...items, created]);
      }
      setEditingProduct(null);
    } catch (err) {
      alert('Ошибка: ' + err.message);
    }
  };

  const handleSaveBrand = async (payload) => {
    try {
      if (editingBrand) {
        await brandApi.update(editingBrand.id, payload);
        setBrands((items) => items.map((item) => (item.id === editingBrand.id ? { ...item, ...payload } : item)));
      } else {
        const created = await brandApi.create(payload);
        setBrands((items) => [...items, created]);
      }
      setEditingBrand(null);
    } catch (err) {
      alert('Ошибка: ' + err.message);
    }
  };

  if (loading) return <div className="loading">Загрузка административных данных...</div>;

  return (
    <div className="admin-container">
      <h1>Админ-панель</h1>
      {error && <div className="error">Ошибка: {error}</div>}
      <div className="admin-tabs">
        <button className={`tab-btn ${activeTab === 'products' ? 'active' : ''}`} onClick={() => setActiveTab('products')}>
          Товары
        </button>
        <button className={`tab-btn ${activeTab === 'brands' ? 'active' : ''}`} onClick={() => setActiveTab('brands')}>
          Бренды
        </button>
        <button className={`tab-btn ${activeTab === 'users' ? 'active' : ''}`} onClick={() => setActiveTab('users')}>
          Пользователи
        </button>
        <button className={`tab-btn ${activeTab === 'reviews' ? 'active' : ''}`} onClick={() => setActiveTab('reviews')}>
          Отзывы
        </button>
      </div>

      <div className="admin-content">
        {activeTab === 'products' && (
          <div>
            <h2>Управление товарами</h2>
            <ProductForm
              brands={brands}
              editingItem={editingProduct}
              onSave={handleSaveProduct}
              onCancel={() => setEditingProduct(null)}
            />
            <div className="items-list">
              {products.length === 0 ? (
                <p>Товары не найдены.</p>
              ) : (
                <table>
                  <thead>
                    <tr>
                      <th>Название</th>
                      <th>Бренд</th>
                      <th>Цена</th>
                      <th>Рейтинг</th>
                      <th>Остаток</th>
                      <th>Действия</th>
                    </tr>
                  </thead>
                  <tbody>
                    {products.map((product) => (
                      <tr key={product.id}>
                        <td>{product.name}</td>
                        <td>{brands.find((brand) => brand.id === product.brandId)?.name || '—'}</td>
                        <td>{product.price} ₽</td>
                        <td>{product.rating?.toFixed(1) || '—'}</td>
                        <td>{product.amount}</td>
                        <td className="actions">
                          <button className="edit-btn" onClick={() => setEditingProduct(product)}>
                            Изменить
                          </button>
                          <button className="delete-btn" onClick={() => handleDeleteProduct(product.id)}>
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

        {activeTab === 'brands' && (
          <div>
            <h2>Управление брендами</h2>
            <BrandForm
              editingItem={editingBrand}
              onSave={handleSaveBrand}
              onCancel={() => setEditingBrand(null)}
            />
            <div className="items-list">
              {brands.length === 0 ? (
                <p>Бренды не найдены.</p>
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
                        <td>{brand.description || '—'}</td>
                        <td className="actions">
                          <button className="edit-btn" onClick={() => setEditingBrand(brand)}>
                            Изменить
                          </button>
                          <button className="delete-btn" onClick={() => handleDeleteBrand(brand.id)}>
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

        {activeTab === 'users' && (
          <div>
            <h2>Пользователи</h2>
            <div className="items-list">
              {users.length === 0 ? (
                <p>Пользователи не найдены.</p>
              ) : (
                <table>
                  <thead>
                    <tr>
                      <th>Имя</th>
                      <th>Email</th>
                      <th>Баланс</th>
                      <th>Действия</th>
                    </tr>
                  </thead>
                  <tbody>
                    {users.map((user) => (
                      <tr key={user.id}>
                        <td>{user.name}</td>
                        <td>{user.email}</td>
                        <td>{user.balance}</td>
                        <td className="actions">
                          <button className="delete-btn" onClick={() => handleDeleteUser(user.id)}>
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

        {activeTab === 'reviews' && (
          <div>
            <h2>Отзывы</h2>
            <div className="items-list">
              {reviews.length === 0 ? (
                <p>Отзывы не найдены.</p>
              ) : (
                <table>
                  <thead>
                    <tr>
                      <th>Пользователь</th>
                      <th>Текст</th>
                      <th>Оценка</th>
                      <th>Действия</th>
                    </tr>
                  </thead>
                  <tbody>
                    {reviews.map((review) => (
                      <tr key={review.id}>
                        <td>{review.userName || 'Пользователь'}</td>
                        <td>{review.text || '-'}</td>
                        <td>{review.rating ?? '-'}</td>
                        <td className="actions">
                          <button className="delete-btn" onClick={() => handleDeleteReview(review.id)}>
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
