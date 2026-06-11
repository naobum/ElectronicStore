import { useEffect, useState } from 'react';
import { productApi, brandApi, userApi } from '../services/api.js';
import ProductForm from '../components/ProductForm.jsx';
import BrandForm from '../components/BrandForm.jsx';
import '../styles/Admin.css';

export default function Admin() {
  const [activeTab, setActiveTab] = useState('products');
  const [products, setProducts] = useState([]);
  const [brands, setBrands] = useState([]);
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [editingProduct, setEditingProduct] = useState(null);
  const [editingBrand, setEditingBrand] = useState(null);
  const [newUserName, setNewUserName] = useState('');

  useEffect(() => {
    fetchAllData();
  }, []);

  const fetchAllData = async () => {
    try {
      setLoading(true);
      const [productsData, brandsData, usersData] = await Promise.all([
        productApi.getAll(),
        brandApi.getAll(),
        userApi.getAll(),
      ]);
      setProducts(productsData);
      setBrands(brandsData);
      setUsers(usersData);
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

  const [topUpAmounts, setTopUpAmounts] = useState({});

  const handleDeleteUser = async (id) => {
    if (!confirm('Удалить пользователя?')) return;
    try {
      await userApi.delete(id);
      setUsers((items) => items.filter((item) => item.id !== id));
    } catch (err) {
      alert('Ошибка: ' + err.message);
    }
  };

  const handleCreateUser = async () => {
    const name = newUserName.trim();
    if (!name) {
      alert('Введите имя аккаунта');
      return;
    }

    try {
      const created = await userApi.create({ name });
      setUsers((items) => [...items, created]);
      setNewUserName('');
      alert('Аккаунт создан');
    } catch (err) {
      alert('Ошибка: ' + err.message);
    }
  };

  const handleTopUp = async (userId) => {
    const amount = Number(topUpAmounts[userId]);
    if (!amount || amount <= 0) {
      alert('Введите корректную сумму пополнения');
      return;
    }

    try {
      await userApi.topUp(userId, amount);
      setUsers((items) =>
        items.map((item) =>
          item.id === userId ? { ...item, balance: Number(item.balance) + amount } : item
        )
      );
      setTopUpAmounts((items) => ({ ...items, [userId]: '' }));
      alert(`Баланс пополнен на ${amount} ₽`);
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
            <div className="panel-card">
              <h3>Создать аккаунт</h3>
              <div className="form-row">
                <input
                  type="text"
                  value={newUserName}
                  onChange={(event) => setNewUserName(event.target.value)}
                  placeholder="Имя пользователя"
                />
                <button className="primary-btn" onClick={handleCreateUser}>Создать</button>
              </div>
            </div>
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
                      <th>Пополнение</th>
                      <th>Действия</th>
                    </tr>
                  </thead>
                  <tbody>
                    {users.map((user) => (
                      <tr key={user.id}>
                        <td>{user.name}</td>
                        <td>{user.email}</td>
                        <td>{user.balance} ₽</td>
                        <td>
                          <input
                            type="number"
                            min="1"
                            value={topUpAmounts[user.id] ?? ''}
                            onChange={(event) =>
                              setTopUpAmounts((state) => ({
                                ...state,
                                [user.id]: event.target.value,
                              }))
                            }
                            placeholder="Сумма"
                          />
                          <button className="primary-btn" onClick={() => handleTopUp(user.id)}>
                            Пополнить
                          </button>
                        </td>
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
      </div>
    </div>
  );
}
